using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
namespace Mvc.Models
{
    public static class globalvariables
    {
        public static HttpClient webapiclent = new HttpClient();
         static globalvariables()
        {
            webapiclent.BaseAddress = new Uri("https://localhost:44371//api");
            webapiclent.DefaultRequestHeaders.Clear();
            webapiclent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("/appliction/json"));

        }

    }
}