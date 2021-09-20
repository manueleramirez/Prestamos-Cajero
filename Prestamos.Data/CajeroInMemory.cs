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

            double retiroAcumula = Datos[0].RetiroAcumulado;

            if (retiroAcumula == 10000 && banco == "BancoABC" && Datos[0].RetiroAcumulado > 0)
            {
                return "El Limite Para su banco es de RD$10000 por dia";
            }

            else if (montoRetiro > 2000 && banco == "BancoABC")
            {
                return "El Limite Para su banco es de RD$2000 por transaccion";
            }

            else  if (montoRetiro <= 2000 && banco == "BancoABC")
            {
                Calcular(montoRetiro);
                Datos[0].RetiroAcumulado = Datos[0].RetiroAcumulado + montoRetiro;
                return "Retiro Satisfactorio";
            }
           
            else if (montoRetiro >= 100 && banco != "BancoABC" )
            {
                Calcular(montoRetiro);
                return "Retiro Satisfactorio";
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
            Datos[0].RetiroAcumulado = 0;



        }

        public void Calcular(double montoRetiro) 
        {
            double residuo = montoRetiro;
            int cantidadMil = 0;
            int cantidadQuinientos = 0;
            int cantidadCien = 0;


            do
            {
                if (residuo >= 1000 && Total() > montoRetiro && cantidadMil <= Datos[0].BilletesMil-1)
                {
                    cantidadMil++;
                    residuo -= 1000;
                }


                else if (residuo >= 500 && Total() > montoRetiro && cantidadQuinientos <= Datos[0].BilletesQuiniento-1)
                {
                    cantidadQuinientos++;
                    residuo -= 500;
                }

                else if (residuo >= 100 && Total() > montoRetiro && cantidadCien <= Datos[0].BilletesCien-1 )
                {
                    cantidadCien++;
                    residuo -= 100;
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
