using QuanLyKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

        public DateTime GetTimeStartByRoom(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select DateCheckIn from BILL where idRoom = " + id + "and status = 0");
            try
            {
                Bill bill = new Bill(data);
                return (DateTime)bill.DateCheckIn;
            }
            catch { }

            return DateTime.Now;
        }

        public int getUncheckedBillByRoomID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from BILL where idRoom = " + id + "  and status = 0");

            if(data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }

            return -1;
        }

        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBill @idRoom", new object[] { id });
        }

        public DataTable GetListBillByDate(DateTime datecheckin, DateTime datecheckout)
        {
            return DataProvider.Instance.ExecuteQuery("Exec USP_GetListBillByDate @datecheckin , @datecheckout", new object[] { datecheckin, datecheckout});
        }

        public void CheckOut(int id, int discount, float totalprice, float totalFoodPrice, float totalRoomPrice)
        {
            string query = String.Format("Update bill set status = 1, totalprice = {0}, dateCheckOut = getdate(), discount = {1}, totalRoomPrice = {2}, totalFoodPrice = {3} where id = {4}", totalprice, discount, totalRoomPrice, totalFoodPrice, id);//"Update Bill Set status = 1, totalprice = " + totalprice + " , dateCheckOut = getdate()" + ", discount = "+discount +" where id = "+id;

            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
