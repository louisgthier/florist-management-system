using System.Data;
using MySql.Data.MySqlClient;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UtilityLibrary
{
	public static partial class MySQLUtil
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

        public static (bool, string) CheckDate(string input, DateTime? minimumDate = null, DateTime? maximumDate=null)
        {
            MySql.Data.Types.MySqlDateTime converted;

            try
            {
                converted = new MySql.Data.Types.MySqlDateTime(input);
                converted.GetDateTime();
            } catch (Exception ex) when (ex is System.FormatException || ex is MySql.Data.Types.MySqlConversionException || ex is System.ArgumentOutOfRangeException || ex is System.IndexOutOfRangeException)
            {
                return (false, "The date is not in the correct format");
            }

            if (minimumDate != null)
            {
                if (converted.GetDateTime() < minimumDate)
                {
                    return (false, "Date should be after: " + minimumDate.ToString());
                }
            }

            if (maximumDate != null)
            {
                if (converted.GetDateTime() > maximumDate)
                {
                    return (false, "Date should be before: " + maximumDate.ToString());
                }
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

        public static List<StandardBouquet> GetStandardBouquets()
        {
            List<StandardBouquet> result = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Create a MySqlCommand object
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT * FROM standard_bouquet;";

                    MySqlDataReader reader;
                    reader = command.ExecuteReader();

                    result = new List<StandardBouquet>();


                    string marque;
                    while (reader.Read())// parcourt ligne par ligne
                    {
                        result.Add(new StandardBouquet(reader.GetString(0), reader.GetString(1), float.Parse(reader.GetString(2)), reader.GetString(3)));
                    }
                }
            }

            return result;
        }

        public static bool OrderStandardBouquet(StandardBouquet bouquet, string deliveryAddress, string message, string deliveryDate)
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
                using (MySqlCommand command = new MySqlCommand("order_standard_bouquet", connection))
                {
                    // Set the command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@delivery_address_param", deliveryAddress);
                    command.Parameters.AddWithValue("@message_param", message);
                    command.Parameters.AddWithValue("@delivery_date_param", deliveryDate);
                    command.Parameters.AddWithValue("@bouquet_name_param", bouquet.Name);

                    // Output parameters
                    command.Parameters.Add("@success", MySqlDbType.Int32);
                    command.Parameters["@success"].Direction = ParameterDirection.Output;

                    // Execute the command
                    command.ExecuteNonQuery();

                    success = Convert.ToInt32(command.Parameters["@success"].Value) == 1;

                    // Display the result
                    if (success)
                        Console.WriteLine("Purchase order completed successfully!");
                    else
                        Console.WriteLine("Could not complete order");

                    return success;
                }
            }
        }

        //public static List<PurchaseOrder> GetPurchaseOrders()
        //{
        //    List<PurchaseOrder> result = null;
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

        //            result = new List<PurchaseOrder>();


        //            string marque;
        //            while (reader.Read())// parcourt ligne par ligne
        //            {
        //                result.Add(new StandardBouquet(reader.GetString(0), reader.GetString(1), float.Parse(reader.GetString(2)), reader.GetString(3)));
        //            }
        //        }
        //    }

        //    return result;
        //}
    }
}