using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using ToDoApp.DataAccess.Context;
using ToDoApp.Business.DependencyResolvers;
using ToDoApp.Business.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutofacBusinessModule()); });
builder.Services.AddAutoMapper(typeof(MapProfile));


var connectionBackend = builder.Configuration["ConnectionStrings:Connectionstring"];
//var hangfireConnection = builder.Configuration["ConnectionStrings:HangfireConnection"];
builder.Services.AddDbContext<BackendContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Connectionstring")));

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
