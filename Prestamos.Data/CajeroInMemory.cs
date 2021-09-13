using Prestamos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prestamos.Data;

namespace Prestamos.Data
{
    public class CajeroInMemory : ICajeroRepository
    {
        List<Cajero> Datos;
        public CajeroInMemory()
        {
            Datos = new List<Cajero>() {
                new Cajero(){BilletesMil = 9,BilletesQuiniento = 19, BilletesCien = 99, RetiroAcumulado = 0 }
            };
        }

        public string Retirar(double montoRetiro, string banco)
        {

            

            if (montoRetiro <= 2000 && banco == "BancoABC")
            {
                calcular(montoRetiro);
                return "Retiro Satisfactorio";
                Datos[0].RetiroAcumulado = Datos[0].RetiroAcumulado + 1;
             }
            else if (montoRetiro > 2000 && banco == "BancoABC")
            {
                return "El Limite Para su banco es de RD$2000 por transaccion";
            }
            else if (montoRetiro > 100)
            {
                calcular(montoRetiro);
                return "El Limite Para su banco es de RD$2000 por transaccion";
            }

            else
            {
                
                return "No se pudo realizar su retiro vuelva a intentar";
               
            }
            

            
        }

        public void Recargar()
        {
            Datos[0].BilletesMil = 9; 
            Datos[0].BilletesQuiniento = 19;
            Datos[0].BilletesCien = 99;

        }

        public void calcular(double montoRetiro) 
        {
            //double resultadoRet = 0;
            double residuo = montoRetiro;
            int cantidadMil = 0;
            int cantidadQuinientos = 0;
            int cantidadCien = 0;



            do
            {
                if (residuo >= 1000 && Total() > montoRetiro && Datos[0].BilletesMil > 0)
                {
                    cantidadMil = cantidadMil + 1;
                    //resultadoRet = Total()-montoRetiro;
                    residuo = residuo - 1000;
                }


                else if (residuo >= 500 && Total() > montoRetiro && Datos[0].BilletesQuiniento > 0)
                {
                    cantidadQuinientos = cantidadQuinientos + 1;
                    //resultadoRet = Total()-montoRetiro;
                    residuo = residuo - 500;
                }

                else if (residuo >= 100 && Total() > montoRetiro && Datos[0].BilletesCien > 0)
                {
                    cantidadCien = cantidadCien + 1;
                    //resultadoRet = Total()-montoRetiro;
                    residuo = residuo - 100;
                }





            }
            while (residuo >= 100);

            if (residuo == 0)
            {
                Datos[0].BilletesMil = Datos[0].BilletesMil - cantidadMil;
                Datos[0].BilletesQuiniento = Datos[0].BilletesQuiniento - cantidadQuinientos;
                Datos[0].BilletesCien = Datos[0].BilletesCien - cantidadCien;
            }

        }




        public IEnumerable<Cajero> GetAll()
        {

            return Datos;
        }

        public double Total()
        {

            double resultado = (Datos[0].BilletesMil*1000)+(Datos[0].BilletesQuiniento*500)+(Datos[0].BilletesCien*100);
            return resultado;
        }
    }
}
