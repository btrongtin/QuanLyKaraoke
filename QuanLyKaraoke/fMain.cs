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
            LoadCategory();
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }

        void loadRoom()
        {
            flpRoom.Controls.Clear();

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

            float totalPrice = 0;

            List<QuanLyKaraoke.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByRoom(id);
            foreach (QuanLyKaraoke.DTO.Menu item in listBillInfo) //listmenu
            {
                ListViewItem lsvitem = new ListViewItem(item.FoodName.ToString());
                lsvitem.SubItems.Add(item.Count.ToString());
                lsvitem.SubItems.Add(item.Price.ToString());
                lsvitem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvitem);
            }

            txtTotalPrice.Text = totalPrice.ToString();

        }

        void btn_Click(object sender, EventArgs e)
        {
            int Roomid = ((sender as Button).Tag as Room).ID;
            lsvBill.Tag = (sender as Button).Tag;
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

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.Id;

            LoadFoodListByCategoryID(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Room room = lsvBill.Tag as Room;

            int idBill = BillDAO.Instance.getUncheckedBillByRoomID(room.ID);
            int foodID = (cbFood.SelectedItem as Food).Id;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1) //phong chua co bill
            {
                BillDAO.Instance.InsertBill(room.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }

            showBill(room.ID);

            loadRoom();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Room room = lsvBill.Tag as Room;

            int idBill = BillDAO.Instance.getUncheckedBillByRoomID(room.ID);

            if(idBill != -1)
            {
                if(MessageBox.Show("Thanh toán cho phòng "+room.Name+"?", "Xác nhận thanh toán", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill);
                    showBill(room.ID);
                    loadRoom();
                }
            }
        }
    }
}
