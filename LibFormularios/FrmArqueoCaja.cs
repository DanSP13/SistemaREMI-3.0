using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LibClases;
namespace LibFormularios
{
    public partial class FrmArqueoCaja:Form
    {
        CArqueoCaja aArqueoCaja;
        public FrmArqueoCaja(string CodigoUsuario)
        {
            InitializeComponent();
            aArqueoCaja = new CArqueoCaja();
            TxtCodUsuario.Text = CodigoUsuario;
            TxtNroArqueo.Text = aArqueoCaja.UltimoArqueoCaja();
            FechaActual();
            DgvDetalleArqueoContado.DataSource = aArqueoCaja.CargarArqueoCajaContado(CodigoUsuario);
            DgvDetalleArqueoCredito.DataSource = aArqueoCaja.CargarArqueoCajaCredito(CodigoUsuario);
            Calcular();
        }
        //-----------------------------------------------------------
        public void FechaActual()
        {
            TxtFecha.Text = string.Format(DateTime.Now.ToString());
        }
        ///////------------Metodos----------
        public string[] AsignarValoresAtributos()
        {
            return new string[]{TxtNroArqueo.Text,TxtFecha.Text,TxtSolesInicio.Text
                ,TxtSolesFinal.Text,TxtCodUsuario.Text};
        }
        //---------------------------------------------------------------------
        //---------Mostrar los datos de un arqueo de caja ---------------------
        public void MostrarDatos()
        {
            TxtFecha.Text = aArqueoCaja.ValorAtributo("Fecha");
            TxtSolesInicio.Text = aArqueoCaja.ValorAtributo("SolesInicio");
            TxtSolesFinal.Text = aArqueoCaja.ValorAtributo("SolesFinal");
            TxtCodUsuario.Text = aArqueoCaja.ValorAtributo("CodUsuario");
        }
        //---------------------------------------------------------------------
        //---------Iniciar atributos clave y no clave -------------------------
        public void InicializarAtributoClave()
        {
            TxtNroArqueo.Text = "";
        }
        public void InicializarAtributosNoClave()
        {
            TxtFecha.Text = "";
            TxtSolesInicio.Text = "";
            TxtSolesFinal.Text = "";
            TxtCodUsuario.Text = "";
        }
        //-------------------------------------------
        //---Listar los registros y mostrarlos en el datagrid
        public  void ListaRegistros()
        {   //--Mostrar todos loslibros de la tabla en el grid
            //DgvDetalleArqueoContado.DataSource = aArqueoCaja.ListaGeneral();
        }
        //----------------------------------------
        //---Verificar los campos obligatorios(codigo y titulo)
        //---Esten llenos
        public bool EsRegistroValido()
        {
            if (TxtNroArqueo.Text.Trim() != "" && TxtCodUsuario.Text.Trim() != "" 
                && TxtSolesInicio.Text.Trim() != "" && TxtSolesFinal.Text.Trim() != "" && TxtFecha.Text.Trim() != "")
                return true;
            else
                return false;
        }
        public void ProcesarClave()
        {
            //-----Recuperar atributos,el primer atributo es clave 
            string[] Atributos = AsignarValoresAtributos();
            //-----Verificar si existe clave primaria 
            if (aArqueoCaja.ExisteClavePrimaria(Atributos))
            {   //--------Registro existente,Recuperar Atributos y mostrarlos
                MostrarDatos();
                aArqueoCaja.Nuevo = false;
            }
            else
            {   //----Registro nuevo, inicializar atributos no clave
                InicializarAtributosNoClave();
            }
        }
        //=========================EVENTOS ==================
        private void TxtCodProducto_Leave(object sender, EventArgs e)
        {
            ProcesarClave();
        }
        private void FrmProducto_Load(object sender, EventArgs e)
        {
            ListaRegistros();
        }
        /// <summary>
        /// Metodo que bloquea y desbloquea  los controles del formulario 
        /// </summary>
        /// <param name="Tipo"></param>false si es para bloquear y true para desbloquear
        public void BloqueoFormulario(Boolean Tipo)
        {
            
        }
        private void BtnBuscarDoc_Click(object sender, EventArgs e)
        {
            if (aArqueoCaja.ExisteClavePrimaria(TxtNroArqueo.Text))
            {
                DgvDetalleArqueoContado.Columns.Clear();
                BloqueoFormulario(false);
                ProcesarClave();
            }
            else
            {
                MessageBox.Show("No Existe Documento Venta");
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {

        }
        public  void Grabar()
        {
            try
            {
                if (EsRegistroValido())
                {   //-----Recuperar atributos,el primer atributo es la clave
                    string[] Atributos = AsignarValoresAtributos();
                    //----Verificar si existe clave primaria
                    aArqueoCaja.Insertar(Atributos);
                    if (aArqueoCaja.ValorAtributo("CodError") == "0")
                    {
                        //---Inicializar el formulario
                        MessageBox.Show(aArqueoCaja.ValorAtributo("Mensaje"), "CONFIRMACION");
                        //Limpiar el formulario
                        LimpiarTodo();
                        //Limpiar el datagridview
                        DgvDetalleArqueoContado.Rows.Clear();
                        DgvDetalleArqueoCredito.Rows.Clear();
                        TxtNroArqueo.Text = aArqueoCaja.UltimoArqueoCaja();
                    }
                    else
                        MessageBox.Show(aArqueoCaja.ValorAtributo("Mensaje"), "ERROR");
                }
                else MessageBox.Show("DEBE COMPLETAR EL LLENADO DEL FORMULARIO", "ALERTA");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR AL REALIZAR LA OPERACION");
            }
        }

        private void LimpiarTodo()
        {
            DgvDetalleArqueoContado.DataSource = null;
            DgvDetalleArqueoCredito.DataSource = null;
            txtTotal.Text = "";
            TxtSolesInicio.Text = "";
            TxtSolesFinal.Text = "";
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Grabar();
        }
        public void Calcular()
        {
            double Total = 0;
            for (int k = 0; k < DgvDetalleArqueoContado.RowCount; k++)
            {
                double subtotal = double.Parse(DgvDetalleArqueoContado["Total", k].Value.ToString());
                Total += subtotal;
            }
            double Total2 = 0;
            for (int k = 0; k < DgvDetalleArqueoCredito.RowCount; k++)
            {
                double subtotal = double.Parse(DgvDetalleArqueoCredito["Total", k].Value.ToString());
                Total += subtotal;
            }
            txtTotal.Text = Convert.ToString(Total + Total2);
        }
    }
}
