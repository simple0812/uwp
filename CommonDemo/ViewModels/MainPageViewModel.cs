using System.Diagnostics;
using System.Windows.Input;
using CommonDemo.Commands;

namespace CommonDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        private ICommand _testCommand;
        public ICommand TestCommand
        {
            get
            {
                return _testCommand;
            }
            set
            {
                _testCommand = value;
                OnPropertyChanged();
            }
        }

        private int _x;
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            TestCommand = new RelayCommand(TestCmd);
            X = 100;
        }

        public void TestCmd(object parameter)
        {
            Debug.WriteLine("TestCmd");
        }
    }
}