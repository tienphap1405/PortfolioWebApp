using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;


namespace LoginAndRegistration.Database
{
    //This class is use for accessing the database to implement functionalities that would require the database to be opened and connected
    public class UsersDatabase
    {
        private readonly string connectionString;
        private readonly PasswordHasher hasher;
        public UsersDatabase() 
        {
            // Create a new instance of MySqlConnectionStringBuilder to build MySQL connection strings
            MySqlConnectionStringBuilder builder = 
            new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "password",
                Database = "userdatabase",
            };
            connectionString = builder.ConnectionString; 
            hasher = new PasswordHasher();        }
        public async Task<bool> CreateUser(string email, string password, string address1, 
        string address2, string city, string state, string postalCode)
        {
            try
            { 
                // Create a MySqlConnection object using the connection string built with the provided builder
                MySqlConnection connection = new MySqlConnection(connectionString);
                // Open the connection to the MySQL database
                await connection.OpenAsync();
                // Hash the password with salt using bcrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); 

                string sql = "INSERT INTO users (UserEmail, Password, Address1, Address2, City, State, PostalCode) VALUES (@UserEmail, @Password, @Address1, @Address2, @City, @State, @PostalCode)";
                MySqlCommand command = new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("@UserEmail", email);
                command.Parameters.AddWithValue("@Password", hashedPassword);
                command.Parameters.AddWithValue("@Address1", address1);
                command.Parameters.AddWithValue("@Address2", address2);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@State", state);
                command.Parameters.AddWithValue("@PostalCode", postalCode);

                //This method returns the number of rows affected by the query.
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
                // Return true if the user was successfully created
                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AuthenticateUsers(string email, string password)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                string sql = "SELECT Password FROM users WHERE UserEmail = @UserEmail";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserEmail", email);

				// Execute the query and retrieve the hashed password
				string hashedPassword = (string)await command.ExecuteScalarAsync();

                // If the hashed password is null, the user does not exist
                if(hashedPassword == null)
                {
                    return false;
                }

                await connection.CloseAsync();
                // Verify the provided password against the stored hashed password
                return hasher.VerifyPassword(password, hashedPassword);


            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"An error occurred: {ex.Message}");
                return false;
            }
        }
         // This function is used for retrieving the users data from the database
         //And read the users by saving it into the list
        public async Task<List<User>> GetUsers()
        {
            List<User> userslist = new List<User>();
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                string sql = "SELECT * FROM users";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    User user = new User()
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        UserEmail = reader["UserEmail"].ToString(),
                        Password = reader["Password"].ToString(),
                        Address1 = reader["Address1"].ToString(),
                        Address2 = reader["Address2"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),                    
                        PostalCode = reader["PostalCode"].ToString()
                    };
                    userslist.Add(user);
                }
            }
            //This exception use for handling database errors
            catch (MySqlException ex)
            {
                await Console.Out.WriteLineAsync($"Database error occurred: {ex.Message}");

            }
            //This exception use for handling general errors
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Errors occurred: {ex.Message}");
            }
            return userslist;
        }
    }
}
