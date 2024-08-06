using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebLogin.Models;

[assembly: OwinStartup(typeof(WebLogin.DAL.UserDAL))]

namespace WebLogin.DAL
{
    public class UserDAL
    {
        private SqlConnection _connection;

        public UserDAL()
        {
            ConnectionDatabase cd = new ConnectionDatabase();
            _connection = cd.SqlConnection;
        }

        public List<LoginViewModel> GetUserListFromDB()
        {
            List<LoginViewModel> users = new List<LoginViewModel>();

            _connection.Open();
            try
            {

                string query = "SELECT * FROM users;";
                SqlCommand cmd = new SqlCommand(query, this._connection);

                // Recuperamos un lector...
                SqlDataReader records = cmd.ExecuteReader();

                while (records.Read())
                {
                    LoginViewModel user = new LoginViewModel();
                    user.Email = records.GetString(records.GetOrdinal("Email"));
                    user.Password = records.GetString(records.GetOrdinal("Password"));

                    // Agrega más campos según la estructura de tu tabla y tu clase Job
                    users.Add(user);
                }
                _connection.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return users;
        }

        public bool ValidateUser(string email, string password)
        {
            bool isValid = false;
            _connection.Open();
            try
            {
                string query = "SELECT COUNT(*) FROM users WHERE Email = @Email AND Password = @Password;";
                SqlCommand cmd = new SqlCommand(query, this._connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                int userCount = (int)cmd.ExecuteScalar();

                if (userCount > 0)
                {
                    isValid = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return isValid;
        }
    }
}


