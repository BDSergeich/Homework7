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
            Diary diary = new Diary(@"D:\Diary.txt");
            //for (int i = 0; i < 5; i++)
            //    diary.Create();
            //diary.Save();
            //diary.Delete(1);
            //diary.Sort(3);
            //diary.Save();
            //diary.Edit(6);
            //diary.Save();

        }
    }
}
