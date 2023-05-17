using System.Data;
using MySql.Data.MySqlClient;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
using UtilityLibrary;
using MySqlX.XDevAPI.Common;

namespace UtilityLibrary
{
    public static partial class MySQLUtil
    {
        public static List<Client> GetClients()
        {
            List<Client> result = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Create a MySqlCommand object
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM client;";


                    MySqlDataReader reader;
                    reader = command.ExecuteReader();

                    result = new List<Client>();


                    while (reader.Read())// parcourt ligne par ligne
                    {
                        result.Add(new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetString(3), reader.GetString(4),
                            reader.GetString(5), reader.GetString(6), reader.GetDateTime(7)));
                    }
                }
            }

            return result;
        }
        public static void UpdateOrderState(int orderId, string orderState)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();


                string updateQuery = "UPDATE purchase_order SET order_state = @NewState WHERE purchase_order.id=@OrderId";

                // Créez une commande SQL avec la requête et la connexion associées
                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewState", orderState);
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    int rowsAffected = command.ExecuteNonQuery();
                }


            }
        }
    }
}
