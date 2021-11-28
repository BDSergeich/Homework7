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
        
        // Индекс текущего элемента массива для добавления
        private int index;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Path">Путь к файлу ежедневника</param>
        public Diary(string Path)
        {
            this.path = Path;
            this.records = new Record[1];
            this.index = 0;
            this.Load(Path);
        }

        /// <summary>
        /// Количество записей в дневнике
        /// </summary>
        public int Count { get { return this.index; } }

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
        /// Создание записи в ежедневнике вручную
        /// </summary>
        public bool Create(string title, string dateTime, string task, string contact)
        {
            try
            {
                Add(new Record(title, DateTime.Parse(dateTime), task, contact));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Удаление записи из ежедневника
        /// </summary>
        /// <param name="rec">Ежедневник</param>
        /// <param name="index">Индекс записи для удаления</param>
        public bool Delete(int index)
        {
            try
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Редактирование записи в ежедневнике
        /// </summary>
        /// <param name="index"></param>
        public bool Update(int index, string title, string dateTime, string task, string contact)
        {
            try
            {
                for (int i = 0; i < this.index; i++)
                    if (this.records[i].RecordId == index) index = i;

                ref Record rec = ref this.records[index];

                if (title != string.Empty) rec.Title = title;
                if (dateTime != string.Empty) rec.DateAndTime = DateTime.Parse(dateTime);
                if (task != string.Empty) rec.Task = task;
                if (contact != string.Empty) rec.Contact = contact;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Загрузка данных из файла по указанному пути
        /// </summary>
        /// <param name="path">Путь к файлу данных для импорта</param>
        public bool Load(string path)
        {
            try
            {
                if (!IsExistedFile(path))
                {
                    File.Create(path).Close();
                }
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] args = sr.ReadLine().Split(';');
                        Add(new Record(args[0], DateTime.Parse(args[1]), args[2], args[3]));
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Импорт данных по выбранному диапазону дат
        /// </summary>
        /// <param name="path">Путь к файлу для импорта</param>
        /// <param name="start">Начало диапазона дат для импора</param>
        /// <param name="end">Конец диапазона дат для импорта</param>
        public bool Load(string path, string start, string end)
        {
            try
            {
                if (!IsExistedFile(path))
                {
                    File.Create(path).Close();
                }

                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] args = sr.ReadLine().Split(';');
                        if (DateTime.Parse(args[1]) >= DateTime.Parse(start) &&
                            DateTime.Parse(args[1]) <= DateTime.Parse(end))
                        {
                            Add(new Record(args[0], DateTime.Parse(args[1]), args[2], args[3]));
                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Сохранение данных в файл по указанному пути
        /// </summary>
        /// <param name="path">Путь к файлу для сохранения данных</param>
        public bool Save()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(this.path))
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
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
        public bool Sort(int numField)
        {
            try
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
                return true;

            }
            catch (Exception ex)
            {
                return false;
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

        public void Print()
        {
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine("{0}   {1}   {2}   {3}   {4}",
                    this.records[i].RecordId,
                    this.records[i].Title,
                    this.records[i].DateAndTime.ToString("g"),
                    this.records[i].Task,
                    this.records[i].Contact);
            }
        }


        
        
    }
}
