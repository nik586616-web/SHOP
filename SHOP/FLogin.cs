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
    public partial class FLogin : Form
    {
        ClassAdo classAdo = new ClassAdo();
        public FLogin()
        {
            InitializeComponent();
        }
        private void btnVhod_Click(object sender, EventArgs e)
        {
            string sqlUser = "select * from users, roles where users.id_role = roles.id_role and" +
                " users.[login] = " + "'" + txtLogin.Text + "'" + " and users.[password] = " + "'" + txtPassword.Text + "'";
            DataSet ds = classAdo.GetDataSet(sqlUser);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Пользователь не найден!");
            }
            else
            {
                string idUser = ds.Tables[0].Rows[0][0].ToString();
                string userName = ds.Tables[0].Rows[0][4].ToString();
                string role = ds.Tables[0].Rows[0][8].ToString();
                MessageBox.Show(role + " " + userName + ", добро пожаловать в систему");
                if (role == "Клиент")
                {
                    FProduct frm = new FProduct();
                    frm.IdUser.Text = idUser;
                    frm.lblUser.Text = userName;
                    frm.Text = "Список товаров. " + role;
                    frm.ShowDialog();
                }
                if (role == "Администратор")
                {
                    FOrdersAdmin frm = new FOrdersAdmin();
                    frm.lblUser.Text = userName;
                    frm.Text = "Все заказы." +role;
                    frm.ShowDialog();
                }
            }
        }

        private void lnklblAutoriz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FRegistr frm = new FRegistr();
            frm.ShowDialog();
        }
    }
}
