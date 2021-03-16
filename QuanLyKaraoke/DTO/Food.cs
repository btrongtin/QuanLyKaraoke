using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DTO
{
    public class Food
    {
        private int id;
        private string name;
        private int categoryID;
        private float price;

        public Food(int id, string name, int categoryID, float price)
        {
            this.id = id;
            this.name = name;
            this.categoryID = categoryID;
            this.price = price;
        }

        public Food(DataRow row)
        {
            this.id = (int)row["id"];
            this.name = row["name"].ToString();
            this.categoryID = (int)row["ID"];
            this.price = (float)Convert.ToDouble(row["price"].ToString());
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int CategoryID
        {
            get
            {
                return categoryID;
            }

            set
            {
                categoryID = value;
            }
        }

        public float Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }
    }
}
