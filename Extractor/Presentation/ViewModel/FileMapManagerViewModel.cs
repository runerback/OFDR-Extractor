using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Presentation.ViewModel
{
	public class FileMapManagerViewModel : Common.ViewModelBase
	{
		public FileMapManagerViewModel()
		{
			Business.DAT dat = new Business.DAT();
			dat.Exited += onDATExited;
			dat.Call();
		}

		private void onDATExited(object sender, Business.DAT.ExitedEventArgs e)
		{
			Business.DAT dat = sender as Business.DAT;
			dat.Exited -= onDATExited;

			if (e.HasError)
				throw new Exception(e.Error);

			string output = e.Output;
			if (!string.IsNullOrEmpty(output))
			{
				var root = Business.LZSSFileMapParser.Parse(output);
				if (root != null)
				{
					this.rootSource.Add(root);
					this.NotifyPropertyChanged("RootSource");
				}
			}
		}

		public FileMapManagerViewModel(Data.FolderData root)
		{
			if (root == null)
				throw new ArgumentNullException("root");

			this.rootSource.Add(root);
			this.NotifyPropertyChanged("RootSource");
		}

		private Common.AutoInvokeObservableCollection<Data.FolderData> rootSource =
			new Common.AutoInvokeObservableCollection<Data.FolderData>();
		public Common.AutoInvokeObservableCollection<Data.FolderData> RootSource
		{
			get { return this.rootSource; }
		}

		private FileMapMenuViewModel menuDataContext = new FileMapMenuViewModel();
		public FileMapMenuViewModel MenuDataContext
		{
			get { return this.menuDataContext; }
		}
	}
}
