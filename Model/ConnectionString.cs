using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataViewer.Model
{
    public class ConnectionString
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Provider { get; set; }

        public ConnectionString(string server, string database, string user, string password, int port, string provider)
        {
            Server = server;
            Database = database;
            User = user;
            Password = password;
            Port = port;
            Provider = provider;
        }
    }
}
