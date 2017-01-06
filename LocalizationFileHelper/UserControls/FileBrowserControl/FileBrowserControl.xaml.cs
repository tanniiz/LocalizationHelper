using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LocalizationFileHelper.UserControls
{
    /// <summary>
    /// Interaction logic for FileBrowserControl.xaml
    /// </summary>
    public partial class FileBrowserControl : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty FilePathProperty =
                    DependencyProperty.Register
                    (
                        "FilePath", typeof(string), typeof(FileBrowserControl),
                        new UIPropertyMetadata(string.Empty)
                    );
        public string FilePath
        {
            get
            {
                return (string)GetValue(FilePathProperty);
            }
            set
            {
                SetValue(FilePathProperty, value);
            }
        }

        public static readonly DependencyProperty BrowsingTypeProperty =
                    DependencyProperty.Register
                    (
                        "BrowsingType", typeof(BrowsingType), typeof(FileBrowserControl),
                        new UIPropertyMetadata(BrowsingType.File)
                    );
        public BrowsingType BrowsingType
        {
            get
            {
                return (BrowsingType)GetValue(BrowsingTypeProperty);
            }
            set
            {
                SetValue(BrowsingTypeProperty, value);
            }
        }
        #endregion

        public FileBrowserControl()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            if (BrowsingType == BrowsingType.File)
            {
                OpenFileBrowserDialog();   
            } else if (BrowsingType == BrowsingType.Directory)
            {
                OpenFolderBrowserDialog();
            }
        }

        private void OpenFileBrowserDialog()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
 
            dlg.DefaultExt = ".json";
            dlg.Filter = "JSON File (*.json)|*.json";
            
            Nullable<bool> result = dlg.ShowDialog();
            
            if (result == true)
            {
                FilePath = dlg.FileName;
            }
        }

        private void OpenFolderBrowserDialog()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                FilePath = dialog.SelectedPath;
            }
        }
    }

    public enum BrowsingType
    {
        File,
        Directory
    }
}
