using System.Collections.Generic;
using System.Linq;
using DocumentUploader.Core.Interfaces;
using DocumentUploader.Core.Services;
using DocumentUploader.Infrastructure.Data.Contexts;
using DocumentUploader.Infrastructure.Data.Repositories;
using DocumentUploader.Infrastructure.Data.Repositories.Interfaces.Infrastructure;
using DocumentUploader.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DocumentUploader
{
    //dotnet ef migrations add InitialCreate
    //ef migrations remove
    //dotnet ef database update
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<FormInputValidator>();
                    x.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    o.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                }); 
       
            services.AddDbContext<FileUploaderDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("DocumentUploader")));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("document-uploader",
                    new Info
                    {
                        Title = "Document uploader API",
                        Version = "v1",
                        Description = "Document uploader API"
                    });
                c.DescribeAllEnumsAsStrings();
            });
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/document-uploader/swagger.json", "Document uploader API");
                c.RoutePrefix = "go";
            });
        }
    }
}
