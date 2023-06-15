using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString) { }

        public List<Note> GetAll()
        {
            return null;
        }

        public Note Get(int id)
        {
            return null;
        }

        public void Update(Note note)
        {

        }

        public void Delete(int id)
        {

        }

        public void Insert(Note note)
        {

        }
    }
}
