using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DTO
{
    public class Room
    {
        private int iD;
        private string name;
        private float price;
        private string status;

        public Room() { }

        public Room(DataRow row)
        {
            this.iD = (int)row["id"];
            this.name = row["name"].ToString();
            this.price = (float)Convert.ToDouble(row["price"].ToString());
            this.status = row["status"].ToString();
        }

        public Room(int id, string name, float price, string status)
        {
            this.iD = id;
            this.name = name;
            this.price = price;
            this.status = status;
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

    }
}
