using DataAccess.Data;
using DataAccess.Repository;
using Domain.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Metrics;
using System.Net;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // ------------------------------------------------------------------------------------------------
            // Here after we added the JWT in appSetting.Jason we have to add a service to read this:
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateAudience = true, // This means I want to validate Audience 
            //            ValidateIssuer = true, // This means I want to validate Issuer
            //            ValidateIssuerSigningKey = true, // This means I want to validate IssuerSigningKey
            //            ValidateLifetime = true, // This means I want to validate Lifetime
            //            // Now after we specify things that I want to validate I have to till service where is 
            //            //   the values that the service will validate with, which I already put them in the 
            //            //    appSetting.Jason file.
            //            ValidIssuer = builder.Configuration["Jwt.Issuer"],
            //            ValidAudience = builder.Configuration["Jwt.Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            //            .GetBytes(builder.Configuration["Jwt.key"]))
            //        };
            //    });
            // ------------------------------------------------------------------------------------------------
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();// Here we added the authentication pipeline
            app.UseAuthorization(); // Here we added the authorization pipeline

            // To demonstrate the different between authentication and authorization we have to know that the 
            // Authentication is the first layer of secure in web app as we can't use web app without login in to
            //   this app if it contains a login request, so Authentication is responsible for Authenticate
            //   the user login by checking if the user already has an account and if the userName and password
            //   are correct and then give the user permission to use the web app.

            // But the authorization pipeline is responsible for give a role and authorize the user role inside
            //   web app, as in our web app each user may has a different role like ("Admin", "user", "manager")
            //   and each one of these could have a different way to access data of system so the Authorization is
            //   responsible for checking roles for each user and give it the access that he could use.

            // Authentication is the process of verifying the identity of a user.It involves establishing who the
            //   user is by checking their credentials, such as a username and password.Once a user is
            //   authenticated, the application can determine that the user is who they claim to be.In the
            //   provided code, the AddAuthentication method is used to set up the authentication middleware,
            //   which is responsible for validating the user's credentials.

            // Authorization is the process of determining what a user is allowed to do within the application
            //   after they have been authenticated.It involves granting or denying access to specific resources
            //   or actions based on the user's role or permissions. For example, an authenticated user may have
            //   different levels of access depending on their role, such as an "Admin" user having more
            //   privileges than a "User" user. In the provided code, the UseAuthorization method is used to set
            //   up the authorization middleware, which is responsible for checking the user's role or permissions
            //   before granting access to specific resources or actions.

            // This is the reason of putting Authentication middleware befor Authorization middleware as the
            //   authentication must work before the authorization.
            app.MapControllers();

            app.Run();
        }
    }
}
