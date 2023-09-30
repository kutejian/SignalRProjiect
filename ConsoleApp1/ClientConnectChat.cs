using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    public class ClientConnectChat
    {
        //创建连接 连接到服务端的Hub
        public HubConnection GetHubConnectionBuilder()
        {
            HubConnection hubConnection;

            //注意连接地址必须是 前端ip加端口和文件夹路径 和服务端的app.MapHub<ConnectChat>("/Chat/ConnectChat");这个一样说清楚要不然说404
            hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7050/Chat/ConnectChat").Build();

            //断线重连
            hubConnection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 30) * 1000);
                await hubConnection.StartAsync();
            };
            //注册服务响应 这个得写在await hubConnection.StartAsync();这个前面
            //这个表示接收服务发过来的数据 CallByHub是服务写的一个名
            //订阅服务端发过来的数据
            hubConnection.On<string>("CallByHub", (message) =>
            {
               Console.WriteLine("接收到来自 SignalR Hub 的消息：" + message);
            });

            return hubConnection;
        }
        public async Task CallByClient(HubConnection hubConnection, string msg)
        {
            Console.WriteLine("================前端:被客户端调用================");
            //这里的第一个参数是根据 你服务端的方法名相关的
            Console.WriteLine("你发送给后端的信息:" + msg);
            await hubConnection.SendAsync("CallByClient", msg);
        }
        public async Task<string?> CallByClientWithReturnValue(HubConnection hubConnection,string msg)
        {
            Console.WriteLine("================前端:被客户端调用(有返回值)================");
            Console.WriteLine("你发送给后端的信息:"+msg);
            var ret = await hubConnection.InvokeAsync<string>("CallByClientWithReturnValue", "发送给后端的数据"+ msg);
            return "后端的返回值" + ret;
        }
        public async Task CallClient(HubConnection hubConnection,string msg)
        {
            Console.WriteLine("================前端:调用客户端方法================");
            Console.WriteLine("你发送给后端的信息:" + msg);

            await hubConnection.SendAsync("CallClient", msg);
        }
        public async Task CallAllClients(HubConnection hubConnection,string msg)
        {
            Console.WriteLine("================前端:全员发送================");
            Console.WriteLine("你发送给后端的信息:" + msg);
             await hubConnection.SendAsync("CallAllClients", "发送给后端的数据" + msg);
        }
        public async Task CallOtherClients(HubConnection hubConnection,string msg)
        {
            Console.WriteLine("================前端:除自己以为发送信息================");
            Console.WriteLine("你发送给后端的信息:" + msg);
             await hubConnection.SendAsync("CallOtherClients", "发送给后端的数据" + msg);
        }
        public async Task RegisterToGroup(HubConnection hubConnection, string group)
        {
            Console.WriteLine("================前端:创建分组================");

            await hubConnection.SendAsync("RegisterToGroup", group);
        }
        public async Task SendToGroup(HubConnection hubConnection,string name)
        {
            Console.WriteLine("================前端:分组发送================");
            await hubConnection.SendAsync("SendToGroup", name);
        }
        public async Task SentToClient(HubConnection hubConnection, string id)
        {
            Console.WriteLine("================前端:指定发送================");
            await hubConnection.SendAsync("SentToClient", id, hubConnection.ConnectionId);
        }
    }
}
