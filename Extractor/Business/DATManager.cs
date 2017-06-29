using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace Extractor.Business
{
	public static class DATManager
	{
		private static ConcurrentDictionary<int, StringBuilder> receivedDataMap = new ConcurrentDictionary<int, StringBuilder>();

		public static int Call(params string[] args)
		{
			try
			{
				var processInfo = new ProcessStartInfo(
					ConfigManager.DATFilePath,
					args == null ? null : string.Join(" ", args))
				{
					CreateNoWindow = true,
					ErrorDialog = false,
					RedirectStandardError = true,
					RedirectStandardInput = false,
					RedirectStandardOutput = true,
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden,
					WorkingDirectory = ConfigManager.OFDRRootFolder
				};

				Process process = new Process()
				{
					EnableRaisingEvents = true,
					StartInfo = processInfo
				};
				process.ErrorDataReceived += onProcessErrorDataReceived;
				process.OutputDataReceived += onProcessOutputDataReceived;

				int processId;
				try
				{
					process.Start();
					processId = process.Id;
				}
				catch
				{
					return -1;
				}

				process.BeginErrorReadLine();
				process.BeginOutputReadLine();

				Task.Factory.StartNew(waitForReleaseProcess, process);

				return processId;
			}
			catch
			{
				throw;
			}
		}

		private static void onProcessErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				MessageBox.Show(e.Data, "Process error");
				(sender as Process).Kill();
			}
		}

		private static void onProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			var process = sender as Process;
			var output = receivedDataMap.GetOrAdd(
				process.Id,
				id => new StringBuilder());
			output.AppendLine(e.Data);
		}

		private static void waitForReleaseProcess(object processObj)
		{
			var process = processObj as Process;
			process.WaitForExit();

			StringBuilder output;
			if (receivedDataMap.TryRemove(process.Id, out output))
			{
				string receivedData = output.ToString();
				output.Clear();
				raiseDataReceived(process.Id, receivedData);
			}

			raiseExited(process.Id);
			process.Dispose();
		}

		public static event Action<int, string> DataReceived;

		private static void raiseDataReceived(int processID, string data)
		{
			if (DataReceived != null)
			{
				Task.Factory.StartNew(delegate
				{
					DataReceived(processID, data);
				});
			}
		}

		public static event Action<int> Exited;

		private static void raiseExited(int processID)
		{
			if (Exited != null)
			{
				Task.Factory.StartNew(delegate
				{
					Exited(processID);
				});
			}
		}
	}
}
