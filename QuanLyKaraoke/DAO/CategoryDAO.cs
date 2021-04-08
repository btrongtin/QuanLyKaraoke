using QuanLyKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get
            {
               if (instance == null) instance = new CategoryDAO(); return instance;
            }

            private set
            {
                instance = value;
            }
        }

        private CategoryDAO() { }

        public bool UpdateCategory(int id, string name)
        {
            string query = String.Format("UPDATE FOODCATEGORY SET name = N'{0}'WHERE ID = {1}", name, id);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteCategory(int id)
        {
            string query = String.Format("DELETE FOODCATEGORY WHERE ID = {0}", id);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool InsertCategory(string name)
        {
            string query = String.Format("INSERT INTO FOODCATEGORY(name) VALUES (N'{0}')", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();

            string query = "select * from FoodCategory";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }

            return list;
        }

        public Category GetCategoryByID(int id)
        {
            Category category = null;

            string query = "select * from FoodCategory where id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }

            return category;
        }
    }
}

