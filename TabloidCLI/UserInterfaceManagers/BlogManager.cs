using System;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using System.Collections.Generic;

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
            Console.Clear();
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blog Entries");
            Console.WriteLine(" 2) Add Blog Entry");
            Console.WriteLine(" 3) Edit Blog Entry");
            Console.WriteLine(" 4) Remove Blog Entry");
            Console.WriteLine(" 5) Blog Details");
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
                case "5": 
                    List();
                    Console.WriteLine("Select a blog");
                    Blog selectedBlog = _blogRepository.Get(Int32.Parse(Console.ReadLine()));
                    return new BlogDetailManager(this, _connectionString, selectedBlog.Id);
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id} - {blog.Title} - {blog.Url}");
            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
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
            List();
            Console.WriteLine("What blog entry would you like to Edit?");

           //ERRORS IF LEFT BLANK - BUG
           int selection = Int32.Parse(Console.ReadLine());
            Blog selectedBlog = _blogRepository.Get(selection);

            if (selectedBlog == null)
            {
                return;
            }
            Console.WriteLine();
            Console.WriteLine("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if(!string.IsNullOrEmpty(title))
            {
                selectedBlog.Title = title;
            }
            Console.WriteLine("New Url (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                selectedBlog.Url = url;
            }
            _blogRepository.Update(selectedBlog);
        }

        private void Remove()
        {
            List();
            Console.Write("Which Blog should you remove? ");
            int selection = Int32.Parse(Console.ReadLine());
            _blogRepository.Delete(selection);
            Console.WriteLine("Blog deleted");
            Console.Write("Press any Key to Continue");
            Console.ReadKey();
        }


    }
}
