using System;
using System.Diagnostics;

namespace CommonDemo.Commands
{
    public class TestCommand : CommandBase
    {
        public override bool CanExecute => true;

        protected override void Execute(object parameter)
        {
            Debug.WriteLine(parameter + ".....");
        }
    }
}