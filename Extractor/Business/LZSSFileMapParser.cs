using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace Extractor.Business
{
	public static class LZSSFileMapParser
	{
		public static Models.FolderData Parse(string fileMap)
		{
			try
			{
				return
					Task.Factory.StartNew<IList<Models.LZSS.FileItem>>(parseNameSize, fileMap)
					.ContinueWith<IList<Models.LZSS.FileItem>>(parseIndexInParallel, TaskContinuationOptions.NotOnFaulted)
					.ContinueWith<Models.FolderData>(convert, TaskContinuationOptions.NotOnFaulted)
					.Result;
			}
			catch
			{
				throw;
			}
		}

		private static readonly string parserToken = @"(?<name>(\w+\.)*\w+|\w+\.\w+)\s+(?<size>\d+)";

		private static IList<Models.LZSS.FileItem> parseNameSize(object fileMapObj)
		{
			Console.WriteLine("parseNameSize start");
			try
			{
				var fileMap = fileMapObj as string;
				if (string.IsNullOrEmpty(fileMap))
				{
					return null;
				}

				var items = new List<Models.LZSS.FileItem>();

				Regex parser = new Regex(parserToken);
				var matches = parser.Matches(fileMap);
				foreach (Match match in matches)
				{
					long size;
					if (long.TryParse(match.Groups["size"].Value, out size))
					{
						var item = new Models.LZSS.FileItem();
						item.Name = match.Groups["name"].Value;
						item.Size = size;

						items.Add(item);
					}
				}
				return items.Count == 0 ? null : items;
			}
			catch
			{
				throw;
			}
			finally
			{
				Console.WriteLine("parseNameSize end");
			}
		}

		/// <remarks>slower than parseNameSize method</remarks>
		private static IList<Models.LZSS.FileItem> parseNameSizeUpdated(object fileMapObj)
		{
			Console.WriteLine("parseNameSizeUpdated start");
			try
			{
				var fileMap = fileMapObj as string;
				if (string.IsNullOrEmpty(fileMap))
				{
					return null;
				}

				var items = new List<Models.LZSS.FileItem>();

				Regex parser = new Regex(parserToken);

				using (StringReader reader = new StringReader(fileMap))
				{
					while (reader.Peek() > 0)
					{
						var match = parser.Match(reader.ReadLine());
						if (match.Success)
						{
							long size;
							if (long.TryParse(match.Groups["size"].Value, out size))
							{
								var item = new Models.LZSS.FileItem();
								item.Name = match.Groups["name"].Value;
								item.Size = size;

								items.Add(item);
							}
						}
					}
				}

				return items.Count == 0 ? null : items;
			}
			catch
			{
				throw;
			}
			finally
			{
				Console.WriteLine("parseNameSizeUpdated end");
			}
		}

		private static IList<Models.LZSS.FileItem> parseIndex(Task<IList<Models.LZSS.FileItem>> parseNameSizeTask)
		{
			Console.WriteLine("parseIndex start");
			try
			{
				if (parseNameSizeTask.Exception != null)
				{
					throw parseNameSizeTask.Exception;
				}

				var items = parseNameSizeTask.Result;
				if (items == null || items.Count == 0) { return null; }

				Dictionary<string, int> indexMap = new Dictionary<string, int>();
				List<string> names = new List<string>();
				foreach (var item in items)
				{
					if (item.Size == 0) { continue; } //0 means folder	
					
					string name = item.Name;
					if (!names.Contains(name))
					{
						names.Add(name);
						indexMap.Add(name, 0);
					}
					else
					{
						item.Index = ++indexMap[name];
					}
				}

				//Console.WriteLine("distCount - {0}", indexMap.Count);

				//int mission_xml_count = items.Reverse()
				//    .First(item => item.Name == "mission.xml")
				//    .Index + 1;
				//Console.WriteLine("mission_xml_count - {0}", mission_xml_count);

				return items;
			}
			catch
			{
				throw;
			}
			finally
			{
				Console.WriteLine("parseIndex end");
			}
		}

		private static IList<Models.LZSS.FileItem> parseIndexInParallel(Task<IList<Models.LZSS.FileItem>> parseNameSizeTask)
		{
			Console.WriteLine("parseIndexInParallel start");
			try
			{
				if (parseNameSizeTask.Exception != null)
				{
					throw parseNameSizeTask.Exception;
				}

				var items = parseNameSizeTask.Result;
				if (items == null || items.Count == 0) { return null; }

				int partialSize = 1000;
				int partialCount = items.Count / partialSize + (items.Count % partialSize == 0 ? 0 : 1);
				var partials = new List<List<Models.LZSS.FileItem>>(partialCount);
				for (int i = 0; i < partialCount; i++)
				{
					partials.Add(items
						.Skip(i * partialSize)
						.Take(partialSize)
						.ToList());
				}

				var maps = new List<Dictionary<string, int>>();
				Parallel.ForEach(partials, partial =>
				{
					var partialMap = parseIndexPartial(partial);
					if (partialMap != null)
					{
						maps.Add(partialMap);
					}
				});

				//var combinedMap = new Dictionary<string, int>();
				//maps.ForEach(map =>
				//{
				//    foreach (var pair in map)
				//    {
				//        if (!combinedMap.ContainsKey(pair.Key))
				//        {
				//            combinedMap.Add(pair.Key, pair.Value);
				//        }
				//        else
				//        {
				//            combinedMap[pair.Key] += pair.Value;
				//        }
				//    }
				//});

				//Console.WriteLine("distCount - {0}", combinedMap.Count);

				foreach (var item in items.Reverse())
				{
					if (item.Size == 0) { continue; } //0 means folder	

					item.Index = calculateIndex(item.Name, maps);

					//if (item.Name == "mission.xml" && item.Index == 2)
					//{

					//}
				}

				//int mission_xml_count = items.Reverse()
				//    .First(item => item.Name == "mission.xml")
				//    .Index + 1;
				//Console.WriteLine("mission_xml_count - {0}", mission_xml_count);

				//var aaa = items
				//    .Where(item => item.Name == "mission.xml")
				//    .Select(item => item.Index)
				//    .ToList();

				return items;
			}
			catch
			{
				throw;
			}
			finally
			{
				Console.WriteLine("parseIndexInParallel end");
			}
		}

		private static Dictionary<string, int> parseIndexPartial(IList<Models.LZSS.FileItem> partialItems)
		{
			try
			{
				Dictionary<string, int> indexMap = new Dictionary<string, int>();
				List<string> names = new List<string>();
				foreach (var item in partialItems)
				{
					if (item.Size == 0) { continue; } //0 means folder	

					string name = item.Name;
					if (!names.Contains(name))
					{
						names.Add(name);
						indexMap.Add(name, 1);
					}
					else
					{
					    indexMap[name]++;
					}
				}

				return indexMap;
			}
			catch
			{
				throw;
			}
		}

		private static int calculateIndex(string name, List<Dictionary<string, int>> maps)
		{
			int result = 0;
			bool spottedFlag = false;
			foreach (var map in maps)
			{
				if (map.ContainsKey(name))
				{
					if (!spottedFlag)
					{
						spottedFlag = true;
						map[name]--;
					}

					int index = map[name];
					result += index;

					if (index == 0)
					{
						map.Remove(name);
					}
				}
			}

			return result;

			/*
			without parallel:
			5.01, 3.26, 3.38, 3.48, 3.36, 3.20, 3.13, 3.08, 3.08, 3.33	-	3.431

			with parallel:
			8.58, 4.71, 4.87, 4.68, 4.82, 5.10, 5.20, 4.89, 4.72, 4.77	-	5.234

			1.803 !!!
			*/
		}

		private static readonly string folderFilterToken = @"^\w+$";

		private static readonly string rootFolderName = "data_win";

		private static Models.FolderData convert(Task<IList<Models.LZSS.FileItem>> parseIndexTask)
		{
			Console.WriteLine("convert start");
			try
			{
				if (parseIndexTask.Exception != null)
				{
					throw parseIndexTask.Exception;
				}

				var lzssFiles = parseIndexTask.Result;
				if (lzssFiles == null || lzssFiles.Count == 0) { return null; }

				Regex folderFilter = new Regex(folderFilterToken);

				var rootFolder = new Models.FolderData(rootFolderName);
				var currentFolder = rootFolder;
				foreach (var lzssItem in lzssFiles)
				{
					if (folderFilter.IsMatch(lzssItem.Name)) //folder
					{
						if (lzssItem.Name == currentFolder.Name) { continue; }

						var folder = new Models.FolderData(lzssItem.Name);
						rootFolder.Add(folder);
						currentFolder = folder;
					}
					else //file
					{
						var file = new Models.FileData(lzssItem);
						currentFolder.Add(file);
					}
				}

				return rootFolder;
			}
			catch
			{
				throw;
			}
			finally
			{
				Console.WriteLine("convert end");
			}
		}
	}
}
