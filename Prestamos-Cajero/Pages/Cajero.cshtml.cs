using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using Microsoft.Extensions.Logging;
using Prestamos.Data;
using Prestamos.Core;

namespace Prestamos_Cajero.Pages
{
    public class CajeroModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICajeroRepository cajeroRepository;

        public List<Cajero> DatosCajero {get; set;}

        public CajeroModel(ILogger<IndexModel> logger, ICajeroRepository cajeroRepository)
        {

            _logger = logger;
            this.cajeroRepository = cajeroRepository;

        }


        




        public void OnGet()
        {
            this.DatosCajero = cajeroRepository.GetAll().ToList();
             
        }

        public void OnPost() 
        {
            
        }

    }
}
