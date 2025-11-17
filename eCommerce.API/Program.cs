using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.MiddleWare;
using System.Text.Json.Serialization;
using eCommerce.Core.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

//AddCors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "http://localhost:5067")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                  .AllowCredentials();
        });
});


//Adding Services
builder.Services.AddInfraStructure();
builder.Services.AddCore();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = "<License Key Here>", typeof(ApplicationUserMapping).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audiences = builder.Configuration.GetSection("Jwt:Audience").GetChildren()
                          .Select(x => x.Value)
                          .ToArray();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{

   options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudiences = audiences,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))

    };

});


var app = builder.Build();
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService");

});
app.MapControllers();


app.Run();
