using System;
using System.Windows.Forms;
using BLL;
using DTO;
using Common;

namespace GUI
{
    public partial class frmAllRole : Form
    {
        private RoleBLL _roleBLL;

        public frmAllRole()
        {
            InitializeComponent();
            _roleBLL = new RoleBLL();
            this.Load += FrmAllRole_Load;
        }

        private void FrmAllRole_Load(object sender, EventArgs e)
        {
            // 1. Check Permission: VIEW
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_ROLE, PermCode.TYPE_VIEW))
            {
                MessageBox.Show("You do not have permission to access this page!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // 2. Check Permission: CREATE (Hide Add Button)
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_ROLE, PermCode.TYPE_CREATE))
            {
                btnAdd.Visible = false;
            }

            // 3. Check Permission: DELETE (Hide Delete Column)
            bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_ROLE, PermCode.TYPE_DELETE);
            if (dgvRoles.Columns.Contains("colDelete"))
            {
                dgvRoles.Columns["colDelete"].Visible = canDelete;
            }

            // 4. Check Permission: EDIT (Hide Edit Column)
            bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_ROLE, PermCode.TYPE_EDIT);
            if (dgvRoles.Columns.Contains("colEdit"))
            {
                dgvRoles.Columns["colEdit"].Visible = canEdit;
            }

            LoadDataRoles();
        }

        private void LoadDataRoles(string keyword = "")
        {
            try
            {
                dgvRoles.AutoGenerateColumns = false;
                dgvRoles.DataSource = _roleBLL.GetAllRoles(keyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDataRoles(txtSearch.Text.Trim());
        }

        // --- BUTTON ADD ---
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Pass 0 and empty string to indicate ADD MODE
            frmAddRole f = new frmAddRole(0, "");

            var result = f.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadDataRoles();
            }
        }

        // --- GRID ACTIONS (DELETE / EDIT) ---
        private void dgvRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Variables to store data from the clicked row
            int roleId = 0;
            string currentRoleName = "";

            // 1. Get Role ID (Uid) - Assuming Column 0 is hidden ID
            if (dgvRoles.Rows[e.RowIndex].Cells[0].Value != null)
            {
                roleId = Convert.ToInt32(dgvRoles.Rows[e.RowIndex].Cells[0].Value);
            }

            // 2. Get Role Name - Assuming Column 1 is Name
            // NOTE: Check your DataGridView Design to ensure Column Index 1 is RoleName
            if (dgvRoles.Rows[e.RowIndex].Cells[1].Value != null)
            {
                currentRoleName = dgvRoles.Rows[e.RowIndex].Cells[1].Value.ToString();
            }

            // --- DELETE LOGIC ---
            // Check if clicked column is 'colDelete' (Check Name property in Design)
            if (dgvRoles.Columns[e.ColumnIndex].Name == "colDelete")
            {
                if (MessageBox.Show("Are you sure you want to delete this role?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string error = "";
                    // Gọi hàm xóa mới có biến error
                    if (_roleBLL.DeleteRole(roleId, out error))
                    {
                        MessageBox.Show("Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataRoles();
                    }
                    else
                    {
                        // Hiện thông báo lỗi nghiệp vụ (Vd: Đang có người dùng...)
                        MessageBox.Show("Delete failed: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            // --- EDIT LOGIC ---
            // Check if clicked column is 'colEdit'
            if (dgvRoles.Columns[e.ColumnIndex].Name == "colEdit")
            {
                // Pass RoleId and CurrentName to indicate EDIT MODE
                frmAddRole f = new frmAddRole(roleId, currentRoleName);

                var result = f.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadDataRoles(); // Reload grid to show updated name
                }
            }
        }
    }
}