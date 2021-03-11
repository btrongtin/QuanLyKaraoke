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

                btn.Click += btn_Click;

                btn.Tag = item;

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
            }
        }

        void showBill(int id)
        {
            lsvBill.Items.Clear();

            List<QuanLyKaraoke.DTO.Menu> listMenu = MenuDAO.Instance.GetListMenuByRoom(BillDAO.Instance.getUncheckedBillByRoomID(id));

            foreach (QuanLyKaraoke.DTO.Menu item in listMenu)
            {
                ListViewItem lsvitem = new ListViewItem(item.FoodName.ToString());
                lsvitem.SubItems.Add(item.Count.ToString());
                lsvitem.SubItems.Add(item.Price.ToString());
                lsvitem.SubItems.Add(item.TotalPrice.ToString());
                lsvBill.Items.Add(lsvitem);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            int Roomid = ((sender as Button).Tag as Room).ID;

            showBill(Roomid);
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
