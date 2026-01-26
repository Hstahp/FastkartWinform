using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class frmPermission : Form
    {
        private RoleBLL _roleBLL;

        public frmPermission()
        {
            InitializeComponent();
            _roleBLL = new RoleBLL();
            this.Load += FrmPermission_Load;
        }

        private void FrmPermission_Load(object sender, EventArgs e)
        {
            LoadMatrix();
        }

        private void LoadMatrix()
        {
            try
            {
                dgvMatrix.Columns.Clear();
                dgvMatrix.Rows.Clear();

                // 1. Hidden IDs Columns
                dgvMatrix.Columns.Add("colFuncId", "FuncID");
                dgvMatrix.Columns["colFuncId"].Visible = false;

                dgvMatrix.Columns.Add("colTypeId", "TypeID");
                dgvMatrix.Columns["colTypeId"].Visible = false;

                // 2. Visible Columns (Translated to English)
                var colFunc = new DataGridViewTextBoxColumn
                {
                    HeaderText = "FUNCTION", // Translated: CHỨC NĂNG
                    Name = "colFuncName",
                    ReadOnly = true,
                    Width = 250
                };
                colFunc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                colFunc.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvMatrix.Columns.Add(colFunc);

                var colType = new DataGridViewTextBoxColumn
                {
                    HeaderText = "PERMISSION", // Translated: QUYỀN
                    Name = "colTypeName",
                    ReadOnly = true,
                    Width = 150
                };
                dgvMatrix.Columns.Add(colType);

                // 3. Dynamic Columns based on Roles (Admin, Staff, etc.)
                var roles = _roleBLL.GetAllRoles();

                foreach (var role in roles)
                {
                    var colCheck = new DataGridViewCheckBoxColumn();
                    colCheck.HeaderText = role.RoleName;
                    colCheck.Name = "role_" + role.Uid;
                    colCheck.Tag = role.Uid;
                    colCheck.Width = 120;
                    dgvMatrix.Columns.Add(colCheck);
                }

                // 4. Populate Data
                var matrixData = _roleBLL.GetPermissionMatrix();

                string currentFuncName = ""; // Used for visual grouping

                foreach (var item in matrixData)
                {
                    // Skip if necessary based on your business logic
                    if (item.PermissionTypeName.Equals("Permission", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    int idx = dgvMatrix.Rows.Add();
                    var row = dgvMatrix.Rows[idx];

                    row.Cells["colFuncId"].Value = item.FunctionId;
                    row.Cells["colTypeId"].Value = item.PermissionTypeId;

                    // Grouping Logic: Only show Function Name once per group
                    if (item.FunctionName != currentFuncName)
                    {
                        row.Cells["colFuncName"].Value = item.FunctionName;
                        currentFuncName = item.FunctionName;
                    }
                    else
                    {
                        row.Cells["colFuncName"].Value = "";
                    }

                    row.Cells["colTypeName"].Value = item.PermissionTypeName;

                    // Check/Uncheck boxes based on database values
                    foreach (var rolePerm in item.RolePermissions)
                    {
                        int roleId = rolePerm.Key;
                        bool isAllowed = rolePerm.Value;

                        string colName = "role_" + roleId;
                        if (dgvMatrix.Columns.Contains(colName))
                        {
                            row.Cells[colName].Value = isAllowed;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading permissions: " + ex.Message); // Translated
            }
        }

        private void dgvMatrix_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Commits the edit immediately when checkbox is clicked
            if (dgvMatrix.IsCurrentCellDirty)
            {
                dgvMatrix.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvMatrix_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = dgvMatrix.Columns[e.ColumnIndex];

            // Only process if a Role Column (checkbox) was changed
            if (col.Name.StartsWith("role_") && col.Tag != null)
            {
                int roleId = (int)col.Tag;

                var row = dgvMatrix.Rows[e.RowIndex];
                int funcId = Convert.ToInt32(row.Cells["colFuncId"].Value);
                int typeId = Convert.ToInt32(row.Cells["colTypeId"].Value);

                bool isChecked = Convert.ToBoolean(row.Cells[e.ColumnIndex].Value);

                // Update Database
                _roleBLL.UpdatePermission(roleId, funcId, typeId, isChecked);
            }
        }
    }
}