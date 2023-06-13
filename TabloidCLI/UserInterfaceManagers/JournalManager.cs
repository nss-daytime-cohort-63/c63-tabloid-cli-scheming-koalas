using System;
using TabloidCLI.Repositories;
using TabloidCLI.Models;
using System.Globalization;
using System.Collections.Generic;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;

        private JournalRepository _journalRepository;
        private string _connectionString;
        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Add Journal Entry");
            Console.WriteLine(" 3) Edit Journal Entry");
            Console.WriteLine(" 4) Remove Journal Entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal journal in journals)
            {
                Console.WriteLine($"{journal.Id} - {journal.Title} - Written at {journal.CreateDateTime} - {journal.Content}");
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            Console.Write("What is the Title: ");
           journal.Title = Console.ReadLine();
            Console.Write("What is the Content: ");
            journal.Content = Console.ReadLine();

            journal.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(journal);

            Console.WriteLine("New Journal Added");
            Console.Write("Press any Key to Continue");
            Console.ReadKey();

        }

        private void Edit()
        {
            throw new NotImplementedException();
        }

        private void Remove()
        {
            throw new NotImplementedException();
        }


    }
}
