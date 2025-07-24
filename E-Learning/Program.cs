using AutoMapper;
using Domain.Contract;
using Domain.Models.IdentityModel;
using E_Learning.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositorice;
using Services;
using Services.MappingProfil;
using ServicesAbstraction;
using Share.Settings;
using Store.Web.Extensions;
using System;
using System.Reflection.Metadata;

namespace E_Learning
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region services to the container.


            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddAplicationServices(builder.Configuration);
            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTService(builder.Configuration);
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });




            #endregion

            var app = builder.Build();

            #region configure http request
            app.SeedDataBaseAsync();

            app.UseCustomExceptionMiddelWare();
        


            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWear();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}
