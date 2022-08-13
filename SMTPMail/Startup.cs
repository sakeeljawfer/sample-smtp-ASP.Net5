using System;
using SMTPMail.Utils;

namespace SMTPMail
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
            services.AddControllersWithViews();

            //---Gmail
            var from = Configuration.GetSection("Email")["From"];

            var gmailSender = Configuration.GetSection("Gmail")["Sender"];
            var gmailPassword = Configuration.GetSection("Gmail")["Password"];
            var gmailPort = Convert.ToInt32(Configuration.GetSection("Gmail")["Port"]);

            //---Sendgrid
            var sendGridSender = Configuration.GetSection("Sendgrid")["Sender"];
            var sendGridKey = Configuration.GetSection("Sendgrid")["SendgridKey"];

            //--Uncomment to use Gmail
            //services
            //    .AddFluentEmail(gmailSender, from)
            //    .AddRazorRenderer()
            //    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
            //    {
            //        UseDefaultCredentials = false,
            //        Port = gmailPort,
            //        Credentials = new NetworkCredential(gmailSender, gmailPassword),
            //        EnableSsl = true,
            //    });

            services
                .AddFluentEmail(sendGridSender, from)
                .AddRazorRenderer()
                .AddSendGridSender(sendGridKey);

            services.AddScoped<IMailSender, MailSender>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}

