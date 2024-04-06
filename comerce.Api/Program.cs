using comerce.Api.Configuracao;
using comerce.aplication.contract;
using comerce.aplication.services;
using comerce.data.context;
using comerce.data.Contract;
using comerce.data.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProdutoService, ProdutoService>();
//Add Repositorys to the container;
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddDbContext<ComerceContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<ComerceContext>();
context.Database.Migrate();
context.MigrateScripts("Script");
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
