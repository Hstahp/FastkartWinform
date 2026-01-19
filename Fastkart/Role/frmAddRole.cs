using System;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class frmAddRole : Form
    {
        private RoleBLL _roleBLL;
        private int _uid = 0; // 0 = ADD, >0 = EDIT

        public frmAddRole(int uid = 0, string currentName = "")
        {
            InitializeComponent();
            _roleBLL = new RoleBLL();
            _uid = uid;

            // Setup UI
            if (_uid > 0)
            {
                this.Text = "Edit Role";
                btnSave.Text = "Update";
                txtRoleName.Text = currentName;
            }
            else
            {
                this.Text = "Add New Role";
                btnSave.Text = "Add";
                txtRoleName.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string error = "";
            RoleDTO roleDto = new RoleDTO();
            roleDto.RoleName = txtRoleName.Text;
            roleDto.Uid = _uid; // Map Uid correctly

            bool result = false;

            if (_uid == 0)
            {
                // ADD logic
                result = _roleBLL.AddNewRole(roleDto.RoleName, out error);
            }
            else
            {
                // EDIT logic
                result = _roleBLL.EditRole(roleDto, out error);
            }

            if (result)
            {
                MessageBox.Show("Success!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}