using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UIFlights
{
    public class CloseCommand : ICommand 
    { 
        public bool CanExecute(object parameter) 
        { return true; } 
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        } 
        public void Execute(object wnd) {
            if (wnd is Window) ((Window)wnd).Close();
        } 
    }

}
