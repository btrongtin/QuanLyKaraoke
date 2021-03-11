using QuanLyKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }

        private MenuDAO() { }

        public List<Menu> GetListMenuByRoom(int id)
        {
            List<Menu> listmenu = new List<Menu>();

            DataTable data = DataProvider.Instance.ExecuteQuery("Select f.name, bi.count, f.price, totalPrice = f.price * bi.count from bill b, BILLINFO bi, FOOD f where b.id = bi.idBill and f.id = bi.idFood and b.idRoom = "+id);

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listmenu.Add(menu);
            }

            return listmenu;
        }
    }
}
