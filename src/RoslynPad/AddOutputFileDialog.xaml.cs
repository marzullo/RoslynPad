using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Composition;
using System.Composition.Hosting;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Avalon.Windows.Annotations;
using Avalon.Windows.Controls;
using NuGet.Packaging;
using RoslynPad.UI;
using RoslynPad.UI.Dialogs;

namespace RoslynPad
{
    /// <summary>
    /// Interaction logic for AddOutputFileDialog.xaml
    /// </summary>
    [Export(typeof(IAddOutputFileDialog))]
    internal partial class AddOutputFileDialog : INotifyPropertyChanged, IAddOutputFileDialog
    {
        private InlineModalDialog _dialog;

        #pragma warning disable CS8618 // Non-nullable field is uninitialized.
        public AddOutputFileDialog()
        #pragma warning restore CS8618 // Non-nullable field is uninitialized.
        {
            DataContext = this;
            InitializeComponent();
        }

        public Task ShowAsync()
        {
            _dialog = new InlineModalDialog
                      {
                          Owner = Application.Current.MainWindow,
                          Content = this
                      };
            _dialog.Show();
            return Task.CompletedTask;
        }

        private async void AddFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialogAdapter();
            dialog.Filter = new FileDialogFilter("All Files", "*");

            var selectedFiles = await dialog.ShowAsync();

            CurrentFilePaths.AddRange(selectedFiles ?? new string[]{});
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Close()
        {
            _dialog?.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<string> CurrentFilePaths
        {
            get;
            set;
        } = new ObservableCollection<string>();

        public int SelectedFile { get; set; }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
           CurrentFilePaths.RemoveAt(SelectedFile);
        }
    }
}
