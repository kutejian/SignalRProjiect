using Microsoft.AspNetCore.SignalR;
using WebApplication1.Chat;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IConnectChat, ConnectChat>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
//参数中的值一定要和以上设置的跨域名字一样
app.MapHub<ConnectChat>("/Chat/ConnectChat");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
