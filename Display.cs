using System;
using System.Collections.Generic;

namespace CatsArena
{
    internal class Display
    {
        //katev01a 7Jul
        
        public static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n=== Моля изберете ===");
            Console.ResetColor();

            Console.WriteLine("1) Създай нов турнир");
            Console.WriteLine("2) Добави нов катлет");
            Console.WriteLine("3) Изтрий катлет от общия списък");
            Console.WriteLine("4) Покажи списъка с постижения за всеки катлет");

            Console.WriteLine("5) Изход");

            Console.ForegroundColor= ConsoleColor.DarkBlue;
            Console.Write("Вашият избор:");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            int choose = int.Parse(Console.ReadLine());
            Console.ResetColor();

            switch (choose)
            {
                case 1: TournamentMenu(); break;
                case 2: Cathlete.AddNewCathlete(); break;
                case 3: RemoveCathlete(); break;
                case 4: Cathlete.ShowAllCathletes() ; break;

                default: Environment.Exit(0); break;
            }

            MainMenu();
        }

        private static void RemoveCathlete()
        {
            Cathlete.ShowAllCathletes(); //Показва списъка за да си изберем

            int chosen = 0; //задаваме грешна стойност за да се покаже надписа
            while( chosen < 1 || chosen > Cathlete.Cathletes.Count) //докато не е коректна въвеждай
            {
                Console.Write("Изберете катлета, който трябва да се премахне чрез номерът му: ");
                chosen = int.Parse(Console.ReadLine());
            }

            Cathlete.Cathletes.RemoveAt(chosen - 1); //-1 защото са с 1 по-големи визуално
            ReadWrite.SaveAthletesToCSV(); //запиши промените във файла
        }

        public static void TournamentMenu()
        {
            Console.Write("Въведете име на турнира: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string name = Console.ReadLine();
            Console.ResetColor();

            Cathlete.ShowAllCathletes();
            Console.WriteLine("\nИзберете кои ще са участниците в турнира");
            Console.Write("като ги изброите със запетайки:");

            Console.ForegroundColor = ConsoleColor.Magenta;
            string playerNums = Console.ReadLine();
            Console.ResetColor();

            string[] playerStartNumbers = playerNums.Split(',');

            List<Cathlete> playersForThisTournament = new List<Cathlete>();   

            foreach (string s in playerStartNumbers)
            {
                int num = int.Parse(s); //конвертираме текста до число
                playersForThisTournament.Add(Cathlete.Cathletes[num-1]); 
                //и добавяме катлета от пълния списък с катлети
            }

            Tournament myTournament = new Tournament(name, playersForThisTournament);
        } 
    }
}
