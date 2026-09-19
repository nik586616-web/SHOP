using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHOP
{
    public partial class FRefund : Form
    {
        ClassAdo classAdo = new ClassAdo();
        int currentOrderId = 0;
        string currrentComment = "";
        public FRefund(int currentOrderId, string currrentComment)
        {
            InitializeComponent();
            this.currentOrderId = currentOrderId;
            this.currrentComment = currrentComment;
            string sqlProduct = $@"select products.product_name as Товар, products.id_product as ID from sostav, 
                products, orders where sostav.id_product =products.id_product and orders.id_order = sostav.id_order and 
                sostav.id_order={currentOrderId}";
            classAdo.comboBoxBind(sqlProduct, cbProduct, "Товар", "ID");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtxtComment.Text != "" && cbProduct.Text != "")
                {
                    int count = 0;
                    string sql = $@"update sostav set status = 'возвращено' where id_order = {currentOrderId} 
                        and id_product = {cbProduct.SelectedValue}";
                    classAdo.Proc(sql);
                    string sqlCount = $@"select sostav.status from sostav, orders where 
                        orders.id_order = sostav.id_order and sostav.id_order= {currentOrderId}";
                    DataSet ds = classAdo.GetDataSet(sqlCount);
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if(ds.Tables[0].Rows[i]["status"].ToString() == "возвращено")
                        {
                            count++;
                        }
                    }
                    if(ds.Tables[0].Rows.Count == count)
                    {
                        string sql1 = $@"update orders set comment= '{rtxtComment.Text}', status = 'возвращено' 
                            where id_order = {currentOrderId}";
                        classAdo.Proc(sql1);
                    }
                    else
                    {
                        string sql1 = $@"update orders set comment= '{rtxtComment.Text}', status = 'частично возвращено' 
                            where id_order = {currentOrderId}";
                        classAdo.Proc(sql1);
                    }
                    MessageBox.Show("Возврат успешно оформлен!");
                }
                else if (rtxtComment.Text == "")
                {
                    MessageBox.Show("Обязательно напишите отзыв!");
                }
                else
                {
                    MessageBox.Show("Обязательно выберите товар, который хотите возвратить");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
