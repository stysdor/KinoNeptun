using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Cinema.Core.Domain;
using Cinema.Core.Repositories;
using Dapper;

namespace Cinema.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for category object.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        /// <summary>
        /// Gets Category by id
        /// </summary>
        /// <param name="id">id of the category.</param>
        /// <returns>Instance of Category class</returns>
        public Category Get(int id)
        {
            Category category = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                category = db.Query<Category>("SELECT * FROM Category " +
                                                $"WHERE Id = {id} ", new { id }).SingleOrDefault();
            }
            return category;
        }

        /// <summary>
        /// Gets list of all categories.
        /// </summary>
        /// <returns>IList of all categories.</returns>
        public IList<Category> GetAll()
        {
            IList<Category> categories = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                categories = db.Query<Category>("SELECT * FROM Category ").ToList();
            }
            return categories;
        }

        /// <summary>
        /// Gets category object by its name. If category of that name doesn't exist it returns null.
        /// </summary>
        /// <param name="name">Name of category</param>
        /// <returns>Categry object ar null.</returns>
        public Category GetByName(string name)
        {
            Category category = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                try
                {
                    category = db.Query<Category>($"SELECT * FROM Category " +
                                                        $" WHERE CategoryName = '{name}'").First();
                }
                catch { }       
            }
            return category;
        }

        /// <summary>
        /// Gets category object if category with that categoryName exists. Otherwise it insterts a new category into datebase. 
        /// </summary>
        /// <param name="name">Name of category</param>
        /// <returns>Category object.</returns>
        public Category GetOrAddByName(string name)
        {
            int id;
            Category category = GetByName(name);
            if(category == null)
            {
                id = Insert(name);
                category = Get(id);
            }
            return category;
        }

        private int Insert(string name)
        {
            int id;
            string sql = $"INSERT INTO Category (CategoryName) Values ('{name}');" 
                + "SELECT CAST(SCOPE_IDENTITY() as int);";
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {

                id = db.Query<int>(sql, new {
                    CategoryName = name
                }).Single();
            }     
            return id;
        }
    }
}
