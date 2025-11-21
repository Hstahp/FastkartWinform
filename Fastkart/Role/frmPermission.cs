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


                // Cột ID ẩn
                dgvMatrix.Columns.Add("colFuncId", "FuncID"); dgvMatrix.Columns["colFuncId"].Visible = false;
                dgvMatrix.Columns.Add("colTypeId", "TypeID"); dgvMatrix.Columns["colTypeId"].Visible = false;

                // Cột hiển thị
                var colFunc = new DataGridViewTextBoxColumn { HeaderText = "CHỨC NĂNG", Name = "colFuncName", ReadOnly = true, Width = 250 };
                colFunc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                colFunc.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); // In đậm tên chức năng
                dgvMatrix.Columns.Add(colFunc);

                var colType = new DataGridViewTextBoxColumn { HeaderText = "QUYỀN", Name = "colTypeName", ReadOnly = true, Width = 150 };
                dgvMatrix.Columns.Add(colType);

                
                var roles = _roleBLL.GetAllRoles();

                foreach (var role in roles)
                {
                    var colCheck = new DataGridViewCheckBoxColumn();
                    colCheck.HeaderText = role.RoleName; // Header là tên Role (Admin, Kế toán...)
                    colCheck.Name = "role_" + role.Uid;  // Đặt tên theo ID role
                    colCheck.Tag = role.Uid;             // Lưu ID vào Tag
                    colCheck.Width = 120;
                    dgvMatrix.Columns.Add(colCheck);
                }

                // 3. Đổ dữ liệu
                var matrixData = _roleBLL.GetPermissionMatrix();

                string currentFuncName = ""; // Dùng để group visually

                foreach (var item in matrixData)
                {
                    if (item.PermissionTypeName.Equals("Permission", StringComparison.OrdinalIgnoreCase))
                    {
                        continue; 
                    }
                    int idx = dgvMatrix.Rows.Add();
                    var row = dgvMatrix.Rows[idx];

                    row.Cells["colFuncId"].Value = item.FunctionId;
                    row.Cells["colTypeId"].Value = item.PermissionTypeId;

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
                MessageBox.Show("Lỗi load phân quyền: " + ex.Message);
            }
        }

        private void dgvMatrix_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvMatrix.IsCurrentCellDirty)
            {
                dgvMatrix.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvMatrix_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = dgvMatrix.Columns[e.ColumnIndex];

            if (col.Name.StartsWith("role_") && col.Tag != null)
            {
                int roleId = (int)col.Tag;

                var row = dgvMatrix.Rows[e.RowIndex];
                int funcId = Convert.ToInt32(row.Cells["colFuncId"].Value);
                int typeId = Convert.ToInt32(row.Cells["colTypeId"].Value);

                bool isChecked = Convert.ToBoolean(row.Cells[e.ColumnIndex].Value);

                
                _roleBLL.UpdatePermission(roleId, funcId, typeId, isChecked);
            }
        }
    }
}