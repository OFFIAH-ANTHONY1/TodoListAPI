using Microsoft.EntityFrameworkCore;
using TodoListAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TodoListContext>(options =>
options.UseSqlServer(builder.Configuration["ConnectionStrings:FinancialDbContext"]));

builder.Services.AddScoped<TodoItem>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var context = services.GetRequiredService<TodoListContext>();
    context.Database.Migrate();
}

app.Run();
