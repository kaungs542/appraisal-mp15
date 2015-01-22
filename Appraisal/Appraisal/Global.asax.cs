using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;


namespace Appraisal
{
	public class Global : System.Web.HttpApplication
	{

        private const string DummyCacheItemKey = "GagaGuguGigi";

        protected void Application_Start(Object sender, EventArgs e)
        {
            RegisterCacheEntry();
        }

        private bool RegisterCacheEntry()
        {
            if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return false;

            HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", null,
                DateTime.MaxValue, TimeSpan.FromMinutes(1),
                CacheItemPriority.Normal,
                new CacheItemRemovedCallback(CacheItemRemovedCallback));

            return true;
        }

        public void CacheItemRemovedCallback(string key,
            object value, CacheItemRemovedReason reason)
        {
            Debug.WriteLine("Cache item callback: " + DateTime.Now.ToString());

            HitPage();

            // Do the service works

            DoWork();
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
	}
}