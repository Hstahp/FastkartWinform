using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI;

namespace Fastkart
{
    public partial class frmMainAdmin : Form
    {
        // Trạng thái menu
        private Panel currentSubMenuPanel = null;
        private Button currentParentButton = null;
        private Label currentArrowLabel = null;
        private Button currentActiveButton = null;

        private Color sidebarBg = Color.FromArgb(31, 41, 55);     
        private Color sidebarHover = Color.FromArgb(55, 65, 81);      
        private Color submenuBg = Color.FromArgb(17, 24, 39);        
        private Color activeColor = Color.FromArgb(59, 130, 246);     
        private Color activeBg = Color.FromArgb(37, 99, 235);        
        private Color textNormal = Color.FromArgb(229, 231, 235);     
        private Color textMuted = Color.FromArgb(156, 163, 175);       

        public frmMainAdmin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            CollapseAllSubMenus();
            AddHoverEvents();
            InitializeSearchBox();
 
            this.FormClosing += frmMainAdmin_FormClosing;
        }

        private void frmMainAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hiển thị xác nhận đăng xuất
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Tìm hoặc tạo mới form Login
                Form loginForm = Application.OpenForms["frmLogin"];
                if (loginForm != null)
                {
                    loginForm.Show();
                }
                else
                {
                    frmLogin newLoginForm = new frmLogin();
                    newLoginForm.Show();
                }
            }
            else
            {
                // Hủy việc đóng form
                e.Cancel = true;
            }
        }

        /*
        private void ClearUserSession()
        {
            // Ví dụ: Xóa thông tin user đã lưu
            Properties.Settings.Default.LoggedInUserId = 0;
            Properties.Settings.Default.LoggedInUserEmail = string.Empty;
            Properties.Settings.Default.Save();
        }
        */

        // Sự kiện Form Load
        private void frmMainAdmin_Load(object sender, EventArgs e)
        {
            picUser.Paint += new PaintEventHandler(picUser_Paint);
            if (!this.DesignMode)
            {
                btnDashboard.PerformClick();
            }
        }

        // Làm tròn ảnh user
        private void picUser_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, picUser.Width - 1, picUser.Height - 1);
            picUser.Region = new Region(gp);

            // Thêm border cho ảnh
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(Color.FromArgb(209, 213, 219), 2))
            {
                e.Graphics.DrawEllipse(pen, 1, 1, picUser.Width - 3, picUser.Height - 3);
            }
        }

        // Khởi tạo search box
        private void InitializeSearchBox()
        {
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == "🔍  Search products, orders, customers...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.FromArgb(31, 41, 55);
                }
            };

            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "🔍  Search products, orders, customers...";
                    txtSearch.ForeColor = Color.FromArgb(107, 114, 128);
                }
            };

            // Thêm padding cho TextBox
            txtSearch.Padding = new Padding(12, 8, 12, 8);
            txtSearch.Height = 40;
        }

        // Mở 1 form con vào pnlMainContent
        private void OpenChildForm(Form childForm, Button clickedButton)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlMainContent.Controls.Clear();
            pnlMainContent.Controls.Add(childForm);
            pnlMainContent.Tag = childForm;

            childForm.Show();
            HighlightActiveButton(clickedButton);
        }

        #region Xử lý Submenu

        // Đóng tất cả menu con khi khởi động
        private void CollapseAllSubMenus()
        {
            pnlProductSub.Height = 0;
            pnlCategorySub.Height = 0;
            pnlAttributesSub.Height = 0;
            pnlUserSub.Height = 0;
            pnlRolesSub.Height = 0;
        }

        // Đóng menu con HIỆN TẠI đang mở
        private void CollapseCurrentSubMenu()
        {
            if (currentSubMenuPanel != null)
            {
                currentSubMenuPanel.Height = 0;
            }
            if (currentArrowLabel != null)
            {
                currentArrowLabel.Text = "›";
            }
            currentSubMenuPanel = null;
            currentParentButton = null;
            currentArrowLabel = null;
        }

        // Mở một menu con MỚI
        private void ExpandSubMenu(Panel subMenu, Label arrowLabel)
        {
            int expandedHeight = subMenu.Controls.OfType<Button>().Sum(b => b.Height);
            expandedHeight += subMenu.Padding.Top + subMenu.Padding.Bottom;

            subMenu.Height = expandedHeight;
            arrowLabel.Text = "▾";
            currentSubMenuPanel = subMenu;
            currentArrowLabel = arrowLabel;
            currentParentButton = (Button)arrowLabel.Parent;
        }

        // Hàm Click chung cho các button CHA
        private void HandleParentMenuClick(Panel subMenu, Label arrowLabel)
        {
            bool isAlreadyOpen = (currentSubMenuPanel == subMenu);
            CollapseCurrentSubMenu();
            if (!isAlreadyOpen)
            {
                ExpandSubMenu(subMenu, arrowLabel);
            }
        }

        // Sự kiện Click của các button CHA
        private void btnProduct_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlProductSub, lblProductArrow);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlCategorySub, lblCategoryArrow);
        }

        private void btnAttributes_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlAttributesSub, lblAttributesArrow);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlUserSub, lblUserArrow);
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlRolesSub, lblRolesArrow);
        }

        #endregion

        #region Xử lý Hover & Highlight

        // Reset màu tất cả các button
        private void ResetButtonColors()
        {
            foreach (Button btn in pnlSidebar.Controls.OfType<Button>())
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = textNormal;
                foreach (Label lbl in btn.Controls.OfType<Label>())
                {
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = textMuted;
                }
            }
            foreach (Panel pnl in pnlSidebar.Controls.OfType<Panel>())
            {
                foreach (Button btn in pnl.Controls.OfType<Button>())
                {
                    btn.BackColor = Color.Transparent;
                    btn.ForeColor = Color.FromArgb(209, 213, 219);
                }
            }
        }

        // Highlight button được chọn
        private void HighlightActiveButton(Button activeButton)
        {
            if (activeButton == null) return;

            ResetButtonColors();

            activeButton.BackColor = activeBg;
            activeButton.ForeColor = Color.White;
            currentActiveButton = activeButton;

            Label arrowLabel = activeButton.Controls.OfType<Label>().FirstOrDefault();
            if (arrowLabel != null)
            {
                arrowLabel.BackColor = activeBg;
                arrowLabel.ForeColor = Color.White;
            }

            // Highlight parent button nếu là submenu item
            Control parentPanel = activeButton.Parent;
            if (parentPanel == pnlProductSub)
            {
                btnProduct.BackColor = sidebarHover;
                lblProductArrow.BackColor = sidebarHover;
            }
            else if (parentPanel == pnlCategorySub)
            {
                btnCategory.BackColor = sidebarHover;
                lblCategoryArrow.BackColor = sidebarHover;
            }
            else if (parentPanel == pnlAttributesSub)
            {
                btnAttributes.BackColor = sidebarHover;
                lblAttributesArrow.BackColor = sidebarHover;
            }
            else if (parentPanel == pnlUserSub)
            {
                btnUser.BackColor = sidebarHover;
                lblUserArrow.BackColor = sidebarHover;
            }
            else if (parentPanel == pnlRolesSub)
            {
                btnRoles.BackColor = sidebarHover;
                lblRolesArrow.BackColor = sidebarHover;
            }
        }

        // Gắn tất cả sự kiện hover
        private void AddHoverEvents()
        {
            // Button cha
            AddHoverToParentButton(btnDashboard, null);
            AddHoverToParentButton(btnProduct, lblProductArrow);
            AddHoverToParentButton(btnCategory, lblCategoryArrow);
            AddHoverToParentButton(btnAttributes, lblAttributesArrow);
            AddHoverToParentButton(btnUser, lblUserArrow);
            AddHoverToParentButton(btnRoles, lblRolesArrow);

            // Button con
            AddHoverToChildButton(btnProducts);
            AddHoverToChildButton(btnAddProduct);
            AddHoverToChildButton(btnCategoryList);
            AddHoverToChildButton(btnAddCategory);
            AddHoverToChildButton(btnAttributesList);
            AddHoverToChildButton(btnAddAttribute);
            AddHoverToChildButton(btnAllUser);
            AddHoverToChildButton(btnAddUser);
            AddHoverToChildButton(btnAllRoles);
            AddHoverToChildButton(btnCreateRole);
        }

        // Hover cho Button CHA
        private void AddHoverToParentButton(Button btn, Label lbl)
        {
            if (btn == null) return;

            btn.MouseEnter += (s, e) =>
            {
                if (btn != currentActiveButton)
                {
                    btn.BackColor = sidebarHover;
                    if (lbl != null) lbl.BackColor = sidebarHover;
                }
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != currentActiveButton)
                {
                    btn.BackColor = Color.Transparent;
                    if (lbl != null) lbl.BackColor = Color.Transparent;
                }

                // Keep parent highlighted nếu child đang active
                if (currentActiveButton != null && currentActiveButton.Parent != pnlSidebar)
                {
                    Button parentBtnToKeepHovered = null;
                    if (currentActiveButton.Parent == pnlProductSub) parentBtnToKeepHovered = btnProduct;
                    else if (currentActiveButton.Parent == pnlCategorySub) parentBtnToKeepHovered = btnCategory;
                    else if (currentActiveButton.Parent == pnlAttributesSub) parentBtnToKeepHovered = btnAttributes;
                    else if (currentActiveButton.Parent == pnlUserSub) parentBtnToKeepHovered = btnUser;
                    else if (currentActiveButton.Parent == pnlRolesSub) parentBtnToKeepHovered = btnRoles;

                    if (btn == parentBtnToKeepHovered)
                    {
                        btn.BackColor = sidebarHover;
                        if (lbl != null) lbl.BackColor = sidebarHover;
                    }
                }
            };

            if (lbl != null)
            {
                lbl.MouseEnter += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.BackColor = sidebarHover;
                        lbl.BackColor = sidebarHover;
                    }
                };

                lbl.MouseLeave += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.BackColor = Color.Transparent;
                        lbl.BackColor = Color.Transparent;
                    }

                    if (currentActiveButton != null && currentActiveButton.Parent != pnlSidebar)
                    {
                        Button parentBtnToKeepHovered = null;
                        if (currentActiveButton.Parent == pnlProductSub) parentBtnToKeepHovered = btnProduct;
                        else if (currentActiveButton.Parent == pnlCategorySub) parentBtnToKeepHovered = btnCategory;
                        else if (currentActiveButton.Parent == pnlAttributesSub) parentBtnToKeepHovered = btnAttributes;
                        else if (currentActiveButton.Parent == pnlUserSub) parentBtnToKeepHovered = btnUser;
                        else if (currentActiveButton.Parent == pnlRolesSub) parentBtnToKeepHovered = btnRoles;

                        if (btn == parentBtnToKeepHovered)
                        {
                            btn.BackColor = sidebarHover;
                            lbl.BackColor = sidebarHover;
                        }
                    }
                };
            }
        }

        // Hover cho Button CON
        private void AddHoverToChildButton(Button btn)
        {
            if (btn == null) return;

            btn.MouseEnter += (s, e) =>
            {
                if (btn != currentActiveButton)
                    btn.BackColor = sidebarHover;
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != currentActiveButton)
                    btn.BackColor = Color.Transparent;
            };
        }

        #endregion

        #region Sự kiện Click mở Form con

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnDashboard);
            CollapseCurrentSubMenu();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnProducts);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnAddProduct);
        }

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnCategoryList);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnAddCategory);
        }

        private void btnAttributesList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnAttributesList);
        }

        private void btnAddAttribute_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnAddAttribute);
        }

        private void btnAllUser_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnAllUser);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnAddUser);
        }

        private void btnAllRoles_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnAllRoles);
        }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.FromArgb(249, 250, 251) }, btnCreateRole);
        }

        #endregion
    }
}