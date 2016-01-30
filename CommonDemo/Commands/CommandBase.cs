using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommonDemo.Commands
{
    /// <summary>Provides a base implementation of the <see cref="ICommand"/> interface. </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>Gets a value indicating whether the command can execute in its current state. </summary>
        public abstract bool CanExecute { get; }

        /// <summary>Defines the method to be called when the command is invoked. </summary>
        protected abstract void Execute(object parameter);

        /// <summary>Tries to execute the command by checking the <see cref="CanExecute"/> property 
        /// and executes the command only when it can be executed. </summary>
        /// <returns>True if command has been executed; false otherwise. </returns>
        public bool TryExecute(object parameter)
        {
            if (!CanExecute)
                return false;
            Execute(parameter);
            return true;
        }

        /// <summary>Occurs when changes occur that affect whether or not the command should execute. </summary>
        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }
    }
}
