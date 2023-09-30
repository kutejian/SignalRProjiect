using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace WebApplication1.Chat
{
    public class ConnectChat: Hub,IConnectChat
    {
        public void CallByClient(string msg)
        {
            Console.WriteLine("================服务端:被客户端调用================");
            Console.WriteLine(msg);
        }
        public string? CallByClientWithReturnValue(string msg)
        {
            Console.WriteLine("================服务端:被客户端调用(有返回值)================");
            Console.WriteLine(msg);
            return MethodInfo.GetCurrentMethod()?.Name+"的返回值";
        }
        public string? CallClient(string msg)
        {
            Console.WriteLine("================服务端:调用客户端方法================");
            Console.WriteLine("客户端传来的数据"+msg);
            Clients.Caller.SendAsync("CallByHub","发送给客户端的数据1").Wait();
            return MethodInfo.GetCurrentMethod()?.Name;
        }
        public string? CallAllClients(string msg)
        {
            Console.WriteLine("================服务端:全员发送================");
            Console.WriteLine("客户端传来的数据" + msg);
            Clients.All.SendAsync("CallByHub", "发送给客户端的数据2").Wait();
            return MethodInfo.GetCurrentMethod()?.Name;
        }
        public string? CallOtherClients(string msg)
        {
            Console.WriteLine("================服务端:除自己以为发送信息================");
            Console.WriteLine("客户端传来的数据" + msg);
            Clients.Others.SendAsync("CallByHub", "发送给客户端的数据3").Wait();
            return MethodInfo.GetCurrentMethod()?.Name;
        }
        public void RegisterToGroup(string groupname)
        {
            Console.WriteLine("================服务端:创建分组================");

            Groups.AddToGroupAsync(Context.ConnectionId,groupname);
        }
        public void SendToGroup(string name)
        {
            Console.WriteLine("================服务端:分组发送================");
            Console.WriteLine("客户端传来的数据" + name);
            Clients.Group(name).SendAsync("CallByHub", "发送给客户端的数据4").Wait();
        }
        public void SentToClient(string id, string msg)
        {
            Console.WriteLine("================服务端:指定发送================");
            Console.WriteLine("客户端传来的数据" + msg);
            Clients.Client(id).SendAsync("CallByHub", "发送给客户端的数据5").Wait();
        }
    }
}
