using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using CatProjService.DataObjects;
using CatProjService.Models;
using Owin;

namespace CatProjService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new CatProjInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }

    public class CatProjInitializer : DropCreateDatabaseIfModelChanges<CatProjContext>
    {
        protected override void Seed(CatProjContext context)
        {

            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false, OS = "SEED" },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false, OS = "SEED" },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            base.Seed(context);

            List<CatItem> catItems = new List<CatItem>
            {
                new CatItem { Id = "1", Name = "Books About Cats", OS = "SEED" },
                new CatItem { Id = "2", Name = "Movies About Dogs", OS = "SEED" },
            };

            foreach (CatItem catItem in catItems)
            {
                context.Set<CatItem>().Add(catItem);
            }

            base.Seed(context);

            List<EleItem> eleItems = new List<EleItem>
            {
                new EleItem { Id = "1", CatId = "1", Name = "The Cat in the Hat", OS = "SEED" },
                new EleItem { Id = "2", CatId = "1", Name = "A Street Cat Named Bob", OS = "SEED" },
                new EleItem { Id = "3", CatId = "2", Name = "A Dogs Purpose", OS = "SEED" },
                new EleItem { Id = "4", CatId = "2", Name = "Show Dogs", OS = "SEED" },
            };

            foreach (EleItem eleItem in eleItems)
            {
                context.Set<EleItem>().Add(eleItem);
            }

            base.Seed(context);

        }
    }
}

