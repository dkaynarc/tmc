namespace Tmc.Scada.App
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.loginAndLogoutButton = new System.Windows.Forms.ToolStripButton();
            this.currentUserLabel = new System.Windows.Forms.ToolStripLabel();
            this.eStopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.alarmStatusLabel = new System.Windows.Forms.ToolStripLabel();
            this.alarmTypeLabel = new System.Windows.Forms.ToolStripLabel();
            this.alarmsListButton = new System.Windows.Forms.ToolStripButton();
            this.alarmsCountLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.reportsTabButton = new System.Windows.Forms.ToolStripButton();
            this.ordersTabButton = new System.Windows.Forms.ToolStripButton();
            this.environmentTabButton = new System.Windows.Forms.ToolStripButton();
            this.controlTabButton = new System.Windows.Forms.ToolStripButton();
            this.plantMimicTabButton = new System.Windows.Forms.ToolStripButton();
            this.tablessControlPanel = new TablessControl();
            this.plantMimicTab = new System.Windows.Forms.TabPage();
            this.Button_AddNewOrder = new System.Windows.Forms.Button();
            this.controlTab = new System.Windows.Forms.TabPage();
            this.environmentTab = new System.Windows.Forms.TabPage();
            this.ordersTab = new System.Windows.Forms.TabPage();
            this.reportsTab = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.orderListViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.orderIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blackDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.redDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.greenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whiteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberOfProductsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tablessControlPanel.SuspendLayout();
            this.plantMimicTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderListViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginAndLogoutButton,
            this.currentUserLabel,
            this.eStopButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(696, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // loginAndLogoutButton
            // 
            this.loginAndLogoutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loginAndLogoutButton.Image = ((System.Drawing.Image)(resources.GetObject("loginAndLogoutButton.Image")));
            this.loginAndLogoutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loginAndLogoutButton.Name = "loginAndLogoutButton";
            this.loginAndLogoutButton.Size = new System.Drawing.Size(41, 22);
            this.loginAndLogoutButton.Text = "Login";
            // 
            // currentUserLabel
            // 
            this.currentUserLabel.Name = "currentUserLabel";
            this.currentUserLabel.Size = new System.Drawing.Size(73, 22);
            this.currentUserLabel.Text = "Current User";
            // 
            // eStopButton
            // 
            this.eStopButton.BackColor = System.Drawing.Color.Red;
            this.eStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.eStopButton.Image = ((System.Drawing.Image)(resources.GetObject("eStopButton.Image")));
            this.eStopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eStopButton.Name = "eStopButton";
            this.eStopButton.Size = new System.Drawing.Size(97, 22);
            this.eStopButton.Text = "Emergency Stop";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alarmStatusLabel,
            this.alarmTypeLabel,
            this.alarmsListButton,
            this.alarmsCountLabel});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip2.Size = new System.Drawing.Size(696, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // alarmStatusLabel
            // 
            this.alarmStatusLabel.Name = "alarmStatusLabel";
            this.alarmStatusLabel.Size = new System.Drawing.Size(74, 22);
            this.alarmStatusLabel.Text = "Alarm Status";
            // 
            // alarmTypeLabel
            // 
            this.alarmTypeLabel.Name = "alarmTypeLabel";
            this.alarmTypeLabel.Size = new System.Drawing.Size(133, 22);
            this.alarmTypeLabel.Text = "Alarm Type/Description";
            // 
            // alarmsListButton
            // 
            this.alarmsListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.alarmsListButton.Image = ((System.Drawing.Image)(resources.GetObject("alarmsListButton.Image")));
            this.alarmsListButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alarmsListButton.Name = "alarmsListButton";
            this.alarmsListButton.Size = new System.Drawing.Size(69, 22);
            this.alarmsListButton.Text = "Alarms List";
            // 
            // alarmsCountLabel
            // 
            this.alarmsCountLabel.Name = "alarmsCountLabel";
            this.alarmsCountLabel.Size = new System.Drawing.Size(80, 22);
            this.alarmsCountLabel.Text = "Alarms Count";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportsTabButton,
            this.ordersTabButton,
            this.environmentTabButton,
            this.controlTabButton,
            this.plantMimicTabButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 50);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip3.Size = new System.Drawing.Size(696, 25);
            this.toolStrip3.TabIndex = 3;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // reportsTabButton
            // 
            this.reportsTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.reportsTabButton.Image = ((System.Drawing.Image)(resources.GetObject("reportsTabButton.Image")));
            this.reportsTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reportsTabButton.Name = "reportsTabButton";
            this.reportsTabButton.Size = new System.Drawing.Size(51, 22);
            this.reportsTabButton.Text = "Reports";
            this.reportsTabButton.Click += new System.EventHandler(this.reportsTabButton_Click);
            // 
            // ordersTabButton
            // 
            this.ordersTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ordersTabButton.Image = ((System.Drawing.Image)(resources.GetObject("ordersTabButton.Image")));
            this.ordersTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ordersTabButton.Name = "ordersTabButton";
            this.ordersTabButton.Size = new System.Drawing.Size(46, 22);
            this.ordersTabButton.Text = "Orders";
            this.ordersTabButton.Click += new System.EventHandler(this.ordersTabButton_Click);
            // 
            // environmentTabButton
            // 
            this.environmentTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.environmentTabButton.Image = ((System.Drawing.Image)(resources.GetObject("environmentTabButton.Image")));
            this.environmentTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.environmentTabButton.Name = "environmentTabButton";
            this.environmentTabButton.Size = new System.Drawing.Size(79, 22);
            this.environmentTabButton.Text = "Environment";
            this.environmentTabButton.Click += new System.EventHandler(this.environmentTabButton_Click);
            // 
            // controlTabButton
            // 
            this.controlTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.controlTabButton.Image = ((System.Drawing.Image)(resources.GetObject("controlTabButton.Image")));
            this.controlTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.controlTabButton.Name = "controlTabButton";
            this.controlTabButton.Size = new System.Drawing.Size(51, 22);
            this.controlTabButton.Text = "Control";
            this.controlTabButton.Click += new System.EventHandler(this.controlTabButton_Click);
            // 
            // plantMimicTabButton
            // 
            this.plantMimicTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.plantMimicTabButton.Image = ((System.Drawing.Image)(resources.GetObject("plantMimicTabButton.Image")));
            this.plantMimicTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.plantMimicTabButton.Name = "plantMimicTabButton";
            this.plantMimicTabButton.Size = new System.Drawing.Size(75, 22);
            this.plantMimicTabButton.Text = "Plant Mimic";
            this.plantMimicTabButton.Click += new System.EventHandler(this.plantMimicScreenButton_Click);
            // 
            // tablessControlPanel
            // 
            this.tablessControlPanel.Controls.Add(this.plantMimicTab);
            this.tablessControlPanel.Controls.Add(this.controlTab);
            this.tablessControlPanel.Controls.Add(this.environmentTab);
            this.tablessControlPanel.Controls.Add(this.ordersTab);
            this.tablessControlPanel.Controls.Add(this.reportsTab);
            this.tablessControlPanel.Location = new System.Drawing.Point(0, 72);
            this.tablessControlPanel.Name = "tablessControlPanel";
            this.tablessControlPanel.SelectedIndex = 0;
            this.tablessControlPanel.Size = new System.Drawing.Size(694, 378);
            this.tablessControlPanel.TabIndex = 4;
            // 
            // plantMimicTab
            // 
            this.plantMimicTab.Controls.Add(this.dataGridView1);
            this.plantMimicTab.Controls.Add(this.Button_AddNewOrder);
            this.plantMimicTab.Location = new System.Drawing.Point(4, 22);
            this.plantMimicTab.Name = "plantMimicTab";
            this.plantMimicTab.Padding = new System.Windows.Forms.Padding(3);
            this.plantMimicTab.Size = new System.Drawing.Size(686, 352);
            this.plantMimicTab.TabIndex = 0;
            this.plantMimicTab.Text = "Plant Mimic";
            this.plantMimicTab.UseVisualStyleBackColor = true;
            // 
            // Button_AddNewOrder
            // 
            this.Button_AddNewOrder.Location = new System.Drawing.Point(21, 274);
            this.Button_AddNewOrder.Name = "Button_AddNewOrder";
            this.Button_AddNewOrder.Size = new System.Drawing.Size(183, 57);
            this.Button_AddNewOrder.TabIndex = 2;
            this.Button_AddNewOrder.Text = "AddNewOrder";
            this.Button_AddNewOrder.UseVisualStyleBackColor = true;
            this.Button_AddNewOrder.Click += new System.EventHandler(this.button_AddNewOrder_Click);
            // 
            // controlTab
            // 
            this.controlTab.Location = new System.Drawing.Point(4, 22);
            this.controlTab.Name = "controlTab";
            this.controlTab.Padding = new System.Windows.Forms.Padding(3);
            this.controlTab.Size = new System.Drawing.Size(686, 352);
            this.controlTab.TabIndex = 1;
            this.controlTab.Text = "Control";
            this.controlTab.UseVisualStyleBackColor = true;
            // 
            // environmentTab
            // 
            this.environmentTab.Location = new System.Drawing.Point(4, 22);
            this.environmentTab.Name = "environmentTab";
            this.environmentTab.Padding = new System.Windows.Forms.Padding(3);
            this.environmentTab.Size = new System.Drawing.Size(686, 352);
            this.environmentTab.TabIndex = 2;
            this.environmentTab.Text = "Environment";
            this.environmentTab.UseVisualStyleBackColor = true;
            // 
            // ordersTab
            // 
            this.ordersTab.Location = new System.Drawing.Point(4, 22);
            this.ordersTab.Name = "ordersTab";
            this.ordersTab.Padding = new System.Windows.Forms.Padding(3);
            this.ordersTab.Size = new System.Drawing.Size(686, 352);
            this.ordersTab.TabIndex = 3;
            this.ordersTab.Text = "Orders";
            this.ordersTab.UseVisualStyleBackColor = true;
            // 
            // reportsTab
            // 
            this.reportsTab.Location = new System.Drawing.Point(4, 22);
            this.reportsTab.Name = "reportsTab";
            this.reportsTab.Padding = new System.Windows.Forms.Padding(3);
            this.reportsTab.Size = new System.Drawing.Size(686, 352);
            this.reportsTab.TabIndex = 4;
            this.reportsTab.Text = "Reports";
            this.reportsTab.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderIDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.blackDataGridViewTextBoxColumn,
            this.blueDataGridViewTextBoxColumn,
            this.redDataGridViewTextBoxColumn,
            this.greenDataGridViewTextBoxColumn,
            this.whiteDataGridViewTextBoxColumn,
            this.startTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn,
            this.numberOfProductsDataGridViewTextBoxColumn,
            this.statusIDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.orderListViewBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(21, 18);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(540, 225);
            this.dataGridView1.TabIndex = 3;
            // 
            // orderListViewBindingSource
            // 
            this.orderListViewBindingSource.DataSource = typeof(TmcData.OrderListView);
            // 
            // orderIDDataGridViewTextBoxColumn
            // 
            this.orderIDDataGridViewTextBoxColumn.DataPropertyName = "OrderID";
            this.orderIDDataGridViewTextBoxColumn.HeaderText = "OrderID";
            this.orderIDDataGridViewTextBoxColumn.Name = "orderIDDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // blackDataGridViewTextBoxColumn
            // 
            this.blackDataGridViewTextBoxColumn.DataPropertyName = "Black";
            this.blackDataGridViewTextBoxColumn.HeaderText = "Black";
            this.blackDataGridViewTextBoxColumn.Name = "blackDataGridViewTextBoxColumn";
            // 
            // blueDataGridViewTextBoxColumn
            // 
            this.blueDataGridViewTextBoxColumn.DataPropertyName = "Blue";
            this.blueDataGridViewTextBoxColumn.HeaderText = "Blue";
            this.blueDataGridViewTextBoxColumn.Name = "blueDataGridViewTextBoxColumn";
            // 
            // redDataGridViewTextBoxColumn
            // 
            this.redDataGridViewTextBoxColumn.DataPropertyName = "Red";
            this.redDataGridViewTextBoxColumn.HeaderText = "Red";
            this.redDataGridViewTextBoxColumn.Name = "redDataGridViewTextBoxColumn";
            // 
            // greenDataGridViewTextBoxColumn
            // 
            this.greenDataGridViewTextBoxColumn.DataPropertyName = "Green";
            this.greenDataGridViewTextBoxColumn.HeaderText = "Green";
            this.greenDataGridViewTextBoxColumn.Name = "greenDataGridViewTextBoxColumn";
            // 
            // whiteDataGridViewTextBoxColumn
            // 
            this.whiteDataGridViewTextBoxColumn.DataPropertyName = "White";
            this.whiteDataGridViewTextBoxColumn.HeaderText = "White";
            this.whiteDataGridViewTextBoxColumn.Name = "whiteDataGridViewTextBoxColumn";
            // 
            // startTimeDataGridViewTextBoxColumn
            // 
            this.startTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.HeaderText = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.Name = "startTimeDataGridViewTextBoxColumn";
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            // 
            // numberOfProductsDataGridViewTextBoxColumn
            // 
            this.numberOfProductsDataGridViewTextBoxColumn.DataPropertyName = "NumberOfProducts";
            this.numberOfProductsDataGridViewTextBoxColumn.HeaderText = "NumberOfProducts";
            this.numberOfProductsDataGridViewTextBoxColumn.Name = "numberOfProductsDataGridViewTextBoxColumn";
            // 
            // statusIDDataGridViewTextBoxColumn
            // 
            this.statusIDDataGridViewTextBoxColumn.DataPropertyName = "StatusID";
            this.statusIDDataGridViewTextBoxColumn.HeaderText = "StatusID";
            this.statusIDDataGridViewTextBoxColumn.Name = "statusIDDataGridViewTextBoxColumn";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 450);
            this.Controls.Add(this.tablessControlPanel);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "TMC System";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tablessControlPanel.ResumeLayout(false);
            this.plantMimicTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderListViewBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel currentUserLabel;
        private System.Windows.Forms.ToolStripButton loginAndLogoutButton;
        private System.Windows.Forms.ToolStripButton eStopButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel alarmStatusLabel;
        private System.Windows.Forms.ToolStripLabel alarmTypeLabel;
        private System.Windows.Forms.ToolStripButton alarmsListButton;
        private System.Windows.Forms.ToolStripLabel alarmsCountLabel;
        private System.Windows.Forms.ToolStripButton reportsTabButton;
        private System.Windows.Forms.ToolStripButton ordersTabButton;
        private System.Windows.Forms.ToolStripButton environmentTabButton;
        private System.Windows.Forms.ToolStripButton controlTabButton;
        private System.Windows.Forms.ToolStripButton plantMimicTabButton;
        private TablessControl tablessControlPanel;
        public System.Windows.Forms.TabPage plantMimicTab;
        public System.Windows.Forms.TabPage controlTab;
        private System.Windows.Forms.TabPage environmentTab;
        private System.Windows.Forms.TabPage ordersTab;
        private System.Windows.Forms.TabPage reportsTab;
        private System.Windows.Forms.Button Button_AddNewOrder;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blackDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn redDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn greenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn whiteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberOfProductsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource orderListViewBindingSource;
    }
}

