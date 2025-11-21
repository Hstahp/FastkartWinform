using System;
using System.Windows.Forms;
using BLL;

namespace GUI
{
    public partial class frmAddRole : Form
    {
        private RoleBLL _roleBLL;

        public frmAddRole()
        {
            InitializeComponent();
            _roleBLL = new RoleBLL();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string roleName = txtRoleName.Text;
            string errorMsg = "";

            if (_roleBLL.AddNewRole(roleName, out errorMsg))
            {
                MessageBox.Show("Thêm vai trò mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                txtRoleName.Text = "";
                //this.Close(); 
            }
            else
            {
                MessageBox.Show("Lỗi: " + errorMsg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoleName.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtRoleName.Text = "";
            //this.Close();
        }
    }
}