using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Prestamos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prestamos_Cajero.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICajeroRepository cajeroRepository;

        public IndexModel(ILogger<IndexModel> logger, ICajeroRepository cajeroRepository)
        {
            _logger = logger;
            this.cajeroRepository = cajeroRepository;
        }
        

        public decimal CalcMensualidad(decimal monto,decimal porcentaje,int cuotas) 
        {
            decimal resultado;
            
            decimal porcentajeMensual = (porcentaje / 100)/12 ;
            resultado =  monto*(porcentajeMensual/Convert.ToDecimal((1-Math.Pow((1 + (double)porcentajeMensual),(-(cuotas))))));

            return resultado;
        
        }

        public decimal CalcAmortizacionCap(decimal montoCuota, decimal interes) => montoCuota - interes;
        public decimal CalcInteres(decimal porcentaje, decimal capitalPend ) => ((porcentaje/100) / 12) * capitalPend;
        public decimal CalcCapPendiente(decimal AmortizacionCap, decimal capitalPendAnt) => capitalPendAnt - AmortizacionCap;
        public decimal CalcCapAmortizado(decimal AmortizacionCap, decimal CapAmortizadoAnt) => AmortizacionCap + CapAmortizadoAnt;
        



        public List<TablaAmortizacion> tabla = new List<TablaAmortizacion>();


        public void OnGet(decimal Monto,decimal Porcentaje,int Cuotas)
        {

            

            for (int i = 0; i <= Cuotas; i++)
            {
                
                if(i == 0)
                {
                   
                    tabla.Add(new TablaAmortizacion { NoCuota = i, MontoCuota = i, CapPendiente = Monto}); 
                }
                else
                {
                    decimal montoCuota = CalcMensualidad(Monto, Porcentaje, Cuotas);
                    decimal interes = CalcInteres(Porcentaje, tabla[i-1].CapPendiente);
                    decimal capital = CalcAmortizacionCap(montoCuota, interes);
                    decimal capAmotizado = CalcCapAmortizado(capital,tabla[i-1].CapAmortizado);
                    decimal capPendiente = CalcCapPendiente(capital,tabla[i-1].CapPendiente);

                    


                    tabla.Add(new TablaAmortizacion { NoCuota = i, MontoCuota = montoCuota,Capital = capital,Interes = interes , CapPendiente = capPendiente, CapAmortizado = capAmotizado });
                }
                

            }
        }
    }

    public class TablaAmortizacion
    {
        public int NoCuota { get; set; }
        public decimal MontoCuota { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal CapAmortizado { get; set; }
        public decimal CapPendiente { get; set; }

    }

}
