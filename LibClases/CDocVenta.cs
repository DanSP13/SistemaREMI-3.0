using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClases
{
    public class CDocVenta:CEntidad
    {
        //===============ATRIBUTOS ====================
        //---   Todos heredados de CEntidad        ----
        //=============================================
        //--------constructores -----------------------
        public CDocVenta() : base("TDocVenta")
        {
        }
        //-------Implementacion de Metodos Abstractos--
        public override string[] NombresAtributos()
        {
            return new string[] { "NroDocVenta", "Fecha", "Tipo", "TipoPago", "CodCliente", "CodUsuario" };
        }
        public override int NroDeClaves()
        {
            return 1;///Falta mejores 
        }

        public string UltimoNroDocVenta(string Tipo)
        {
            CConexion C = new CConexion();
            string Consulta = "exec spu_UltimoNroDocVenta '" + Tipo + "'";
            C.EjecutarSelect(Consulta);
            return C.Datos.Tables[0].Rows[0]["Nro"].ToString();
        }
    }
}
