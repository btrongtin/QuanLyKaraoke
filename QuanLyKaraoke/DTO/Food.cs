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
        private int idCategory;
        private float price;

        public Food(int id, string name, int idCategory, float price)
        {
            this.id = id;
            this.name = name;
            this.idCategory = idCategory;
            this.price = price;
        }

        public Food(DataRow row)
        {
            this.id = (int)row["id"];
            this.name = row["name"].ToString();
            var idCategoryTemp = row["idCategory"];
            if (idCategoryTemp.ToString() != "")
                this.idCategory = (int)row["idCategory"];
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

        public int IdCategory
        {
            get
            {
                return idCategory;
            }

            set
            {
                idCategory = value;
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
