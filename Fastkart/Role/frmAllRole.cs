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
           if (!UserSessionDTO.HasPermission(PermCode.FUNC_ROLE, PermCode.TYPE_VIEW))
            {
                MessageBox.Show("Bạn không có quyền truy cập trang này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return; 
            }

            if (!UserSessionDTO.HasPermission(PermCode.FUNC_ROLE, PermCode.TYPE_CREATE))
            {
                btnAdd.Visible = false;
            }

            bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_ROLE, PermCode.TYPE_DELETE);
            if (dgvRoles.Columns.Contains("colDelete"))
            {
                dgvRoles.Columns["colDelete"].Visible = canDelete;
            }

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
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDataRoles(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddRole f = new frmAddRole();

            var result = f.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadDataRoles();
            }
        }

        private void dgvRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int roleId = 0;
            if (dgvRoles.Rows[e.RowIndex].Cells[0].Value != null)
            {
                roleId = Convert.ToInt32(dgvRoles.Rows[e.RowIndex].Cells[0].Value);
            }

            
            if (dgvRoles.Columns[e.ColumnIndex] == colDelete)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa vai trò này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_roleBLL.DeleteRole(roleId))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadDataRoles();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }

            if (dgvRoles.Columns[e.ColumnIndex] == colEdit)
            {
                MessageBox.Show("Chức năng sửa đang phát triển! ID: " + roleId);
            }
        }
    }
}