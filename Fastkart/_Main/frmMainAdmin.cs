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

namespace Fastkart
{
    public partial class frmMainAdmin : Form
    {
        // Trạng thái menu
        private Panel currentSubMenuPanel = null;
        private Button currentParentButton = null;
        private Label currentArrowLabel = null;
        private Button currentActiveButton = null;

        // Định nghĩa màu sắc
        private Color parentColor = Color.FromArgb(46, 139, 87);
        private Color parentHoverColor = Color.FromArgb(34, 102, 64);
        private Color childColor = Color.FromArgb(60, 150, 100);

        // === THAY ĐỔI THEO YÊU CẦU CỦA BẠN ===
        // Đặt màu hover của button CON giống hệt màu hover của button CHA
        private Color childHoverColor = Color.FromArgb(34, 102, 64); // Giống parentHoverColor

        private Color activeColor = Color.FromArgb(52, 152, 219);

        public frmMainAdmin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            CollapseAllSubMenus();
            AddHoverEvents();
        }

        // Sự kiện Form Load
        private void frmMainAdmin_Load(object sender, EventArgs e)
        {
            picUser.Paint += new PaintEventHandler(picUser_Paint);
            if (!this.DesignMode)
            {
                btnDashboard.PerformClick();
            }
        }

        // Làm tròn ảnh
        private void picUser_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, picUser.Width - 1, picUser.Height - 1);
            picUser.Region = new Region(gp);
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
                currentArrowLabel.Text = "❯";
            }
            currentSubMenuPanel = null;
            currentParentButton = null;
            currentArrowLabel = null;
        }

        // Mở một menu con MỚI
        private void ExpandSubMenu(Panel subMenu, Label arrowLabel)
        {
            int expandedHeight = subMenu.Controls.OfType<Button>().Sum(b => b.Height);

            subMenu.Height = expandedHeight;
            arrowLabel.Text = "▼";
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

        // Sự kiện Click của các button CHA (trỏ vào hàm chung)
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
                btn.BackColor = parentColor;
                btn.ForeColor = Color.White;
                foreach (Label lbl in btn.Controls.OfType<Label>())
                {
                    lbl.BackColor = Color.Transparent;
                }
            }
            foreach (Panel pnl in pnlSidebar.Controls.OfType<Panel>())
            {
                foreach (Button btn in pnl.Controls.OfType<Button>())
                {
                    btn.BackColor = childColor;
                }
            }
        }

        // Highlight button được chọn
        private void HighlightActiveButton(Button activeButton)
        {
            if (activeButton == null) return;

            ResetButtonColors();

            activeButton.BackColor = activeColor;
            activeButton.ForeColor = Color.White;
            currentActiveButton = activeButton;

            Label arrowLabel = activeButton.Controls.OfType<Label>().FirstOrDefault();
            if (arrowLabel != null)
            {
                arrowLabel.BackColor = activeColor;
            }

            Control parentPanel = activeButton.Parent;
            if (parentPanel == pnlProductSub)
            {
                btnProduct.BackColor = parentHoverColor;
                lblProductArrow.BackColor = parentHoverColor;
            }
            else if (parentPanel == pnlCategorySub)
            {
                btnCategory.BackColor = parentHoverColor;
                lblCategoryArrow.BackColor = parentHoverColor;
            }
            else if (parentPanel == pnlAttributesSub)
            {
                btnAttributes.BackColor = parentHoverColor;
                lblAttributesArrow.BackColor = parentHoverColor;
            }
            else if (parentPanel == pnlUserSub)
            {
                btnUser.BackColor = parentHoverColor;
                lblUserArrow.BackColor = parentHoverColor;
            }
            else if (parentPanel == pnlRolesSub)
            {
                btnRoles.BackColor = parentHoverColor;
                lblRolesArrow.BackColor = parentHoverColor;
            }
        }


        // Hàm này gắn tất cả sự kiện hover
        private void AddHoverEvents()
        {
            // Button cha (và label mũi tên của chúng)
            AddHoverToParentButton(btnDashboard, null, parentColor, parentHoverColor);
            AddHoverToParentButton(btnProduct, lblProductArrow, parentColor, parentHoverColor);
            AddHoverToParentButton(btnCategory, lblCategoryArrow, parentColor, parentHoverColor);
            AddHoverToParentButton(btnAttributes, lblAttributesArrow, parentColor, parentHoverColor);
            AddHoverToParentButton(btnUser, lblUserArrow, parentColor, parentHoverColor);
            AddHoverToParentButton(btnRoles, lblRolesArrow, parentColor, parentHoverColor);

            // Button con (sử dụng childHoverColor đã được cập nhật)
            AddHoverToChildButton(btnProducts, childColor, childHoverColor);
            AddHoverToChildButton(btnAddProduct, childColor, childHoverColor);
            AddHoverToChildButton(btnCategoryList, childColor, childHoverColor);
            AddHoverToChildButton(btnAddCategory, childColor, childHoverColor);
            AddHoverToChildButton(btnAttributesList, childColor, childHoverColor);
            AddHoverToChildButton(btnAddAttribute, childColor, childHoverColor);
            AddHoverToChildButton(btnAllUser, childColor, childHoverColor);
            AddHoverToChildButton(btnAddUser, childColor, childHoverColor);
            AddHoverToChildButton(btnAllRoles, childColor, childHoverColor);
            AddHoverToChildButton(btnCreateRole, childColor, childHoverColor);
        }

        // Hàm gán sự kiện hover cho Button CHA (và label con)
        private void AddHoverToParentButton(Button btn, Label lbl, Color normalColor, Color hoverColor)
        {
            if (btn == null) return;

            btn.MouseEnter += (s, e) =>
            {
                if (btn != currentActiveButton)
                {
                    btn.BackColor = hoverColor;
                    if (lbl != null) lbl.BackColor = hoverColor;
                }
            };

            // === SỬA LỖI CRASH TRONG LOGIC MOUSELEAVE ===
            btn.MouseLeave += (s, e) =>
            {
                if (btn != currentActiveButton)
                {
                    btn.BackColor = normalColor;
                    if (lbl != null) lbl.BackColor = Color.Transparent;
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
                        btn.BackColor = hoverColor;
                        if (lbl != null) lbl.BackColor = hoverColor;
                    }
                }
            };

            if (lbl != null)
            {
                lbl.MouseEnter += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.BackColor = hoverColor;
                        lbl.BackColor = hoverColor;
                    }
                };

                // === SỬA LỖI CRASH TRONG LOGIC MOUSELEAVE ===
                lbl.MouseLeave += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.BackColor = normalColor;
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
                            btn.BackColor = hoverColor;
                            lbl.BackColor = hoverColor;
                        }
                    }
                };
            }
        }

        // Hàm gán sự kiện hover cho Button CON
        private void AddHoverToChildButton(Button btn, Color normalColor, Color hoverColor)
        {
            if (btn == null) return;
            btn.MouseEnter += (s, e) => { if (btn != currentActiveButton) btn.BackColor = hoverColor; };
            btn.MouseLeave += (s, e) => { if (btn != currentActiveButton) btn.BackColor = normalColor; };
        }


        #endregion

        #region Sự kiện Click mở Form con

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnDashboard);
            CollapseCurrentSubMenu();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnProducts);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnAddProduct);
        }

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnCategoryList);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnAddCategory);
        }

        private void btnAttributesList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnAttributesList);
        }

        private void btnAddAttribute_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnAddAttribute);
        }

        private void btnAllUser_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnAllUser);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnAddUser);
        }

        private void btnAllRoles_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnAllRoles);
        }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form() { BackColor = Color.White }, btnCreateRole);
        }

        #endregion
    }
}