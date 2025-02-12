using MessageService.Hubs;
using MessageService.Repository;
using Microsoft.AspNetCore.Cors.Infrastructure;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddSignalR();

var app = builder.Build();


app.UseCors(builder =>
{
    builder.AllowAnyHeader()
           .AllowAnyMethod()
           .SetIsOriginAllowed((host) => true)
           .AllowCredentials();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<MessageHub>("/chat");


app.Run();
