using QuanLyKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool UpdateAccount(string username, string displayName, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", username, displayName, type);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteAccount(string username)
        {
            string query = string.Format("Delete Account where UserName = N'{0}'", username);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool InsertAccount(string username, string displayname, int type)
        {
            string query = String.Format("INSERT INTO ACCOUNT(username, displayname, type) VALUES (N'{0}', N'{1}' , {2})", username, displayname, type);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ResetPassword(string username)
        {
            string query = string.Format("update account set password = N'0' where UserName = N'{0}'", username);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("Select username, displayname, type From Account");
        }

        public Account GetAccountByUsername(string username)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from Account where username = '" + username + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public bool Login(string username, string password)
        {
            string query = "USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });

            return result.Rows.Count > 0;
        }

        public bool UpdateAccount(string username, string displayname, string pass, string newpass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_UpdateAccount @userName , @displayName , @password , @newPassword ", new object[] { username, displayname, pass, newpass });
            return result > 0;
        }
    }
}
