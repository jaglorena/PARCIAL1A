using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Inicializando los context
builder.Services.AddDbContext<PostsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("parcial1Dbconnection"))
);

// Inicializando los context
builder.Services.AddDbContext<LibrosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("parcial1Dbconnection"))
);

// Inicializando los context
builder.Services.AddDbContext<AutorLibroContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("parcial1Dbconnection"))
);

// Inicializando los context
builder.Services.AddDbContext<AutoresContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("parcial1Dbconnection"))
);

builder.Services.AddDbContext<Parcial1aContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("parcial1Dbconnection"))
);


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
