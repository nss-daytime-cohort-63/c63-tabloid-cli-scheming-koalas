using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using System.Collections.Generic;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BackgroundManager : IUserInterfaceManager
    {
        private string _connectionString;
        private readonly IUserInterfaceManager _parentUI;

        public BackgroundManager(IUserInterfaceManager parentUI, string connectionString)
            {
            _parentUI = parentUI;
            _connectionString = connectionString;
            }
        public IUserInterfaceManager Execute()
        {
            Console.Clear();
            Console.WriteLine("Choose Your Background Color");
            Console.WriteLine(" 1) Blue");
            Console.WriteLine(" 2) Red");
            Console.WriteLine(" 3) Gray");
            Console.WriteLine(" 4) Green");
            Console.WriteLine(" 5) Black");
            Console.WriteLine(" 6) White");
            Console.WriteLine();
            Console.WriteLine("Choose Your Text Color");
            Console.WriteLine(" 7) Blue");
            Console.WriteLine(" 8) Red");
            Console.WriteLine(" 9) Gray");
            Console.WriteLine(" 10) Green");
            Console.WriteLine(" 11) Black");
            Console.WriteLine(" 12) White");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.Black;
                    return this;
                case "6":
                    Console.BackgroundColor = ConsoleColor.White;
                    return this;
                case "7":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    return this;
                case "8":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    return this;
                case "9":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    return this;
                case "10":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    return this;
                case "11":
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "12":
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
    }
}
