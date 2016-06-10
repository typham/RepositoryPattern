using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProductRepository _Product = new ProductRepository();
        Product Product = new Product();

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            _Product.Insert(new Product
            {
                ProductName = txtProductName.Text,
                Price = double.Parse(txtPrice.Text),
                Description = txtDescription.Text
            });
            _Product.Save();
            LoadProduct();
        }

        public void LoadProduct()
        {
            dgvProduct.DataSource = _Product.SelectAll().ToList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product.ProductName = txtProductName.Text;
            Product.Price = double.Parse(txtPrice.Text);
            Product.Description = txtDescription.Text;

            _Product.Update(Product);
            _Product.Save();
            LoadProduct();
        }

        private void dgvProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Product = _Product.SelectById(dgvProduct.Rows[e.RowIndex].Cells["Id"].Value);
            txtProductName.Text = Product.ProductName;
            txtPrice.Text = Product.Price.ToString();
            txtDescription.Text = Product.Description;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _Product.Delete(Product.Id);
            _Product.Save();
            LoadProduct();
        }
    }
}
