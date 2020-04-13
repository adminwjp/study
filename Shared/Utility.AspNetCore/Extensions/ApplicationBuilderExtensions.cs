using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.AspNetCore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseV1(this IApplicationBuilder app, IWebHostEnvironment env,string name)
        {
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                //options.AllowCredentials();
            });
            //app.UseCors("AllowAllOrigins");

            // app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            //要在应用的根 (http://localhost:<port>/) 处提供 Swagger UI，请将 RoutePrefix 属性设置为空字符串
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", name);
                c.RoutePrefix = string.Empty;
            });
            app.UseApiVersioning();
            app.UseHttpsRedirection();
            var cachePeriod = env.IsDevelopment() ? "600" : "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                //FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                //RequestPath = "/StaticFiles",
                OnPrepareResponse = ctx =>
                {
                    // Requires the following import:
                    // using Microsoft.AspNetCore.Http;
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });
            //app.UseCookiePolicy();
            // app.UseAuthentication();
            // app.UseAuthorization();   
            app.UseRouting();
            app.UseEndpoints(options =>
            {
                //iis 不支持 
                options.MapAreaControllerRoute(
                  name: "area",
                  areaName: "areas",
                  pattern: "{area:exists}/{controller}/{action}/{id?}"
                );
                options.MapControllerRoute(
                  name: "default",
                  pattern: "{controller}/{action}/{id?}"
                );
                options.MapControllers();
            });
        }
    }
}
