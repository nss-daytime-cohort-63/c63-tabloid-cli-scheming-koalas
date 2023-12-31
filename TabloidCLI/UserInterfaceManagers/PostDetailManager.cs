﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private PostTagRepository _postTagRepository;
        private int _postId;
        private string _connectionString;
        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _postTagRepository = new PostTagRepository(connectionString);
            _postId = postId;
            _connectionString = connectionString;

        }
        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) View Blog Posts");
            Console.WriteLine(" 3) Add Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 5) Note Management");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    //ViewBlogPosts();
                    return this;
                case "3":
                    AddTag();
                    return this;
                case "4":
                    RemoveTag();
                    return this;
                case "5":

                    return new NoteManager(this, _connectionString,_postId);
                    //RemoveTag();
                   // return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            List<Tag> tags = _postRepository.GetTags(_postId);
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"URL: {post.Url}");
            Console.WriteLine($"Publication Date: {post.PublishDateTime}");
            Console.WriteLine("Tags:");
            foreach (Tag tag in tags)
            {
                Console.WriteLine($"{tag.Id} - {tag.Name}");
            }
        }

        private void AddTag()
        {
            // Add method to only show tags they don't have
            List<Tag> allTags = _tagRepository.GetAll();

            foreach (Tag tag in allTags)
            {
                Console.WriteLine($"{tag.Id} - {tag.Name}");
            }

            Console.WriteLine("What tag would you like to add?");
            int selectedTag = Int32.Parse(Console.ReadLine());
            PostTag newTag = new PostTag()
            {
                PostId = _postId,
                TagId = selectedTag
            };
            _postTagRepository.Insert(newTag);
            Console.WriteLine("Tag added successfully");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
              
        }

        private void RemoveTag()
        {
            List<Tag> tags = _postRepository.GetTags(_postId);
            Console.WriteLine("Post Tags");
            foreach (Tag tag in tags)
            {
                Console.WriteLine($"{tag.Id} - {tag.Name}");
            }
            Console.WriteLine("Which Tag Do You Want To Remove?");
            int tagSelection = Int32.Parse(Console.ReadLine());
            _postTagRepository.removeTag(_postId, tagSelection);
            Console.WriteLine("You have successfully removed the tag");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

    }
}