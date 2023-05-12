using System.Data;
using MySql.Data.MySqlClient;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UtilityLibrary
{
	public static class MySQLUtil
	{
        private static string connectionString = "";

        public static void StartConnection(string server, int port, string database, string uid, string password)
		{
            connectionString = string.Format("server={0};port={1};database={2};uid={3};password={4};", server, port, database, uid, password);
        }

        public static bool LoginAsUser(string server, int port, string database, string uid, string password)
        {
            string tempConnectionString = string.Format("server={0};port={1};database={2};uid={3};password={4};", server, port, database, uid, password);

            bool success = false;

            using (MySqlConnection connection = new MySqlConnection(tempConnectionString))
            {
                try
                {
                    connection.Open();
                    connectionString = tempConnectionString;
                    success = true;
                } catch (MySqlException e)
                {
                    // Can't login
                    //Console.WriteLine(e.Message);
                    Console.WriteLine("Can't connect with the given credentials.");
                    Thread.Sleep(2000);
                    //Console.ReadKey();
                }
            }
            return success;
        }

        public static (bool, string) CheckEmailAvailable(string email)
        {
            if (connectionString == "")
            {
                throw new Exception("Connection string not set");
            }

            bool emailExists = false;
            bool emailValid = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("check_email", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@email_param", email);
                    command.Parameters.Add("@is_valid", MySqlDbType.Int32);
                    command.Parameters.Add("@email_exists", MySqlDbType.Int32);
                    command.Parameters["@is_valid"].Direction = ParameterDirection.Output;
                    command.Parameters["@email_exists"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    emailExists = Convert.ToInt32(command.Parameters["@email_exists"].Value) == 1;
                    emailValid = Convert.ToInt32(command.Parameters["@is_valid"].Value) == 1;
                }
            }

            if (!emailValid)
            {
                return (false, "Email is not valid");
            }
            if (emailExists)
            {
                return (false, "Email is already registered");
            }

            return (true, "");
        }

        public static (bool, string) CheckEmailExists(string email)
        {
            if (connectionString == "")
            {
                throw new Exception("Connection string not set");
            }

            bool emailExists = false;
            bool emailValid = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("check_email", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@email_param", email);
                    command.Parameters.Add("@is_valid", MySqlDbType.Int32);
                    command.Parameters.Add("@email_exists", MySqlDbType.Int32);
                    command.Parameters["@is_valid"].Direction = ParameterDirection.Output;
                    command.Parameters["@email_exists"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    emailExists = Convert.ToInt32(command.Parameters["@email_exists"].Value) == 1;
                    emailValid = Convert.ToInt32(command.Parameters["@is_valid"].Value) == 1;
                }
            }

            if (!emailValid)
            {
                return (false, "Email is not valid");
            }
            if (!emailExists)
            {
                return (false, "Email is not registered");
            }

            return (true, "");
        }

        public static (bool, string) CheckLength(string input, int minimumLength=0, int maximumLength=int.MaxValue)
        {
            if (input.Length < minimumLength)
            {
                return (false, "Minimum length is: " + minimumLength);
            }
            if (input.Length > maximumLength)
            {
                return (false, "Maximum length is: " + maximumLength);
            }
            return (true, "");
        }


        public static void RegisterClient(string email, string password, string nom, string prenom, string numTel, string adresse, string numCb)
        {
            if (connectionString == "")
            {
                throw new Exception("Connection string not set");
            }

            bool success;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Open the database connection
                connection.Open();

                // Create a MySqlCommand object
                using (MySqlCommand command = new MySqlCommand("create_client", connection))
                {
                    // Set the command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@email_param", email);
                    command.Parameters.AddWithValue("@password_param", password);
                    command.Parameters.AddWithValue("@first_name_param", prenom);
                    command.Parameters.AddWithValue("@name_param", nom);
                    command.Parameters.AddWithValue("@phone_number_param", numTel);
                    command.Parameters.AddWithValue("@address_param", adresse);
                    command.Parameters.AddWithValue("@credit_card_param", numCb);

                    // Output parameters
                    command.Parameters.Add("@success", MySqlDbType.Int32);
                    command.Parameters["@success"].Direction = ParameterDirection.Output;

                    // Execute the command
                    command.ExecuteNonQuery();

                    success = Convert.ToInt32(command.Parameters["@success"].Value) == 1;

                    // Display the result
                    Console.WriteLine("Client registration completed successfully!");
                }
            }
        }

        //public static StandardBouquet[] GetStandardBouquets()
        //{
        //    StandardBouquet[] result = null;
        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // Create a MySqlCommand object
        //        using (MySqlCommand command = connection.CreateCommand())
        //        {
        //            command.CommandText =
        //                "SELECT * FROM standard_bouquet;";

        //            MySqlDataReader reader;
        //            reader = command.ExecuteReader();

        //            string marque;
        //            while (reader.Read())// parcourt ligne par ligne
        //            {
        //                // prix = Convert.ToInt32(Console.ReadLine());
        //                marque = reader.GetString(0);  // récupération de la 1ère colonne (il n'y en a qu'une dans la requête !)
        //                Console.WriteLine(marque);
        //            }

        //            // Add parameters to the command
        //            command.Parameters.AddWithValue("@email_param", email);
        //            command.Parameters.AddWithValue("@password_param", password);
        //            command.Parameters.AddWithValue("@nom_param", nom);
        //            command.Parameters.AddWithValue("@prenom_param", prenom);
        //            command.Parameters.AddWithValue("@num_tel_param", numTel);
        //            command.Parameters.AddWithValue("@adresse_param", adresse);
        //            command.Parameters.AddWithValue("@num_cb_param", numCb);

        //            // Output parameters
        //            command.Parameters.Add("@success", MySqlDbType.Int32);
        //            command.Parameters["@success"].Direction = ParameterDirection.Output;

        //            // Execute the command
        //            command.ExecuteNonQuery();

        //            success = Convert.ToInt32(command.Parameters["@success"].Value) == 1;

        //            // Display the result
        //            Console.WriteLine("Client registration completed successfully!");
        //        }
        //    }

        //    return result;
        //}
    }
}