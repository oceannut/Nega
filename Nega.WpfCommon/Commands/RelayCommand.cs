using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Nega.WpfCommon
{

    /// <summary>
    /// 中继命令。
    /// </summary>
    /// <typeparam name="T">参数值类型。</typeparam>
    public class RelayCommand<T> : ICommand
    {

        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;

        /// <summary>
        /// 中继命令。
        /// </summary>
        /// <param name="execute">执行命令的操作。</param>
        public RelayCommand(Action<T> execute) 
            : this(execute, null)
        {
        }

        /// <summary>
        /// 中继命令。
        /// </summary>
        /// <param name="execute">执行命令的操作。</param>
        /// <param name="canExecute">判断命令是否能执行的操作。</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return (canExecute == null) ? true : canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }

    }
}
