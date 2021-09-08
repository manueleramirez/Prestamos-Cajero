using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Prestamos_Cajero.Pages
{
    public class CajeroModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CajeroModel(IHttpContextAccessor httpContextAccessor)
        {


            _httpContextAccessor = httpContextAccessor;

        }



        //public void Write(string key, string value, bool isPersistent) 
        //{
        //    CookieOptions options = new CookieOptions();

        //    if (isPersistent) 
        //    {
        //        options.Expires = DateTime.Now.AddDays(1);
        //    }
        //    else 
        //    {
        //        options.Expires = DateTime.Now.AddSeconds(10);
        //        _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, options);
        //    }

        //}

        public void RecargarCajero() 
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            const string billetesMil = "9";
            const string billetesQuiniento = "19";
            const string billetesCien = "99";
            
            _httpContextAccessor.HttpContext.Response.Cookies.Append("billetesMil", billetesMil, options);

            //Write("billetesMil", billetesMil, true);
            //Write("billetesQuiniento", billetesQuiniento, true);
            //Write("billetesCien", billetesCien, true);
        }


        public string Read(string Key) 
        {

            string v = _httpContextAccessor.HttpContext.Request.Cookies[Key];

            return v;
        
        }

        public void OnGet()
        {

        }

        public void OnPost(string recargar) 
        {
            if(recargar == "True") 
            {
                RecargarCajero();
            
            };
        }

    }
}
