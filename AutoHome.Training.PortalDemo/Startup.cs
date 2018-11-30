using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoHome.Training.Repository;
using AutoHome.Training.Core.Events;
using AutoHome.Training.RabbitMq;
using Microsoft.Extensions.Logging;
using EasyNetQ;
using Microsoft.Extensions.Options;
using AutoHome.Training.PortalDemo.Events;
using AutoHome.Training.PortalDemo.EventHandlers;

namespace AutoHome.Training.PortalDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseSQLDapper(Configuration.GetSection("Connection").Value);
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //ע��RabbitMQ��EasyNetQ,ֱ���ڹ��캯����ʹ��IBus����
            services.AddSingleton<IBus>(service =>
            {
                var connection = Configuration.GetSection("RabbitMQConnection").Value;


                return RabbitHutch.CreateBus(connection);
            });
            //ע��EventBus
            services.AddSingleton<IEventExecutContext, EventExecuteContext>(sp=> {
                return new EventExecuteContext(sp);
            });
            services.AddSingleton<Core.Events.IEventBus, RabbitMqEventBus>(
                provider => new RabbitMqEventBus(
                    provider.GetService<IEventExecutContext>(),
                    provider.GetService<ILogger<RabbitMqEventBus>>(),
                     provider.GetService<IBus>()
                    )
                );
            //ִ�ж���
            ConfigureEventBus(services.BuildServiceProvider());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();


        }
        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureEventBus(IServiceProvider sp)
        {
            var eventBus = sp.GetService<Core.Events.IEventBus>();
            eventBus.Subscribe<OrderConfirmedEventData, OrderConfirmedEventHandler>();
        }
    }
}
