using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace ConsoleApp8
{
    class Program
    {
        /// ГЛОБАЛЬНЫЕ ПЕРЕМЕННЫЕ
        public static string current_login = string.Empty; // ДАННЫЙ ПОЛЬЗОВАТЕЛЬ СИСТЕМЫ
        public static string current_password = string.Empty; // ПАРОЛЬ ДАННОГО ПОЛЬЗОВАТЕЛЯ СИСТЕМЫ
        public static bool admin_changed = false; // ПЕРЕМЕННАЯ ДЛЯ ПРОВЕРКИ СМЕНЫ АДМИНИСТРАТОРА
        public static int user_id;  //id ПОЛЬЗОВАТЕЛЯ

        private static void SystemLogin() // ВХОД В СИСТЕМУ
        {
            int Choise_Login;
            do{
                Console.Clear();
                admin_changed = false;
                GreenText("Генерация кораблей стоящих в порту и их динамическое хранение \nв виде кортежей, списков и обобщенных коллекций\n\n");
                YellowText("Войти в систему?\n\n1.Войти\n0.Завыршить работу\n\n");
                Choise_Login = ReadOnlyInt(0, 1);    
                switch(Choise_Login)
                {
                    case 1:
                        {
                            try
                            {
                                Console.Clear();
                                GreenText("Генерация кораблей стоящих в порту и их динамическое хранение \nв виде кортежей, списков и обобщенных коллекций\n\n");
                                YellowText("Вход в систему\n\n");
                                FileStream fs = new FileStream("Users.txt", FileMode.Open);
                                BinaryFormatter formatter = new BinaryFormatter();
                                Users user = (Users)formatter.Deserialize(fs);
                                fs.Close();
                                CyanText("Пользователь: ");
                                string _login = GetLogin();
                                CyanText("Пароль: ");
                                string _password = GetPassword();
                                for (int i = 0; i < user.Logins.Count; i++) // Ищем пользователя и проверяем правильность пароля.
                                {
                                    if (user.Logins[i] == _login && user.Passwords[i] == _password)
                                    {
                                        Console.WriteLine();
                                        Console.Write("Вы вошли в систему!\nДля продолжения нажмите любую клавишу...");
                                        current_login = user.Logins[i];
                                        current_password = user.Passwords[i];

                                        {// ЗАПИСЬ
                                            user_id = i;
                                            Logs.LogIn(current_login, user_id);
                                        }

                                        Console.ReadKey();
                                        Console.Clear();
                                        TheSystem();
                                        break;
                                    }
                                    else if (user.Logins[i] == _login & _password != user.Passwords[i])
                                    {
                                        RedText("\nНеверный пароль!\n");
                                        int Counter_Wrong_Pass = 0;
                                        while (_password != user.Passwords[i])
                                        {
                                            CyanText("Пароль: ");
                                            _password = GetPassword();
                                            if (_password != user.Passwords[i])
                                            {
                                                RedText("\nНеверный пароль!\n");
                                                Counter_Wrong_Pass++;
                                                if(Counter_Wrong_Pass==3)
                                                {
                                                    RedText("\nВы ввели пароль неверно более 3 раз\nДля продолдения нажмите любую клавишу...");
                                                    Logs.InvalidPass(_login);
                                                    Console.ReadKey();
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Console.Write("Вы вошли в систему!\nДля продолжения нажмите любую клавишу...");
                                                current_login = user.Logins[i];
                                                current_password = user.Passwords[i];
                                                {// ЗАПИСЬ
                                                    user_id = i;
                                                    Logs.LogIn(current_login, i);
                                                }
                                                Console.ReadKey();
                                                Console.Clear();
                                                TheSystem();
                                            }
                                        }
                                        break;
                                    }
                                    else if (i == user.Logins.Count - 1)
                                    {
                                        Console.WriteLine();
                                        RedText("Пользователь " + _login + " не найден!\nДля продолжения нажмите любую клавишу...");
                                        Console.ReadKey();
                                    }

                                }
                            }
                            catch { Console.WriteLine("Error acquired!"); return; }
                            break;
                        }
                    case 0:
                        {
                            Console.Write("\n\nДля продолжения нажмите любую клавишу...");
                            Console.ReadKey();
                            break;
                        }
                }
            }while(Choise_Login!=0);

        }
        private static void TheSystem() // СИСТЕМА ПОД ДАННОГО ПОЛЬЗОВАТЕЛЯ 
        {
            int Choise_System;
            do
            {
                Console.Clear();
                GreenText("Генерация кораблей стоящих в порту и их динамическое хранение\nв виде кортежей, списков и обобщенных коллекций\n\n");
                DarkCyanText("Системное меню:\n1.Выполнение программы\n2.Управление пользователями\n0.Выход\n\n");
                Choise_System = ReadOnlyInt(0, 2);
                switch (Choise_System)
                {
                    case 1:
                        {
                            MainProg();
                            break;
                        }
                    case 2:
                        {
                            UsersConfig();
                            //проверка
                            if (admin_changed==true)
                            {
                                Choise_System = 0;
                            }
                            break;
                        }
                    case 0:
                        {
                            Logs.LogOut(current_login, user_id);
                            break;
                        }
                        
                }
            } while (Choise_System != 0);
        }
        static void MainProg() // ОСНОВНАЯ ПРОГРАММА
        {
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                int Choise_0;
                do
                {
                    GreenText("11 Лабораторная");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Главное Меню:\n 1. Работа со Stack (Стек)\n 2. Работа с List<T> (Кортеж)\n 3. Работа с MySortedDictionary<T> (Обобщенная коллекция)\n 0. Выход\n");
                    Choise_0 = ReadOnlyInt(0, 3);
                    switch (Choise_0)
                    {
                        case 1:
                            {
                                //Настройка под 1 Задание
                                Stack<Class_Ships> Pringles_Ships = new Stack<Class_Ships>();
                                Pringles_Ships.Push(new Class_Steamboat_Ships());
                                Pringles_Ships.Push(new Class_Corvette_Ships());
                                Pringles_Ships.Push(new Class_Sailboat_Ships());
                                Pringles_Ships.Push(new Class_Steamboat_Ships("Удача", 31, 150, 275));
                                Pringles_Ships.Push(new Class_Corvette_Ships("Испепелитель", 32, 90, 10, 35));
                                Pringles_Ships.Push(new Class_Corvette_Ships("Грохот", 15, 54, 6, 15));
                                Pringles_Ships.Push(new Class_Steamboat_Ships("Геракл", 45, 140, 350));
                                Pringles_Ships.Push(new Class_Sailboat_Ships("Ленивец", 9, 70, 5));
                                Pringles_Ships.Push(new Class_Steamboat_Ships("Новая Земля", 35, 110, 310));
                                Pringles_Ships.Push(new Class_Corvette_Ships("Негатив", 3, 20, -100, 1));
                                int Counter_Stack = Pringles_Ships.Count();
                                //Конец настройки для 1 Задания
                                Console.Clear();
                                int Choise_1;
                                do
                                {
                                    GreenText("1 Задание 11 Лабораторной");
                                    Console.WriteLine();
                                    Console.WriteLine();
                                    Console.WriteLine("Второстепенное Меню:\n 1. Добавление объекта в словарь\n 2. Удаление объекта из словарь\n 3. Печать всех элементов в списке\n 4. Количество созданных объектов типа \"Корвет\"\n 5. Количество созданных объектов типа \"Пароход\"\n 6. Вывести на экран все объекты типа \"Парусник\" \n 0. Выход");
                                    Console.WriteLine();
                                    Choise_1 = ReadOnlyInt(0, 6);

                                    switch (Choise_1)
                                    {
                                        case 1:
                                            {
                                                Console.Clear();
                                                GreenText("1 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                int Choise_Add_Ships;
                                                do
                                                {
                                                    Console.WriteLine("Какой корабль хотели бы добавит?\n 1. Добавление Парохода\n 2. Добавление Парусника\n 3. Добавление Корвета\n 0. Выход");
                                                    Choise_Add_Ships = ReadOnlyInt(0, 3);
                                                    switch (Choise_Add_Ships)
                                                    {
                                                        case 1:
                                                            {
                                                                Console.Clear();
                                                                GreenText("Добавление элемента");
                                                                Console.WriteLine();
                                                                Console.WriteLine();
                                                                string a;
                                                                int b, c, d;
                                                                Console.Write("(Имя) Введите значение: ");
                                                                a = Console.ReadLine();
                                                                Console.Write("(Скорость) ");
                                                                b = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Водоизмещение) ");
                                                                c = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Мощьность) ");
                                                                d = ReadOnlyInt(0, 1000);
                                                                Pringles_Ships.Push(new Class_Steamboat_Ships(a, b, c, d));
                                                                Console.WriteLine();
                                                                GreenText("Корабль создан.\n\nДля продолжения нажмите любую кнопку...");
                                                                Console.ReadKey();
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Console.Clear();
                                                                GreenText("Добавление элемента");
                                                                Console.WriteLine();
                                                                Console.WriteLine();
                                                                string a;
                                                                int b, c, d;
                                                                Console.Write("(Имя) Введите значение: ");
                                                                a = Console.ReadLine();
                                                                Console.Write("(Скорость) ");
                                                                b = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Водоизмещение) ");
                                                                c = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Колличество парусов) ");
                                                                d = ReadOnlyInt(0, 1000);
                                                                Pringles_Ships.Push(new Class_Sailboat_Ships(a, b, c, d));
                                                                Console.WriteLine();
                                                                GreenText("Корабль создан.\n\nДля продолжения нажмите любую кнопку...");
                                                                Console.ReadKey();
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                Console.Clear();
                                                                GreenText("Добавление элемента");
                                                                Console.WriteLine();
                                                                Console.WriteLine();
                                                                string a;
                                                                int b, c, d, e;
                                                                Console.Write("(Имя) Введите значение: ");
                                                                a = Console.ReadLine();
                                                                Console.Write("(Скорость) ");
                                                                b = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Водоизмещение) ");
                                                                c = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Колличество парусов) ");
                                                                d = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Колличество огнестрельного орудия) ");
                                                                e = ReadOnlyInt(0, 1000);
                                                                Pringles_Ships.Push(new Class_Corvette_Ships(a, b, c, d, e));
                                                                Console.WriteLine();
                                                                GreenText("Корабль создан.\n\nДля продолжения нажмите любую кнопку...");
                                                                Console.ReadKey();
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        case 0: break;
                                                    }
                                                } while (Choise_Add_Ships != 0);
                                                Console.Clear();
                                                break;
                                            }
                                        case 2:
                                            {
                                                Console.Clear();
                                                GreenText("1 Задание 11 Лабораторной");
                                                Counter_Stack = Pringles_Ships.Count();
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                if (Counter_Stack > 0)
                                                {
                                                    Pringles_Ships.Pop();
                                                    GreenText("Эдемент Удален\n\nДля продолжения нажмите любую кнопку...");
                                                }
                                                else
                                                {
                                                    GreenText("В Словаре нет элементов!!!\n\nДля продолжения нажмите любую кнопку...");
                                                }
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 3:
                                            {
                                                Console.Clear();
                                                GreenText("1 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                Counter_Stack = Pringles_Ships.Count();
                                                if (Counter_Stack > 0)
                                                {
                                                    foreach (Class_Ships i in Pringles_Ships)
                                                    {
                                                        i.Show();
                                                    }
                                                    GreenText("Для продолжения нажмите любую кнопку...");
                                                }
                                                else
                                                {
                                                    GreenText("В Словаре нет элементов!!!\n\nДля продолжения нажмите любую кнопку...");
                                                }

                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 4:
                                            {
                                                Console.Clear();
                                                GreenText("1 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                Class_Corvette_Ships.Numb_Of_Corvette();
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 5:
                                            {
                                                Console.Clear();
                                                GreenText("1 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                int Counter_For_Steamboat = 0;
                                                foreach (Class_Ships i in Pringles_Ships)
                                                {
                                                    if (i is Class_Steamboat_Ships)
                                                        Counter_For_Steamboat++;
                                                }
                                                Console.WriteLine("Количество имеющихся объектов типа \"Пароход\" в списке: " + Counter_For_Steamboat);
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");

                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 6:
                                            {
                                                Console.Clear();
                                                GreenText("1 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                foreach (Class_Ships i in Pringles_Ships)
                                                {
                                                    if (i is Class_Sailboat_Ships)
                                                        i.Show();
                                                }
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");

                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 0: break;
                                    }


                                } while (Choise_1 != 0);
                                Console.Clear();
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                //Настройка под 2 Задание
                                List<Class_Ships> Convoy_Ships = new List<Class_Ships>();
                                Convoy_Ships.Add(new Class_Steamboat_Ships());
                                Convoy_Ships.Add(new Class_Corvette_Ships());
                                Convoy_Ships.Add(new Class_Sailboat_Ships());
                                Convoy_Ships.Add(new Class_Steamboat_Ships("Удача", 31, 150, 275));
                                Convoy_Ships.Add(new Class_Corvette_Ships("Испепелитель", 32, 90, 10, 35));
                                Convoy_Ships.Add(new Class_Corvette_Ships("Грохот", 15, 54, 6, 15));
                                Convoy_Ships.Add(new Class_Steamboat_Ships("Геракл", 45, 140, 350));
                                Convoy_Ships.Add(new Class_Sailboat_Ships("Ленивец", 9, 70, 5));
                                Convoy_Ships.Add(new Class_Steamboat_Ships("Новая Земля", 35, 110, 310));
                                Convoy_Ships.Add(new Class_Corvette_Ships("Негатив", 3, 20, -100, 1));
                                int Counter_Stack = Convoy_Ships.Count();
                                //Конец настройки для 2 Задания
                                int Choise_2;
                                do
                                {
                                    GreenText("2 Задание 11 Лабораторной");
                                    Console.WriteLine();
                                    Console.WriteLine();
                                    Console.WriteLine("Второстепенное Меню:\n 1. Добавление объекта в словарь\n 2. Удаление объектов с заданным именем из словаря\n 3. Вывод всех элементов словаря\n 4. Количество созданных объектов типа \"Корвет\"\n 5. Количество созданных объектов типа \"Пароход\"\n 6. Вывести на экран все объекты типа \"Парусник\" \n 0. Выход");
                                    Console.WriteLine();
                                    Choise_2 = ReadOnlyInt(0, 6);

                                    switch (Choise_2)
                                    {
                                        case 1:
                                            {
                                                Console.Clear();
                                                GreenText("2 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                int Choise_Add_Ships;
                                                do
                                                {
                                                    Console.WriteLine("Какой корабль хотели бы добавит?\n 1. Добавление Парохода\n 2. Добавление Парусника\n 3. Добавление Корвета\n 0. Выход");
                                                    Choise_Add_Ships = ReadOnlyInt(0, 3);
                                                    switch (Choise_Add_Ships)
                                                    {
                                                        case 1:
                                                            {
                                                                Console.Clear();
                                                                GreenText("Добавление элемента");
                                                                Console.WriteLine();
                                                                Console.WriteLine();
                                                                string a;
                                                                int b, c, d;
                                                                Console.Write("(Имя) Введите значение: ");
                                                                a = Console.ReadLine();
                                                                Console.Write("(Скорость) ");
                                                                b = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Водоизмещение) ");
                                                                c = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Мощьность) ");
                                                                d = ReadOnlyInt(0, 1000);
                                                                Convoy_Ships.Add(new Class_Steamboat_Ships(a, b, c, d));
                                                                Console.WriteLine();
                                                                GreenText("Корабль создан.\n\nДля продолжения нажмите любую кнопку...");
                                                                Console.ReadKey();
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Console.Clear();
                                                                GreenText("Добавление элемента");
                                                                Console.WriteLine();
                                                                Console.WriteLine();
                                                                string a;
                                                                int b, c, d;
                                                                Console.Write("(Имя) Введите значение: ");
                                                                a = Console.ReadLine();
                                                                Console.Write("(Скорость) ");
                                                                b = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Водоизмещение) ");
                                                                c = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Колличество парусов) ");
                                                                d = ReadOnlyInt(0, 1000);
                                                                Convoy_Ships.Add(new Class_Sailboat_Ships(a, b, c, d));
                                                                Console.WriteLine();
                                                                GreenText("Корабль создан.\n\nДля продолжения нажмите любую кнопку...");
                                                                Console.ReadKey();
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                Console.Clear();
                                                                GreenText("Добавление элемента");
                                                                Console.WriteLine();
                                                                Console.WriteLine();
                                                                string a;
                                                                int b, c, d, e;
                                                                Console.Write("(Имя) Введите значение: ");
                                                                a = Console.ReadLine();
                                                                Console.Write("(Скорость) ");
                                                                b = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Водоизмещение) ");
                                                                c = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Колличество парусов) ");
                                                                d = ReadOnlyInt(0, 1000);
                                                                Console.Write("(Колличество огнестрельного орудия) ");
                                                                e = ReadOnlyInt(0, 1000);
                                                                Convoy_Ships.Add(new Class_Corvette_Ships(a, b, c, d, e));
                                                                Console.WriteLine();
                                                                GreenText("Корабль создан.\n\nДля продолжения нажмите любую кнопку...");
                                                                Console.ReadKey();
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        case 0: break;
                                                    }
                                                } while (Choise_Add_Ships != 0);
                                                Console.Clear();
                                                break;
                                            }
                                        case 2:
                                            {
                                                Console.Clear();
                                                GreenText("2 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                int Counter_Of_Deletes = 0;
                                                string a;
                                                Console.Write("Введите имя коробля(ей) который(ые) надо удалить: ");
                                                a = Console.ReadLine();
                                                foreach (Class_Ships i in Convoy_Ships)
                                                {
                                                    if (a == i.Name)
                                                    {
                                                        Console.Write("Удаляется элемент: ");
                                                        i.Show();
                                                        Convoy_Ships.Remove(i);
                                                        Counter_Of_Deletes++;
                                                        break;
                                                    }

                                                }
                                                if (Counter_Of_Deletes > 0)
                                                {
                                                    Console.WriteLine("Элемент(ы) удален(ы)!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Ошибка!!! Элемент не найден!!!");
                                                }
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");

                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 3:
                                            {
                                                Console.Clear();
                                                GreenText("2 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                Counter_Stack = Convoy_Ships.Count();
                                                if (Counter_Stack > 0)
                                                {
                                                    foreach (Class_Ships i in Convoy_Ships)
                                                    {
                                                        i.Show();
                                                    }
                                                    GreenText("Для продолжения нажмите любую кнопку...");
                                                }
                                                else
                                                {
                                                    GreenText("В Словаре нет элементов!!!\n\nДля продолжения нажмите любую кнопку...");
                                                }
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 4:
                                            {
                                                Console.Clear();
                                                GreenText("2 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                Class_Corvette_Ships.Numb_Of_Corvette();
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");

                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 5:
                                            {
                                                Console.Clear();
                                                GreenText("2 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                int Counter_For_Steamboat = 0;
                                                foreach (Class_Ships i in Convoy_Ships)
                                                {
                                                    if (i is Class_Steamboat_Ships)
                                                    {
                                                        Counter_For_Steamboat++;
                                                    }
                                                }
                                                Console.WriteLine("Количество имеющихся объектов типа \"Пароход\" в списке: " + Counter_For_Steamboat);
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");

                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 6:
                                            {
                                                Console.Clear();
                                                GreenText("2 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                foreach (Class_Ships i in Convoy_Ships)
                                                {
                                                    if (i is Class_Sailboat_Ships)
                                                        i.Show();
                                                }
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");

                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 0: break;
                                    }
                                    Console.Clear();

                                } while (Choise_2 != 0);
                                break;
                            }
                        case 3:
                            {
                                Console.Clear();
                                ///
                                MySortedDictionary<int, Class_Ships> Key_Value_Ships = new MySortedDictionary<int, Class_Ships>();
                                Key_Value_Ships.Add(1, new Class_Steamboat_Ships());
                                Key_Value_Ships.Add(2, new Class_Corvette_Ships());
                                Key_Value_Ships.Add(3, new Class_Sailboat_Ships());
                                Key_Value_Ships.Add(4, new Class_Steamboat_Ships("Удача", 31, 150, 275));
                                Key_Value_Ships.Add(5, new Class_Corvette_Ships("Испепелитель", 32, 90, 10, 35));
                                Key_Value_Ships.Add(6, new Class_Corvette_Ships("Грохот", 15, 54, 6, 15));
                                Key_Value_Ships.Add(7, new Class_Steamboat_Ships("Геракл", 45, 140, 350));
                                Key_Value_Ships.Add(8, new Class_Sailboat_Ships("Ленивец", 9, 70, 5));
                                Key_Value_Ships.Add(9, new Class_Steamboat_Ships("Новая Земля", 35, 110, 310));
                                Key_Value_Ships.Add(10, new Class_Corvette_Ships("Негатив", 3, 20, -100, 1));
                                int[] arr = new int[10];
                                for (int j = 0; j < arr.Length; j++)
                                {
                                    arr[j] = j + 1;
                                }


                                ///

                                GreenText("3 Задание  11 Лабораторной");
                                Console.WriteLine();
                                Console.WriteLine();
                                int Choise_3;

                                do
                                {
                                    Console.WriteLine("Второстепенное Меню:\n 1. Добавление объекта в словарь\n 2. Удаление объектов с ключем\n 3. Вывод всех элементов словаря\n 4. Создание клона\n 0. Выход");
                                    Console.WriteLine();
                                    Choise_3 = ReadOnlyInt(0, 4);
                                    switch (Choise_3)
                                    {
                                        case 1:
                                            {
                                                Console.Clear();
                                                GreenText("3 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                int Choise_Add_Ships;
                                                do
                                                {
                                                    Console.WriteLine("Какой корабль хотели бы добавит?\n 1. Добавление Парохода\n 2. Добавление Парусника\n 3. Добавление Корвета\n 0. Выход");
                                                    Choise_Add_Ships = ReadOnlyInt(0, 3);
                                                    switch (Choise_Add_Ships)
                                                    {
                                                        case 1:
                                                            {
                                                                Console.Clear();
                                                                GreenText("Добавление элемента");
                                                                Console.WriteLine();
                                                                Console.WriteLine();
                                                                string a;
                                                                int b, c, d, Booler;
                                                                Console.Write("(Ключ) ");
                                                                Booler = ReadOnlyInt(0, 1000000);
                                                                if (Key_Value_Ships.GetByIndex(Booler) == null)
                                                                {
                                                                    Console.Write("(Имя) Введите значение: ");
                                                                    a = Console.ReadLine();
                                                                    Console.Write("(Скорость) ");
                                                                    b = ReadOnlyInt(0, 1000);
                                                                    Console.Write("(Водоизмещение) ");
                                                                    c = ReadOnlyInt(0, 1000);
                                                                    Console.Write("(Мощьность) ");
                                                                    d = ReadOnlyInt(0, 1000);
                                                                    Key_Value_Ships.Add(Booler, new Class_Sailboat_Ships(a, b, c, d));
                                                                    Console.WriteLine();
                                                                    GreenText("Корабль создан.\n\nДля продолжения нажмите любую кнопку...");
                                                                }
                                                                else
                                                                {
                                                                    GreenText("Корабль не создан - такой ключ уже существует.\n\nДля продолжения нажмите любую кнопку...");
                                                                }
                                                                Console.ReadKey();
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Console.Clear();
                                                                GreenText("Добавление элемента");
                                                                Console.WriteLine();
                                                                Console.WriteLine();
                                                                string a;
                                                                int b, c, d, Booler;
                                                                Console.Write("(Ключ) ");
                                                                Booler = ReadOnlyInt(0, 1000000);
                                                                if (Key_Value_Ships.GetByIndex(Booler) == null)
                                                                {
                                                                    Console.Write("(Имя) Введите значение: ");
                                                                    a = Console.ReadLine();
                                                                    Console.Write("(Скорость) ");
                                                                    b = ReadOnlyInt(0, 1000);
                                                                    Console.Write("(Водоизмещение) ");
                                                                    c = ReadOnlyInt(0, 1000);
                                                                    Console.Write("(Количество парусов) ");
                                                                    d = ReadOnlyInt(0, 1000);
                                                                    Key_Value_Ships.Add(Booler, new Class_Steamboat_Ships(a, b, c, d));
                                                                    Console.WriteLine();
                                                                    GreenText("Корабль создан.\n\nДля продолжения нажмите любую кнопку...");
                                                                }
                                                                else
                                                                {
                                                                    GreenText("Корабль не создан - такой ключ уже существует.\n\nДля продолжения нажмите любую кнопку...");
                                                                }
                                                                Console.ReadKey();
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                Console.Clear();
                                                                GreenText("Добавление элемента");
                                                                Console.WriteLine();
                                                                Console.WriteLine();
                                                                string a;
                                                                int b, c, d, e, Buf;
                                                                Console.Write("(Ключ) ");
                                                                Buf = ReadOnlyInt(0, 100000);
                                                                if (Key_Value_Ships.GetByIndex(Buf) == null)
                                                                {
                                                                    Console.Write("(Имя) Введите значение: ");
                                                                    a = Console.ReadLine();
                                                                    Console.Write("(Скорость) ");
                                                                    b = ReadOnlyInt(0, 1000);
                                                                    Console.Write("(Водоизмещение) ");
                                                                    c = ReadOnlyInt(0, 1000);
                                                                    Console.Write("(Количество парусов) ");
                                                                    d = ReadOnlyInt(0, 1000);
                                                                    Console.Write("(Количество огнестрельных орудий) ");
                                                                    e = ReadOnlyInt(0, 1000);
                                                                    Key_Value_Ships.Add(Buf, new Class_Corvette_Ships(a, b, c, d, e));
                                                                    Console.WriteLine();
                                                                    GreenText("Корабль создан.\n\nДля продолжения нажмите любую кнопку...");
                                                                }
                                                                else
                                                                {
                                                                    GreenText("Корабль не создан - такой ключ уже существует.\n\nДля продолжения нажмите любую кнопку...");
                                                                }
                                                                Console.ReadKey();
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        case 0: break;
                                                    }
                                                } while (Choise_Add_Ships != 0);
                                                Console.Clear();
                                                break;
                                            }
                                        case 2:
                                            {
                                                Console.Clear();
                                                GreenText("3 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                int Deleter = ReadOnlyInt(0, 1000);
                                                if (Key_Value_Ships.ContainsKey(Deleter) == false)////////////////////////////
                                                {
                                                    Console.WriteLine("Элемет не найден");
                                                    Console.WriteLine();
                                                }
                                                else
                                                {
                                                    Key_Value_Ships.RemoveAtKey(Deleter);
                                                    Console.WriteLine("Элемет удалён");
                                                    Console.WriteLine();
                                                }
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");

                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 3:
                                            {
                                                Console.Clear();
                                                GreenText("3 Задание 11 Лабораторной");
                                                var enumerator = Key_Value_Ships.GetEnumerator();
                                                while (enumerator.MoveNext())
                                                {
                                                    if (enumerator.Current is Class_Sailboat_Ships)
                                                    {
                                                        Class_Sailboat_Ships SailBoat = new Class_Sailboat_Ships();
                                                        SailBoat = (Class_Sailboat_Ships)enumerator.Current;
                                                        SailBoat.Show();
                                                    }
                                                    if (enumerator.Current is Class_Corvette_Ships)
                                                    {
                                                        Class_Corvette_Ships Corvette = new Class_Corvette_Ships();
                                                        Corvette = (Class_Corvette_Ships)enumerator.Current;
                                                        Corvette.Show();
                                                    }
                                                    if (enumerator.Current is Class_Steamboat_Ships)
                                                    {
                                                        Class_Steamboat_Ships SteamBoat = new Class_Steamboat_Ships();
                                                        SteamBoat = (Class_Steamboat_Ships)enumerator.Current;
                                                        SteamBoat.Show();
                                                    }
                                                }
                                                //
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 4:
                                            {
                                                Console.Clear();
                                                GreenText("3 Задание 11 Лабораторной");
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                MySortedDictionary<int, Class_Ships> Clone = Key_Value_Ships.Clone();
                                                Console.WriteLine("Клон создан, удаляем в основном массиве 1 элемент, если он есть, и выводим оба массива");
                                                Console.WriteLine();
                                                if (Key_Value_Ships.GetByIndex(0) != null)
                                                {
                                                    Key_Value_Ships.RemoveAt(0);
                                                    Console.WriteLine("Элемет удалён");
                                                    Console.WriteLine();
                                                    Console.Write("Основной ");
                                                    Key_Value_Ships.Show();
                                                    Console.WriteLine();
                                                    Console.WriteLine();
                                                    Console.Write("Клон ");
                                                    Clone.Show();
                                                }
                                                else
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("Элемет не найден");
                                                    Console.WriteLine();
                                                }
                                                Console.WriteLine();
                                                Console.WriteLine();
                                                GreenText("Для продолжения нажмите любую кнопку...");

                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                        case 0: break;
                                    }
                                } while (Choise_3 != 0);

                                Console.Clear();
                                break;
                            }
                        case 0: break;

                    }
                } while (Choise_0 != 0);
            }
        }
        private static void UsersConfig() // УПРАВЛЕНИЕ ПОЛЬЗОВАТЕЛЯМИ
        {
            int Choise_System_Config;
            do
            {
                Console.Clear();
                GreenText("Выбор действий над пользователем\n\n");
                DarkCyanText("Системное меню:\n1.Показать пользователей\n2.Добавление пользователя\n3.Удаление пользователя\n4.Просмотр логов пользователей\n5.Передача прав администрара\n0.Выход\n\n");
                Choise_System_Config = ReadOnlyInt(0, 5);
                switch (Choise_System_Config)
                {
                    case 1:
                        {
                            Console.Clear();
                            ShowUsers();
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            AddUser();
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            DeleteUser();
                            Console.ReadKey();

                            //проверка
                            if (admin_changed == true)
                            {
                                Choise_System_Config = 0;
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            ShowLogsUsers();
                            Console.ReadKey();
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            AdminRightsTransfer();
                            Console.ReadKey();
                            break;
                        }  
                    case 0: break;
                }
            } while (Choise_System_Config != 0);
        }

        static void Main(string[] args)
        {
            {
                SystemLogin();
            }
        }

        ///ФУНКЦИИ АДМИНИСТРИРОВАНИЯ
        
        private static void ShowUsers() // ПОКАЗ / ПОДСЧЁТ ПОЛЬЗОВАТЕЛЕЙ
        {
            GreenText("Показ пльзователей\n\n");
            try
            {

                FileStream fs = new FileStream("Users.txt", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                Users user = (Users)formatter.Deserialize(fs);

                DarkCyanText("Количество пользователей: ");
                RedText(user.Logins.Count + "\n\n");

                fs.Close();
                for (int i = 0; i < user.Logins.Count; i++) // Ищем пользователя и проверяем правильность пароля.
                {
                    Console.Write("Пользователь [" + i + "]: ");
                    CyanText(user.Logins[i]);
                    if (i == 0)
                    {
                        RedText("\t{Admin} ");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }
            catch { Console.WriteLine("Данных нет"); }
            DarkCyanText("Для подолжения нажмите любую клавишу...");
        }
        private static void AddUser() // ДОБАВЛЕНИЕ ПОЛЬЗОВАТЕЛЯ
        {


            if (Admin_Check())
            {
                FileStream fs = new FileStream("Users.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryFormatter formatter = new BinaryFormatter();
                Users user;
                try
                {
                    user = (Users)formatter.Deserialize(fs);
                }
                catch
                {
                    user = new Users();
                }
                bool login_absent = true;
                GreenText("Добавление нового пользователя\n\n");
                CyanText("Пользователь: ");
                string _login = GetLogin();
                for (int i = 0; i < user.Logins.Count; i++)
                {
                    if (_login == user.Logins[i])
                    {
                        login_absent = false;
                        break;
                    }
                }
                if (login_absent)
                {
                    CyanText("Пароль Пользователя: ");
                    string _password = GetPassword();
                    if (_login == "" || _password == "") { Console.WriteLine("Не введен логин или пароль!"); }
                    else
                    {
                        fs.SetLength(0);
                        user.Logins.Add(_login);
                        user.Passwords.Add(_password);
                        formatter = new BinaryFormatter();
                        formatter.Serialize(fs, user); // Сериализуем класс.
                        Logs.AddUser(_login);
                        DarkCyanText("\nПользователь Добавлен! Для продолжения нажмите любую клавишу...");
                    }
                }
                else
                {
                    DarkCyanText("\nТакой пользователь уже существует!\nДля продолжения нажмите любую клавишу...");
                }
                fs.Close();
            }

        }
        private static void DeleteUser() // УДАЛЕНИЕ ПОЛЬЗОВАТЕЛЯ
        {
            if (Admin_Check())
            {
                GreenText("Удаление пользователя\n\n");
                FileStream fs = new FileStream("Users.txt", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                Users user = (Users)formatter.Deserialize(fs);
                if (user.Logins.Count == 1)
                {
                    DarkRedText("Единственный пользователь не может быть удалён!\nДля прододжения нажмите любую клавишу...");
                }
                else
                {
                    CyanText("Пользователь: ");
                    string _login = GetLogin();
                    CyanText("Пароль Пользователя: ");
                    string _password = GetPassword();
                    if (current_login == _login && current_password == _password)
                    {
                        int Choise_Delete;
                        RedText("\nВы уверены что хотите удалить Администатора?\nВ данном случае система перезапустится и роль администатора перейдёт следующему по спику пользователю\n\n");
                        CyanText("1.Да\n2.Нет\n\n");
                        Choise_Delete = ReadOnlyInt(1, 2);
                        switch (Choise_Delete)
                        {
                            case 1:
                                {
                                    fs.SetLength(0);
                                    admin_changed = true;
                                    user.Logins.Remove(_login);
                                    user.Passwords.Remove(_password);
                                    formatter.Serialize(fs, user); // Сериализуем класс.
                                    DarkCyanText("\nУчётная запись Администратора удалёна! Данная роль перешла следующему по списку пользователю!\nДля продолжения нажмите любую клавишу...");
                                    break;
                                }
                            case 2:
                                {
                                    DarkCyanText("\nУчётная запись администратора не удалена!\n Для продолжения нажмите любую клавишу...");
                                    break;
                                }
                        }

                    }
                    else
                    {
                        {
                            try
                            {
                                bool user_absent = true;
                                for (int i = 0; i < user.Logins.Count; i++)
                                {
                                    if (_login == user.Logins[i] & _password == user.Passwords[i])
                                    {
                                        user_absent = false;
                                        break;
                                    }
                                }
                                if (user_absent == true)
                                {
                                    DarkRedText("\nПользователь или пароль не верны! Для продолжения нажмите любую клавишу...");
                                }
                                else
                                {
                                    fs.SetLength(0);
                                    user.Logins.Remove(_login);
                                    user.Passwords.Remove(_password);
                                    formatter.Serialize(fs, user); // Сериализуем класс.
                                    Logs.DeleteUser(_login);
                                    DarkCyanText("\nПользователь удалён! Для продолжения нажмите любую клавишу...");
                                }
                            }
                            catch
                            {
                                DarkRedText("\nПользователь не навйлен! Для продолжения нажмите любую клавишу...");
                            }
                        }
                    }
                }
                fs.Close();

            }
        }
        private static void ShowLogsUsers() // ЛОГИ ПОЛЬЗОВАТЕЛЕЙ
        {
            if (Admin_Check())
            {
                GreenText("Показ логов пользователей\n\n");
                FileStream stream = new FileStream("LOGS.txt", FileMode.OpenOrCreate);
                StreamReader reader = new StreamReader(stream);
                string str = reader.ReadToEnd();
                stream.Close();
                CyanText(str + "\n\n");
                DarkCyanText("Для продолжения нажмите любую клавишу...");
            }
        }
        private static void AdminRightsTransfer() // ПЕРЕДАЧА ПРАВ
        {
            if (Admin_Check())
            {
                GreenText("Передача прав Администрора\n\n");
                FileStream fs = new FileStream("Users.txt", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                Users user = (Users)formatter.Deserialize(fs);
                if (user.Logins.Count == 1)
                {
                    DarkRedText("Единственный пользователь не может передать права!\nДля прододжения нажмите любую клавишу...");
                }
                else
                {
                    CyanText("Пользователь: ");
                    string _login = GetLogin();
                    CyanText("Пароль Пользователя: ");
                    string _password = GetPassword();
                    if (current_login == _login && current_password == _password)
                    {
                        RedText("\nВы уже являетесь администратором\nДля прододжения нажмите любую клавишу...");
                    }
                    else
                    {
                        {
                            try
                            {
                                bool user_absent = true;
                                int NewAdmin=0;
                                for (int i = 0; i < user.Logins.Count; i++)
                                {
                                    if (_login == user.Logins[i] & _password == user.Passwords[i])
                                    {
                                        user_absent = false;
                                        NewAdmin = i;
                                        break;
                                    }
                                }
                                if (user_absent == true)
                                {
                                    DarkRedText("\nЛогин пользователя или пароль не верны! Для продолжения нажмите любую клавишу...");
                                }
                                else
                                {
                                    fs.SetLength(0);
                                    user.Logins[0] = user.Logins[NewAdmin];
                                    user.Passwords[0] = user.Passwords[NewAdmin];
                                    user.Logins[NewAdmin] = current_login;
                                    user.Passwords[NewAdmin] = current_password;
                                    formatter.Serialize(fs, user); // Сериализуем класс.
                                    Logs.TransferRights(_login, current_login);
                                    DarkCyanText("\nПрава Переданы! Для продолжения нажмите любую клавишу...");
                                }
                            }
                            catch
                            {
                                DarkRedText("\nПользователь не навйлен! Для продолжения нажмите любую клавишу...");
                            }
                        }
                    }
                }
                fs.Close();
            }
        }

        /// ФУНКЦИИ ВСПОМОГАТЕЛЬНЫЕ (ВВОД)
        
        static int ReadOnlyInt(int a, int b) // ЦИФР-ВВОД
        {
            int value;
            bool ok = false;
            ConsoleKeyInfo key;
            do
            {
                CyanText("Введите значение: ");
                string buf="";
                
                do
                {
                    key = Console.ReadKey(true);

                    // Ignore any key out of range.
                    if (((int)key.Key) >= 48 && ((int)key.Key <= 57))
                    {
                        // Append the character to the password.
                        buf += key.KeyChar;
                        DarkCyanText(key.KeyChar.ToString());
                    }
                    if (buf.Length > 5)
                    {
                        RedText("\t\t(допустимый разер не больше 5 символов)");
                        break;
                    }
                    // Exit if Enter key is pressed.
                } while (key.Key != ConsoleKey.Enter);
                
                ok = Int32.TryParse(buf, out value);
                if (value >= a && value <= b)
                {

                }
                else
                {
                    ok = false;
                }
                if (ok == false)
                {
                    DarkRedText("\nОшибка: ");
                    RedText("Значение не правильно!!!");
                    Console.WriteLine();
                    Console.WriteLine();
                }
            } while (ok == false);
            return value;
        }
        public static String GetLogin() // ЛОГИН-ВВОД
        {
            String Loggin = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Ignore any key out of range.
                if (((int)key.Key) >= 65 && ((int)key.Key <= 90))
                {
                    // Append the character to the password.
                    Loggin += key.KeyChar;
                    DarkCyanText(key.KeyChar.ToString());
                }
                if(Loggin.Length>15)
                {
                    RedText("\t\t(допустимый разер логина не больше 15 символов)");
                    break;
                }
                // Exit if Enter key is pressed.
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return Loggin;
        }
        public static String GetPassword() // ПАРОЛЬ-ВВОД
        {
            String Password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Ignore any key out of range.
                if (((int)key.Key) >= 65 && ((int)key.Key <= 90) || ((int)key.Key) >= 48 && ((int)key.Key <= 57))
                {
                    // Append the character to the password.
                    Password += key.KeyChar;
                    DarkCyanText("*");
                }
                if (Password.Length > 15)
                {
                    RedText("\t\t(допустимый разер пароля не больше 15 символов)");
                    break;
                }
                // Exit if Enter key is pressed.
            } while (key.Key != ConsoleKey.Enter);

            System.Threading.Thread.Sleep(500);
            Console.WriteLine();
            return Password;
        }

        /// ФУНКЦИИ ВСПОМОГАТЕЛЬНЫЕ (ВЫВОД)
        
        static void DarkRedText(string str) // ТЁМНО-КРАСНЫЙ ВЫВОД
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        static void RedText(string str) // КРАСНВЫЙ ВЫВОД
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        static void DarkCyanText(string str) // ТЁМНО-ЦИАНОВЫЙ ВЫВОД
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        static void CyanText(string str) // ЦИАНОВЫЙ ВЫВОД
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        static void YellowText(string str) // ЖЁЛТЫЙ ВЫВОД
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        static void GreenText(string str) // ЗЕЛЁНЫЙ ВЫВОД
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }

        /// ПРОЧИЕ ФУНКЦИИ
        
        private static bool Admin_Check() // АДМИН-ПРИВЕЛЕГИИ
        {
            try
            {
                FileStream fs = new FileStream("Users.txt", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                Users user = (Users)formatter.Deserialize(fs);
                fs.Close();
                if (user.Logins[0] == current_login && user.Passwords[0] == current_password)
                {
                    return true;
                }
                else
                {
                    RedText("Пользователь не является Администаратором и не может выполнить данное действие!\nДля продолжения нажмите любую клавишу...");
                    return false;
                }
            }
            catch
            {
                RedText("Неизвестная ошибка, возможно нет пользователей!\nДля продолжения нажмите любую клавишу...");
                return false;
            }
        }
    }
}
    
