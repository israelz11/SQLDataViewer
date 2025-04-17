using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SQLDataViewer.Helper
{
    public static class ConnectionHelper
    {
        public static bool SaveConnection(string connectionString)
        {
            try
            {
                //Leer la cadena de texto connectionString y tranformarla a json
                string json = JsonConvert.SerializeObject(connectionString);
                List<string> listParameters = connectionString.Split(';').ToList();
                string server = listParameters.FirstOrDefault(x => x.StartsWith("Server="))?.Split('=')[1];
                string database = listParameters.FirstOrDefault(x => x.StartsWith("Database="))?.Split('=')[1];
                string user = listParameters.FirstOrDefault(x => x.StartsWith("User Id="))?.Split('=')[1];
                string password = listParameters.FirstOrDefault(x => x.StartsWith("Password="))?.Split('=')[1];
                string connectionStringJson = JsonConvert.SerializeObject(new
                {
                    Server = server,
                    Database = database,
                    User = user,
                    Password = password,
                });

                // Guardar el JSON en el archivo de configuración
                string configFilePath = database + "_connection.json";
                if (!System.IO.File.Exists(configFilePath))
                {
                    System.IO.File.Create(configFilePath).Close();
                }
                System.IO.File.WriteAllText(configFilePath, connectionStringJson);
                // Leer el archivo de configuración y deserializar el JSON
                string jsonFromFile = System.IO.File.ReadAllText(configFilePath);
                var connectionStringFromFile = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonFromFile);
                // Verificar que la cadena de conexión se haya guardado correctamente
                if (connectionStringFromFile == null)
                {
                    Console.WriteLine("Error al guardar la cadena de conexión.");
                    return false;
                }
                if (connectionStringFromFile["Server"] != server ||
                    connectionStringFromFile["Database"] != database ||
                    connectionStringFromFile["User"] != user ||
                    connectionStringFromFile["Password"] != password)
                {
                    Console.WriteLine("Error al guardar la cadena de conexión.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la cadena de conexion: {ex.Message}");
                return false;
            }
        }

    }
}
