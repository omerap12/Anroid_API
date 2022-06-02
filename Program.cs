using WebApi.Hubs;
using WebShop;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
            }));

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow All", builder =>
    {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials();
    });
});*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{contoller=Home}/{action=index}/{id?}"
);

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/MyHub");
});

using(var database = new ItemsContext())
{
    var users = database.Users.Include(p => p.Contacts).Include(p => p.Conversations).ToList();
    var convOfFirst = users.FirstOrDefault().Conversations;
    var conv = database.Conversations.Include(p => p.Messages).ToList();
    var msgs = conv.FirstOrDefault().Messages;
    var num = 1;
}
app.Run();
