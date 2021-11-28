using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_002_Diary
{
    internal class Program
    {

        #region Домашнее задание. 
        /// Разработать ежедневник.
        /// В ежедневнике реализовать возможность 
        /// - создания
        /// - удаления
        /// - реактирования 
        /// записей
        /// 
        /// В отдельной записи должно быть не менее пяти полей
        /// 
        /// Реализовать возможность 
        /// - Загрузки даннах из файла
        /// - Выгрузки даннах в файл
        /// - Добавления данных в текущий ежедневник из выбранного файла
        /// - Импорт записей по выбранному диапазону дат
        /// - Упорядочивания записей ежедневника по выбранному полю

        #endregion

        static void Main(string[] args)
        {
            string mainPath = @"D:\Diary.txt";

            Diary diary = new Diary(mainPath);

            Console.WriteLine("-- == Е Ж Е Д Н Е В Н И К == --");
            Console.WriteLine();
            Console.WriteLine($"Фаил дневника:         {mainPath}");
            Console.WriteLine($"Записей в дневнике:    {diary.Count}");
            Console.WriteLine();

            Prompt(ref diary);
        }
        /// <summary>
        /// Приглашение пользователя к дальнейшим действиям
        /// </summary>
        /// <param name="diary"></param>
        static void Prompt(ref Diary diary)
        {
            Console.WriteLine("Выберите дальнейшее действие:");
            Console.WriteLine("1 - Создать запись вручную");
            if (diary.Count != 0) Console.WriteLine("2 - Удалить запись");
            if (diary.Count != 0) Console.WriteLine("3 - Изменить запись");
            if (diary.Count > 1) Console.WriteLine("4 - Упорядочить записи в дневнике");
            Console.WriteLine("5 - Импорт записей из файла");
            Console.WriteLine("6 - Сохранить дневник в файл");
            if (diary.Count != 0) Console.WriteLine("7 - Вывести содержимое дневника в консоль");

            do
            {
                string key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        CreateRecord(ref diary);
                        break;
                    case "2":
                        DeleteRecord(ref diary);
                        break;
                    case "3":
                        EditRecord(ref diary);
                        break;
                    case "4":
                        SortRecords(ref diary);
                        break;
                    case "5":
                        ImportFile(ref diary);
                        break;
                    case "6":
                        SaveDiaryToFile(ref diary);
                        break;
                    case "7":
                        Print(ref diary);
                        break;
                    default:
                        continue;
                }

                break;

            } while (true);

        }


        static void CreateRecord(ref Diary diary)
        {
            Console.WriteLine("Создание записи в ежедневнике");
            Console.WriteLine("Введите заголовок записи:");
            string title = Console.ReadLine(); // title
            Console.WriteLine();
            Console.WriteLine("Введите дату /дд.мм.гггг чч:мм/ (пустое поле - сегодня)");
            string datetime = Console.ReadLine(); // datetime
            if (datetime.Length == 0) datetime = DateTime.Now.ToString("g");
            Console.WriteLine();
            Console.WriteLine("Введите текст записи:");
            string task = Console.ReadLine(); // task
            Console.WriteLine();
            Console.WriteLine("Введите контакт:");
            string contact = Console.ReadLine(); // contact

            Success(diary.Create(title, datetime, task, contact));

            Console.WriteLine();
            Prompt(ref diary);

        }

        static void DeleteRecord(ref Diary diary)
        {
            Console.WriteLine("Удаление записи");
            Console.Write("Укажите ID записи для удаления:   ");
            int id = int.Parse(Console.ReadLine());
            Success(diary.Delete(id));

            Console.WriteLine();
            Prompt(ref diary);

        }

        static void EditRecord(ref Diary diary)
        {
            Console.WriteLine("Редактирование записи в ежедневнике");
            Console.WriteLine("Введите номер записи для редактирования (оставить пустым - без изменения):");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите заголовок записи:");
            string title = Console.ReadLine(); // title
            Console.WriteLine();
            Console.WriteLine("Введите дату /дд.мм.гггг чч:мм/ (пустое поле - сегодня)");
            string datetime = Console.ReadLine(); // datetime
            Console.WriteLine();
            Console.WriteLine("Введите текст записи:");
            string task = Console.ReadLine(); // task
            Console.WriteLine();
            Console.WriteLine("Введите контакт:");
            string contact = Console.ReadLine(); // contact

            Success(diary.Update(id, title, datetime, task, contact));

            Console.WriteLine();
            Prompt(ref diary);

        }

        static void SortRecords(ref Diary diary)
        {
            Console.WriteLine("Упорядочивание записей в ежедневнике");
            Console.Write("Укажите номер поля для упорядочивания:");
            Console.Write("1 - ID, 2 - Заголовок, 3 - Дата, 4 - Контакт:");
            int key = int.Parse(Console.ReadLine());
            diary.Sort(key);

            Console.WriteLine();
            Prompt(ref diary);

        }

        static void ImportFile(ref Diary diary)
        {
            Console.WriteLine("Импорт данных из файла");
            Console.WriteLine("Укажите путь к файлу для импорта:");
            string path = Console.ReadLine();

            Console.WriteLine("Что импортировать?:");
            Console.WriteLine("1 - Импорт файла целиком, 2 - импорт по диапазону дат");
            do
            {
                string key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        Success(diary.Load(path));
                        break;
                    case "2":
                        Console.WriteLine("Укажите диапазон дат:");
                        string date1 = Console.ReadLine();
                        string date2 = Console.ReadLine();
                        Success(diary.Load(path, date1, date2));
                        break;
                    default:
                        continue;
                }

                break;

            } while(true);

            Console.WriteLine();
            Prompt(ref diary);

        }

        static void SaveDiaryToFile(ref Diary diary)
        {
            Console.WriteLine("Сохранение данных в файл");
            Success(diary.Save());
            Console.WriteLine();
            Prompt(ref diary);

        }

        static void Print(ref Diary diary)
        {
            Console.WriteLine();
            diary.Print();
            Console.WriteLine();
            Prompt(ref diary);
        }

        static void Success(bool flag)
        {
            if (flag)
            {
                Console.WriteLine("Успешно!");
            }
            else
            {
                Console.WriteLine("Действие не удалось!");
            }

        }

    }
}
