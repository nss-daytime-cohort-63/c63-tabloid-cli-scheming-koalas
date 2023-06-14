using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.Repositories
{
    public class BlogTagRepository : DatabaseConnector, IRepository<BlogTag>
    {
        public BlogTagRepository(string connectionString) : base(connectionString) { }

        public List<BlogTag> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM BlogTag";

                    List<BlogTag> blogTags = new List<BlogTag>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BlogTag blogTag = new BlogTag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            BlogId = reader.GetInt32(reader.GetOrdinal("BlogId")),
                            TagId = reader.GetInt32(reader.GetOrdinal("TagId"))
                        };
                        blogTags.Add(blogTag);
                    }
                    reader.Close();
                    return blogTags;
                }
            }
        }

        public BlogTag Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM BlogTag WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        BlogTag blogTag = null;

                        if(reader.Read())
                        {
                            blogTag = new BlogTag()
                            {
                                Id = id,
                                BlogId = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                TagId = reader.GetInt32(reader.GetOrdinal("TagId"))
                            };
                        }
                        return blogTag;

                    }
                }
            }
        }

        public void Insert(BlogTag blogTag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO BlogTag (BlogId, TagId) VALUES (@blogId, @tagId)";
                    cmd.Parameters.AddWithValue("@blogId", blogTag.BlogId);
                    cmd.Parameters.AddWithValue("@tagId", blogTag.TagId);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(BlogTag blogTag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE BlogTag SET BlogId = @blogId, TagId = @tagId WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@blogId", blogTag.BlogId);
                    cmd.Parameters.AddWithValue("@tagId", blogTag.TagId);
                    cmd.Parameters.AddWithValue("id", blogTag.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM BlogTag WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
