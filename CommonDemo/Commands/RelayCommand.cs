using System;
using System.Windows.Input;

namespace CommonDemo.Commands
{
     /// <summary>Provides an implementation of the <see cref="ICommand"/> interface. </summary>
    public class RelayCommand : CommandBase
    {
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>Initializes a new instance of the <see cref="RelayCommand"/> class. </summary>
        /// <param name="execute">The action to execute. </param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        { }

        /// <summary>Initializes a new instance of the <see cref="RelayCommand"/> class. </summary>
        /// <param name="execute">The action to execute. </param>
        /// <param name="canExecute">The predicate to check whether the function can be executed. </param>
        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>Defines the method to be called when the command is invoked. </summary>
        protected override void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>Gets a value indicating whether the command can execute in its current state. </summary>
        public override bool CanExecute => _canExecute?.Invoke() ?? true;
    }
}