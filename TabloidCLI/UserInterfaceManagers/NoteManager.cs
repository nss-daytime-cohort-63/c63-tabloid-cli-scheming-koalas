using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;


namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
    private readonly IUserInterfaceManager _parentUI;
    private NoteRepository _noteRepository;
    private string _connectionString;
        private int _postId;
        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId) {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;
            _postId = postId;
        }
        public IUserInterfaceManager Execute()
        {
            Console.Clear();
            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) List Note Entries");
            Console.WriteLine(" 2) Add Note Entry");
            Console.WriteLine(" 3) Remove Note Entry");
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
            Console.WriteLine("Notes for this Post:");
            

            List<Note> notes = _noteRepository.GetAllByPost(_postId);
            foreach (Note note in notes)
            {
                Console.WriteLine($"{note.Id} - {note.Title} - {note.Content} - Created On: {note.CreateDateTime}");
            }

            Console.Write("Press any key to continue");
            Console.ReadKey();

        }

        private void Add()
        {
            Console.WriteLine("New Note Entry");
            Note note = new Note();

            Console.WriteLine("What is the title?");
            note.Title = Console.ReadLine();
            Console.WriteLine("What is the content?");
            note.Content = Console.ReadLine();
            note.CreateDateTime = DateTime.Now;
            note.postId = _postId;
            _noteRepository.Insert(note);
            Console.WriteLine("New Note Successfully Added");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        private void Remove()
        {
            List();
            Console.WriteLine("What post would you like to remove?");
            int selection = Int32.Parse(Console.ReadLine());
            _noteRepository.Delete(selection);
            Console.WriteLine("Note has been deleted");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
    }
}
