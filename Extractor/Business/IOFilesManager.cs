using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Extractor.Business
{
	public static class IOFilesManager
	{
		public static void Export(Models.FolderData root, string path)
		{
			try
			{
				Models.IOFiles.Folder outerRoot = toOuter(root);
				if (outerRoot == null) return;
				if (string.IsNullOrWhiteSpace(path)) return;

				using (var stream = createStream(path, InOut.Out))
				{
					using (var writer = createWriter(stream))
					{
						serializer.Value.Serialize(writer, outerRoot, xmlNamespaces.Value);
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public static Models.FolderData Import(string path)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(path)) return null;

				object data;
				using (var stream = createStream(path, InOut.In))
				{
					using (var reader = createReader(stream))
					{
						data = serializer.Value.Deserialize(reader);
					}
				}

				return toInner(data as Models.IOFiles.Folder);
			}
			catch
			{
				throw;
			}
		}

		private static FileStream createStream(string path, InOut io)
		{
			if (io == InOut.In)
			{
				FileInfo inputFile = new FileInfo(path);
				if (!inputFile.Exists)
				{
					throw new FileNotFoundException(inputFile.FullName);
				}
				return new FileStream(inputFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			else if (io == InOut.Out)
			{
				FileInfo outputFile = new FileInfo(path);
				if (!outputFile.Directory.Exists)
				{
					outputFile.Directory.Create();
				}
				return new FileStream(outputFile.FullName, FileMode.Create, FileAccess.Write, FileShare.Write);
			}
			else
			{
				throw new NotImplementedException(string.Format("InOut.{0}", io));
			}
		}

		private enum InOut
		{
			In,
			Out
		}

		#region convert

		private static Models.FileData toInner(Models.IOFiles.File outer)
		{
			if (outer == null) return null;
			return new Models.FileData(outer);
		}

		private static Models.IOFiles.File toOuter(Models.FileData inner)
		{
			if (inner == null) return null;
			return new Models.IOFiles.File()
			{
				Name = inner.Name,
				Size = inner.Size,
				Index = inner.Index
			};
		}

		private static Models.FolderData toInner(Models.IOFiles.Folder outer)
		{
			if (outer == null) return null;

			var innerFolder = new Models.FolderData(outer.Name);
			if (outer.ShouldSerializeSubFolders())
			{
				foreach (var subFolder in outer.SubFolders)
				{
					var inner = toInner(subFolder);
					if (inner != null)
						innerFolder.Add(inner);
				}
			}
			if (outer.ShouldSerializeFiles())
			{
				foreach (var file in outer.Files)
				{
					var inner = toInner(file);
					if (inner != null)
						innerFolder.Add(inner);
				}
			}
			return innerFolder;
		}

		private static Models.IOFiles.Folder toOuter(Models.FolderData inner)
		{
			if (inner == null) return null;

			var outerFolder = new Models.IOFiles.Folder() { Name = inner.Name };
			if (inner.SubFolders.Count > 0)
			{
				outerFolder.SubFolders = new List<Models.IOFiles.Folder>();
				foreach (var subFolder in inner.SubFolders)
				{
					var outer = toOuter(subFolder);
					if (outer != null)
						outerFolder.SubFolders.Add(outer);
				}
			}
			if (inner.Files.Count > 0)
			{
				outerFolder.Files = new List<Models.IOFiles.File>();
				foreach (var file in inner.Files)
				{
					var outer = toOuter(file);
					if (file != null)
						outerFolder.Files.Add(outer);
				}
			}
			return outerFolder;
		}

		#endregion convert

		#region serialize

		private static Lazy<XmlSerializer> serializer = new Lazy<XmlSerializer>(
			() => new XmlSerializer(typeof(Models.IOFiles.Folder)),
			false);

		private static XmlWriter createWriter(Stream output)
		{
			return XmlWriter.Create(output, writerSettings.Value);
		}

		private static XmlReader createReader(Stream input)
		{
			return XmlReader.Create(input, readerSettings.Value);
		}

		private static Lazy<XmlWriterSettings> writerSettings = new Lazy<XmlWriterSettings>(
			() => new XmlWriterSettings() { Indent = true },
			true);

		private static Lazy<XmlReaderSettings> readerSettings = new Lazy<XmlReaderSettings>(
			() =>
			{
				return new XmlReaderSettings()
				{
					IgnoreComments = true,
					IgnoreWhitespace = true
				};
			},
			true);

		private static Lazy<XmlSerializerNamespaces> xmlNamespaces = new Lazy<XmlSerializerNamespaces>(
			() =>
			{
				XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
				xmlNamespaces.Add("", "");
				return xmlNamespaces;
			},
			true);

		#endregion serialize
	}
}
