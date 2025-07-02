using System;

namespace CatsArena
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cathlete.Cathletes = ReadWrite.LoadAthletesFromCsv();
            Display.MainMenu();
        }
    }
}
