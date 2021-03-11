using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKaraoke.DAO;
using QuanLyKaraoke.DTO;

namespace QuanLyKaraoke
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
            loadRoom();
        }

        void loadRoom()
        {
            List<Room> roomlist = RoomDAO.Instance.LoadRoomList();

            foreach (Room item in roomlist)
            {
                Button btn = new Button() { Width = RoomDAO.RoomWidth, Height = RoomDAO.RoomHeight };

                btn.Text = item.Name + Environment.NewLine + item.Status;

                switch (item.Status)
                {
                    case "Có người":
                        btn.BackColor = Color.SpringGreen;
                        break;
                    default:
                        btn.BackColor = Color.LightGray;
                        break;
                }

                flpRoom.Controls.Add(btn);
                //them mot ti comment ne hihi
                //test xem doi duoc tai khoan chua
                //Test account lan 2 
                //Test push may truong
            }
        }

        private void quảnTrịViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();      
            f.ShowDialog();

        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile();
            f.ShowDialog();
        }
    }
}
