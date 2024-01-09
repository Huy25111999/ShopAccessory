using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Website.Controllers;
using System.Data;
using System.Data.SqlClient;
using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.Data.SqlClient;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using ShopAccessory.Models;

namespace ShopAccessory.Controllers
{
    public class ContactUsController : SurfaceController
    {
        /*
           public IActionResult Index()
           {
               return View();
           }
        */
        public ContactUsController(IUmbracoContextAccessor umbracoContextAccessor,
                                  IUmbracoDatabaseFactory umbracoDatabaseFactory,
                                  ServiceContext serviceContext,
                                  AppCaches appCaches,
                                  IProfilingLogger profilingLogger,
                                  IPublishedUrlProvider publishedUrlProvider)
          : base(umbracoContextAccessor, umbracoDatabaseFactory, serviceContext, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        [HttpPost]
        public IActionResult SubmitForm(RequestContact model)
        {
            try
            {
                Console.Write("abc");
                SaveFormDataToDatabase(model);
                return RedirectToCurrentUmbracoPage();
            }
            catch (Exception ex)
            {
                return CurrentUmbracoPage();
            }
        }

        public void SaveFormDataToDatabase(RequestContact model)
        {
            string connect = "";
            using (SqlConnection cn = new SqlConnection(connect))
            {
                cn.Open();
                string query = "INSERT INTO ContactUs (Name, Email, Message) VALUES (@Name, @Email, @Message)";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Message", model.Message);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
