using System.Net;
using System.Net.Http;
using System.Data;
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
        private readonly ILogger<CajeroModel> _logger;
        private readonly ICajeroRepository cajeroRepository;

        public List<Cajero> DatosCajero {get; set;}
        

        public CajeroModel(ILogger<CajeroModel> logger, ICajeroRepository cajeroRepository)
        {

            _logger = logger;
            this.cajeroRepository = cajeroRepository;

        }

        

            
        
        public double TotalCajero()
        {
                double total = cajeroRepository.Total();
                return total;
        }



        public IActionResult OnGet(string banco, double monto,bool recargar,bool retirar)
        {
            this.DatosCajero = cajeroRepository.GetAll().ToList();

            if (retirar)
            {
                @ViewData["Respuesta"] = cajeroRepository.Retirar(monto,banco);
                ViewData["cerrar"] = "d-flex";
            }
           

            if (recargar)
            {
                cajeroRepository.Recargar();
            }

            
            return Page();
             
        }

        public IActionResult Cerrar(bool cerrar) 
        {
            if (cerrar) 
            {
                ViewData["cerrar"] = "d-none";
            }

            return Page();
        
        }

        

        // public IActionResult  OnPost() 
        // {

            
            
        // }

        
    }
}
