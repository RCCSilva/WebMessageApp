using Confluent.Kafka;
using Gateway.Consumers;
using Gateway.Hubs;
using Gateway.Services;
using KafkaLibrary;
using KafkaLibrary.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });

            services.AddSingleton<GuidService>();
            
            services.AddSingleton<KafkaClientHandle>();
            services.AddSingleton<UserConnectionService>();
            

            services.AddSingleton<ReceiveMessageService>();

            services.AddScoped<KafkaProducer<Null, SessionCreate>>();
            services.AddScoped<KafkaProducer<Null, ChatMessage>>();

            services.AddScoped<SessionService>();
            services.AddScoped<MessageService>();

            services.AddHostedService<SessionReplyConsumer>();
            services.AddHostedService<ReceiveMessageConsumer>();
            

            services.AddControllers();
            services.AddSignalR();
            services.AddSwaggerGen(
                c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Gateway", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway v1"));
                app.UseCors("ClientPermission");
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}