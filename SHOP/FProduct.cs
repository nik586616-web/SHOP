using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace SHOP
{
    public partial class FProduct : Form
    {
        ClassAdo classAdo = new ClassAdo();
        //String sqlProduct = "select * from products, categories, manufacturer, provider where " +
        //    "products.id_category = categories.id_category and  products.id_manufacturer = manufacturer.id_manufacturer and " +
        //    "products.id_provider = provider.id_provider";
        String sqlProduct = $@"select products.id_product as 'Номер', products.product_name as 'Товар', categories.category_name as 'Категория', manufacturer.manufacturer_name as 'Производитель', provider.provider_name as 'Поставщик', products.size as 'Размер', products.description as 'Описание', products.price as 'Цена' from products, categories, manufacturer, provider where
            products.id_category = categories.id_category and  products.id_manufacturer = manufacturer.id_manufacturer and 
            products.id_provider = provider.id_provider ";
        String sqlNow; 
        int cartId = 0;
        public void addOrder(String sqlNow)
        {
            String sql = sqlNow;
            if (chbStock.Checked)
            {
                sql += " and products.stock = 1";
            }
            DataSet ds = classAdo.GetDataSet(sql);

            classAdo.DataGridBind(sql, dgvProduct);
            //getDgvProducts(ds);
        }
        public FProduct()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void getDgvProducts(DataSet ds)
        {
            classAdo.DataGridBind(sqlNow, dgvProduct);
            //dgvProduct.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dgvProduct.Rows.Clear();
            //dgvProduct.Columns.Clear();
            //DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            //imageColumn.Name = "photo";
            //dgvProduct.Columns.Add(imageColumn);
            //dgvProduct.Columns.Add("id_product", "id_product");
            //dgvProduct.Columns.Add("tovar", "tovar");
            //dgvProduct.Columns["photo"].Width = 150;
            //dgvProduct.Columns["id_product"].Width = 5;
            //dgvProduct.Columns["tovar"].Width = 400;
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    dgvProduct.Rows.Add();
            //    dgvProduct.Rows[i].Height = 150;
            //    String imgFile = "";
            //    if (ds.Tables[0].Rows[i]["photo"].ToString() != "")
            //    {
            //        imgFile = ds.Tables[0].Rows[i]["photo"].ToString();
            //    }
            //    else
            //    {
            //        imgFile = "picture.png";
            //    }
            //    imgFile = "img/" + imgFile;
            //    Image image = new Bitmap(imgFile);
            //    Bitmap result = new Bitmap(image, 150, 150);
            //    dgvProduct.Rows[i].Cells["photo"].Value = result;
            //    dgvProduct.Rows[i].Cells["id_product"].Value = ds.Tables[0].Rows[i]["id_product"];
            //    String str = "Наимнование товара: " + ds.Tables[0].Rows[i]["product_name"].ToString() + "\n" +
            //        "Категория: " + ds.Tables[0].Rows[i]["category_name"].ToString() + "\n" +
            //        "Производитель: " + ds.Tables[0].Rows[i]["manufacturer_name"].ToString() + "\n" +
            //        "Поставщик: " + ds.Tables[0].Rows[i]["provider_name"].ToString() + "\n" +
            //        "Размер: " + ds.Tables[0].Rows[i]["size"].ToString() + "\n" +
            //        "Описание товара: " + ds.Tables[0].Rows[i]["description"].ToString() + "\n" +
            //        "Цена: " + ds.Tables[0].Rows[i]["price"].ToString();
            //    dgvProduct.Rows[i].Cells["tovar"].Value = str;
        //}
        }

        private void FProduct_Load(object sender, EventArgs e)
        {
            sqlNow = sqlProduct;
            DataSet ds = classAdo.GetDataSet(sqlNow);
            getDgvProducts(ds);
        }

        private void chbStock_CheckedChanged(object sender, EventArgs e)
        {
            addOrder(sqlNow);
        }

        private void добавитьВКорзинуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlCart = $@"select * from carts, users where users.id_user = carts.id_user and 
                carts.id_user = {int.Parse(IdUser.Text)}";
            DataSet ds = classAdo.GetDataSet(sqlCart);
            if (ds.Tables[0].Rows.Count == 0)
            {
                SqlCommand sqlCommand1 = classAdo.stProcExec("addCart");
                sqlCommand1.Parameters.AddWithValue("@id_user", int.Parse(IdUser.Text));
                sqlCommand1.Parameters.Add("@id_cart", SqlDbType.Int);
                sqlCommand1.Parameters["@id_cart"].Direction = ParameterDirection.Output;
                sqlCommand1.ExecuteNonQuery();
                cartId = int.Parse(sqlCommand1.Parameters["@id_cart"].Value.ToString());
                MessageBox.Show("Корзина была успешна создана!");
            }
            else
            {
                cartId =int.Parse(ds.Tables[0].Rows[0]["id_cart"].ToString());
            }
            int nRow = dgvProduct.CurrentRow.Index;
            String id_product = dgvProduct[0, nRow].Value.ToString();
            string chCartItQuanSql = $@"select * from cart_items where cart_items.id_cart = {cartId} and 
                cart_items.id_product= {id_product}";
            DataSet ds1 = classAdo.GetDataSet(chCartItQuanSql);
            if (ds1.Tables[0].Rows.Count == 0)
            {
                SqlCommand sqlCommand2 = classAdo.stProcExec("addCartItem");
                sqlCommand2.Parameters.AddWithValue("@id_cart", cartId);
                sqlCommand2.Parameters.AddWithValue("@id_product", id_product);
                sqlCommand2.ExecuteNonQuery();
            }
            else
            {
                string sql = $@"update cart_items set quantity += 1 where cart_items.id_product={id_product} 
                            and cart_items.id_cart = {cartId}";
                classAdo.Proc(sql);
            }
            MessageBox.Show("Товар " + id_product + " добавлен в корзину ");
            btnOrder.Visible = true;
        }
        private void btnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                FCart fCart = new FCart(cartId);
                fCart.IdCart.Text = cartId.ToString();
                fCart.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAllOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = int.Parse(IdUser.Text);
                FOrdersClient frm = new FOrdersClient(userId);
                frm.IdUser.Text = userId.ToString();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
