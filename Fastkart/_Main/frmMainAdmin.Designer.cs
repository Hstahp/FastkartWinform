namespace GUI
{
    partial class frmMainAdmin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlSettingsSub = new System.Windows.Forms.Panel();
            this.btnProfileSetting = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.lblSettingsArrow = new System.Windows.Forms.Label();
            this.pnlRolesSub = new System.Windows.Forms.Panel();
            this.btnPermission = new System.Windows.Forms.Button();
            this.btnCreateRole = new System.Windows.Forms.Button();
            this.btnAllRoles = new System.Windows.Forms.Button();
            this.btnRoles = new System.Windows.Forms.Button();
            this.lblRolesArrow = new System.Windows.Forms.Label();
            this.pnlUserSub = new System.Windows.Forms.Panel();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnAllUser = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.lblUserArrow = new System.Windows.Forms.Label();
            this.pnlAttributesSub = new System.Windows.Forms.Panel();
            this.btnAddAttribute = new System.Windows.Forms.Button();
            this.btnAttributesList = new System.Windows.Forms.Button();
            this.btnAttributes = new System.Windows.Forms.Button();
            this.lblAttributesArrow = new System.Windows.Forms.Label();
            this.pnlSubCategorySub = new System.Windows.Forms.Panel();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnCategoryList = new System.Windows.Forms.Button();
            this.btnSubCategory = new System.Windows.Forms.Button();
            this.lblCategoryArrow = new System.Windows.Forms.Label();
            this.pnlCategorySub = new System.Windows.Forms.Panel();
            this.btnCategory = new System.Windows.Forms.Button();
            this.pnlProductSub = new System.Windows.Forms.Panel();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnProduct = new System.Windows.Forms.Button();
            this.lblProductArrow = new System.Windows.Forms.Label();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.pnlOrderSub = new System.Windows.Forms.Panel();
            this.btnOrder = new System.Windows.Forms.Button();
            this.lblOrderArrow = new System.Windows.Forms.Label();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnInvoiceManagement = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.btnAddSubCategory = new System.Windows.Forms.Button();
            this.btnSubCategoryList = new System.Windows.Forms.Button();
            this.lblSubCategoryArrow = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnNotifications = new System.Windows.Forms.Button();
            this.pnlUserInfo = new System.Windows.Forms.Panel();
            this.btnUserDropdown = new System.Windows.Forms.Button();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.pnlPOSSub = new System.Windows.Forms.Panel();
            this.btnScanQR = new System.Windows.Forms.Button();
            this.btnPOS = new System.Windows.Forms.Button();
            this.btnPOSMenu = new System.Windows.Forms.Button();
            this.lblPOSArrow = new System.Windows.Forms.Label();
            this.pnlMarketingSub = new System.Windows.Forms.Panel();
            this.btnAddCoupon = new System.Windows.Forms.Button();
            this.btnCouponList = new System.Windows.Forms.Button();
            this.btnMarketing = new System.Windows.Forms.Button();
            this.lblMarketingArrow = new System.Windows.Forms.Label();
            // -----------------------------------------------------
            this.pnlSidebar.SuspendLayout();
            this.pnlOrderSub.SuspendLayout();
            this.pnlSettingsSub.SuspendLayout();
            this.btnSettings.SuspendLayout();
            this.pnlRolesSub.SuspendLayout();
            this.btnRoles.SuspendLayout();
            this.pnlUserSub.SuspendLayout();
            this.btnUser.SuspendLayout();
            this.pnlAttributesSub.SuspendLayout();
            this.btnAttributes.SuspendLayout();
            this.pnlSubCategorySub.SuspendLayout();
            this.btnSubCategory.SuspendLayout();
            this.pnlCategorySub.SuspendLayout();
            this.btnCategory.SuspendLayout();
            this.pnlProductSub.SuspendLayout();
            this.btnProduct.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.AutoScroll = true;
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.pnlSidebar.Controls.Add(this.pnlSettingsSub);
            this.pnlSidebar.Controls.Add(this.btnSettings);
            this.pnlSidebar.Controls.Add(this.pnlMarketingSub);
            this.pnlSidebar.Controls.Add(this.btnMarketing);
            this.pnlSidebar.Controls.Add(this.pnlRolesSub);
            this.pnlSidebar.Controls.Add(this.btnRoles);
            this.pnlSidebar.Controls.Add(this.pnlUserSub);
            this.pnlSidebar.Controls.Add(this.btnUser);
            this.pnlSidebar.Controls.Add(this.pnlAttributesSub);
            this.pnlSidebar.Controls.Add(this.btnAttributes);
            this.pnlSidebar.Controls.Add(this.pnlSubCategorySub);
            this.pnlSidebar.Controls.Add(this.btnSubCategory);
            this.pnlSidebar.Controls.Add(this.pnlOrderSub);      // ✅ THÊM: Submenu Order
            this.pnlSidebar.Controls.Add(this.btnOrder);         // ✅ THÊM: Button Order
            this.pnlSidebar.Controls.Add(this.pnlCategorySub);
            this.pnlSidebar.Controls.Add(this.btnCategory);
            this.pnlSidebar.Controls.Add(this.pnlPOSSub);
            this.pnlSidebar.Controls.Add(this.btnPOSMenu);
            this.pnlSidebar.Controls.Add(this.pnlProductSub);
            this.pnlSidebar.Controls.Add(this.btnProduct);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(325, 1042);
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlSettingsSub
            // 
            this.pnlSettingsSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlSettingsSub.Controls.Add(this.btnProfileSetting);
            this.pnlSettingsSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSettingsSub.Location = new System.Drawing.Point(0, 655);
            this.pnlSettingsSub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSettingsSub.Name = "pnlSettingsSub";
            this.pnlSettingsSub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlSettingsSub.Size = new System.Drawing.Size(325, 0);
            this.pnlSettingsSub.TabIndex = 13;
            // 
            // btnProfileSetting
            // 
            this.btnProfileSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProfileSetting.FlatAppearance.BorderSize = 0;
            this.btnProfileSetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnProfileSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfileSetting.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfileSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnProfileSetting.Location = new System.Drawing.Point(0, 5);
            this.btnProfileSetting.Margin = new System.Windows.Forms.Padding(0);
            this.btnProfileSetting.Name = "btnProfileSetting";
            this.btnProfileSetting.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnProfileSetting.Size = new System.Drawing.Size(325, 48);
            this.btnProfileSetting.TabIndex = 3;
            this.btnProfileSetting.Text = "   Profile Setting";
            this.btnProfileSetting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfileSetting.UseVisualStyleBackColor = true;
            this.btnProfileSetting.Click += new System.EventHandler(this.btnProfileSetting_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Controls.Add(this.lblSettingsArrow);
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnSettings.Location = new System.Drawing.Point(0, 590);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(0);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnSettings.Size = new System.Drawing.Size(325, 65);
            this.btnSettings.TabIndex = 12;
            this.btnSettings.Text = "⚙️  Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // lblSettingsArrow
            // 
            this.lblSettingsArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblSettingsArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSettingsArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSettingsArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblSettingsArrow.Location = new System.Drawing.Point(275, 0);
            this.lblSettingsArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSettingsArrow.Name = "lblSettingsArrow";
            this.lblSettingsArrow.Size = new System.Drawing.Size(50, 65);
            this.lblSettingsArrow.TabIndex = 0;
            this.lblSettingsArrow.Text = "›";
            this.lblSettingsArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSettingsArrow.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // pnlRolesSub
            // 
            this.pnlRolesSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlRolesSub.Controls.Add(this.btnPermission);
            this.pnlRolesSub.Controls.Add(this.btnCreateRole);
            this.pnlRolesSub.Controls.Add(this.btnAllRoles);
            this.pnlRolesSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRolesSub.Location = new System.Drawing.Point(0, 590);
            this.pnlRolesSub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRolesSub.Name = "pnlRolesSub";
            this.pnlRolesSub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlRolesSub.Size = new System.Drawing.Size(325, 0);
            this.pnlRolesSub.TabIndex = 11;
            // 
            // btnPermission
            // 
            this.btnPermission.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPermission.FlatAppearance.BorderSize = 0;
            this.btnPermission.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnPermission.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPermission.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPermission.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnPermission.Location = new System.Drawing.Point(0, 101);
            this.btnPermission.Margin = new System.Windows.Forms.Padding(0);
            this.btnPermission.Name = "btnPermission";
            this.btnPermission.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnPermission.Size = new System.Drawing.Size(325, 48);
            this.btnPermission.TabIndex = 5;
            this.btnPermission.Text = "   Permission";
            this.btnPermission.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPermission.UseVisualStyleBackColor = true;
            this.btnPermission.Click += new System.EventHandler(this.btnPermission_Click);
            // 
            // btnCreateRole
            // 
            this.btnCreateRole.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCreateRole.FlatAppearance.BorderSize = 0;
            this.btnCreateRole.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCreateRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRole.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnCreateRole.Location = new System.Drawing.Point(0, 53);
            this.btnCreateRole.Margin = new System.Windows.Forms.Padding(0);
            this.btnCreateRole.Name = "btnCreateRole";
            this.btnCreateRole.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnCreateRole.Size = new System.Drawing.Size(325, 48);
            this.btnCreateRole.TabIndex = 4;
            this.btnCreateRole.Text = "   Create Roles";
            this.btnCreateRole.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateRole.UseVisualStyleBackColor = true;
            this.btnCreateRole.Click += new System.EventHandler(this.btnCreateRole_Click);
            // 
            // btnAllRoles
            // 
            this.btnAllRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAllRoles.FlatAppearance.BorderSize = 0;
            this.btnAllRoles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAllRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllRoles.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllRoles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnAllRoles.Location = new System.Drawing.Point(0, 5);
            this.btnAllRoles.Margin = new System.Windows.Forms.Padding(0);
            this.btnAllRoles.Name = "btnAllRoles";
            this.btnAllRoles.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnAllRoles.Size = new System.Drawing.Size(325, 48);
            this.btnAllRoles.TabIndex = 3;
            this.btnAllRoles.Text = "   All Roles";
            this.btnAllRoles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllRoles.UseVisualStyleBackColor = true;
            this.btnAllRoles.Click += new System.EventHandler(this.btnAllRoles_Click);
            // 
            // btnRoles
            // 
            this.btnRoles.Controls.Add(this.lblRolesArrow);
            this.btnRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRoles.FlatAppearance.BorderSize = 0;
            this.btnRoles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnRoles.Location = new System.Drawing.Point(0, 525);
            this.btnRoles.Margin = new System.Windows.Forms.Padding(0);
            this.btnRoles.Name = "btnRoles";
            this.btnRoles.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnRoles.Size = new System.Drawing.Size(325, 65);
            this.btnRoles.TabIndex = 10;
            this.btnRoles.Text = "🔐  Roles";
            this.btnRoles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoles.UseVisualStyleBackColor = true;
            this.btnRoles.Click += new System.EventHandler(this.btnRoles_Click);
            // 
            // lblRolesArrow
            // 
            this.lblRolesArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblRolesArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRolesArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblRolesArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblRolesArrow.Location = new System.Drawing.Point(275, 0);
            this.lblRolesArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRolesArrow.Name = "lblRolesArrow";
            this.lblRolesArrow.Size = new System.Drawing.Size(50, 65);
            this.lblRolesArrow.TabIndex = 0;
            this.lblRolesArrow.Text = "›";
            this.lblRolesArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRolesArrow.Click += new System.EventHandler(this.btnRoles_Click);
            // 
            // pnlUserSub
            // 
            this.pnlUserSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlUserSub.Controls.Add(this.btnAddUser);
            this.pnlUserSub.Controls.Add(this.btnAllUser);
            this.pnlUserSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUserSub.Location = new System.Drawing.Point(0, 525);
            this.pnlUserSub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlUserSub.Name = "pnlUserSub";
            this.pnlUserSub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlUserSub.Size = new System.Drawing.Size(325, 0);
            this.pnlUserSub.TabIndex = 9;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddUser.FlatAppearance.BorderSize = 0;
            this.btnAddUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnAddUser.Location = new System.Drawing.Point(0, 53);
            this.btnAddUser.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnAddUser.Size = new System.Drawing.Size(325, 48);
            this.btnAddUser.TabIndex = 4;
            this.btnAddUser.Text = "   Add New User";
            this.btnAddUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnAllUser
            // 
            this.btnAllUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAllUser.FlatAppearance.BorderSize = 0;
            this.btnAllUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAllUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnAllUser.Location = new System.Drawing.Point(0, 5);
            this.btnAllUser.Margin = new System.Windows.Forms.Padding(0);
            this.btnAllUser.Name = "btnAllUser";
            this.btnAllUser.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnAllUser.Size = new System.Drawing.Size(325, 48);
            this.btnAllUser.TabIndex = 3;
            this.btnAllUser.Text = "   All Users";
            this.btnAllUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllUser.UseVisualStyleBackColor = true;
            this.btnAllUser.Click += new System.EventHandler(this.btnAllUser_Click);
            // 
            // btnUser
            // 
            this.btnUser.Controls.Add(this.lblUserArrow);
            this.btnUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUser.FlatAppearance.BorderSize = 0;
            this.btnUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnUser.Location = new System.Drawing.Point(0, 460);
            this.btnUser.Margin = new System.Windows.Forms.Padding(0);
            this.btnUser.Name = "btnUser";
            this.btnUser.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnUser.Size = new System.Drawing.Size(325, 65);
            this.btnUser.TabIndex = 8;
            this.btnUser.Text = "👥  Users";
            this.btnUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // lblUserArrow
            // 
            this.lblUserArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblUserArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUserArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblUserArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblUserArrow.Location = new System.Drawing.Point(275, 0);
            this.lblUserArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserArrow.Name = "lblUserArrow";
            this.lblUserArrow.Size = new System.Drawing.Size(50, 65);
            this.lblUserArrow.TabIndex = 0;
            this.lblUserArrow.Text = "›";
            this.lblUserArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserArrow.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // pnlAttributesSub
            // 
            this.pnlAttributesSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlAttributesSub.Controls.Add(this.btnAddAttribute);
            this.pnlAttributesSub.Controls.Add(this.btnAttributesList);
            this.pnlAttributesSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAttributesSub.Location = new System.Drawing.Point(0, 460);
            this.pnlAttributesSub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAttributesSub.Name = "pnlAttributesSub";
            this.pnlAttributesSub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlAttributesSub.Size = new System.Drawing.Size(325, 0);
            this.pnlAttributesSub.TabIndex = 7;
            // 
            // btnAddAttribute
            // 
            this.btnAddAttribute.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddAttribute.FlatAppearance.BorderSize = 0;
            this.btnAddAttribute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAddAttribute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAttribute.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAttribute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnAddAttribute.Location = new System.Drawing.Point(0, 53);
            this.btnAddAttribute.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddAttribute.Name = "btnAddAttribute";
            this.btnAddAttribute.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnAddAttribute.Size = new System.Drawing.Size(325, 48);
            this.btnAddAttribute.TabIndex = 4;
            this.btnAddAttribute.Text = "   Add Attributes";
            this.btnAddAttribute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddAttribute.UseVisualStyleBackColor = true;
            this.btnAddAttribute.Click += new System.EventHandler(this.btnAddAttribute_Click);
            // 
            // btnAttributesList
            // 
            this.btnAttributesList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAttributesList.FlatAppearance.BorderSize = 0;
            this.btnAttributesList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAttributesList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttributesList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttributesList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnAttributesList.Location = new System.Drawing.Point(0, 5);
            this.btnAttributesList.Margin = new System.Windows.Forms.Padding(0);
            this.btnAttributesList.Name = "btnAttributesList";
            this.btnAttributesList.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnAttributesList.Size = new System.Drawing.Size(325, 48);
            this.btnAttributesList.TabIndex = 3;
            this.btnAttributesList.Text = "   Attributes";
            this.btnAttributesList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttributesList.UseVisualStyleBackColor = true;
            this.btnAttributesList.Click += new System.EventHandler(this.btnAttributesList_Click);
            // 
            // btnAttributes
            // 
            this.btnAttributes.Controls.Add(this.lblAttributesArrow);
            this.btnAttributes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAttributes.FlatAppearance.BorderSize = 0;
            this.btnAttributes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAttributes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttributes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttributes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnAttributes.Location = new System.Drawing.Point(0, 395);
            this.btnAttributes.Margin = new System.Windows.Forms.Padding(0);
            this.btnAttributes.Name = "btnAttributes";
            this.btnAttributes.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnAttributes.Size = new System.Drawing.Size(325, 65);
            this.btnAttributes.TabIndex = 6;
            this.btnAttributes.Text = "🏷️  Attributes";
            this.btnAttributes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttributes.UseVisualStyleBackColor = true;
            this.btnAttributes.Click += new System.EventHandler(this.btnAttributes_Click);
            // 
            // lblAttributesArrow
            // 
            this.lblAttributesArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblAttributesArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAttributesArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblAttributesArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblAttributesArrow.Location = new System.Drawing.Point(275, 0);
            this.lblAttributesArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttributesArrow.Name = "lblAttributesArrow";
            this.lblAttributesArrow.Size = new System.Drawing.Size(50, 65);
            this.lblAttributesArrow.TabIndex = 0;
            this.lblAttributesArrow.Text = "›";
            this.lblAttributesArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAttributesArrow.Click += new System.EventHandler(this.btnAttributes_Click);
            // 
            // pnlSubCategorySub
            // 
            this.pnlSubCategorySub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlSubCategorySub.Controls.Add(this.btnAddSubCategory);
            this.pnlSubCategorySub.Controls.Add(this.btnSubCategoryList);
            this.pnlSubCategorySub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubCategorySub.Location = new System.Drawing.Point(0, 395);
            this.pnlSubCategorySub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSubCategorySub.Name = "pnlSubCategorySub";
            this.pnlSubCategorySub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlSubCategorySub.Size = new System.Drawing.Size(325, 0);
            this.pnlSubCategorySub.TabIndex = 5;
            // 
            // btnAddSubCategory
            // 
            this.btnAddSubCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddSubCategory.FlatAppearance.BorderSize = 0;
            this.btnAddSubCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAddSubCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSubCategory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSubCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnAddSubCategory.Location = new System.Drawing.Point(0, 53);
            this.btnAddSubCategory.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddSubCategory.Name = "btnAddSubCategory";
            this.btnAddSubCategory.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnAddSubCategory.Size = new System.Drawing.Size(325, 48);
            this.btnAddSubCategory.TabIndex = 4;
            this.btnAddSubCategory.Text = "   Add New SubCategory";
            this.btnAddSubCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddSubCategory.UseVisualStyleBackColor = true;
            this.btnAddSubCategory.Click += new System.EventHandler(this.btnAddSubCategory_Click);
            // 
            // btnSubCategoryList
            // 
            this.btnSubCategoryList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSubCategoryList.FlatAppearance.BorderSize = 0;
            this.btnSubCategoryList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnSubCategoryList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubCategoryList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubCategoryList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnSubCategoryList.Location = new System.Drawing.Point(0, 5);
            this.btnSubCategoryList.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubCategoryList.Name = "btnSubCategoryList";
            this.btnSubCategoryList.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnSubCategoryList.Size = new System.Drawing.Size(325, 48);
            this.btnSubCategoryList.TabIndex = 3;
            this.btnSubCategoryList.Text = "   SubCategory List";
            this.btnSubCategoryList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSubCategoryList.UseVisualStyleBackColor = true;
            this.btnSubCategoryList.Click += new System.EventHandler(this.btnSubCategoryList_Click);
            // 
            // btnSubCategory
            // 
            this.btnSubCategory.Controls.Add(this.lblSubCategoryArrow);
            this.btnSubCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSubCategory.FlatAppearance.BorderSize = 0;
            this.btnSubCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnSubCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnSubCategory.Location = new System.Drawing.Point(0, 330);
            this.btnSubCategory.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubCategory.Name = "btnSubCategory";
            this.btnSubCategory.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnSubCategory.Size = new System.Drawing.Size(325, 65);
            this.btnSubCategory.TabIndex = 4;
            this.btnSubCategory.Text = "📂  SubCategory";
            this.btnSubCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSubCategory.UseVisualStyleBackColor = true;
            this.btnSubCategory.Click += new System.EventHandler(this.btnSubCategory_Click);
            // 
            // lblSubCategoryArrow
            // 
            this.lblSubCategoryArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblSubCategoryArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSubCategoryArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubCategoryArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblSubCategoryArrow.Location = new System.Drawing.Point(275, 0);
            this.lblSubCategoryArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubCategoryArrow.Name = "lblSubCategoryArrow";
            this.lblSubCategoryArrow.Size = new System.Drawing.Size(50, 65);
            this.lblSubCategoryArrow.TabIndex = 0;
            this.lblSubCategoryArrow.Text = "›";
            this.lblSubCategoryArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSubCategoryArrow.Click += new System.EventHandler(this.btnSubCategory_Click);
            // 
            // pnlCategorySub
            // 
            this.pnlCategorySub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlCategorySub.Controls.Add(this.btnAddCategory);
            this.pnlCategorySub.Controls.Add(this.btnCategoryList);
            this.pnlCategorySub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCategorySub.Location = new System.Drawing.Point(0, 330);
            this.pnlCategorySub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCategorySub.Name = "pnlCategorySub";
            this.pnlCategorySub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlCategorySub.Size = new System.Drawing.Size(325, 0);
            this.pnlCategorySub.TabIndex = 5;
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddCategory.FlatAppearance.BorderSize = 0;
            this.btnAddCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAddCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCategory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnAddCategory.Location = new System.Drawing.Point(0, 53);
            this.btnAddCategory.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnAddCategory.Size = new System.Drawing.Size(325, 48);
            this.btnAddCategory.TabIndex = 4;
            this.btnAddCategory.Text = "   Add New Category";
            this.btnAddCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // btnCategoryList
            // 
            this.btnCategoryList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCategoryList.FlatAppearance.BorderSize = 0;
            this.btnCategoryList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCategoryList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCategoryList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnCategoryList.Location = new System.Drawing.Point(0, 5);
            this.btnCategoryList.Margin = new System.Windows.Forms.Padding(0);
            this.btnCategoryList.Name = "btnCategoryList";
            this.btnCategoryList.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnCategoryList.Size = new System.Drawing.Size(325, 48);
            this.btnCategoryList.TabIndex = 3;
            this.btnCategoryList.Text = "   Category List";
            this.btnCategoryList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCategoryList.UseVisualStyleBackColor = true;
            this.btnCategoryList.Click += new System.EventHandler(this.btnCategoryList_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.Controls.Add(this.lblCategoryArrow);
            this.btnCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCategory.FlatAppearance.BorderSize = 0;
            this.btnCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnCategory.Location = new System.Drawing.Point(0, 265);
            this.btnCategory.Margin = new System.Windows.Forms.Padding(0);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnCategory.Size = new System.Drawing.Size(325, 65);
            this.btnCategory.TabIndex = 4;
            this.btnCategory.Text = "📂  Category";
            this.btnCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCategory.UseVisualStyleBackColor = true;
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // lblCategoryArrow
            // 
            this.lblCategoryArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblCategoryArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCategoryArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCategoryArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblCategoryArrow.Location = new System.Drawing.Point(275, 0);
            this.lblCategoryArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategoryArrow.Name = "lblCategoryArrow";
            this.lblCategoryArrow.Size = new System.Drawing.Size(50, 65);
            this.lblCategoryArrow.TabIndex = 0;
            this.lblCategoryArrow.Text = "›";
            this.lblCategoryArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCategoryArrow.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // pnlProductSub
            // 
            this.pnlProductSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlProductSub.Controls.Add(this.btnAddProduct);
            this.pnlProductSub.Controls.Add(this.btnProducts);
            this.pnlProductSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProductSub.Location = new System.Drawing.Point(0, 265);
            this.pnlProductSub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlProductSub.Name = "pnlProductSub";
            this.pnlProductSub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlProductSub.Size = new System.Drawing.Size(325, 0);
            this.pnlProductSub.TabIndex = 3;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddProduct.FlatAppearance.BorderSize = 0;
            this.btnAddProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnAddProduct.Location = new System.Drawing.Point(0, 53);
            this.btnAddProduct.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnAddProduct.Size = new System.Drawing.Size(325, 48);
            this.btnAddProduct.TabIndex = 4;
            this.btnAddProduct.Text = "   Add New Products";
            this.btnAddProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProducts.FlatAppearance.BorderSize = 0;
            this.btnProducts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducts.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnProducts.Location = new System.Drawing.Point(0, 5);
            this.btnProducts.Margin = new System.Windows.Forms.Padding(0);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnProducts.Size = new System.Drawing.Size(325, 48);
            this.btnProducts.TabIndex = 3;
            this.btnProducts.Text = "   Products";
            this.btnProducts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnProduct
            // 
            this.btnProduct.Controls.Add(this.lblProductArrow);
            this.btnProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProduct.FlatAppearance.BorderSize = 0;
            this.btnProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProduct.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnProduct.Location = new System.Drawing.Point(0, 200);
            this.btnProduct.Margin = new System.Windows.Forms.Padding(0);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnProduct.Size = new System.Drawing.Size(325, 65);
            this.btnProduct.TabIndex = 2;
            this.btnProduct.Text = "📦  Product";
            this.btnProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProduct.UseVisualStyleBackColor = true;
            this.btnProduct.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // lblProductArrow
            // 
            this.lblProductArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblProductArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblProductArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblProductArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblProductArrow.Location = new System.Drawing.Point(275, 0);
            this.lblProductArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductArrow.Name = "lblProductArrow";
            this.lblProductArrow.Size = new System.Drawing.Size(50, 65);
            this.lblProductArrow.TabIndex = 0;
            this.lblProductArrow.Text = "›";
            this.lblProductArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblProductArrow.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnDashboard.Location = new System.Drawing.Point(0, 135);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(325, 65);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "🏠  Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // pnlOrderSub (Menu con)
            // 
            this.pnlOrderSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlOrderSub.Controls.Add(this.btnInvoiceManagement);
            this.pnlOrderSub.Controls.Add(this.btnPayment);
            this.pnlOrderSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderSub.Location = new System.Drawing.Point(0, 330);
            this.pnlOrderSub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlOrderSub.Name = "pnlOrderSub";
            this.pnlOrderSub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlOrderSub.Size = new System.Drawing.Size(325, 0);
            this.pnlOrderSub.TabIndex = 21;

            // 
            // btnPayment
            // 
            this.btnPayment.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPayment.FlatAppearance.BorderSize = 0;
            this.btnPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnPayment.Location = new System.Drawing.Point(0, 5);
            this.btnPayment.Margin = new System.Windows.Forms.Padding(0);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnPayment.Size = new System.Drawing.Size(325, 48);
            this.btnPayment.TabIndex = 1;
            this.btnPayment.Text = "   💳  Payment List";
            this.btnPayment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnInvoiceManagement
            // 
            this.btnInvoiceManagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInvoiceManagement.FlatAppearance.BorderSize = 0;
            this.btnInvoiceManagement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnInvoiceManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvoiceManagement.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnInvoiceManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnInvoiceManagement.Location = new System.Drawing.Point(0, 53);
            this.btnInvoiceManagement.Margin = new System.Windows.Forms.Padding(0);
            this.btnInvoiceManagement.Name = "btnInvoiceManagement";
            this.btnInvoiceManagement.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnInvoiceManagement.Size = new System.Drawing.Size(325, 48);
            this.btnInvoiceManagement.TabIndex = 2;
            this.btnInvoiceManagement.Text = "   📋  Invoice Management";
            this.btnInvoiceManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInvoiceManagement.UseVisualStyleBackColor = true;
            this.btnInvoiceManagement.Click += new System.EventHandler(this.btnInvoiceManagement_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Controls.Add(this.lblOrderArrow);
            this.btnOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOrder.FlatAppearance.BorderSize = 0;
            this.btnOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnOrder.Location = new System.Drawing.Point(0, 330);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(0);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnOrder.Size = new System.Drawing.Size(325, 65);
            this.btnOrder.TabIndex = 20;
            this.btnOrder.Text = "📦  Orders";
            this.btnOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            
            // 
            // lblOrderArrow
            // 
            this.lblOrderArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblOrderArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblOrderArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblOrderArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblOrderArrow.Location = new System.Drawing.Point(275, 0);
            this.lblOrderArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderArrow.Name = "lblOrderArrow";
            this.lblOrderArrow.Size = new System.Drawing.Size(50, 65);
            this.lblOrderArrow.TabIndex = 0;
            this.lblOrderArrow.Text = "›";
            this.lblOrderArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOrderArrow.Click += new System.EventHandler(this.btnOrder_Click);
            
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.pnlLogo.Size = new System.Drawing.Size(325, 135);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(25, 0);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(300, 135);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "   Fastkart";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.txtSearch);
            this.pnlHeader.Controls.Add(this.btnNotifications);
            this.pnlHeader.Controls.Add(this.pnlUserInfo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(325, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.pnlHeader.Size = new System.Drawing.Size(1463, 94);
            this.pnlHeader.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.txtSearch.Location = new System.Drawing.Point(50, 34);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(751, 23);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "🔍  Search products, orders, customers...";
            // 
            // btnNotifications
            // 
            this.btnNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotifications.FlatAppearance.BorderSize = 0;
            this.btnNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotifications.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotifications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnNotifications.Location = new System.Drawing.Point(1056, 22);
            this.btnNotifications.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNotifications.Name = "btnNotifications";
            this.btnNotifications.Size = new System.Drawing.Size(62, 50);
            this.btnNotifications.TabIndex = 1;
            this.btnNotifications.Text = "🔔";
            this.btnNotifications.UseVisualStyleBackColor = true;
            // 
            // pnlUserInfo
            // 
            this.pnlUserInfo.Controls.Add(this.btnUserDropdown);
            this.pnlUserInfo.Controls.Add(this.lblRole);
            this.pnlUserInfo.Controls.Add(this.lblName);
            this.pnlUserInfo.Controls.Add(this.picUser);
            this.pnlUserInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlUserInfo.Location = new System.Drawing.Point(1143, 0);
            this.pnlUserInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlUserInfo.Name = "pnlUserInfo";
            this.pnlUserInfo.Size = new System.Drawing.Size(290, 94);
            this.pnlUserInfo.TabIndex = 0;
            // 
            // btnUserDropdown
            // 
            this.btnUserDropdown.FlatAppearance.BorderSize = 0;
            this.btnUserDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserDropdown.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserDropdown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnUserDropdown.Location = new System.Drawing.Point(240, 30);
            this.btnUserDropdown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUserDropdown.Name = "btnUserDropdown";
            this.btnUserDropdown.Size = new System.Drawing.Size(40, 35);
            this.btnUserDropdown.TabIndex = 3;
            this.btnUserDropdown.Text = "▾";
            this.btnUserDropdown.UseVisualStyleBackColor = true;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblRole.Location = new System.Drawing.Point(88, 48);
            this.lblRole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(53, 20);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Admin";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblName.Location = new System.Drawing.Point(88, 25);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(106, 23);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Emay Walter";
            // 
            // picUser
            // 
            this.picUser.Location = new System.Drawing.Point(15, 20);
            this.picUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(60, 60);
            this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUser.TabIndex = 0;
            this.picUser.TabStop = false;
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(325, 94);
            this.pnlMainContent.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.pnlMainContent.Size = new System.Drawing.Size(1463, 948);
            this.pnlMainContent.TabIndex = 2;
            // 
            // pnlPOSSub
            // 
            this.pnlPOSSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlPOSSub.Controls.Add(this.btnScanQR);
            this.pnlPOSSub.Controls.Add(this.btnPOS);
            this.pnlPOSSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPOSSub.Location = new System.Drawing.Point(0, 265);
            this.pnlPOSSub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPOSSub.Name = "pnlPOSSub";
            this.pnlPOSSub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlPOSSub.Size = new System.Drawing.Size(325, 0);
            this.pnlPOSSub.TabIndex = 14;
            // 
            // btnScanQR
            // 
            this.btnScanQR.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnScanQR.FlatAppearance.BorderSize = 0;
            this.btnScanQR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnScanQR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScanQR.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnScanQR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnScanQR.Location = new System.Drawing.Point(0, 53);
            this.btnScanQR.Margin = new System.Windows.Forms.Padding(0);
            this.btnScanQR.Name = "btnScanQR";
            this.btnScanQR.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnScanQR.Size = new System.Drawing.Size(325, 48);
            this.btnScanQR.TabIndex = 1;
            this.btnScanQR.Text = "   📷  Scan QR Code";
            this.btnScanQR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScanQR.UseVisualStyleBackColor = true;
            this.btnScanQR.Click += new System.EventHandler(this.btnScanQR_Click);
            // 
            // btnPOS
            // 
            this.btnPOS.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPOS.FlatAppearance.BorderSize = 0;
            this.btnPOS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPOS.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnPOS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnPOS.Location = new System.Drawing.Point(0, 5);
            this.btnPOS.Margin = new System.Windows.Forms.Padding(0);
            this.btnPOS.Name = "btnPOS";
            this.btnPOS.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnPOS.Size = new System.Drawing.Size(325, 48);
            this.btnPOS.TabIndex = 0;
            this.btnPOS.Text = "   💳  POS System";
            this.btnPOS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPOS.UseVisualStyleBackColor = true;
            this.btnPOS.Click += new System.EventHandler(this.btnPOS_Click);
            // 
            // btnPOSMenu
            // 
            this.btnPOSMenu.Controls.Add(this.lblPOSArrow);
            this.btnPOSMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPOSMenu.FlatAppearance.BorderSize = 0;
            this.btnPOSMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnPOSMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPOSMenu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPOSMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnPOSMenu.Location = new System.Drawing.Point(0, 200);
            this.btnPOSMenu.Margin = new System.Windows.Forms.Padding(0);
            this.btnPOSMenu.Name = "btnPOSMenu";
            this.btnPOSMenu.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnPOSMenu.Size = new System.Drawing.Size(325, 65);
            this.btnPOSMenu.TabIndex = 15;
            this.btnPOSMenu.Text = "🛒  Point of Sale";
            this.btnPOSMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPOSMenu.UseVisualStyleBackColor = true;
            this.btnPOSMenu.Click += new System.EventHandler(this.btnPOSMenu_Click);
            // 
            // lblPOSArrow
            // 
            this.lblPOSArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblPOSArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPOSArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPOSArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblPOSArrow.Location = new System.Drawing.Point(275, 0);
            this.lblPOSArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPOSArrow.Name = "lblPOSArrow";
            this.lblPOSArrow.Size = new System.Drawing.Size(50, 65);
            this.lblPOSArrow.TabIndex = 0;
            this.lblPOSArrow.Text = "›";
            this.lblPOSArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPOSArrow.Click += new System.EventHandler(this.btnPOSMenu_Click);
            // 
            // frmMainAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1788, 1042);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1370, 863);
            this.Name = "frmMainAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fastkart - Admin Dashboard";
            this.Load += new System.EventHandler(this.frmMainAdmin_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlOrderSub.ResumeLayout(false);
            this.pnlSettingsSub.ResumeLayout(false);
            this.btnSettings.ResumeLayout(false);
            this.pnlRolesSub.ResumeLayout(false);
            this.btnRoles.ResumeLayout(false);
            this.pnlUserSub.ResumeLayout(false);
            this.btnUser.ResumeLayout(false);
            this.pnlAttributesSub.ResumeLayout(false);
            this.btnAttributes.ResumeLayout(false);
            this.pnlSubCategorySub.ResumeLayout(false);
            this.btnSubCategory.ResumeLayout(false);
            this.pnlCategorySub.ResumeLayout(false);
            this.btnCategory.ResumeLayout(false);
            this.pnlProductSub.ResumeLayout(false);
            this.btnProduct.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlUserInfo.ResumeLayout(false);
            this.pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            this.ResumeLayout(false);

            // 
            // pnlMarketingSub
            // 
            this.pnlMarketingSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlMarketingSub.Controls.Add(this.btnAddCoupon);
            this.pnlMarketingSub.Controls.Add(this.btnCouponList);
            this.pnlMarketingSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMarketingSub.Location = new System.Drawing.Point(0, 590);
            this.pnlMarketingSub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMarketingSub.Name = "pnlMarketingSub";
            this.pnlMarketingSub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlMarketingSub.Size = new System.Drawing.Size(325, 0); // Mặc định đóng
            this.pnlMarketingSub.TabIndex = 12;
            // 
            // btnAddCoupon
            // 
            this.btnAddCoupon.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddCoupon.FlatAppearance.BorderSize = 0;
            this.btnAddCoupon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAddCoupon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCoupon.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCoupon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnAddCoupon.Location = new System.Drawing.Point(0, 53);
            this.btnAddCoupon.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddCoupon.Name = "btnAddCoupon";
            // CHUẨN: Padding 60 + 3 dấu cách
            this.btnAddCoupon.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnAddCoupon.Size = new System.Drawing.Size(325, 48);
            this.btnAddCoupon.TabIndex = 4;
            this.btnAddCoupon.Text = "   Add Coupon";
            this.btnAddCoupon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCoupon.UseVisualStyleBackColor = true;
            this.btnAddCoupon.Click += new System.EventHandler(this.BtnAddCoupon_Click);
            // 
            // btnCouponList
            // 
            this.btnCouponList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCouponList.FlatAppearance.BorderSize = 0;
            this.btnCouponList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCouponList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCouponList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCouponList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnCouponList.Location = new System.Drawing.Point(0, 5);
            this.btnCouponList.Margin = new System.Windows.Forms.Padding(0);
            this.btnCouponList.Name = "btnCouponList";
            // CHUẨN: Padding 60 + 3 dấu cách
            this.btnCouponList.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnCouponList.Size = new System.Drawing.Size(325, 48);
            this.btnCouponList.TabIndex = 3;
            this.btnCouponList.Text = "   Coupon List";
            this.btnCouponList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCouponList.UseVisualStyleBackColor = true;
            this.btnCouponList.Click += new System.EventHandler(this.BtnCouponList_Click);
            // 
            // btnMarketing
            // 
            this.btnMarketing.Controls.Add(this.lblMarketingArrow);
            this.btnMarketing.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMarketing.FlatAppearance.BorderSize = 0;
            this.btnMarketing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnMarketing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarketing.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarketing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnMarketing.Location = new System.Drawing.Point(0, 525);
            this.btnMarketing.Margin = new System.Windows.Forms.Padding(0);
            this.btnMarketing.Name = "btnMarketing";
            // CHUẨN: Padding 25 (Bằng Product/Category)
            this.btnMarketing.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnMarketing.Size = new System.Drawing.Size(325, 65);
            this.btnMarketing.TabIndex = 11;
            // CHUẨN: Text là Coupons + 2 dấu cách
            this.btnMarketing.Text = "🎫  Coupons";
            this.btnMarketing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMarketing.UseVisualStyleBackColor = true;
            this.btnMarketing.Click += new System.EventHandler(this.BtnMarketing_Click);
            // 
            // lblMarketingArrow
            // 
            this.lblMarketingArrow.BackColor = System.Drawing.Color.Transparent;
            this.lblMarketingArrow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMarketingArrow.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMarketingArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblMarketingArrow.Location = new System.Drawing.Point(275, 0);
            this.lblMarketingArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMarketingArrow.Name = "lblMarketingArrow";
            this.lblMarketingArrow.Size = new System.Drawing.Size(50, 65);
            this.lblMarketingArrow.TabIndex = 0;
            this.lblMarketingArrow.Text = "›";
            this.lblMarketingArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMarketingArrow.Click += new System.EventHandler(this.BtnMarketing_Click);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Panel pnlProductSub;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Panel pnlCategorySub;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnCategoryList;
        private System.Windows.Forms.Button btnSubCategory;
        private System.Windows.Forms.Panel pnlSubCategorySub;
        private System.Windows.Forms.Button btnAddSubCategory;
        private System.Windows.Forms.Button btnSubCategoryList;
        private System.Windows.Forms.Button btnAttributes;
        private System.Windows.Forms.Panel pnlAttributesSub;
        private System.Windows.Forms.Button btnAddAttribute;
        private System.Windows.Forms.Button btnAttributesList;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Panel pnlUserSub;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnAllUser;
        private System.Windows.Forms.Button btnRoles;
        private System.Windows.Forms.Panel pnlRolesSub;
        private System.Windows.Forms.Button btnCreateRole;
        private System.Windows.Forms.Button btnAllRoles;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Panel pnlUserInfo;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnUserDropdown;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Label lblProductArrow;
        private System.Windows.Forms.Label lblCategoryArrow;
        private System.Windows.Forms.Label lblSubCategoryArrow;
        private System.Windows.Forms.Label lblAttributesArrow;
        private System.Windows.Forms.Label lblUserArrow;
        private System.Windows.Forms.Label lblRolesArrow;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnNotifications;
        private System.Windows.Forms.Panel pnlSettingsSub;
        private System.Windows.Forms.Button btnProfileSetting;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Label lblSettingsArrow;
        private System.Windows.Forms.Button btnPermission;
        private System.Windows.Forms.Panel pnlPOSSub;
        private System.Windows.Forms.Button btnPOS;
        private System.Windows.Forms.Button btnScanQR;
        private System.Windows.Forms.Button btnPOSMenu;
        private System.Windows.Forms.Label lblPOSArrow;
        private System.Windows.Forms.Panel pnlMarketingSub;
        private System.Windows.Forms.Button btnCouponList;
        private System.Windows.Forms.Button btnAddCoupon;
        private System.Windows.Forms.Button btnMarketing;
        private System.Windows.Forms.Label lblMarketingArrow;
        private System.Windows.Forms.Panel pnlOrderSub;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnInvoiceManagement;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Label lblOrderArrow;
    }
} 