using Insql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InSqlDemo.Models;

namespace InSqlDemo
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
            services.AddInsql();  //��ʹ��Ĭ������

            //��AuthDbContext��ӵ�����ע�������У�Ĭ����������ΪScoped��һ��WEB���󽫴���һ��DbContext����һ��DbContext����Ҳ������һ�����ݿ�����
            services.AddDbContext<AuthDbContext>(options =>
            {
                //options.UseSqlServer(this.Configuration.GetConnectionString("sqlserver"));
                options.UseSqlite(this.Configuration.GetConnectionString("sqlite"));
                //options.UseMySql(this.Configuration.GetConnectionString("MySql"));
            });

            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
