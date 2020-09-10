using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RoslynPad.UI.Dialogs
{
    public interface IAddOutputFileDialog : IDialog
    {
        public ObservableCollection<string> CurrentFilePaths { get; set; }
    }
}
