using System.Windows;

namespace Extractor.Data
{
	public class DragDropDataObject
	{
		protected DragDropDataObject(object source, object data)
		{
			this.dataObject = new DataObject();
			this.dataObject.SetData(_sourceFormatName, source);
			this.dataObject.SetData(_dataFormatName, data);
			this.source = source;
			this.data = data;
		}

		protected DragDropDataObject(IDataObject data)
		{
			if (data != null)
			{
				this.source = data.GetData(_sourceFormatName);
				this.data = data.GetData(_dataFormatName);
			}
			this.dataObject = data;
		}

		private IDataObject dataObject;
		public IDataObject DataObject
		{
			get { return this.dataObject; }
		}

		private object source;
		public object Source
		{
			get { return this.source; }
		}

		private object data;
		public object Data
		{
			get { return this.data; }
		}

		private static readonly string _sourceFormatName = "Source";
		private static readonly string _dataFormatName = "Data";

		public static IDataObject Create(object source, object data)
		{
			return new DragDropDataObject(source, data).DataObject;
		}

		public static DragDropDataObject Parse(IDataObject data)
		{
			return new DragDropDataObject(data);
		}
	}
}
