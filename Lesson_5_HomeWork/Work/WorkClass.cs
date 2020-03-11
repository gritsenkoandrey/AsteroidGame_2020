using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5_HomeWork.Work
{
    class WorkClass : INotifyPropertyChanged
    {
        private string _employee;
        private string _department;
        public string Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
            }
        }
        public string Department
        {
            get { return _department; }
            set
            {
                _department = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
