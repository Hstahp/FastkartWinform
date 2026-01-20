using BLL;
using Common;
using DTO;
using GUI.Category;
using GUI.Product;
using GUI.ScanQR;
using GUI.SubCategory;
using GUI.Coupon;
using Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmMainAdmin : Form
    {
        private Panel currentSubMenuPanel = null;
        private Button currentParentButton = null;
        private Label currentArrowLabel = null;
        private Button currentActiveButton = null;
        private Panel pnlUserDropdown;
        private bool isUserDropdownVisible = false;
        
        private frmPOS _currentPosForm;
        private frmScanQR _currentScanQRForm;
        private Color sidebarBg = Color.FromArgb(31, 41, 55);
        private Color sidebarHover = Color.FromArgb(55, 65, 81);
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
            InitializeUserInfo();
            InitializeUserDropdown();

            this.FormClosing += frmMainAdmin_FormClosing;
            this.FormClosed += frmMainAdmin_FormClosed;
        }

        private void frmMainAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn đăng xuất?",
                    "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    UserSession.Clear();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void frmMainAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin loginForm = Application.OpenForms.OfType<frmLogin>().FirstOrDefault();
            if (loginForm != null)
            {
                loginForm.ResetForm(); // Xóa text cũ
                loginForm.Show();
            }
            else
            {
                new frmLogin().Show();
            }
        }

        private void AdjustUserInfoWidth()
        {
            // 1. Cài đặt giới hạn chiều rộng tối đa cho Tên (pixel)
           

            // 2. Đo chiều rộng thực tế của text
            Size textSize = TextRenderer.MeasureText(lblName.Text, lblName.Font);

            if (textSize.Width > AppConstants.MAX_NAME_WIDTH)
            {
                // TRƯỜNG HỢP TÊN DÀI: Cắt bớt và thêm ...
                lblName.AutoSize = false;
                lblName.Width = AppConstants.MAX_NAME_WIDTH;
                lblName.AutoEllipsis = true; // Tự động thêm dấu "..." ở cuối
            }
            else
            {
                // TRƯỜNG HỢP TÊN NGẮN: Hiển thị đầy đủ
                lblName.AutoSize = true;
                lblName.AutoEllipsis = false;
            }

            // 3. Căn chỉnh vị trí Role (luôn nằm thẳng hàng với Name)
            // picUser.Right ~ 60 + 12 padding = 72
            int textStartX = picUser.Right + 12;

            lblName.Left = textStartX;
            lblRole.Left = textStartX;

            // 4. Tính toán vị trí nút Mũi tên (Dropdown)
            // Nó sẽ nằm sau thành phần dài nhất (Name hoặc Role)
            int maxContentRight = Math.Max(lblName.Right, lblRole.Right);

            btnUserDropdown.Left = maxContentRight + 5; // Cách ra 5px

            // 5. Cập nhật độ rộng của cả Panel
            pnlUserInfo.Width = btnUserDropdown.Right + 20;
        }

 
        private async void InitializeUserInfo()
        {
            if (UserSession.CurrentUser != null)
            {
                lblName.Text = UserSession.CurrentUser.FullName;
                lblRole.Text = UserSession.CurrentUser.RoleName;

               
                AdjustUserInfoWidth();
             

                string jsonString = UserSession.CurrentUser.ImgUser;
                if (!string.IsNullOrEmpty(jsonString))
                {
                    try
                    {
                        string imageUrl = "";
                        var jsonArray = JArray.Parse(jsonString);
                        if (jsonArray.Count > 0) imageUrl = jsonArray[0].ToString();

                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            using (HttpClient client = new HttpClient())
                            {
                                byte[] imageData = await client.GetByteArrayAsync(imageUrl);
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    Image originalImage = Image.FromStream(ms);
                                    picUser.Image = CropToSquare(originalImage);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private Image CropToSquare(Image img)
        {
            int originalWidth = img.Width;
            int originalHeight = img.Height;
            int cropSize = Math.Min(originalWidth, originalHeight);
            int cropX = (originalWidth - cropSize) / 2;
            int cropY = (originalHeight - cropSize) / 2;

            Bitmap croppedBmp = new Bitmap(cropSize, cropSize);
            using (Graphics g = Graphics.FromImage(croppedBmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, cropSize, cropSize),
                            new Rectangle(cropX, cropY, cropSize, cropSize),
                            GraphicsUnit.Pixel);
            }
            return croppedBmp;
        }

  
        private void InitializeUserDropdown()
        {
 
            pnlUserDropdown = new Panel
            {
                Width = 200,
                Height = 0,
                BackColor = Color.White,
                Visible = false,
                Location = new Point(
                    pnlHeader.Right - 200 - pnlHeader.Padding.Right,
                    pnlHeader.Bottom
                )
            };

            Button btnMyInfo = CreateDropdownButton("👤  My Info", 0);
            Button btnSettings = CreateDropdownButton("⚙️  Settings", 1);
            Button btnLogout = CreateDropdownButton("🚪  Logout", 2);

            btnMyInfo.Click += (s, e) =>
            {
                frmMyInfo profileForm = new frmMyInfo();
                profileForm.ShowDialog(this);
                HideUserDropdown();
            };

            // Logic mở Setting từ Dropdown -> Kích hoạt Sidebar
            btnSettings.Click += (s, e) => {
                HideUserDropdown();
                if (currentSubMenuPanel != pnlSettingsSub)
                {
                    HandleParentMenuClick(pnlSettingsSub, lblSettingsArrow);
                }
                btnProfileSetting.PerformClick();
            };

            btnLogout.Click += BtnLogout_Click;

            pnlUserDropdown.Controls.Add(btnMyInfo);
            pnlUserDropdown.Controls.Add(btnSettings);
            pnlUserDropdown.Controls.Add(btnLogout);

            pnlUserDropdown.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(229, 231, 235), 1), 0, 0, pnlUserDropdown.Width - 1, pnlUserDropdown.Height - 1);
            };

            this.Controls.Add(pnlUserDropdown);
            pnlUserDropdown.BringToFront();

            pnlUserInfo.Click += (s, e) => ToggleUserDropdown();
            btnUserDropdown.Click += (s, e) => ToggleUserDropdown();
            picUser.Click += (s, e) => ToggleUserDropdown();
            lblName.Click += (s, e) => ToggleUserDropdown();
            lblRole.Click += (s, e) => ToggleUserDropdown();
        }

        private Button CreateDropdownButton(string text, int index)
        {
            Button btn = new Button
            {
                Text = text,
                Width = 200,
                Height = 45,
                Top = index * 45,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Font = new Font("Segoe UI", 9.75F),
                ForeColor = Color.FromArgb(31, 41, 55),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(249, 250, 251);
            return btn;
        }

        private void OpenAddNewProductForm()
        {
            if (currentSubMenuPanel != pnlProductSub)
            {
                HandleParentMenuClick(pnlProductSub, lblProductArrow);
            }

            OpenChildForm(new Product.frmCreate(), btnAddProduct);
        }

        private void OpenEditProductForm(int productId)
        {
            if (currentSubMenuPanel != pnlProductSub)
            {
                HandleParentMenuClick(pnlProductSub, lblProductArrow);
            }

            OpenChildForm(new Product.frmEdit(productId), btnProducts);
        }

        private void OpenAddNewCategoryForm()
        {
            if (currentSubMenuPanel != pnlCategorySub)
            {
                HandleParentMenuClick(pnlCategorySub, lblCategoryArrow);
            }

            OpenChildForm(new Category.frmCreate(), btnAddCategory);
        }

        private void OpenEditCategoryForm(int id)
        {
            if (currentSubMenuPanel != pnlCategorySub)
            {
                HandleParentMenuClick(pnlCategorySub, lblCategoryArrow);
            }

            OpenChildForm(new Category.frmEdit(id), btnAddCategory);
        }

        private void OpenAddNewSubcategoryForm()
        {
            if (currentSubMenuPanel != pnlSubCategorySub)
            {
                HandleParentMenuClick(pnlSubCategorySub, lblSubCategoryArrow);
            }

            OpenChildForm(new SubCategory.frmCreate(), btnAddSubCategory);
        }

        private void OpenEditSubcategoryForm(int id)
        {
            if (currentSubMenuPanel != pnlSubCategorySub)
            {
                HandleParentMenuClick(pnlSubCategorySub, lblSubCategoryArrow);
            }

            OpenChildForm(new SubCategory.frmEdit(id), btnAddSubCategory);
        }

        private void OpenAddNewUserForm()
        {
            if (currentSubMenuPanel != pnlUserSub)
            {
                HandleParentMenuClick(pnlUserSub, lblUserArrow);
            }
           
            OpenChildForm(new frmAddNewUser(), btnAddUser);
        }

        private void OpenAddNewRoleForm()
        {
            if (currentSubMenuPanel != pnlRolesSub)
            {
                HandleParentMenuClick(pnlRolesSub, lblUserArrow);
            }

            OpenChildForm(new frmAddRole(), btnCreateRole);
        }

        private void OpenAllRoleForm()
        {
            if (currentSubMenuPanel != pnlRolesSub)
            {
                HandleParentMenuClick(pnlRolesSub, lblUserArrow);
            }

            OpenChildForm(new frmAllRole(), btnAllRoles);
        }

        private void ToggleUserDropdown()
        {
            isUserDropdownVisible = !isUserDropdownVisible;
            if (isUserDropdownVisible)
            {
                pnlUserDropdown.Height = 135;
                pnlUserDropdown.Visible = true;
                pnlUserDropdown.BringToFront();
            }
            else
            {
                pnlUserDropdown.Height = 0;
                pnlUserDropdown.Visible = false;
            }
        }

        private void HideUserDropdown()
        {
            pnlUserDropdown.Height = 0;
            pnlUserDropdown.Visible = false;
            isUserDropdownVisible = false;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            HideUserDropdown();
            this.Close();
        }

        private void frmMainAdmin_Load(object sender, EventArgs e)
        {
            picUser.Paint += new PaintEventHandler(picUser_Paint);
            if (!this.DesignMode)
            {
                btnDashboard.PerformClick();
            }
            ApplySidebarPermissions();
        }

        private void ApplySidebarPermissions()
        {
            if (!UserSessionDTO.HasPermission("PRODUCT", "VIEW"))
            {
                btnProduct.Visible = false;  
                pnlProductSub.Visible = false;   
            }

            if (!UserSessionDTO.HasPermission("USER", "VIEW"))
            {
                btnUser.Visible = false;
                pnlUserSub.Visible = false;
            }

            if (!UserSessionDTO.HasPermission("ROLE", "VIEW"))
            {
                btnRoles.Visible = false;
                pnlRolesSub.Visible = false;
            }

            // ... Làm tương tự cho Category, Attributes ...
        }

        private void picUser_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, picUser.Width - 1, picUser.Height - 1);
            picUser.Region = new Region(gp);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(Color.FromArgb(209, 213, 219), 2))
            {
                e.Graphics.DrawEllipse(pen, 1, 1, picUser.Width - 3, picUser.Height - 3);
            }
        }

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

            txtSearch.Padding = new Padding(12, 8, 12, 8);
            txtSearch.Height = 40;
        }

        private void OpenChildForm(Form childForm, Button clickedButton)
        {
            HideUserDropdown();

            // BẮT BUỘC PHẢI CÓ DÒNG NÀY:
            childForm.TopLevel = false;

            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // (Phần còn lại giữ nguyên)
            if (childForm is frmProfileSetting profileForm)
            {
                profileForm.ProfileUpdated += (s, e) =>
                {
                    InitializeUserInfo();
                };
            }

            pnlMainContent.Controls.Clear();
            pnlMainContent.Controls.Add(childForm);
            pnlMainContent.Tag = childForm;

            childForm.Show();
            HighlightActiveButton(clickedButton);
        }

        

        // Hàm hỗ trợ tạo nút nhanh (Copy style của bạn)
        private Button CreateSidebarButton(string text)
        {
            return new Button
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                ForeColor = textNormal,
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Cursor = Cursors.Hand
            };
        }

        // 1. Click nút cha Marketing -> Mở/Đóng submenu
        private void BtnMarketing_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlMarketingSub, lblMarketingArrow);
        }

        // 2. Click nút Danh sách -> Mở frmAllCoupons
        private void BtnCouponList_Click(object sender, EventArgs e)
        {
            // Tạo form danh sách
            GUI.Coupon.frmAllCoupons frm = new GUI.Coupon.frmAllCoupons();

            // Hứng sự kiện "Thêm mới" từ form con
            frm.RequestAddCoupon += (s, args) => OpenAddNewCouponForm();

            // Hứng sự kiện "Sửa" từ form con
            frm.RequestEditCoupon += (s, id) => OpenEditCouponForm(id);

            OpenChildForm(frm, btnCouponList);
        }

        // 3. Click nút Thêm mới -> Mở Popup
        private void BtnAddCoupon_Click(object sender, EventArgs e)
        {
            OpenAddNewCouponForm();
        }

        // --- HÀM HỖ TRỢ ---
        private void OpenAddNewCouponForm()
        {
            // Mở Submenu nếu chưa mở
            if (currentSubMenuPanel != pnlMarketingSub)
                HandleParentMenuClick(pnlMarketingSub, lblMarketingArrow);

            GUI.Coupon.frmAddCoupon frm = new GUI.Coupon.frmAddCoupon(0);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Reload danh sách nếu đang mở form danh sách
                if (pnlMainContent.Controls.Count > 0 && pnlMainContent.Controls[0] is GUI.Coupon.frmAllCoupons listForm)
                {
                    listForm.LoadData();
                }
            }
        }

        private void OpenEditCouponForm(int id)
        {
            GUI.Coupon.frmAddCoupon frm = new GUI.Coupon.frmAddCoupon(id);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (pnlMainContent.Controls.Count > 0 && pnlMainContent.Controls[0] is GUI.Coupon.frmAllCoupons listForm)
                {
                    listForm.LoadData();
                }
            }
        }

        #region Xử lý Submenu

        private void CollapseAllSubMenus()
        {
            pnlProductSub.Height = 0;
            pnlCategorySub.Height = 0;
            pnlAttributesSub.Height = 0;
            pnlUserSub.Height = 0;
            pnlRolesSub.Height = 0;
            pnlSettingsSub.Height = 0;
            pnlPOSSub.Height = 0;
            if (pnlMarketingSub != null) pnlMarketingSub.Height = 0;
        }

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

        private void HandleParentMenuClick(Panel subMenu, Label arrowLabel)
        {
            bool isAlreadyOpen = (currentSubMenuPanel == subMenu);
            CollapseCurrentSubMenu();
            if (!isAlreadyOpen)
            {
                ExpandSubMenu(subMenu, arrowLabel);
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlProductSub, lblProductArrow);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlCategorySub, lblCategoryArrow);
        }

        private void btnSubCategory_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlSubCategorySub, lblSubCategoryArrow);
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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlSettingsSub, lblSettingsArrow);
        }
        private void btnPOSMenu_Click(object sender, EventArgs e)
        {
            HandleParentMenuClick(pnlPOSSub, lblPOSArrow);
        }
        #endregion

        #region Xử lý Hover & Highlight

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
            else if (parentPanel == pnlSubCategorySub)
            {
                btnSubCategory.BackColor = sidebarHover;
                lblSubCategoryArrow.BackColor = sidebarHover;
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
            else if (parentPanel == pnlSettingsSub)
            {
                btnSettings.BackColor = sidebarHover;
                lblSettingsArrow.BackColor = sidebarHover;
            }
            else if (parentPanel == pnlPOSSub)
            {
                btnPOSMenu.BackColor = sidebarHover;
                lblPOSArrow.BackColor = sidebarHover;
            }
            else if (parentPanel == pnlMarketingSub)
            {
                btnMarketing.BackColor = sidebarHover;
                lblMarketingArrow.BackColor = sidebarHover;
            }
        }

        private void AddHoverEvents()
        {
            AddHoverToParentButton(btnDashboard, null);
            AddHoverToParentButton(btnProduct, lblProductArrow);
            AddHoverToParentButton(btnCategory, lblCategoryArrow);
            AddHoverToParentButton(btnSubCategory, lblSubCategoryArrow);
            AddHoverToParentButton(btnAttributes, lblAttributesArrow);
            AddHoverToParentButton(btnUser, lblUserArrow);
            AddHoverToParentButton(btnRoles, lblRolesArrow);
            AddHoverToParentButton(btnSettings, lblSettingsArrow);
            AddHoverToChildButton(btnPermission);
            AddHoverToChildButton(btnProducts);
            AddHoverToChildButton(btnAddProduct);
            AddHoverToChildButton(btnCategoryList);
            AddHoverToChildButton(btnAddCategory);
            AddHoverToChildButton(btnSubCategoryList);
            AddHoverToChildButton(btnAddSubCategory);
            AddHoverToChildButton(btnAttributesList);
            AddHoverToChildButton(btnAddAttribute);
            AddHoverToChildButton(btnAllUser);
            AddHoverToChildButton(btnAddUser);
            AddHoverToChildButton(btnAllRoles);
            AddHoverToChildButton(btnCreateRole);
            AddHoverToChildButton(btnProfileSetting);
            AddHoverToChildButton(btnPOS);
            AddHoverToChildButton(btnScanQR);
        }

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

                if (currentActiveButton != null && currentActiveButton.Parent != pnlSidebar)
                {
                    Button parentBtnToKeepHovered = null;
                    if (currentActiveButton.Parent == pnlProductSub) parentBtnToKeepHovered = btnProduct;
                    else if (currentActiveButton.Parent == pnlCategorySub) parentBtnToKeepHovered = btnCategory;
                    else if (currentActiveButton.Parent == pnlSubCategorySub) parentBtnToKeepHovered = btnSubCategory;
                    else if (currentActiveButton.Parent == pnlAttributesSub) parentBtnToKeepHovered = btnAttributes;
                    else if (currentActiveButton.Parent == pnlUserSub) parentBtnToKeepHovered = btnUser;
                    else if (currentActiveButton.Parent == pnlRolesSub) parentBtnToKeepHovered = btnRoles;
                    else if (currentActiveButton.Parent == pnlSettingsSub) parentBtnToKeepHovered = btnSettings;

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
                        else if (currentActiveButton.Parent == pnlSubCategorySub) parentBtnToKeepHovered = btnSubCategory;
                        else if (currentActiveButton.Parent == pnlAttributesSub) parentBtnToKeepHovered = btnAttributes;
                        else if (currentActiveButton.Parent == pnlUserSub) parentBtnToKeepHovered = btnUser;
                        else if (currentActiveButton.Parent == pnlRolesSub) parentBtnToKeepHovered = btnRoles;
                        else if (currentActiveButton.Parent == pnlSettingsSub) parentBtnToKeepHovered = btnSettings;

                        if (btn == parentBtnToKeepHovered)
                        {
                            btn.BackColor = sidebarHover;
                            lbl.BackColor = sidebarHover;
                        }
                    }
                };
            }
        }

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
            OpenChildForm(new frmDashboard(), btnDashboard);
            CollapseCurrentSubMenu();
        }

        private void btnProducts_Click(object sender, EventArgs e) 
        {
            frmAllProduct frmAllProduct = new frmAllProduct();

            frmAllProduct.RequestAddProduct += (s, args) =>
            {
               OpenAddNewProductForm();
            };

            frmAllProduct.RequestEditProduct += (s, productId) =>
            {
                OpenEditProductForm(productId);
            };

            OpenChildForm(frmAllProduct, btnProducts);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Product.frmCreate(), btnAddProduct);
        }

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            frmAllCategory frmAllCategory = new frmAllCategory();

            frmAllCategory.RequestAddCategory += (s, args) =>
            {
                OpenAddNewCategoryForm();
            };

            frmAllCategory.RequestEditCategory += (s, id) =>
            {
                OpenEditCategoryForm(id);
            };
            OpenChildForm(frmAllCategory, btnCategoryList);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Category.frmCreate(), btnAddCategory);
        }

        private void btnSubCategoryList_Click(object sender, EventArgs e)
        {
            frmAllSubCategory frmAllSubCategory = new frmAllSubCategory();

            frmAllSubCategory.RequestAddSubcategory += (s, args) =>
            {
                OpenAddNewSubcategoryForm();
            };

            frmAllSubCategory.RequestEditSubcategory += (s, id) =>
            {
                OpenEditSubcategoryForm(id);
            };

            OpenChildForm(frmAllSubCategory, btnSubCategoryList);
        }

        private void btnAddSubCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SubCategory.frmCreate(), btnAddSubCategory);
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
            frmAllUsers frm = new frmAllUsers();

            // Xử lý Add User
            frm.RequestAddUser += (s, args) =>
            {
                OpenAddNewUserForm();   
            };

            frm.RequestEditUser += (s, user) =>
            {
                // Mở ProfileSetting giống như từ sidebar
                frmProfileSetting editForm = new frmProfileSetting(user);
                editForm.ProfileUpdated += (sender2, e2) =>
                {
                    InitializeUserInfo(); // Cập nhật header nếu edit chính mình
                    frm.LoadData(); // Reload data trong frmAllUsers
                };

                // Mở Settings submenu
                if (currentSubMenuPanel != pnlSettingsSub)
                {
                    HandleParentMenuClick(pnlSettingsSub, lblSettingsArrow);
                }

                // Mở form trong panel chính
                OpenChildForm(editForm, btnProfileSetting);
            };

            OpenChildForm(frm, btnAllUser);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            OpenAddNewUserForm();
        }

        private void btnAllRoles_Click(object sender, EventArgs e)
        {
            OpenAllRoleForm();
        }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            OpenAddNewRoleForm();
        }

        private void btnProfileSetting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmProfileSetting(), btnProfileSetting);
        }

        private void OpenPermissionForm()
        {
            // Mở menu cha nếu chưa mở
            if (currentSubMenuPanel != pnlRolesSub)
            {
                HandleParentMenuClick(pnlRolesSub, lblRolesArrow);
            }

            // Mở form Permission vào panel chính
            OpenChildForm(new frmPermission(), btnPermission);
        }

        private void btnPermission_Click(object sender, EventArgs e)
        {
            OpenPermissionForm();
        }
        private void btnPOS_Click(object sender, EventArgs e)
        {
            OpenPOSForm();
        }
        private void btnScanQR_Click(object sender, EventArgs e)
        {
            OpenScanQRForm();
        }
        private void OpenPOSForm()
        {
            _currentPosForm = new frmPOS();
            _currentPosForm.RequestScanQR += (s, args) =>
            {
                OpenScanQRForm();
            };

            OpenChildForm(_currentPosForm, btnPOS);
        }
        private void OpenScanQRForm()
        {
            _currentScanQRForm = new frmScanQR();

            // ✅ SỬA: Event trả về SKU (string), không phải Product ID (int)
            _currentScanQRForm.QRCodeScanned += (s, scannedData) =>
            {
                if (_currentPosForm != null)
                {
                    try
                    {
                        // ✅ Parse QR Code để lấy SKU
                        var qrCodeBLL = new QRCodeBLL();
                        string sku = qrCodeBLL.ParseQRCode(scannedData);

                        // ✅ Gọi AddProductBySku() thay vì AddProductById()
                        _currentPosForm.AddProductBySku(sku);

                        // Quay lại POS form
                        OpenPOSForm();

                        MessageBox.Show($"✅ Product added (SKU: {sku}) to cart!",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing QR Code: {ex.Message}",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            };

            OpenChildForm(_currentScanQRForm, btnScanQR);
        }
        #endregion
    }
}