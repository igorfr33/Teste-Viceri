using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TesteViceri_Herois.Data;

namespace TesteViceri_Herois
{
    public class Startup
    {
        #region Propriedades Públicas
        public IConfiguration Configuration { get; }
        #endregion

        #region Construtores
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Métodos/Operadores Públicos
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Configurações do Cors
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            #endregion

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("/swagger/v1/swagger.json", "TesteViceri"); });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });



        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Habilitar o Cors
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:4200");
                });
            });
            #endregion

            #region Conexão com o Banco de Dados
            var mySqlConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
            #endregion

            services.AddControllers();

            services.AddRazorPages();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "TesteViceri", Version = "v1", }); });

            services.AddCors();
        }
        #endregion
    }
}
