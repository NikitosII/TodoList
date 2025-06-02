using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    internal class TodoModel: INotifyPropertyChanged
    {
        // Дата и время создания задачи
        public DateTime CreationDate { get; set; } = DateTime.Now;

        // Флаг выполнения задачи
        private bool _isDone;
        public bool IsDone
        {
            get { return _isDone; }
            set 
            {
                if (_isDone == value)
                    return;
                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }

        // Текст задачи
        private string _text;
        public string Text
        {
            get { return _text; }
            set 
            { 
                if (_text == value) return;
                _text = value; 
                OnPropertyChanged("Text");
            }
        }

        // Событие для уведомления об изменении свойств
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для вызова события PropertyChanged
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }   
        }



    }
}
