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
    public partial class FReceipt : Form
    {
        ClassAdo classAdo = new ClassAdo();
        int currentOrderId = 0;
        public FReceipt(int currentOrderId)
        {
            InitializeComponent();
            this.currentOrderId = currentOrderId;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FReceipt_Load(object sender, EventArgs e)
        {
            try
            {
                String sqlReceipt = $@"select products.product_name as 'Наименование товара', sostav.quantity as 'Количество',
                    FORMAT(sostav.price, 'N2') + ' руб.' as 'Цена за ед.', (sostav.quantity * sostav.price) as 'Сумма'
                    from sostav
                    inner join products ON sostav.id_product = products.id_product
                    WHERE sostav.id_order = {currentOrderId}";
                classAdo.DataGridBind(sqlReceipt, dgvReceipt);
                dgvReceipt[0, dgvReceipt.RowCount - 1].Value = "Итого";
                int Sum1 = 0;
                decimal Sum2 = 0;
                for (int i = 0; i < dgvReceipt.RowCount - 1; i++)
                {
                    Sum1 += int.Parse(dgvReceipt[1, i].Value.ToString());
                    Sum2 += decimal.Parse(dgvReceipt[3, i].Value.ToString());
                }
                dgvReceipt[1, dgvReceipt.RowCount - 1].Value = Sum1;
                dgvReceipt[3, dgvReceipt.RowCount - 1].Value = Sum2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
