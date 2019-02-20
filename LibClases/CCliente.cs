using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClases
{
    public class CCliente:CEntidad
    {
        //===============ATRIBUTOS ====================
        //---   Todos heredados de CEntidad        ----
        //=============================================
        //--------constructores -----------------------
        public CCliente() : base("TCliente")
        {
        }        
        //-------Implementacion de Metodos Abstractos--
        public override string[] NombresAtributos()
        {
            return new string[] { "CodCliente", "DNI", "Nombre","Direccion","Email","Telefono" };
        }
        public override int NroDeClaves()
        {
            return 1;///Falta mejores 
        }
        public bool Existe_DNI(string pDNI)
        {
            CConexion Conexion = new CConexion();
            string CadenaConsulta = "spu_BuscarDNI '" + pDNI + "'";
            Conexion.EjecutarSelect(CadenaConsulta);
            //si existen registros en la tabla 0 del dataset,lacalve primaria existe
            return (Conexion.Datos.Tables[0].Rows.Count > 0);
        }

    }
}
