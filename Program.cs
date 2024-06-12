using API.Context;
using API.Contracts;
using API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*Singleton Pattern Implementation In the whole application*/
builder.Services.AddSingleton<DapperContext>();

/*Injecting the Interface for Querying Database from Controller*/
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();


builder.Services.AddControllers();
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

app.Run();
