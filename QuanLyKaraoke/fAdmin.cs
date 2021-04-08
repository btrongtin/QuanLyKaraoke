using QuanLyKaraoke.DAO;
using QuanLyKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKaraoke
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource roomList = new BindingSource();

        public Account loginAccount;

        public fAdmin()
        {
            InitializeComponent();

            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            dtgvCategory.DataSource = categoryList;
            dtgvRoom.DataSource = roomList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

            LoadListFood();
            LoadAccount();
            LoadCategory();
            LoadRoom();
            LoadCategoryIntoCombobox(cbFoodCategory);
            AddFoodBinding();
            AddAccountBinding();
            AddCategoryBinding();
            AddRoomBinding();
        }

        void AddRoomBinding()
        {
            txtRoomId.DataBindings.Add(new Binding("Text", dtgvRoom.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtRoomName.DataBindings.Add(new Binding("Text", dtgvRoom.DataSource, "name", true, DataSourceUpdateMode.Never));
            txtRoomStatus.DataBindings.Add(new Binding("Text", dtgvRoom.DataSource, "status", true, DataSourceUpdateMode.Never));
            nmRoomPrice.DataBindings.Add(new Binding("Value", dtgvRoom.DataSource, "price", true, DataSourceUpdateMode.Never));

        }

        void AddAccountBinding()
        {
            txtAccountUsername.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "username", true, DataSourceUpdateMode.Never));
            txtAccountDisplayname.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "displayname", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "type", true, DataSourceUpdateMode.Never));
        }

        void AddFoodBinding()
        {
            txtFoodId.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "name", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
        }

        void AddCategoryBinding()
        {
            txtCategoryId.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, today.Day);
            dtpkToDate.Value = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59); ;
        }

        void LoadRoom()
        {
            roomList.DataSource = RoomDAO.Instance.LoadRoomList();
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        void LoadListBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDate(dateCheckIn, dateCheckOut);
        }
        
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood(); 
        }

        void LoadCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
        }

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }

        void AddAccount(string username, string displayname, int type)
        {
            if (AccountDAO.Instance.InsertAccount(username, displayname, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }

            LoadAccount();
        }

        void EditAccount(string username, string displayname, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(username, displayname, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.Username.Equals(userName))
            {
                MessageBox.Show("Vui lòng đừng xóa chính bạn chứ");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();
        }

        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }


        private void btnViewBill_Click(object sender, EventArgs e)
        {
            DateTime formatFromDate = dtpkFromDate.Value;
            DateTime formatToDate = dtpkToDate.Value;
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);      
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txtFoodId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["idCategory"].Value;

                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.Id == category.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;

            if(FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();
                if(insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txtFoodId.Text);

            if (FoodDAO.Instance.UpdateFood(id ,name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                if(updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtFoodId.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListFood();
                if(deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        private event EventHandler inserRoom;
        public event EventHandler InsertRoom
        {
            add { inserRoom += value; }
            remove { inserRoom -= value; }
        }

        private event EventHandler deleteRoom;
        public event EventHandler DeleteRoom
        {
            add { deleteRoom += value; }
            remove { deleteRoom -= value; }
        }

        private event EventHandler updateRoom;
        public event EventHandler UpdateRoom
        {
            add { updateRoom += value; }
            remove { updateRoom -= value; }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txtSearchFoodName.Text);
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string username = txtAccountUsername.Text;
            string displayname = txtAccountDisplayname.Text;
            int type = (int)nmAccountType.Value;

            AddAccount(username, displayname, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string username = txtAccountUsername.Text;

            DeleteAccount(username);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string username = txtAccountUsername.Text;
            string displayname = txtAccountDisplayname.Text;
            int type = (int)nmAccountType.Value;

            EditAccount(username, displayname, type);
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string username = txtAccountUsername.Text;

            ResetPass(username);
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text;
            
            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công");
                LoadCategory();
                LoadCategoryIntoCombobox(cbFoodCategory);
                if (insertCategory != null)
                {
                    insertCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryId.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công");
                LoadCategory();
                LoadCategoryIntoCombobox(cbFoodCategory);
                LoadListFood();
                if (deleteCategory != null)
                {
                    deleteCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryId.Text);
            string name = txtCategoryName.Text;

            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Sửa danh mục thành công");
                LoadCategory();
                LoadCategoryIntoCombobox(cbFoodCategory);
                if (updateCategory != null)
                {
                    updateCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa danh mục");
            }
        }

        private void btnShowRoom_Click(object sender, EventArgs e)
        {
            LoadRoom();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            string name = txtRoomName.Text;
            float price = (float)nmRoomPrice.Value;

            if (RoomDAO.Instance.InsertRoom(name, price))
            {
                MessageBox.Show("Thêm phòng thành công");
                LoadRoom();
                if (inserRoom != null)
                {
                    inserRoom(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm phòng");
            }
        }

        private void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtRoomId.Text);

            if (txtRoomStatus.Text == "Có người")
            {
                MessageBox.Show("Không thể xóa phòng đang sử dụng!");
                return;
            }

            if (RoomDAO.Instance.DeleteRoom(id))
            {
                MessageBox.Show("Xóa phòng thành công");
                LoadRoom();
                if (deleteRoom != null)
                {
                    deleteRoom(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa phòng");
            }
        }

        private void btnEditRoom_Click(object sender, EventArgs e)
        {
            string name = txtRoomName.Text;
            float price = (float)nmRoomPrice.Value;
            int id = Convert.ToInt32(txtRoomId.Text);
            if(txtRoomStatus.Text == "Có người")
            {
                MessageBox.Show("Không thể sửa phòng đang sử dụng!");
                return;
            }
            if (RoomDAO.Instance.UpdateRoom(id, name, price))
            {
                MessageBox.Show("Sửa phòng thành công");
                LoadRoom();
                if (updateRoom != null)
                {
                    updateRoom(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa phòng");
            }
        }
    }
}
