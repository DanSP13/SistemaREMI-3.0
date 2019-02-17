using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClases
{
    public class CProducto : CEntidad
    {
        //===============ATRIBUTOS ====================
        //---   Todos heredados de CEntidad        ----
        //=============================================
        //--------constructores -----------------------
        public CProducto() : base("TProducto")
        {
        }
        //-------Implementacion de Metodos Abstractos--
        public override string[] NombresAtributos()
        {
            return new string[] { "CodProducto", "Producto", "Descripcion", "Marca", "Modelo", "Serie", "Stock","PrecioUnitario" };
        }
        public override int NroDeClaves()
        {
            return 1;///Falta mejores 
        }
    }
}

