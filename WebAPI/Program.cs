using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using ToDoApp.DataAccess.Context;
using ToDoApp.Business.DependencyResolvers;
using ToDoApp.Business.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ToDoApp.Core.Utilities.Security.JWT;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.Core.Utilities.Security.Encryption;
using ToDoApp.Core.Utilities.IoC;
using FluentAssertions.Common;
using ToDoApp.Core.Extensions;
using ToDoApp.Core.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutofacBusinessModule()); });
builder.Services.AddAutoMapper(typeof(MapProfile));


var connectionBackend = builder.Configuration["ConnectionStrings:Connectionstring"];
//var hangfireConnection = builder.Configuration["ConnectionStrings:HangfireConnection"];
builder.Services.AddDbContext<BackendContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Connectionstring")));

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});
//ServiceTool.Create(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
