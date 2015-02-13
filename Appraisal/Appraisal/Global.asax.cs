using Appraisal.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mail;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;


namespace Appraisal
{
	public class Global : System.Web.HttpApplication
	{

        private const string DummyCacheItemKey = "GagaGuguGigi";

        private System.ComponentModel.IContainer components = null;

        public Global()
           {
               InitializeComponent();
           }

        protected void Application_Start(Object sender, EventArgs e)
        {
            RegisterCacheEntry();
        }

        private void RegisterCacheEntry()
        {
            if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return;

            HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", null,
                DateTime.MaxValue, TimeSpan.FromMinutes(1),
                CacheItemPriority.NotRemovable,
                new CacheItemRemovedCallback(CacheItemRemovedCallback));

            //return true;
        }

        public void CacheItemRemovedCallback(string key,
            object value, CacheItemRemovedReason reason)
        {
            Debug.WriteLine("Cache item callback: " + DateTime.Now.ToString());
            DoWork();
            

            DoWork1();
            HitPage();
        }

        private const string DummyPageUrl =
    "http://localhost/appraisal/WebForm1.aspx";

        private void HitPage()
        {
            WebClient client = new WebClient();
            client.DownloadData(DummyPageUrl);
        }

        private void DoWork()
        {
            Debug.WriteLine("Begin DoWork...");
            Debug.WriteLine("Running as: " +
                  WindowsIdentity.GetCurrent().Name);

            DoSomeFileWritingStuff();
            
            Debug.WriteLine("End DoWork...");
        }
            

        private void DoWork1()
        {
            Systemtime st = dbmanager.GetSystemTime();
            DateTime today = DateTime.Today;
            int result = 0;
            if (st != null)
            {
                TimeSpan span = st.Enddate.Subtract(today);
                //result = (int)span.Days;

                result = 2;
                if ((result == 2) || (result == 7))
                {
                    dbmanager.SendEmailToNotCompleted();
                }
            }
        }

        private void DoSomeFileWritingStuff()
        {
            Debug.WriteLine("Writing to file...");

            try
            {
                using (StreamWriter writer =
                 new StreamWriter(@"c:\a\Cachecallback.txt", true))
                {
                    writer.WriteLine("Cache Callback: {0}", DateTime.Now);
                    writer.Close();
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x);
            }

            Debug.WriteLine("File write successful");
        }
		
		protected void Session_Start(object sender, EventArgs e) 
		{

		}

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            // If the dummy page is hit, then it means we want to add another item

            // in cache

            if (HttpContext.Current.Request.Url.ToString() == DummyPageUrl)
            {
                // Add the item in cache and when succesful, do the work.

                RegisterCacheEntry();
            }
        }

		protected void Application_AuthenticateRequest(object sender, EventArgs e) 
		{

		}

		protected void Application_Error(object sender, EventArgs e) 
		{

		}

		protected void Session_End(object sender, EventArgs e) 
		{

		}

		protected void Application_End(object sender, EventArgs e) 
		{

        }

        private void InitializeComponent()
          {
              this.components = new System.ComponentModel.Container();
          }

    }
}