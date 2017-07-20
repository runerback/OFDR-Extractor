using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Extractor.Business
{
	public class DAT
	{
		private readonly object locker = new object();

		public void Call(params string[] args)
		{
			ExceptionManager.NewTask(callMethod, args);
		}

		private StringBuilder outputBuilder = new StringBuilder();
		private StringBuilder errorBuilder = new StringBuilder();

		private void callMethod(object args)
		{
			lock (locker)
			{
				try
				{
					startProcess(args as string[]);

					ExitedEventArgs exitedArgs = errorBuilder.Length > 0 ?
						new ExitedEventArgs(outputBuilder.ToString(), errorBuilder.ToString()) :
							 new ExitedEventArgs(outputBuilder.ToString());
					if (this.Exited != null)
					{
						this.Exited(this, exitedArgs);
					}
				}
				finally
				{
					outputBuilder.Clear();
					errorBuilder.Clear();
				}
			}
		}

		private void startProcess(string[] args)
		{
			using (Process process = new Process())
			{
				process.StartInfo.FileName = ConfigManager.DATFilePath;
				process.StartInfo.Arguments = args == null ? null : string.Join(" ", args);
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.ErrorDialog = true;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.StartInfo.WorkingDirectory = ConfigManager.OFDRRootFolder;

				process.OutputDataReceived += onOutputDataReceived;
				process.ErrorDataReceived += onErrorDataReceived;

				process.Start();

				process.BeginOutputReadLine();
				process.BeginErrorReadLine();

				process.WaitForExit();

				process.OutputDataReceived -= onOutputDataReceived;
				process.ErrorDataReceived -= onErrorDataReceived;
			}
		}

		private void onOutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				outputBuilder.AppendLine(e.Data);
			}
		}

		private void onErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				errorBuilder.AppendLine(e.Data);
			}
		}

		public event EventHandler<ExitedEventArgs> Exited;

		public class ExitedEventArgs : EventArgs
		{
			public ExitedEventArgs(string output)
			{
				this.output = output;
			}

			public ExitedEventArgs(string output, string error)
				: this(output)
			{
				this.hasError = true;
				this.error = error;
			}

			private string output;
			public string Output
			{
				get { return this.output; }
			}

			private string error;
			public string Error
			{
				get { return this.error; }
			}

			private bool hasError;
			public bool HasError
			{
				get { return this.hasError; }
			}
		}
	}
}
