using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Extractor.Controls
{
    public class FileMapPresenter : ItemsControl
    {
        public FileMapPresenter()
        {
			VirtualizingStackPanel.SetIsVirtualizing(this, false);
        }
    }
}
