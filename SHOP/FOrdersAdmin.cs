using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHOP
{
    public partial class FOrdersAdmin : Form
    {
        ClassAdo classAdo = new ClassAdo();
        public FOrdersAdmin()
        {
            InitializeComponent();
        }
        private void DGV()
        {
            String sqlOrders = $@"select orders.id_order as 'Номер заказа', orders.id_user as 'Номер клиента', 
                    orders.data_order as 'Дата заказа', 
                    orders.date_delivery as 'Примерная дата доставки', orders.status as 'Статус заказа', orders.comment as 'Отзыв'  
                    from orders, users where orders.id_user = users.id_user";
            classAdo.DataGridBind(sqlOrders, dgvOrdersAdmin);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FOrdersAdmin_Load(object sender, EventArgs e)
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
        private void изменитьСтатусИДатуДоставкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nRow = dgvOrdersAdmin.CurrentRow.Index;
                int orderId = int.Parse(dgvOrdersAdmin[0, nRow].Value.ToString());
                string status = dgvOrdersAdmin[4, nRow].Value.ToString();
                string dateDeliv = dgvOrdersAdmin[3, nRow].Value.ToString();
            if(status == "отменено" || status == "доставлено" || status == "возвращено" || status == "частично возвращено")
            {
            }
            else
            {
                FEditStatusDate frm = new FEditStatusDate(orderId, status, dateDeliv); 
                frm.IdOrder.Text = orderId.ToString();
                frm.cmbStatus.Text = status;
                frm.dtDeliv.Text = dateDeliv;
                frm.ShowDialog();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DGV();
        }
    }
}
