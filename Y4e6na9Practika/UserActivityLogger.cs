using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Y4e6na9Practika
{
    public class UserActivityLogger
    {
        Entities entities = new Entities();
        public void LogUserActivity(int userId, string activityDescription, string NameUser)
        {

            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            string ipAddress = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();

            var userActivity = new LoggerUser
            {
                Users_ID = userId,
                Discription = activityDescription,
                Nameuser = NameUser,
                TimeOut = DateTime.Now,
                AddressIP = ipAddress,
            };

            entities.LoggerUser.Add(userActivity);
            entities.SaveChanges();
        }
    }
}
