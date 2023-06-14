using System;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using System.Collections.Generic;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private TagRepository _tagRepository;
        private readonly IUserInterfaceManager _parentUI;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _tagRepository = new TagRepository(connectionString);
            _parentUI = parentUI;
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
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
            List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag tag in tags)
            {
                Console.WriteLine($"{tag.Id} - {tag.Name}");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private void Add()
        {
            Console.WriteLine("New Tag");
            Tag tag = new Tag();

            Console.WriteLine("Tag Name");
            tag.Name = Console.ReadLine();

            _tagRepository.Insert(tag);


        }

        private void Edit()
        {
            List();
            Console.WriteLine("Which Tag would you like to edit?");
            int selection = Int32.Parse(Console.ReadLine());
            Tag selectedTag =_tagRepository.Get(selection);

            if (selectedTag == null)
            {
                return;
            }
            Console.WriteLine();
            Console.WriteLine("New Name (blank to leave unchanged: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                selectedTag.Name = name;
            }
            _tagRepository.Update(selectedTag);
        }

        private void Remove()
        {
            List();
            Console.Write("Which Tag should you remove? ");
            int selection = Int32.Parse(Console.ReadLine());
            _tagRepository.Delete(selection);
            Console.Write("Press any Key to Continue");
            Console.ReadKey();
        }
    }
}
