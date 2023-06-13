﻿using System;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blog Entries");
            Console.WriteLine(" 2) Add Blog Entry");
            Console.WriteLine(" 3) Edit Blog Entry");
            Console.WriteLine(" 4) Remove Blog Entry");
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
            throw new NotImplementedException();
        }

        private void Add()
        {
            Console.WriteLine("New blog");
            Blog blog = new Blog();

            Console.WriteLine("Blog title");
            blog.Title = Console.ReadLine();

            Console.WriteLine("Blog URL");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);
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
