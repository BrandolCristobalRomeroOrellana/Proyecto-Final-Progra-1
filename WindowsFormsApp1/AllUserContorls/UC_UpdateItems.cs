﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.AllUserContorls
{
    public partial class UC_UpdateItems : UserControl
    {

        function fn = new function();
        String query;


        public UC_UpdateItems()
        {
            InitializeComponent();
        }

        private void UC_UpdateItems_Load(object sender, EventArgs e)
        {
            loadData();

        }
           
        public void loadData()
        {
            query = "select * from items";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];

        }

        private void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            query = "select * from items where itemname like '"+txtSearchItem.Text+"%' ";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];

        }

        int id;
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            String category = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            String itemname = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            int price = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());

            txtCategory.Text = category;
            txtName.Text = itemname;
            txtPrice.Text = price.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            query = "update items set itemname ='" + txtName.Text + "',category = '" + txtCategory.Text + "',price = " + txtPrice.Text + " where iid = " + id + " ";
            fn.setData(query, "Producto actualizado exitosamente.");
            loadData();

            txtName.Clear();
            txtCategory.Clear();
            txtPrice.Clear();
        }


    }
}






