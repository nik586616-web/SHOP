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
    public partial class FEditStatusDate : Form
    {
        ClassAdo classAdo = new ClassAdo();
        int currentOrderId = 0;
        string currentStatus = "";
        string currentDateDeliv = "";
        public FEditStatusDate(int currentOrderId, string currentStatus, string currentDateDeliv)
        {
            InitializeComponent();
            this.currentOrderId = currentOrderId;
            this.currentStatus = currentStatus;
            this.currentDateDeliv=currentDateDeliv;
        }
        private void FEditStatusDate_Load(object sender, EventArgs e)
        {
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime d = DateTime.Parse(dtDeliv.Text);
                if (cmbStatus.Text != "" || dtDeliv.Text != "")
                {
                    string sql = $@"update orders set status= '{cmbStatus.Text}', date_delivery = '{d.ToShortDateString()}' 
                        where id_order = {currentOrderId}";
                    classAdo.Proc(sql);
                    string sql1 = $@"update sostav set status= '{cmbStatus.Text}' where id_order = {currentOrderId}";
                    classAdo.Proc(sql1);
                    MessageBox.Show("Заказ успешно изменен!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
