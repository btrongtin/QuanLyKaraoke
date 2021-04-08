using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DTO
{
    public class Account
    {
        private string username;
        private string displayname;
        private string password;
        private int type;

        public Account(string username, string displayname, int type, string password = null)
        {
            this.username = username;
            this.displayname = displayname;
            this.type = type;
            this.password = password;
        }

        public Account(DataRow row)
        {
            this.username = row["username"].ToString();
            this.displayname = row["displayname"].ToString();
            this.type = (int)row["type"];
            this.password = row["password"].ToString();
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Displayname
        {
            get
            {
                return displayname;
            }

            set
            {
                displayname = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
    }
}
