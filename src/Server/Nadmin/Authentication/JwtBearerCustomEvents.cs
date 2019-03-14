using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Nadmin.Authentication
{
    public class JwtBearerCustomEvents : JwtBearerEvents
    {
        public JwtBearerCustomEvents()
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Query["access_token"];
                return Task.CompletedTask;
            };

            OnTokenValidated = context =>
            {
                //context.SecurityToken.Id
                //context.Fail("用户已退出");
                return Task.CompletedTask;
            };
        }
    }
}