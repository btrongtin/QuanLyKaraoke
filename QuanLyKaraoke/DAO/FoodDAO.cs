using QuanLyKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get
            {
                if (instance == null) instance = new FoodDAO(); return instance;
            }

            private set
            {
                instance = value;
            }
        }

        private FoodDAO() { }

        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();

            string query = string.Format("SELECT * FROM dbo.Food WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }


        public bool DeleteFood(int id)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(id);

            string query = String.Format("DELETE FOOD WHERE ID = {0}",id);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateFood(int id ,string name, int idCategory, float price)
        {
            string query = String.Format("UPDATE FOOD SET name = N'{0}', idcategory = {1}, price = {2} WHERE ID = {3}", name, idCategory, price, id);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool InsertFood(string name, int idCategory, float price)
        {
            string query = String.Format("INSERT INTO FOOD(name, idCategory, price) VALUES (N'{0}', {1} , {2})", name, idCategory, price);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();

            string query = "select * from Food";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();

            string query = "select * from Food where idCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

    }
}
