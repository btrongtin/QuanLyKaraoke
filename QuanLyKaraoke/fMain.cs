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
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public fMain(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;
            loadRoom();
            LoadCategory();

            timerTicker.Start();
        }

        void ChangeAccount(int type)
        {
            quảnTrịViênToolStripMenuItem.Enabled = type == 1;
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
            txtFoodTotalPrice.Text = totalPrice.ToString();

        }





        void btn_Click(object sender, EventArgs e) //btn = Room
        {
            //int Roomid = ((sender as Button).Tag as Room).ID;
            Room room = (sender as Button).Tag as Room;
            lsvBill.Tag = (sender as Button).Tag;

            showBill(room.ID);


            dtpkTimeStart.Value = BillDAO.Instance.GetTimeStartByRoom(room.ID);
            txtRoomId.Text = room.ID.ToString();
            txtRoomName.Text = room.Name.ToString();
            txtRoomPrice.Text = room.Price.ToString();

            DateTime startTime = dtpkTimeStart.Value;

            DateTime endTime = dtpkTimeEnd.Value;

            TimeSpan duration = new TimeSpan(endTime.Ticks - startTime.Ticks);

            txtTotalTime.Text = duration.ToString(@"hh\:mm\:ss");



        }















        private void quảnTrịViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = LoginAccount;
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;
            f.InsertCategory += f_InsertCategory;
            f.DeleteCategory += f_DeleteCategory;
            f.UpdateCategory += f_UpdateCategory;
            f.InsertRoom += f_InsertRoom;
            f.DeleteRoom += f_DeleteRoom;
            f.UpdateRoom += f_UpdateRoom;
            f.ShowDialog();
        }

        private void f_UpdateRoom(object sender, EventArgs e)
        {
            loadRoom();
        }

        private void f_DeleteRoom(object sender, EventArgs e)
        {
            loadRoom();
        }

        private void f_InsertRoom(object sender, EventArgs e)
        {
            loadRoom();
        }

        private void f_UpdateCategory(object sender, EventArgs e)
        {
            LoadCategory();
            if (lsvBill.Tag != null)
                showBill((lsvBill.Tag as Room).ID);
        }

        private void f_DeleteCategory(object sender, EventArgs e)
        {
            LoadCategory();
            if (lsvBill.Tag != null)
                showBill((lsvBill.Tag as Room).ID);
        }

        private void f_InsertCategory(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).Id);
            if (lsvBill.Tag != null)
                showBill((lsvBill.Tag as Room).ID);
        }

        private void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).Id);
            if (lsvBill.Tag != null)
                showBill((lsvBill.Tag as Room).ID);
            loadRoom();
        }

        private void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).Id);
            //if (lsvBill.Tag != null)
            //    showBill((lsvBill.Tag as Room).ID);
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
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

            if (room == null)
            {
                MessageBox.Show("Hãy chọn phòng");
                return;
            }

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
            int discount = (int)nmDiscount.Value;
            double totalPrice = Convert.ToDouble(txtTotalPrice.Text);
            double finalTotalPrice = totalPrice - (totalPrice * discount / 100);

            if(idBill != -1)
            {
                if(MessageBox.Show(string.Format("Thanh toán cho phòng {0}?\nTổng tiền: {1} - ({1}*{2}%) = {3}", room.Name, totalPrice, discount, finalTotalPrice), "Xác nhận thanh toán", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                    showBill(room.ID);
                    loadRoom();
                }
            }
        }

        private void btnRoomStart_Click(object sender, EventArgs e)
        {
            Room room = lsvBill.Tag as Room;
            
            if (room == null)
            {
                MessageBox.Show("Hãy chọn phòng");
                return;
            }

            BillDAO.Instance.InsertBill(room.ID);

            loadRoom();
        }

        private void timerTicker_Tick(object sender, EventArgs e)
        {
            dtpkTimeEnd.Value = DateTime.Now;

            DateTime startTime = dtpkTimeStart.Value;
            DateTime endTime = dtpkTimeEnd.Value;

            if (txtTotalTime.Text != "00:00:00")
            {
                TimeSpan duration = new TimeSpan(endTime.Ticks - startTime.Ticks);

                txtTotalTime.Text = duration.ToString(@"hh\:mm\:ss");
            }
        }
    }
}
