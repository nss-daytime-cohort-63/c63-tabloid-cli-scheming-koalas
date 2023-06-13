using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;


namespace TabloidCLI.Repositories
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>

    {
        public JournalRepository(string connectionString) : base(connectionString) { }

        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * From Journal";
                                             

                    List<Journal> journals = new List<Journal>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Journal journal = new Journal()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        };
                        journals.Add(journal);
                    }

                    reader.Close();

                    return journals;
                }
            }
        }

        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Journal journal)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT  INTO Journal (Title, Content,CreateDateTime)
                                      OUTPUT INSERTED.Id
                                      VALUES (@Title, @Content, @CreateDateTime)";
                    cmd.Parameters.AddWithValue("@Title", journal.Title);
                    cmd.Parameters.AddWithValue("@Content", journal.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", journal.CreateDateTime);
                    int id = (int)cmd.ExecuteScalar();

                    journal.Id = id;
                }                      

            }
        }
    }
}
