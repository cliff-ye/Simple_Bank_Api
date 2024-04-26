using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimpleApiProject.Data;
using SimpleApiProject.UnitofWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Swagger Documentation Section
var api_info = new OpenApiInfo()
{
    Title = "Simple Bank Api",
    Version = "v1",
    Description = "An Api to perform simple bank operations \n" +
    "- Create an Account(account created have a balance of 0) \n" +
    "- Deposit into the account \n" +
    "- Make transfer to an existing account \n" +
    "- Check all your transactions",
    Contact = new OpenApiContact()
    {
        Name = "Clifford Amankwah Agyei",
        Email = "cagyei529@gmail.com",
    }

};

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",api_info);
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

builder.Services.AddScoped<IUnitofWork, UnitofWork>();

//register db context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});



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

app.Run();
