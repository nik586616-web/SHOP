using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SHOP
{
    public partial class FOrdersClient : Form
    {
        ClassAdo classAdo = new ClassAdo();
        int currrentUserId = 0;
        public FOrdersClient(int currrentUserId)
        {
            InitializeComponent();
            this.currrentUserId = currrentUserId;
        }
        public void DGV()
        {
                String sqlOrders = $@"select orders.id_order as 'Номер заказа', orders.data_order as 'Дата заказа', 
                    orders.date_delivery as 'Примерная дата доставки', orders.address as 'Адресс', orders.status as 'Статус заказа', 
                    SUM(quantity * price) as 'Итоговая сумма', orders.comment as 'Отзыв'  from orders, users, sostav where orders.id_user = users.id_user 
                    and sostav.id_order = orders.id_order and orders.id_user = {currrentUserId}
                    group by orders.id_order, orders.data_order, orders.date_delivery, orders.address, orders.status, orders.comment ";
                classAdo.DataGridBind(sqlOrders, dgvOrdersClient);
        }
        private void FOrdersClient_Load(object sender, EventArgs e)
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void отменитьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nRow = dgvOrdersClient.CurrentRow.Index;
            int orderId = int.Parse(dgvOrdersClient[0, nRow].Value.ToString());
            string status = dgvOrdersClient[4, nRow].Value.ToString();
            if (status == "возвращено" || status == "доставлено")
            {
            }
            else
            {
                string sql = $@"update orders set status= 'отменено' where id_order = {orderId} and id_user = {currrentUserId}";
                classAdo.Proc(sql);
                MessageBox.Show("Заказ отменен");
                DGV();
            }
        }
        private void возратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nRow = dgvOrdersClient.CurrentRow.Index;
            int orderId = int.Parse(dgvOrdersClient[0, nRow].Value.ToString());
            string status = dgvOrdersClient[4, nRow].Value.ToString();
            string comment = dgvOrdersClient[6, nRow].Value.ToString();
            if (status == "доставлено" || status == "частично возвращено")
            {
                FRefund frm = new FRefund(orderId, comment);
                frm.IdOrder.Text = orderId.ToString();
                frm.rtxtComment.Text = comment;
                frm.ShowDialog();
            }
        }

        private void dgvOrdersClient_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    string comment = dgvOrdersClient.CurrentCell.Value.ToString();
                    int orderId = int.Parse(dgvOrdersClient[0, dgvOrdersClient.CurrentRow.Index].Value.ToString());
                    string status = dgvOrdersClient[4, dgvOrdersClient.CurrentRow.Index].Value.ToString();
                    if (status == "доставлено" || status == "отменено" || status == "возвращено")
                    {
                        if (MessageBox.Show("Изменить отзыв в заказе?", "Подтверждение", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string sql = $@"update orders set comment= '{comment}' where orders.id_order={orderId}";
                            classAdo.Proc(sql);
                            MessageBox.Show("Отзыв успешно изменено");
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

        private void IdUser_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
