using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace WebApplication1.Chat
{
    public interface IConnectChat
    {
        public void CallByClient(string msg);
        public string? CallByClientWithReturnValue(string msg);
        public string? CallClient(string msg);
        public string? CallAllClients(string msg);
        public string? CallOtherClients(string msg);
    }
}
