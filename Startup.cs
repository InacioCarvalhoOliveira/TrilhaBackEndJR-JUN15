using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Shop.Database;
using System.Text;

namespace Shop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                       .WithOrigins("http://localhost:5039")
                       .WithOrigins("http://localhost:8000")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials(); // Allow credentials if neededs
                    });
            });

            // Add DbContext with SQL Server
            services.AddDbContext<DataContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

            // Add Authentication with JWT
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true
                };
            });

            // Register logger
            services.AddLogging();

            // Add MVC controllers
            services.AddControllers();

            // Add Swagger for API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Shop", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Apply CORS policy
            app.UseCors("AllowAllOrigins");

            // Apply Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Log requests and responses
            app.Use(async (context, next) =>
            {
                await next.Invoke();
                // Log the response headers to verify CORS headers
                var headers = context.Response.Headers;
                Console.WriteLine($"CORS Headers: {string.Join(", ", headers.Keys)}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map API controllers
            });
        }
    }
}
