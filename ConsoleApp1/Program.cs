using ConsoleApp1;
using Microsoft.AspNetCore.SignalR.Client;
using System;

Console.WriteLine("Hello, World!");

ClientConnectChat clientConnectChat = new ClientConnectChat();

HubConnection hubConnection1 = clientConnectChat.GetHubConnectionBuilder();
HubConnection hubConnection2 = clientConnectChat.GetHubConnectionBuilder();
HubConnection hubConnection3 = clientConnectChat.GetHubConnectionBuilder();
HubConnection hubConnection4 = clientConnectChat.GetHubConnectionBuilder();
await hubConnection1.StartAsync();
await hubConnection2.StartAsync();
await hubConnection3.StartAsync();
await hubConnection4.StartAsync();

//被客户端调用
//await clientConnectChat.CallByClient(hubConnection1, "CallByClient的方法");

//被客户端调用(有返回值)
/*var ret  =await clientConnectChat.CallByClientWithReturnValue(hubConnection1, "CallByClientWithReturnValue的方法");
Console.WriteLine(ret);*/

//调用客户端方法
//await hubConnection1.SendAsync("CallClient", "CallClient的方法");


//全员发送
//await clientConnectChat.CallAllClients(hubConnection1, "CallAllClients的方法");

//除自己以为发送信息
//await clientConnectChat.CallOtherClients(hubConnection1, "CallOtherClients的方法");

/*//创建分组
await clientConnectChat.RegisterToGroup(hubConnection1, "grp1");
await clientConnectChat.RegisterToGroup(hubConnection2, "grp1");
await clientConnectChat.RegisterToGroup(hubConnection3, "grp1");
await clientConnectChat.RegisterToGroup(hubConnection4, "grp2");
//分组发送
await clientConnectChat.SendToGroup(hubConnection1, "grp1");*/


//指定发送
await clientConnectChat.SentToClient(hubConnection1, hubConnection1.ConnectionId);


//因为是异步所以一定要加这种类型的 要不然数据会不全
Console.ReadLine();