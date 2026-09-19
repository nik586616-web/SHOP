using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHOP
{
    public partial class FCart : Form
    {
        ClassAdo classAdo = new ClassAdo();
        int currentCartId = 0;
        int orderId = 0;
        public FCart(int currentCartId)
        {
            InitializeComponent();
            this.currentCartId = currentCartId;
        }

        private void FCart_Load(object sender, EventArgs e)
        {
            try
            {
                DGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DGV()
        {
            String sqlCart = $@"select cart_items.id_product as 'Номер товара', products.product_name as 'Товар', 
            categories.category_name 'Категория', manufacturer.manufacturer_name as 'Производитель', 
            provider.provider_name as 'Поставщик',  products.price as 'Цена', cart_items.quantity as 'Количество',  
            (cart_items.quantity * products.price) as 'Сумма'  from products, categories,
            manufacturer, provider, cart_items 
            where products.id_category = categories.id_category and products.id_manufacturer = manufacturer.id_manufacturer 
            and products.id_provider = provider.id_provider and cart_items.id_product = products.id_product and cart_items.id_cart = {currentCartId}";
            classAdo.DataGridBind(sqlCart, dgvCart);
            dgvCart[4, dgvCart.RowCount - 1].Value = "Итого";
            decimal Sum1 = 0;
            for (int i = 0; i < dgvCart.RowCount - 1; i++)
            {
                Sum1 += decimal.Parse(dgvCart[7, i].Value.ToString());
            }
            dgvCart[7, dgvCart.RowCount - 1].Value = Sum1;
        }

        private void IdCart_Click(object sender, EventArgs e)
        {
           
        }

        private void dgvCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    int count = int.Parse(dgvCart.CurrentCell.Value.ToString());
                    int productId = int.Parse(dgvCart[0, dgvCart.CurrentRow.Index].Value.ToString());
                    if(count != 0)
                    {
                        if (MessageBox.Show("Изменить кол-во товара в корзине?","Подтверждение",MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            string sql = $@"update cart_items set quantity = {count} where cart_items.id_product={productId} 
                            and cart_items.id_cart = {currentCartId}";
                            classAdo.Proc(sql);
                            MessageBox.Show("Кол-во успешно изменено");
                            DGV();
                        }
                    }
                    if(count == 0)
                    {
                        if (MessageBox.Show("Вы точно хотите удалить товар из корзины?","Подтверждение",MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            string sql = $@"delete from cart_items where cart_items.id_product = {productId}
                                and cart_items.id_cart = {currentCartId}";
                            classAdo.Proc(sql);
                            MessageBox.Show("Товар успешно удален");
                            DGV();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdderss.Text != "")
                {
                    DateTime now = DateTime.Now;
                    DateTime dateDeliv = now.AddDays(3);
                    string sqlIdUser = $@"select* from carts where carts.id_cart= {currentCartId}";
                    DataSet ds = classAdo.GetDataSet(sqlIdUser);
                    int userId = int.Parse(ds.Tables[0].Rows[0][1].ToString());
                    SqlCommand sqlCommand = classAdo.stProcExec("addOrder");
                    sqlCommand.Parameters.AddWithValue("@id_user", userId);
                    sqlCommand.Parameters.AddWithValue("@data_order", DateTime.Now.ToShortDateString());
                    sqlCommand.Parameters.AddWithValue("@date_delivery", dateDeliv.ToShortDateString());
                    sqlCommand.Parameters.AddWithValue("@address", txtAdderss.Text);
                    sqlCommand.Parameters.Add("@id_order", SqlDbType.Int);
                    sqlCommand.Parameters["@id_order"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    orderId = int.Parse(sqlCommand.Parameters["@id_order"].Value.ToString());
                    string sqlAddSostav = $@"insert into sostav(id_order, id_product, quantity, price)
                        select
                            {orderId},
                            cart_items.id_product,
                            cart_items.quantity,
                            products.price  -- цена на момент заказа
                        from cart_items 
                        inner join carts on cart_items.id_cart = carts.id_cart
                        inner join products on cart_items.id_product = products.id_product
                        where carts.id_user = {userId}";
                    classAdo.Proc(sqlAddSostav); //Добавление позиций из корзины в состав заказа
                    string delCartItems = $@"delete from cart_items
                        where id_cart IN (
                            select id_cart from carts where id_user = {userId}
                        )";
                    classAdo.Proc(delCartItems); //Очищаем корзину клиента после успешного оформления заказа
                    MessageBox.Show("Заказ успешно оформлен!");
                    DGV();
                    FReceipt frm = new FReceipt(orderId);
                    frm.IdOrder.Text = orderId.ToString();
                    frm.ShowDialog();
                }
                else 
                {
                    MessageBox.Show("Введите адресс доставик!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void удалитьТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(dgvCart[0, dgvCart.CurrentRow.Index].Value.ToString());
            if (MessageBox.Show("Вы точно хотите удалить товар из корзины?", "Подтверждение", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = $@"delete from cart_items where cart_items.id_product = {productId}
                                and cart_items.id_cart = {currentCartId}";
                classAdo.Proc(sql);
                MessageBox.Show("Товар успешно удален");
                DGV();
            }
        }
    }
}
