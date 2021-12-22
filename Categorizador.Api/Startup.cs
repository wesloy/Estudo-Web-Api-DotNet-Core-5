
using System;
using System.Text;
using Categorizador.Application.Interfaces;
using Categorizador.Application.Services;
using Categorizador.Persistence.Contexts;
using Categorizador.Persistence.Interfaces;
using Categorizador.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Categorizador.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(IConfiguration configuration, object configurations) 
        {
            this.Configuration = configuration;
            this.Configurations = configurations;               
        }
        public IConfiguration Configuration { get; }
        public object Configurations { get; private set; }

        // Este método é chamado pelo tempo de execução. Use este método para adicionar serviços ao contêiner
        public void ConfigureServices(IServiceCollection services)
        {
            
            //String de conexão herdada de appsetings. O método configuration invoca todas as ações de configuração.
            services.AddDbContext<CategorizadorContext>(
                context => context.UseSqlServer(
                    Configuration.GetConnectionString("Sentinella")
                )                
            );

            //Cortar looping infinito entre tabelas que relacionam entre si
            services.AddControllers()
                    .AddNewtonsoftJson(
                        x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            //capturando os mapiamentos da pasta Maapgins
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Referencias das Interfaces das Controllers
            //Geral
            services.AddScoped<IGeralRepository, GeralRepository>();

            //Grupos
            services.AddScoped<IGrupoService, GrupoService>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
                        
            //Filas
            services.AddScoped<IFilaService, FilaService>();
            services.AddScoped<IFilaRepository, FilaRepository>();

            //Finalizações
            services.AddScoped<IFinalizacaoService, FinalizacaoService>();
            services.AddScoped<IFinalizacaoRepository, FinalizacaoRepository>();          

            //SubFinalizações
            services.AddScoped<ISubFinalizacaoService, SubFinalizacaoService>();
            services.AddScoped<ISubFinalizacaoRepository, SubFinalizacaoRepository>();  

            //Configuração Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Categorizador.Api", Version = "v1" });
            });


            // Auth JWT
            // Authentication Bearer JWT            
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);

            services
                .AddAuthentication(auth => {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => {

                    options.RequireHttpsMetadata = false; //visto que api de autenticação não está HTTPS

                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    
                });

        }

        // Este método é chamado pelo tempo de execução. Use este método para configurar o pipeline de solicitação HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Categorizador.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //vai solicitar autenticacao para acessar a api
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
