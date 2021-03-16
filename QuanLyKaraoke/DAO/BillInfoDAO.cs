using QuanLyKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }

        private BillInfoDAO() { }

        //public List<BillInfo> GetListBillInfo(int id)
        //{
        //    List<BillInfo> listbillinfo = new List<BillInfo>();

        //    DataTable data = DataProvider.Instance.ExecuteQuery("Select * from billinfo where idBill = "+id);

        //    foreach (DataRow item in data.Rows)
        //    {
        //        BillInfo info = new BillInfo(item);
        //        listbillinfo.Add(info);
        //    }

        //    return listbillinfo;
        //}

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }
    }
}
