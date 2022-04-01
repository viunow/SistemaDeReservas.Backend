using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SistemaDeReservas.Aplicacao.Servicos;
using SistemaDeReservas.Infra;
using SistemaDeReservas.Infra.Repositorios;
using System.Text.Json.Serialization;

namespace SistemaDeReservas.API
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
            services.AddScoped<HospedeService>();
            services.AddScoped<QuartoService>();
            services.AddScoped<ReservaService>();

            services.AddScoped<HospedeRepositorio>();
            services.AddScoped<QuartoRepositorio>();
            services.AddScoped<ReservaRepositorio>();

            services.AddSingleton<DbAccess>();

            //permitir as requisicoes que serao feitas pela aplicacao Angular do endereco localhost:4200
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());
            });
            
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
