using System;

namespace FinalProject_5._6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintUser(EnterUser());
            Console.WriteLine("Работа программы окончена, нажмите Enter для выхода...");
            Console.Read();
        }

        static void PrintUser((string firstName, string lastName, byte age, string[] petNames, string[] favColors) user)
        {
            Console.WriteLine("--------");
            Console.WriteLine("Имя: {0}", user.firstName);
            Console.WriteLine("Фамилия: {0}", user.lastName);
            Console.WriteLine("Возраст: {0}", user.age);
            int i = 1;
            if (user.petNames != null)
            {
                Console.WriteLine("Питомцев: {0}", user.petNames.Length);
                foreach (var pet in user.petNames)
                {
                    Console.WriteLine(" - Имя питомца {0}: {1}", i, pet);
                    i++;
                }
            }

            Console.WriteLine("Любимых цветов: {0}", user.favColors.Length);
            i = 1;
            foreach (var color in user.favColors)
            {
                Console.WriteLine(" - Любимый цвет {0}: {1}", i, color);
                i++;
            }
            Console.WriteLine("--------");
        }

        static (string firstName, string lastName, byte age, string[] petNames, string[] favColors) EnterUser()
        {

            (string firstName, string lastName, byte age, string[] petNames, string[] favColors) user;

            Console.Write("Введите имя: ");
            user.firstName = Console.ReadLine();
            Console.Write("Введите фамилию: ");
            user.lastName = Console.ReadLine();
            
            string ageStr;
            int ageInt;
            do
            {
                Console.Write("Введите возраст: ");
                ageStr = Console.ReadLine();
            }
            while (CheckNum(true, ageStr, out ageInt) == false);

            user.age = Convert.ToByte(ageInt);

            while (true)
            {
                Console.Write("Наличие питомцев (Y/N): ");
                string havePetStr = Console.ReadLine();
                string petsCountStr;
                int petsCountInt;
                if (havePetStr == "Y" || havePetStr == "y")
                {
                    do
                    {
                        Console.Write("Количество питомцев: ");
                        petsCountStr = Console.ReadLine();
                    }
                    while (CheckNum(false, petsCountStr, out petsCountInt) == false);

                    user.petNames = CreateArray(petsCountInt, "Введите имя питомца");

                    break;
                }
                else if (havePetStr == "N" || havePetStr == "n")
                {
                    user.petNames = null;
                    break;
                }
                else
                    Console.WriteLine("Неверный ввод!");
            }

            string favColorsCountStr;
            int favColorsCountInt;
            do 
            {
                Console.Write("Введите количество любимых цветов: ");
                favColorsCountStr = Console.ReadLine();
            }
            while (CheckNum(false, favColorsCountStr, out favColorsCountInt) == false);

            user.favColors = CreateArray(favColorsCountInt, "Введите любимый цвет");

            //Console.ReadKey();
            return user;
        }

        static string[] CreateArray(int num, string message)
        {
            string[] array = new string[num];
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("{0} {1}(из {2}): ", message, i+1, array.Length);
                array[i] = Console.ReadLine();
            }
            return array;
        }

        static bool CheckNum(bool ageCheck, string num, out int ageInt)
        {
            if (int.TryParse(num, out ageInt))
            {
                if (ageCheck)  // Если это проверка на возраст, дополнительно проверим на возможность конвертации в byte
                {
                    if (ageInt > 255)
                    {
                        Console.WriteLine("Вам не может быть {0} лет!", ageInt);
                        return false;
                    }
                }
                if (ageInt <= 0)
                {
                    Console.WriteLine("Некорреетное число, введите целое число от 1");
                    return false;
                }
                else
                    return true;
            }
            Console.WriteLine("Некорреетные данные, введите целое число от 1");
            return false;
        }
    }
}
