using System.Text;
using angular_jwt_auth_dotnetcore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace angular_jwt_auth_dotnetcore {
    public static class Extensions {
        public static void ConfigureCORS(this IServiceCollection services) {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCORS", builder =>
                {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });
        }
        public static void ConfigureJWTAuthentication (this IServiceCollection services) {
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "https://debjena.github.io",
                    ValidAudience = "https://debjena.github.io",
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("DebKey@321@secret"))//make sure key length big
                    };
                });
        }
        public static void ConfigureDbContext (this IServiceCollection services, IConfiguration config) {
            services.AddDbContext<UserDbContext> (opt => opt.UseInMemoryDatabase ("UsersDb"));
            //var connectionString = config["mssql:connectionString"];
            //services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));
        }

    }
}