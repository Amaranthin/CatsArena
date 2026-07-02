using System;

namespace CatsArena
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cathlete.Cathletes = ReadWrite.LoadAthletesFromCsv();
            Display.MainMenu();

            //katev01 test

            //dobavqm text na 02-07 ...
        }
    }
}
