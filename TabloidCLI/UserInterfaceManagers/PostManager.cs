using System;
using System.Runtime.CompilerServices;
using TabloidCLI.Repositories;
using TabloidCLI.Models;
using System.Collections.Generic;
using System.Globalization;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Post Entries");
            Console.WriteLine(" 2) Add Post Entry");
            Console.WriteLine(" 3) Edit Post Entry");
            Console.WriteLine(" 4) Remove Post Entry");
            Console.WriteLine(" 5) Post Details");
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
                    Console.WriteLine("Select a post");
                    Post selectedPost = _postRepository.Get(Int32.Parse(Console.ReadLine()));
                    return new PostDetailManager(this, _connectionString, selectedPost.Id);
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"{post.Id} - {post.Title} - Written at {post.PublishDateTime} - {post.Blog}");
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post Entry");
            Post post = new Post();

            Console.WriteLine("What is the title?");
            post.Title = Console.ReadLine();
            Console.WriteLine("What is the url");
            post.Url = Console.ReadLine();
            post.PublishDateTime = DateTime.Now;

            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id} - {author.FullName}");
            }

            Console.WriteLine("Who is the author?");
            int selectedAuthorId = Int32.Parse(Console.ReadLine());
            post.Author = _authorRepository.Get(selectedAuthorId);


            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id} - {blog.Title} - {blog.Url}");
            }

            Console.WriteLine("What is the blog?");
            int selectedBlogId = Int32.Parse(Console.ReadLine());
            post.Blog = _blogRepository.Get(selectedBlogId);


            _postRepository.Insert(post);
            Console.WriteLine("Post added successfully");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();



        }

        private void Edit()
        {
            throw new NotImplementedException();
        }

        private void Remove()
        {
            List();
            Console.WriteLine("What post would you like to remove?");
            int selection = Int32.Parse(Console.ReadLine());
            _postRepository.Delete(selection);
            Console.WriteLine("Post has been deleted");
            Console.Write("Press any key to Continue");
            Console.ReadKey();
        }


    }
}
