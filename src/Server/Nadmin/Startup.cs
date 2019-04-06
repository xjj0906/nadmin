using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nadmin.Authentication;
using Nadmin.Common;
using Nadmin.Common.AppSetting;
using Nadmin.Filter;
using Nadmin.Service;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Text;

namespace Nadmin
{
    public class Startup
    {
        readonly string AllowAllRequests = "_allowAllRequests";
        readonly string LimitRequests = "_limitRequests";

        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy(AllowAllRequests, policy =>
                {
                    policy
                        .AllowAnyOrigin()//允许任何源
                        .AllowAnyMethod()//允许任何方式
                        .AllowAnyHeader();//允许任何头
                                          //.AllowCredentials();//允许cookie
                });

                c.AddPolicy(LimitRequests, policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200", "https://localhost:4200")//端口号后不要带/斜杆
                        .AllowAnyHeader()//Ensures that the policy allows any header.
                        .AllowAnyMethod();
                });
            });

            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalExceptionsFilter>();
                //o.OutputFormatters.RemoveType<JsonOutputFormatter>();
                //o.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings
                //{
                //    ContractResolver = new DefaultContractResolver
                //    {
                //        NamingStrategy = new CamelCaseNamingStrategy(),
                //    },
                //    NullValueHandling = NullValueHandling.Ignore,
                //}, ArrayPool<char>.Shared));
            }).AddJsonOptions(options => { options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; });

            services.Configure<AppSettings>(_config);

            AppSettings appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Events = new JwtBearerCustomEvents();

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,

                    ValidIssuer = "Nadmin",
                    ValidAudience = "api",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.JwtConfig.SecretKey)),

                    ValidateIssuerSigningKey = true,
                    /***********************************TokenValidationParameters的参数默认值***********************************/
                    // RequireSignedTokens = true,
                    // SaveSigninToken = false,
                    // ValidateActor = false,
                    // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
                    // ValidateAudience = true,
                    // ValidateIssuer = true, 
                    // ValidateIssuerSigningKey = false,
                    // 是否要求Token的Claims中必须包含Expires
                    // RequireExpirationTime = true,
                    // 允许的服务器时间偏移量
                    // ClockSkew = TimeSpan.FromSeconds(300),
                    // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    // ValidateLifetime = true
                };
            });


            #region Autofac DI

            var builder = new ContainerBuilder();
            //将services填充到Autofac容器生成器中
            builder.Populate(services);
            var assemblysServices = typeof(BaseService<>).Assembly;//直接采用加载文件的方法
            builder.RegisterAssemblyTypes(assemblysServices)
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();//同一个Lifetime生成的对象是同一个实例

            var container = builder.Build();
            Global.Instance.Init(container);//初始化Global对象
            return new AutofacServiceProvider(container);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(AllowAllRequests);
            }
            else
            {
                app.UseCors(LimitRequests);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            //使用NLog作为日志记录工具
            loggerFactory.AddNLog();
            //引入Nlog配置文件
            env.ConfigureNLog("Nlog.config");

            //应用身份验证
            app.UseAuthentication();//必须在UseMvc前调用

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();
        }
    }
}
