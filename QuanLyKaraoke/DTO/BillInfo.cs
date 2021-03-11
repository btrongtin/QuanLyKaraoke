using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKaraoke.DTO
{
    public class BillInfo
    {
        private int iD;
        private int billID;
        private int foodID;
        private int count;

        public BillInfo(int id, int billID, int foodID, int count)
        {
            this.iD = id;
            this.billID = billID;
            this.foodID = foodID;
            this.count = count;
        }

        public BillInfo(DataRow row)
        {
            this.iD = (int)row["id"];
            this.billID = (int)row["IDbill"];
            this.foodID = (int)row["IDfood"];
            this.count = (int)row["count"];
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public int FoodID
        {
            get { return foodID; }
            set { foodID = value; }
        }

        public int BillID
        {
            get { return billID; }
            set { billID = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
