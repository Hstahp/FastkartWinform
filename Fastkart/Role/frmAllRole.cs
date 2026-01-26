using System;
using System.Drawing; // Required for Cursors
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

            // Prevent auto generation
            dgvRoles.AutoGenerateColumns = false;

            // Register Hand Cursor Effect for Icons
            dgvRoles.CellMouseEnter += dgvRoles_CellMouseEnter;
            dgvRoles.CellMouseLeave += dgvRoles_CellMouseLeave;

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
                // Re-initialize BLL to get fresh data
                _roleBLL = new RoleBLL();
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
            // Ignore Header Click
            if (e.RowIndex < 0) return;

            // Safety check for ID
            if (dgvRoles.Rows[e.RowIndex].Cells["colId"].Value == null) return;
            int roleId = Convert.ToInt32(dgvRoles.Rows[e.RowIndex].Cells["colId"].Value);

            string currentRoleName = "";
            if (dgvRoles.Rows[e.RowIndex].Cells["colName"].Value != null)
            {
                currentRoleName = dgvRoles.Rows[e.RowIndex].Cells["colName"].Value.ToString();
            }

            string colName = dgvRoles.Columns[e.ColumnIndex].Name;

            // --- DELETE LOGIC ---
            if (colName == "colDelete")
            {
                if (MessageBox.Show("Are you sure you want to delete this role?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string error = "";
                    if (_roleBLL.DeleteRole(roleId, out error))
                    {
                        // MessageBox.Show("Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataRoles();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // --- EDIT LOGIC ---
            else if (colName == "colEdit")
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

        // --- HAND CURSOR EFFECT ---
        private void dgvRoles_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string colName = dgvRoles.Columns[e.ColumnIndex].Name;
                if (colName == "colEdit" || colName == "colDelete")
                {
                    dgvRoles.Cursor = Cursors.Hand;
                }
            }
        }

        private void dgvRoles_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvRoles.Cursor = Cursors.Default;
        }
    }
}