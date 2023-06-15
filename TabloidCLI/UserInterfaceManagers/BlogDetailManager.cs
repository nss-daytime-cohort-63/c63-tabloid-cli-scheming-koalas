using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private BlogTagRepository _blogTagRepository;
        private int _blogId;

        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _blogTagRepository = new BlogTagRepository(connectionString);
            _blogId = blogId;
        }

        public IUserInterfaceManager Execute()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"{blog.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) View Posts");
            Console.WriteLine(" 0) Go Back");

            Console.WriteLine("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
                    return this;
                case "4":
                    ViewPosts();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            List<Tag> tags = _blogRepository.GetTags(_blogId);
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"Title: {blog.Title}");
            Console.WriteLine($"Url: {blog.Url}");
            Console.WriteLine("Blog Tags");
            foreach(Tag tag in tags )
            {
                Console.WriteLine($"{tag.Id} - {tag.Name}");
            }
            Console.WriteLine();
        }

        private void AddTag()
        {
            //ADD METHOD TO ONLY SHOW TAGS THEY DON'T HAVE
            List<Tag> allTags = _tagRepository.GetAll();

            foreach (Tag tag in allTags)
            {
                Console.WriteLine($"{tag.Id} - {tag.Name}");
            }

            Console.WriteLine("What tag would you like to add?");
            int selectedTag = Int32.Parse(Console.ReadLine());
            BlogTag newTag = new BlogTag()
            {
                BlogId = _blogId,
                TagId = selectedTag
            };
            _blogTagRepository.Insert(newTag);
            Console.WriteLine("Tag added successfully");
            Console.Write("Press any key to continue...");
            Console.ReadKey();

        }

        private void RemoveTag()
        {
            List<Tag> tags = _blogRepository.GetTags(_blogId);
            Console.WriteLine("Blog Tags");
            foreach (Tag tag in tags)
            {
                Console.WriteLine($"{tag.Id} - {tag.Name}");
            }
            Console.WriteLine("Which Tag Do You Want To Remove?");
            int tagSelection = Int32.Parse(Console.ReadLine());
            _blogTagRepository.removeTag(_blogId, tagSelection);
            Console.WriteLine("You Have Successfully Removed The Tag");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        private void ViewPosts()
        {
            List<Post> posts= _blogRepository.GetPosts(_blogId);
            Console.WriteLine("Blog Post's");
            foreach(Post post in posts)
            {
                Console.WriteLine($"{post.Id} - {post.Title} - {post.Author.FirstName} {post.Author.LastName} - {post.PublishDateTime}");
            }
        }
    }
}

