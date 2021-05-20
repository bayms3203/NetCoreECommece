using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestMVCApp.libs.API;
using TestMVCApp.libs.Application;
using TestMVCApp.libs.Data.Contexts;
using TestMVCApp.libs.Data.Repositories;
using TestMVCApp.libs.Domain;
using TestMVCApp.libs.Sesion;

namespace TestMVCApp
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
            services.AddControllersWithViews().AddJsonOptions(options => {
                     options.JsonSerializerOptions.PropertyNamingPolicy = null; 
                     options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                     options.JsonSerializerOptions.IgnoreNullValues = false;
            
            });

      

      

            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseNpgsql(Configuration.GetConnectionString("TestContext"));
            });

            services.AddDbContext<ApplicationIdentityDbContext>(opt => {
                opt.UseNpgsql(Configuration.GetConnectionString("TestContext"));
            });

            // identity özelliğini kullanmak için sisteme bu servisi tanıtmamız gerekiyor.
            services.AddIdentity<IdentityUser,IdentityRole>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();

            // aşağıdaki bu servis ile authentication cookie ayarlarını yapmış oluruz.
             services.AddAuthentication("CookieAuthentication")  
                 .AddCookie("CookieAuthentication", config =>  
                 {  
                     config.Cookie.Name = "LoginCookie";  
                     config.LoginPath = "/Authentication/Login";  
                     config.Cookie.Expiration = TimeSpan.FromDays(30); // 1 aylık cookie oluştur.
                 });  
  

    
            // Net Core uygulamasında Session default olarak yoktur. Bunu sisteme ekleyip ve tanıtmamız gerekir.

            services.AddSession(); // session özelliğini net core projesine eklemek için kullandığımız servis.
            services.AddMemoryCache(); // session bilgilerini ramde saklamak için kullanılan servis

            // Repoistories => Servis bağımlıklarını burada tanımamamız lazım. Biz uygulamamızda interfaceler üzerinden servisleri consume yani tüketiceğiz. bu sebep ile dependency inversion ile sınıflar arası bağımlılıkları soyut sınıflara bıraktık ve azaltık. uygulamamız loose couple yani zayıf bağlılık prensibine göre geliştiriliyor. 
            // bu sebep ile bu interfaclerin hangi sınıfların instance uygulama genelinde yöneteceğini bu startp dosyasında tanıtmalıyız.


            // using Microsoft.Extensions.DependencyInjection; paketi ile IOC container yapısını NetCore üstlenir.
            // tek yerden uygulama genelinde instance yönetimi yapma olayına IOC (Inversion Of Control) diyoruz. 

            // Repository servislerimiz ve database ile işkili servislerimiz scoped tanımlıyoruz
            // sms, email, notification tarzı altyapı servislerimiz singleton tercih ederiz.
            // her seferinde instance alınması gereken session, validation gibi servislerimiz ise transient olarak tercih ederiz.
            services.AddTransient<ICartSessionService, CartSessionService>(); // classdan her seferinde instance alır
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICartService, CartService>();
            services.AddScoped<IOrderRepository, OrderRepository>(); // classdan web istek request bazlı instance alır
            services.AddScoped<IProductRepository, ProductRepository>();
             services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderService, OrderService>(); // ef core ile db işlemleri için ef core bu instance yöntemini  öneriyor
            services.AddSingleton<IEmailService, EmailService>(); // classdan tek bir instance alır.

   
    
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<ICardStore, CardStore>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // authetication servisini kullan
            // bu servis UseRouting ile UseAuthorization arasına alınmalıdır. yoksa hata alırız.

            app.UseAuthorization();

            app.UseSession(); // uygulamaya genelinde sessiondan verileri okuyabilmemizi sağlayan ara yazılım. yani middleware.
        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
