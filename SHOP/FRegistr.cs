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
    public partial class FRegistr : Form
    {
        ClassAdo classAdo = new ClassAdo();
        public FRegistr()
        {
            InitializeComponent();
        }
        private void btnRegistr_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFIO.Text == "" || txtPhone.Text == "" || txtEmail.Text == "" || txtLogin.Text == "" || txtPassword.Text == "" || txtPovtPasw.Text == "")
                {
                    MessageBox.Show("Заполните поля");
                }
                else
                {
                    if (txtPassword.Text == txtPovtPasw.Text)
                    {
                        string sql = $@"select * from users where users.login = '{txtLogin.Text}'";
                        DataSet ds = classAdo.GetDataSet(sql);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            string sqlIns = $@"insert into users(id_role, login, password, FIO, phone, email) values 
                                (2, '{txtLogin.Text}', '{txtPassword.Text}', '{txtFIO.Text}', '{txtPhone.Text}', '{txtEmail.Text}')";
                            classAdo.Proc(sqlIns);
                            MessageBox.Show("Вы успешно зарегистрированы");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Логин уже занят");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароли должны совпадать");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lnklblAutoriz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
