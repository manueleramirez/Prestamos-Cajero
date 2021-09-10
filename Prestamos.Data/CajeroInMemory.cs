using Prestamos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prestamos.Data
{
    public class CajeroInMemory : ICajeroRepository
    {
        List<Cajero> Datos;
        public CajeroInMemory()
        {
            Datos = new List<Cajero>() {
                new Cajero(){BilletesMil = 9,BilletesQuiniento = 19, BilletesCien = 99 }
            };
        }


        public IEnumerable<Cajero> GetAll()
        {
           

            return Datos;
        }
    }
}
