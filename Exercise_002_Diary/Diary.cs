using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Exercise_002_Diary
{
    struct Diary
    {
        #region Домашнее задание. 
        /// Разработать ежедневник.
        /// В ежедневнике реализовать возможность 
        /// + создания
        /// + удаления
        /// + реактирования 
        /// записей
        /// 
        /// В отдельной записи должно быть не менее пяти полей
        /// 
        /// Реализовать возможность 
        /// + Загрузки даннах из файла
        /// + Выгрузки даннах в файл
        /// + Добавления данных в текущий ежедневник из выбранного файла
        /// + Импорт записей по выбранному диапазону дат
        /// + Упорядочивания записей ежедневника по выбранному полю

        #endregion

        private Record[] records;
        // Путь к файлу ежедневника
        private string path;
        //// Путь к файлу для импорта в ежедневник
        //private string uPath;
        ///// <summary>
        ///// Путь к файлу для импорта
        ///// </summary>
        //public string UPath { set { uPath = value; } }
        
        // Индекс текущего элемента массива для добавления
        private int index;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Path">Путь к файлу ежедневника</param>
        public Diary(string Path)
        {
            this.path = Path;
            //this.uPath = string.Empty;
            this.records = new Record[1];
            this.index = 0;
            this.Load();
        }

        /// <summary>
        /// Добавление записи в ежедневник
        /// </summary>
        /// <param name="rec">Записть</param>
        public void Add(Record rec)
        {
            this.Resize(index >= this.records.Length);
            this.records[index] = rec;
            this.index++;
        }

        /// <summary>
        /// Пользовательский ввод для добавления или изменения записи
        /// </summary>
        /// <returns></returns>
        private string[] GetUserData()
        {
            string[] args = new string[4];

            Console.WriteLine("Введите заголовок записи:");
            args[0] = Console.ReadLine(); // title
            Console.WriteLine();
            Console.WriteLine("Введите дату /дд.мм.гггг чч:мм/ (пустое поле - сегодня)");
            args[1] = Console.ReadLine(); // datetime
            if (args[1].Length == 0) args[1] = DateTime.Now.ToString("g");
            Console.WriteLine();
            Console.WriteLine("Введите текст записи:");
            args[2] = Console.ReadLine(); // task
            Console.WriteLine();
            Console.WriteLine("Введите контакт:");
            args[3] = Console.ReadLine(); // contact

            return args;
        }

        /// <summary>
        /// Создание записи в ежедневнике вручную
        /// </summary>
        public void Create()
        {
            Console.Clear();
            Console.WriteLine("Добавить запись в ежедневник");
            Console.WriteLine();
            string[] args = GetUserData();
            Add(new Record(args[0], DateTime.Parse(args[1]), args[2], args[3]));
        }

        /// <summary>
        /// Удаление записи из ежедневника
        /// </summary>
        /// <param name="rec">Ежедневник</param>
        /// <param name="index">Индекс записи для удаления</param>
        public void Delete(int index)
        {
            Record[] temp = new Record[this.records.Length - 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = this.records[i];
            }
            for (int i = index + 1; i < temp.Length; i++)
            {
                temp[i - 1] = this.records[i];
            }
            this.records = temp;
            this.index--;
        }

        /// <summary>
        /// Редактирование записи в ежедневнике
        /// </summary>
        /// <param name="index"></param>
        public void Edit(int index)
        {
            Console.Clear();
            Console.WriteLine("Редактирование записи");

            for (int i = 0; i < this.index; i++)
                if (this.records[i].RecordId == index) index = i;
                
            ref Record rec = ref this.records[index];

            string[] args = GetUserData();

            rec.Title = (args[0] == string.Empty) ? rec.Title : args[0];
            rec.DateAndTime = (args[1] == string.Empty) ? rec.DateAndTime : DateTime.Parse(args[1]);
            rec.Task = (args[2] == string.Empty) ? rec.Task : args[2];
            rec.Contact = (args[3] == string.Empty) ? rec.Contact : args[3];
        }

        /// <summary>
        /// Загрузка данных из файла по дефолтному пути
        /// </summary>
        public void Load()
        {
            Load(this.path);
        }
        /// <summary>
        /// Загрузка данных из файла по указанному пути
        /// </summary>
        /// <param name="path">Путь к файлу данных для импорта</param>
        public void Load(string path)
        {
            if (!IsExistedFile(path))
            {
                File.Create(path);
            }
            using(StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split(';');
                    Add(new Record(args[0], DateTime.Parse(args[1]), args[2], args[3]));
                }
            }
        }
        /// <summary>
        /// Импорт данных по выбранному диапазону дат
        /// </summary>
        /// <param name="path">Путь к файлу для импорта</param>
        /// <param name="start">Начало диапазона дат для импора</param>
        /// <param name="end">Конец диапазона дат для импорта</param>
        public void Load(string path, DateTime start, DateTime end)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split(';');
                    if (DateTime.Parse(args[1]) >= start && DateTime.Parse(args[1]) <= end)
                    {
                        Add(new Record(args[0], DateTime.Parse(args[1]), args[2], args[3]));
                    }
                }

            }
        }

        /// <summary>
        /// Сохранение данных в файл по дефолтному пути
        /// </summary>
        public void Save()
        {
            Save(this.path);
        }
        /// <summary>
        /// Сохранение данных в файл по указанному пути
        /// </summary>
        /// <param name="path">Путь к файлу для сохранения данных</param>
        public void Save(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < this.index; i++)
                {
                    Record rec = this.records[i];

                    string temp = String.Format("{0};{1};{2};{3}", 
                        rec.Title, 
                        rec.DateAndTime.ToString("g"), 
                        rec.Task, 
                        rec.Contact);
                    sw.WriteLine(temp, (i == 0));
                }
            }
        }


        /// <summary>
        /// Метод меняет местами элементы массива
        /// </summary>
        /// <param name="elem1">Элемент 1</param>
        /// <param name="elem2">Элемент 2</param>
        private void Swap(ref Record elem1, ref Record elem2)
        {
            Record temp = elem1;
            elem1 = elem2;
            elem2 = temp;
        }

        /// <summary>
        /// Сортировка
        /// </summary>
        /// <param name="numField">Условный номер поля. 1-ID, 2-заголовок, 3-дата, 4-контакт</param>
        public void Sort(int numField)
        {
            for (int i = 1; i < this.index; i++)
            {
                for (int j = 0; j < this.index - i; j++)
                {
                    switch (numField)
                    {
                        case 1:
                            if (this.records[j].RecordId > this.records[j + 1].RecordId)
                                Swap(ref this.records[j], ref this.records[j + 1]);
                            break;
                        case 2:
                            if (String.Compare(this.records[j].Title, this.records[j + 1].Title) > 0)
                                Swap(ref this.records[j], ref this.records[j + 1]);
                            break;
                        case 3:
                            if (this.records[j].DateAndTime > this.records[j + 1].DateAndTime)
                                Swap(ref this.records[j], ref this.records[j + 1]);
                            break;
                        default:
                            if (String.Compare(this.records[j].Contact, this.records[j + 1].Contact) > 0)
                                Swap(ref this.records[j], ref this.records[j + 1]);
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Изменение размера массива
        /// </summary>
        /// <param name="flag"></param>
        private void Resize(bool flag)
        {
            if (flag)
            {
                Array.Resize(ref this.records, this.records.Length * 2);
            }
        }
        

        private bool IsExistedFile(string path)
        {
            return File.Exists(path);
        }
        
    }
}
