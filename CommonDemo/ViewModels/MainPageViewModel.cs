using System.Diagnostics;
using System.Windows.Input;
using CommonDemo.Commands;

namespace CommonDemo.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand TestCommand { get; private set; }
        public MainPageViewModel()
        {
            TestCommand = new RelayCommand(TestCmd);
        }

        public void TestCmd(object parameter)
        {
            Debug.WriteLine("TestCmd");
        }
    }
}