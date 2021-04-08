using QuanLyKaraoke.DAO;
using QuanLyKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKaraoke
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public fAccountProfile(Account acc)
        {
            InitializeComponent();

            LoginAccount = acc;
        }

        public Account LoginAccount
        {
            get
            {
                return loginAccount;
            }

            set
            {
                loginAccount = value;
                ChangeAccount(LoginAccount);
            }
        }

        void ChangeAccount(Account acc)
        {
            txtUsername.Text = LoginAccount.Username;
            txtDisplayname.Text = LoginAccount.Displayname;
            if (LoginAccount.Type == 1)
                txtType.Text = "Tài khoản quản trị viên";
            else
                txtType.Text = "Tài khoản nhân viên";  
        }

        void UpdateAccountInfo()
        {
            string displayName = txtDisplayname.Text;
            string password = txtPass.Text;
            string newpass = txtNewPass.Text;
            string reenterPass = txtConfirmPass.Text;
            string userName = txtUsername.Text;

            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới!");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khấu");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }
    }
}
