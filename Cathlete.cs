using System;
using System.Collections.Generic;

namespace CatsArena
{
    internal class Cathlete
    {
        //Списък на всички катлети от файла. * Обърнете внимание на static - това е поле на класа
        public static List<Cathlete> Cathletes { get; set; } = new List<Cathlete>();

        private static Random randomBox = new Random(); 
        //така обекта е единствен, а не се създава за всеки катлети
        //като по този начин се предпазваме от генериране на еднакви числа заради таймера
       
        //==== Индивидуални Характеристики = Индивидуални частни полета и свойства = STATE
        //* Всички са публични, понеже ще трябва да ги показваме и променяме от класа Tournament
        public string Name { get; private set; }
        public int Experience { get; set; } = 50;
        public int[] Medals { get; set; } = new int[4]; 
                                            //ще пазим класиранията му, а в индекс 0 - участията
        public double BestPerformance { get; set; }
        public double CurrentResult { get; set; } //това няма да го записваме във файла,
        //но ще ни трябва когато правим класирането за конкретен турнир
       
        //===== Конструктор - понеже ще ги четем от файл, ще ни трябва пълната информация
        public Cathlete(string name, int experience, int[] medals, double bestPerformance) 
        {
            Name = name;
            Medals = medals;  
            Experience = experience;  
            BestPerformance = bestPerformance;
        }

        public static void AddNewCathlete()
        {
            Console.Write("Въведете име на катлета: ");
            string name = Console.ReadLine();

            Cathlete newCat = new Cathlete(name, 50, new int[4], 0.00);
            Cathletes.Add(newCat);

            ReadWrite.SaveAthletesToCSV(); //Записваме промените в текстовия файл
        }

        public void Train(string type)
        {
            if (type == "фитнес") Experience += 3;
            if (type == "бягане") Experience += 5;
        }

        public double DiskThrow() //Нека връща резултат, може да е по-удобно за печатане
        {
            double part1 =  Experience / 10.0; // 1/10 = 0.10 = 10% от опита и ще бъде в метри
            double part2 = randomBox.Next(1, 101) / 100.0; //случайно число между 1 и 100 см

            CurrentResult = part1 + part2; //но запомняме резултата и в стейта на обекта
            return part1 + part2; //Дробно число в метри
        }

        public void ShowInfo() //Ако искаме визитка на конкретен катлет 
        {
            Console.WriteLine($"Визитка на Катлета {Name}");
            Console.WriteLine(String.Join("=",35));
            Console.WriteLine("Злато: " + Medals[1]); 
            Console.WriteLine("Сребро: " + Medals[2]); 
            Console.WriteLine("Бронз: " + Medals[3]);
            Console.WriteLine(String.Join("=", 35));
            Console.WriteLine("Общо състезания: " + Medals[0]); 
            Console.WriteLine($"Личен рекорд: {BestPerformance:F2} метра"); 
        }

        public string ToCSVString() //За да записваме промените във файла
        {
            string csvInfo = $"{Name},{Experience},{Medals[0]},{Medals[1]},{Medals[2]},";
                   csvInfo += $"{Medals[3]},{BestPerformance}";
            return csvInfo;
        }

        public string ShortInfo()
        {
            string csvInfo = $"{Name.PadRight(11)}, exp:{Experience}, wins:{Medals[1]}, second:{Medals[2]}, third:{Medals[3]}, ";
            csvInfo += $" best:{BestPerformance}";
            return csvInfo;
        }

        public static void ShowAllCathletes()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n======== Постижения на всички катлети =======================");
            Console.ResetColor();

            int br = 1;
            foreach(Cathlete c in Cathletes)
            {
                Console.WriteLine($"{br}) {c.ShortInfo()}");
                br++;
            }
        }
    }
}
