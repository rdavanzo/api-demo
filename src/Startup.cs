using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonApiDotNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ApiDemo.Models;
using ApiDemo.Persistence;

namespace ApiDemo
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
            // Todo: add to config file...
            var connection = @"Data Source=(LocalDB)\v11.0;;Database=ApiDemo;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<DefaultDbContext>(
                options => options.UseSqlServer(connection), 
                ServiceLifetime.Transient);
            services.AddJsonApi<DefaultDbContext>();
            services.AddMvc();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DefaultDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            seedTheDatabase(context);

            app.UseJsonApi();
        }

        private void seedTheDatabase(DefaultDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Books.Any())
            {
                var book = new Book("The Silmarilion", "0-04-823139-8", new DateTime(1977, 9, 15));
                book.Author = new Author("J.R.R.", "Tolkien");
                book.Reviews.Add(new Review("aragorn", "it was pretty good..."));
                book.Reviews.Add(new Review("frodo", "the what???"));

                context.Books.Add(book);
                context.SaveChanges();
            }
        }
    }
}
