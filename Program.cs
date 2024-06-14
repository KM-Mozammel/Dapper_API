using API.Context;
using API.Contracts;
using API.Repository;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
/*Singleton Pattern Implementation In the whole application*/
builder.Services.AddSingleton<DapperContext>();
/*Injecting the Interface for Querying Database from Controller*/
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

// Adding Cors and Configuring for Localhost:3000
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
