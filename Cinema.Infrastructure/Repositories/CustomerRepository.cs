using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain;
using Cinema.Core.Repositories;
using Dapper;

namespace Cinema.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for customer object.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Gets customer by id.
        /// </summary>
        /// <param name="id">id of the customer</param>
        /// <returns>Customer object</returns>
        public Customer Get(int id)
        {
            Customer customer = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                customer = db.Query<Customer>("SELECT * FROM Customer " +
                                                $"WHERE Id = {id} ", new { id }).SingleOrDefault();
            }
            return customer;
        }

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>List of Customers</returns>
        public IList<Customer> GetAll()
        {
            IList<Customer> customers = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                customers = db.Query<Customer>("SELECT * FROM Customer ").ToList();
            }
            return customers;
        }

        /// <summary>
        /// Gets a customer by input data like Name, Surname, Phone or Email. Checks if customer with this data exists in database. Otherwise returns null.
        /// </summary>
        /// <param name="customerInput">Customer object with received data.</param>
        /// <returns></returns>
        private Customer GetByData(Customer customerInput)
        {
            Customer customer = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                string sql1 = $"SELECT * FROM Customer  WHERE CustomerName = '{customerInput.CustomerName}' " +
                             $" AND CustomerSurname = '{customerInput.CustomerSurname}' AND Phone = '{customerInput.Phone}'";
                string sql2 = $"SELECT * FROM Customer  WHERE CustomerName = '{customerInput.CustomerName}' " +
                             $" AND CustomerSurname = '{customerInput.CustomerSurname}' AND Email = '{customerInput.Email}'";
                try
                { 
                    customer = db.Query<Customer>(sql1).First();
                }
                catch { }
                if (customer == null) {
                    try
                    {
                        customer = db.Query<Customer>(sql2).First();
                    }
                    catch { }
                }

            }
            return customer;
        }

        /// <summary>
        /// Gets a customer with input data like Name, Surname, Phone, Email or adds if doesn't exist. 
        /// Customer exists if there is a customer in database with the same name, surname and phone or name, surname and e-mail.
        /// </summary>
        /// <param name="customerInput">Customer object with input data.</param>
        /// <returns>Customer object</returns>
        public Customer GetOrAddByData(Customer customerInput)
        {

            int id;
            Customer customer = GetByData(customerInput);
            if (customer == null)
            {
                id = Insert(customerInput);
                customer = Get(id);
            }
            return customer;
        }

        private int Insert(Customer item)
        {
            int id;
            string sql = @"INSERT INTO Customer (CustomerName, CustomerSurname, Phone, Email) Values (@CustomerName, @CustomerSurname, @Phone, @Email);
                            SELECT CAST(SCOPE_IDENTITY() as int);";
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                id = db.Query<int>(sql, new
                {
                    item.CustomerName,
                    item.CustomerSurname,
                    item.Phone,
                    item.Email
                }).Single();
            }
            return id;
        }

        /// <summary>
        /// Removes a custome customer.
        /// </summary>
        /// <param name="id">id of custom customer.</param>
        public void Remove(int id)
        {
            string sql = $"DELETE FROM Customer WHERE Id= {id}";
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                var affectedRows = db.Execute(sql, new { Id = id });
            }
        }
      }
}
