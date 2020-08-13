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
using System.Security;
using System.Collections.ObjectModel;

using Booth.WpfControls;

namespace Booth.WpfControls.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (DataContext as MainViewModel);
            vm.MyDialog.Show();
        }
    }

    public class MainViewModel
    {
        public LogonWindow MyDialog { get; set; }

        public MainViewModel()
        {
            MyDialog = new LogonWindow();
        }

    }

    public class LogonWindow : PopupWindow
    {
        public string User { get; set; }
        public string Password { get; set; }
        public SecureString SecurePassword { get; set; }

        public LogonWindow()
        {
            Commands.Add(new DialogCommand("OK", new RelayCommand(this.OK)) { IsDefault = true });
            Commands.Add(new DialogCommand("Cancel", new RelayCommand(this.Cancel)) { IsCancel = true });
        }

        public void OK()
        {
            User = "";
            Password = null;
            SecurePassword = null;
            OnPropertyChanged("");

            Close();
        }

        public void Cancel()
        {
            Close();
        }
    }

    public class PopupWindow : NotifyClass
    {
        private bool _IsOpen;
        public bool IsOpen
        {
            get
            {
                return _IsOpen;
            }
            set
            {
                if (_IsOpen != value)
                {
                    _IsOpen = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<DialogCommand> Commands { get; private set; }

        public PopupWindow()
        {
            Commands = new ObservableCollection<DialogCommand>();
        }

        public void Show()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }

    }

}
