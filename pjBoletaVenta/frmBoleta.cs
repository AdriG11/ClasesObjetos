using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjBoletaVenta
{
    public partial class frmBoleta : Form
    {
        //Variables globales
        int num;
        Boleta objB = new();
        public frmBoleta()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

            num++;
            lblNumero.Text = num.ToString("S5");
            txtFecha.Text =DateTime.Now.ToShortDateString();
        }

        
        private void cboProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            objB.DescripciónProducto = cboProductos.Text;
            txtPrecio.Text = objB.determinaPrecio().ToString("C");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Valida() == "")
            {
                // capturar datos
                CapturaDatos();
                // determinar los calculos de la apalicacion
                double precio = objB.determinaPrecio();
                double importe = objB.calculaImporte();
                // imprimir oos detalles de la venta
                ImprimirDetalle();

                // imprimir el total acumulado
                lblTotal.Text = DeterminaTotal().ToString("C");
          
            }
            else
                MessageBox.Show($"El error se encuentra en {Valida()}");
                
                
        }

        private object DeterminaTotal()
        {
            double total = 0;
            for (int i = 0; i < lvDetalles.Items.Count; i++)
            {
                total += double.Parse (lvDetalles.Items[i].SubItems[3].Text)
            }
            return total;
        }

        private void ImprimirDetalle()
        {
            ListViewItem fila = new(objB.CantidadComprada.ToString);
            fila.SubItems.Add(objB.DescripciónProducto);
            fila.SubItems.Add(precio.toString("0.00"));
            fila.SubItems.Add(importe.toString("0.00"));
            lvDetalles.Items.Add(fila);
        }

        private void CapturaDatos()
        {
            objB.NumeroBoleta = int.Parse(lblNumero.Text);
            objB.NombreCliente = txtCliente.Text;
            objB.DirecciónCliente = txtDireccion.Text;
            objB.CédulaCliente = txtCedula.Text;
            objB.FechaRegistro = DateTime.Parse(txtFecha.Text);
            objB.DescripciónProducto = cboProductos.Text;
            objB.CantidadComprada = int.Parse(txtCantidad.Text);

        }

        private void valida()
        {
            if (txtCliente.Text.Trim().Length == 0)
            {
                txtCliente.Focus();
                return "nombre del cliente";

            }
            else if (txtDireccion.Text.Trim().Length == 0)
            {
                txtDireccion.Focus();
                return "direccion del cliente";
            }
            else if (txtCedula.Text.Trim().Length == 0)
            {
                txtDireccion.Focus();
                return "cedula del cliente";
            }
            else if (cboProductos.SelectedIndex == -1)
            {
                cboProductos.Focus();
                return "descripcion del producto";
            }
            else if (txtCantidad.Text.Trim().Length == 0)
            {
                txtCantidad.Focus();
                return "cantidad comprada";
            }
            else
                return "";
        }
        private void btnRegistrar_Click(object sender , EventArgs e)
        {
            ListViewItem fila = new ListViewItem(lblNumero.Text);
            fila.SubItems.Add(txtFecha.Text);
            fila.SubItems.Add(TotalCantidad().ToString());
            fila.SubItems.Add(DeterminaTotal().ToString());
            lvEstadistica.Items.Add(fila);
            LimpiarControles();

        }
        private void LimpiarControles()
        {
            num++;


        }
        // Total productos de boletas

        private void TotalCantidad()
        {
            int total = 0;
            for (int i = 0; i < lvEstadistica.Items.Count; i++)
            {
                total += int.Parse(lvDetalles.Items[i].SubItems[0].Text);
            }
            return total;

        }
    }
}
