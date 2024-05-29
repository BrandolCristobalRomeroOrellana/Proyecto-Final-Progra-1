using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.AllUserContorls
{
    public partial class UC_PlaceOrder : UserControl
    {
        function fn = new function();
        String query;


        public UC_PlaceOrder()
        {
            InitializeComponent();
        }

        private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            String category = comboCategory.Text;
            query = "Select itemname from items where category = '"+category+"'";
            DataSet ds = fn.getData(query);
            showItemList(query);

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            String category = comboCategory.Text;
            query = "Select itemname from items where category = '" + category + "' and itemname like '" + txtSearch.Text + "%' ";
            showItemList(query);

        }   
          private void showItemList(String query)
            {
                listBox1.Items.Clear();
                DataSet ds = fn.getData(query);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    listBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());

                }
          
          }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantityUpDown.ResetText();
            txtTotal.Clear();

            String text = listBox1.GetItemText(listBox1.SelectedItem);
            txtItemName.Text = text;
            query = "select price from items where itemname = '"+text+"'";
            DataSet ds = fn.getData(query);

            try
            {
                txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {

                throw;
            }
         

        }

        private void txtQuantityUpDown_ValueChanged(object sender, EventArgs e)
        {
            Int64 quan = Int64.Parse(txtQuantityUpDown.Value.ToString());
            Int64 price = Int64.Parse(txtPrice.Text);
            txtTotal.Text = (quan * price).ToString();
        }


        protected int n, total = 0;

        private void btnAddtoCard_Click(object sender, EventArgs e)
        {

            if (txtTotal.Text != "0" && txtTotal.Text != "" )
            {
                n = guna2DataGridView1.Rows.Add();
                guna2DataGridView1.Rows[n].Cells[0].Value = txtItemName.Text;
                guna2DataGridView1.Rows[n].Cells[1].Value = txtPrice.Text;
                guna2DataGridView1.Rows[n].Cells[2].Value = txtQuantityUpDown.Value;
                guna2DataGridView1.Rows[n].Cells[3].Value = txtTotal.Text;

                total += int.Parse(txtTotal.Text);
                labelTotalAmount.Text = "$." + total;
            }
            else
            {
                MessageBox.Show("Favor seleccionar minimo 1 producto", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);                          

            }

        }

        int amount;
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                amount = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());

            }
            catch { }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                guna2DataGridView1.Rows.RemoveAt(this.guna2DataGridView1.SelectedRows[0].Index);

            }
            catch { }

            total -= amount;
            labelTotalAmount.Text = "$." + total;
                
        }

        private void UC_PlaceOrder_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Recibo Virtual Cofee Shop";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Monto Total a Pagar es : " + labelTotalAmount.Text;
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(guna2DataGridView1);

            total = 0;
            guna2DataGridView1.Rows.Clear();
            labelTotalAmount.Text = "$. " + total; 

        }


    }

}




