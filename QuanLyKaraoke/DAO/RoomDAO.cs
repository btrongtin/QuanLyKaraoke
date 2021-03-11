using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKaraoke.DTO;

namespace QuanLyKaraoke.DAO
{
    public class RoomDAO
    {
        private static RoomDAO instance;

        public static RoomDAO Instance
        {
            get { if (instance == null) instance = new RoomDAO(); return RoomDAO.instance; }
            private set { RoomDAO.instance = value; }
        }

        public static int RoomWidth = 150;

        public static int RoomHeight = 150;

        private RoomDAO() { }

        public List<Room> LoadRoomList()
        {
            List<Room> roomList = new List<Room>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetRoomList");

            foreach (DataRow item in data.Rows)
            {
                Room room = new Room(item);
                roomList.Add(room);
            }

            return roomList;
        }
    }
}
