using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note
                    (Title, Content, CreateDateTime, postId)
                    OUTPUT INSERTED.Id
                    VALUES (@title, @content, @createDateTime, @postId)";
                    cmd.Parameters.AddWithValue("@title", note.Title);
                    cmd.Parameters.AddWithValue("@content", note.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", note.CreateDateTime);
                    cmd.Parameters.AddWithValue("@postId", note.postId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Note> GetAllByPost(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Note WHERE PostId = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Note> notes = new List<Note>();

                    while(reader.Read())
                    {
                        Note note = new Note()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            postId = reader.GetInt32(reader.GetOrdinal("postId"))
                        };

                        notes.Add(note);
                    }
                    conn.Close();
                    return notes;
                }
            }
        }
    }
}
