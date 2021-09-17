using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prestamos.Core;

namespace Prestamos.Data
{
    public interface ICajeroRepository
    {
        public IEnumerable<Cajero> GetAll();
        public string Retirar(double montoRetiro,string banco);
        public void Recargar();
        public double Total();


    }
}
