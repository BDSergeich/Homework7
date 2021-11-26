using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_002_Diary
{
    struct Record
    {
        #region Поля
        private static int count;
        private int recordId;
        //private string title;
        //private DateTime dateAndTime;
        //private string task;
        //private string contact;
        #endregion

        #region Свойства

        public int RecordId
        {
            get { return recordId; }
        }

        public string Title { get; set; }

        public DateTime DateAndTime { get; set; }

        public string Task { get; set; }

        public string Contact { get; set; }

        #endregion

        #region Конструкторы
        public Record(string Title, DateTime DateAndTime, string Task, string Contact)
        {
            count++;
            this.recordId = count;
            this.Title = Title;
            this.DateAndTime = DateAndTime;
            this.Task = Task;
            this.Contact = Contact;
        }

        public Record(DateTime DateAndTime, string Task, string Contact) :
            this(string.Empty, DateAndTime, Task, Contact)
        {
            Title = $"Запсь {recordId}";
        }


        public Record(string Title, string Task, string Contact) :
            this(Title, DateTime.Now, Task, Contact)
        {
        }

        public Record(string Title, string Task) :
            this(Title, DateTime.Now, Task, string.Empty)
        {
        }

        public Record(string Task) :
            this(string.Empty, DateTime.Now, Task, string.Empty)
        {
            Title = $"Запсь {recordId}";
        }
        #endregion

        #region Методы

        public string Print()
        {
            return $"{recordId,5} {this.Title} {this.DateAndTime.ToString("g"), 16} {this.Task} {this.Contact}";
        }
        #endregion
    }
}
