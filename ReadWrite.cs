using System;
using System.IO;
using System.Collections.Generic;

namespace CatsArena
{
    internal class ReadWrite //Класът ще е за входно-изходни операции с файловете
    {
        //Много често ще срещате съкращението I/O  = Input/Output
        static string _filePath = "../../Data/players.txt";
        //static string _filePath = @"..\..\Data\players.txt";

        public static List<Cathlete> LoadAthletesFromCsv()
        {
            //Този метод ще чете от файла и ще ги прехвърля към Cathlete.Cathletes
            //Като за начинаещи, ще го правим без try-catch,
            //в 11 клас ще учите обработка на грешки

            List<Cathlete> cathletes = new List<Cathlete>();
            string[] lines = File.ReadAllLines(_filePath); //ИЗИСКВА USING SYSTEM.IO;

            for (int i = 1; i < lines.Length; i++) //Прескачаме антетката която е i=0
            {
                string[] parts = lines[i].Split(',');

                string name = parts[0];
                int experience = int.Parse(parts[1]);
                int[] medals = new int[]
                {
                    int.Parse(parts[2]), //medals[0]
                    int.Parse(parts[3]), //medals[1]
                    int.Parse(parts[4]), //medals[2]
                    int.Parse(parts[5]) //medals[3]
                };
                double bestPerformance = double.Parse(parts[6]);

                //създаваме такъв катлет и директно го добавяме в този списък
                cathletes.Add(new Cathlete(name, experience, medals, bestPerformance));
            }

            return cathletes; //Ще подадем този резултат на статичния списък
            //и от тук нататък в програмата ползваме данните от статичния списък
            //ще записваме в CSV файла при промени - нов катлет, премахване на катлет и след състезание
        }

        public static void SaveAthletesToCSV() 
        {
            //Понеже пишем в един и същи файл, а извикваме метода от други класове
            //тук няма да подаваме _filePath като параметър, а го ползваме вграден в кода
            //* Не е препоръчително при по-сложни проекти

            List<string> lines = new List<string>();

            // Първи ред – заглавията на колоните
            lines.Add("Name,Experience,Participations,Gold,Silver,Bronze,BestPerformance");
            //Име,опит,участия,злато,сребро,бронз,най-добро хвърляне
            
            foreach (Cathlete player in Cathlete.Cathletes)//и си вграждам директно от къде 
            {
                string line = player.ToCSVString();
                lines.Add(line);
            }

            File.WriteAllLines(_filePath, lines);
        }

        public static void SaveTournamentToTXT(string turnamentName, List<string> results)
        {
            string filePath = $"../../Data/{turnamentName}.txt"; 
            File.WriteAllLines(filePath, results);
        }
    }
}
