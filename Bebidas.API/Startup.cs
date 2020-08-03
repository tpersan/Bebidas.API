using System.IO;
using API.Infraestrutura.Base;
using API.Infraestrutura.Base.BancoDeDados;
using API.Infraestrutura.Base.Contexto;
using Autofac;
using Autofac.Core;
using Bebidas.Implementacao.BD;
using Bebidas.Implementacao.Repositorio.Inclusao;
using Bebidas.Implementacao.Servico;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Bebidas.API
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
            var configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddEnvironmentVariables()
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .Build();

            Recurso.DefinirOrigemDoRecurso(new RecursoDeConfiguracao(configuration));

            services.AddHttpContextAccessor();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Bebidas.API", Version = "v1" });

                c.DescribeAllParametersInCamelCase();
                c.DescribeStringEnumsInCamelCase();

                var caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                var nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                var caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });

            services.AddTransient<IContexto, ContextoRequest>();

            services.AddSingleton<IBancoDeDados, BancoDeDados>();
            services.AddSingleton<BancoDeDadosConexao>();

            services.AddScoped<ICervejaServico, CervejaServico>();
            services.AddScoped<IInclusaoCerveja, InclusaoCerveja>();

            services.AddMvcCore().AddApiExplorer();
            services.AddApplicationInsightsTelemetry(Configuration["ea501d85-6435-48ff-9a2e-10a66fa39fd2"]);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bebidas.API");
            });

            app.UseMvc();

        }
    }
}
