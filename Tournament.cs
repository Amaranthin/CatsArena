using System;
using System.Collections.Generic;
using System.Linq;

namespace CatsArena
{
    internal class Tournament
    {
        public string TournamentName { get; set; } 

        //Всеки турнир ще има СОБСТВЕН списък с участници
        //следователно полето не трябва да е статично, а на инстанциите от клас Турнир
        private List<Cathlete> _players; //тук няма нужда от запазване на място защото...
        
        public Tournament(string tournamentName, List<Cathlete> players)
        { 
           TournamentName = tournamentName;
           _players = players; //директно пренасочваме към паметта където е създаден списъка
                               //който подаваме като параметър
            start();
            showStandings();
        } 

        //Действия, които ще можем да правим
        private void start()
        {
            //Всеки катлет хвърля диск и се сортират спрямо резултатите им
            foreach (Cathlete player in _players)
            {
                player.DiskThrow();
            }

            //Този вграден метод за сортиране и много други ще ги учите в 11-ти клас
            //Просто може да подмените CurrentResult с друго поле при нужда 
            _players = _players.OrderByDescending(p => p.CurrentResult).ToList();
            //запомняме резултата директно в _players, но можеше и в отделен нов списък 
        }

        private void showStandings()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            string header = $"=== {TournamentName} === {DateTime.Now} === ";
            Console.WriteLine(header);
            Console.ResetColor();

            List<string> results = new List<string>();  //Тук ще трупаме класирането за txt file
            results.Add(header);

            int br = 1;
            foreach(Cathlete player in _players)
            {
                string line = $"{br}) {player.Name.PadLeft(12)} {player.CurrentResult:F2} м";
                Console.WriteLine(line); //показваме на екрана резултата на катлета
                results.Add(line);  //и го добавяме в списъка, който ще създаде txt файла

                updateCathleteCard(player, br); 
                br++;
            }

            ReadWrite.SaveAthletesToCSV(); //презаписва актуалните данни във файла с катлети
            ReadWrite.SaveTournamentToTXT(TournamentName, results); //създава txt с info точно за този турнир
        }

        private void updateCathleteCard(Cathlete cat, int place)
        {
            if (place < 4) cat.Medals[place]++; //добавя спечеления медал
            
            cat.Medals[0]++; //увеличава броя участия в турнири
            cat.Experience += 2;

            if (cat.BestPerformance < cat.CurrentResult) cat.BestPerformance = cat.CurrentResult;
            if (place == 1) cat.Experience += 5; //Ако е първи бонус 7 опит
        }
    }
}
