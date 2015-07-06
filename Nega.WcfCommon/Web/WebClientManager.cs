using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

using Nega.Modularity;

namespace Nega.WcfCommon
{
    public class WebClientManager : IModule, IClientManager, IClientFinder
    {

        private readonly object lockObj = new object();
        private List<Client> clients;

        public string Name
        {
            get { return "Client Manager"; }
        }

        public IModuleLicence Licence
        {
            get { return Unlimited.Solo; }
        }

        public Client OperationClient
        {
            get
            {
                return GetClientByUserToken(GetUserToken());
            }
        }

        public IEnumerable<Client> Clients
        {
            get
            {
                lock (lockObj)
                {
                    return clients.ToArray();
                }
            }
        }

        public WebClientManager()
        {
            this.clients = new List<Client>();
        }

        public Client AddClient(string username, string userToken = null)
        {
            lock (lockObj)
            {
                Client client = GetClient(username);
                if (client != null)
                {
                    this.clients.Remove(client);
                }
                client = new Client
                    {
                        Username = username,
                        IsAuthenticated = true,
                        IP = OperationContextHelper.GetIP()
                    };
                if (string.IsNullOrWhiteSpace(userToken))
                {
                    client.UserToken = GetUserToken();
                }
                else
                {
                    client.UserToken = userToken;
                }
                this.clients.Add(client);

                return client;
            }
        }

        public Client RemoveClient(string username)
        {
            lock (lockObj)
            {
                Client client = GetClient(username);
                if (client != null)
                {
                    this.clients.Remove(client);
                }

                return client;
            }
        }

        public Client GetClient(string username)
        {
            foreach (var client in Clients)
            {
                if (client.Username == username)
                {
                    return client;
                }
            }

            return null;
        }

        public Client GetClientByUserToken(string userToken)
        {
            foreach (var client in Clients)
            {
                if (client.UserToken == userToken)
                {
                    return client;
                }
            }

            return null;
        }

        public void Initialize()
        {

        }

        public void Destroy()
        {

        }

        public void Dispose()
        {
            Destroy();
        }

        private string GetUserToken()
        {
            string token = WebOperationContextHelper.GetUserToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidOperationException();
            }

            return token;
        }

    }
}
