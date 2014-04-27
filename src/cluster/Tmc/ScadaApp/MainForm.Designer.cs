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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Assembling Robot");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Loading Robot");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Sorting Robot");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Palletising Robot");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Conveyor 1");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Conveyor 2");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Light");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Temperature");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Humidity");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Noise");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Dust");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "1034",
            "A. User",
            "Processing",
            "90 seconds"}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "1",
            "Total No. of products produced"}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "2",
            "Total average production times"}, -1);
            this.hiddenTabsControl = new Tmc.Scada.App.HiddenTabsControl();
            this.loginPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userIDTextBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.statusPage = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.controlPage = new System.Windows.Forms.TabPage();
            this.resumeProductionButton = new System.Windows.Forms.Button();
            this.stopProductionButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.orderPage = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.currentOrdersListView = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.createOrderButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.reportPage = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.recentReportsListView = new System.Windows.Forms.ListView();
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.createReportButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.eStopButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.dataSet1 = new System.Data.DataSet();
            this.hiddenTabsControl.SuspendLayout();
            this.loginPage.SuspendLayout();
            this.statusPage.SuspendLayout();
            this.controlPage.SuspendLayout();
            this.orderPage.SuspendLayout();
            this.reportPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // hiddenTabsControl
            // 
            this.hiddenTabsControl.Controls.Add(this.loginPage);
            this.hiddenTabsControl.Controls.Add(this.statusPage);
            this.hiddenTabsControl.Controls.Add(this.controlPage);
            this.hiddenTabsControl.Controls.Add(this.orderPage);
            this.hiddenTabsControl.Controls.Add(this.reportPage);
            this.hiddenTabsControl.Location = new System.Drawing.Point(11, 40);
            this.hiddenTabsControl.Name = "hiddenTabsControl";
            this.hiddenTabsControl.SelectedIndex = 0;
            this.hiddenTabsControl.Size = new System.Drawing.Size(584, 386);
            this.hiddenTabsControl.TabIndex = 0;
            // 
            // loginPage
            // 
            this.loginPage.Controls.Add(this.label4);
            this.loginPage.Controls.Add(this.label3);
            this.loginPage.Controls.Add(this.label2);
            this.loginPage.Controls.Add(this.label1);
            this.loginPage.Controls.Add(this.passwordTextBox);
            this.loginPage.Controls.Add(this.userIDTextBox);
            this.loginPage.Controls.Add(this.loginButton);
            this.loginPage.Location = new System.Drawing.Point(4, 22);
            this.loginPage.Name = "loginPage";
            this.loginPage.Padding = new System.Windows.Forms.Padding(3);
            this.loginPage.Size = new System.Drawing.Size(576, 360);
            this.loginPage.TabIndex = 0;
            this.loginPage.Text = "Login";
            this.loginPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Forgot your password?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "If you are a new user, click here to register for access";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Please login to access system";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(84, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Login";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(82, 115);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 9;
            this.passwordTextBox.Text = "Password";
            // 
            // userIDTextBox
            // 
            this.userIDTextBox.Location = new System.Drawing.Point(82, 89);
            this.userIDTextBox.Name = "userIDTextBox";
            this.userIDTextBox.Size = new System.Drawing.Size(100, 20);
            this.userIDTextBox.TabIndex = 8;
            this.userIDTextBox.Text = "User ID";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(83, 141);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 7;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // statusPage
            // 
            this.statusPage.Controls.Add(this.label6);
            this.statusPage.Controls.Add(this.label7);
            this.statusPage.Controls.Add(this.listView2);
            this.statusPage.Controls.Add(this.listView1);
            this.statusPage.Controls.Add(this.label8);
            this.statusPage.Controls.Add(this.label9);
            this.statusPage.Location = new System.Drawing.Point(4, 22);
            this.statusPage.Name = "statusPage";
            this.statusPage.Padding = new System.Windows.Forms.Padding(3);
            this.statusPage.Size = new System.Drawing.Size(576, 360);
            this.statusPage.TabIndex = 2;
            this.statusPage.Text = "Status";
            this.statusPage.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(79, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 16);
            this.label6.TabIndex = 50;
            this.label6.Text = "Status";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(82, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "Environmental Conditions";
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView2.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.listView2.Location = new System.Drawing.Point(82, 67);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(341, 139);
            this.listView2.TabIndex = 48;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Component";
            this.columnHeader3.Width = 95;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Operation";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Cycle Time";
            this.columnHeader5.Width = 65;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Components Handled";
            this.columnHeader6.Width = 114;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11});
            this.listView1.Location = new System.Drawing.Point(82, 231);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(183, 116);
            this.listView1.TabIndex = 47;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Condition";
            this.columnHeader1.Width = 93;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status";
            this.columnHeader2.Width = 86;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(214, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Analysing, Sorting and Producing";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(82, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Current Operation Mode: ";
            // 
            // controlPage
            // 
            this.controlPage.Controls.Add(this.resumeProductionButton);
            this.controlPage.Controls.Add(this.stopProductionButton);
            this.controlPage.Controls.Add(this.label15);
            this.controlPage.Controls.Add(this.comboBox2);
            this.controlPage.Controls.Add(this.comboBox1);
            this.controlPage.Controls.Add(this.label17);
            this.controlPage.Controls.Add(this.label18);
            this.controlPage.Location = new System.Drawing.Point(4, 22);
            this.controlPage.Name = "controlPage";
            this.controlPage.Padding = new System.Windows.Forms.Padding(3);
            this.controlPage.Size = new System.Drawing.Size(576, 360);
            this.controlPage.TabIndex = 3;
            this.controlPage.Text = "Control";
            this.controlPage.UseVisualStyleBackColor = true;
            // 
            // resumeProductionButton
            // 
            this.resumeProductionButton.Location = new System.Drawing.Point(212, 176);
            this.resumeProductionButton.Name = "resumeProductionButton";
            this.resumeProductionButton.Size = new System.Drawing.Size(75, 23);
            this.resumeProductionButton.TabIndex = 71;
            this.resumeProductionButton.Text = "Resume";
            this.resumeProductionButton.UseVisualStyleBackColor = true;
            this.resumeProductionButton.Click += new System.EventHandler(this.resumeProductionButton_Click);
            // 
            // stopProductionButton
            // 
            this.stopProductionButton.Location = new System.Drawing.Point(80, 177);
            this.stopProductionButton.Name = "stopProductionButton";
            this.stopProductionButton.Size = new System.Drawing.Size(75, 23);
            this.stopProductionButton.TabIndex = 70;
            this.stopProductionButton.Text = "Stop Production";
            this.stopProductionButton.UseVisualStyleBackColor = true;
            this.stopProductionButton.Click += new System.EventHandler(this.stopProductionButton_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(77, 34);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 16);
            this.label15.TabIndex = 69;
            this.label15.Text = "Control";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "100%",
            "75%",
            "50%",
            "25%"});
            this.comboBox2.Location = new System.Drawing.Point(80, 134);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(186, 21);
            this.comboBox2.TabIndex = 68;
            this.comboBox2.Text = "Select Processing Rate";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Analysing & Sorting",
            "Producing",
            "Analysing, Sorting & Producing"});
            this.comboBox1.Location = new System.Drawing.Point(80, 99);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(186, 21);
            this.comboBox1.TabIndex = 67;
            this.comboBox1.Text = "Select Operation Mode";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(209, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(163, 13);
            this.label17.TabIndex = 64;
            this.label17.Text = "Analysing, Sorting and Producing";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(77, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(126, 13);
            this.label18.TabIndex = 63;
            this.label18.Text = "Current Operation Mode: ";
            // 
            // orderPage
            // 
            this.orderPage.Controls.Add(this.label11);
            this.orderPage.Controls.Add(this.currentOrdersListView);
            this.orderPage.Controls.Add(this.createOrderButton);
            this.orderPage.Controls.Add(this.label12);
            this.orderPage.Location = new System.Drawing.Point(4, 22);
            this.orderPage.Name = "orderPage";
            this.orderPage.Padding = new System.Windows.Forms.Padding(3);
            this.orderPage.Size = new System.Drawing.Size(576, 360);
            this.orderPage.TabIndex = 4;
            this.orderPage.Text = "Orders";
            this.orderPage.UseVisualStyleBackColor = true;
            this.orderPage.Click += new System.EventHandler(this.orderPage_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(83, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 64;
            this.label11.Text = "Current Orders";
            // 
            // currentOrdersListView
            // 
            this.currentOrdersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16});
            this.currentOrdersListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem12});
            this.currentOrdersListView.Location = new System.Drawing.Point(83, 107);
            this.currentOrdersListView.Name = "currentOrdersListView";
            this.currentOrdersListView.Size = new System.Drawing.Size(399, 187);
            this.currentOrdersListView.TabIndex = 63;
            this.currentOrdersListView.UseCompatibleStateImageBehavior = false;
            this.currentOrdersListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.DisplayIndex = 1;
            this.columnHeader11.Text = "Owner";
            // 
            // columnHeader12
            // 
            this.columnHeader12.DisplayIndex = 2;
            this.columnHeader12.Text = "Status";
            this.columnHeader12.Width = 64;
            // 
            // columnHeader13
            // 
            this.columnHeader13.DisplayIndex = 0;
            this.columnHeader13.Text = "Order";
            this.columnHeader13.Width = 64;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Est. Run Time";
            this.columnHeader14.Width = 85;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Modify";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Cancel";
            // 
            // createOrderButton
            // 
            this.createOrderButton.Location = new System.Drawing.Point(83, 52);
            this.createOrderButton.Name = "createOrderButton";
            this.createOrderButton.Size = new System.Drawing.Size(75, 23);
            this.createOrderButton.TabIndex = 62;
            this.createOrderButton.Text = "Create Order";
            this.createOrderButton.UseVisualStyleBackColor = true;
            this.createOrderButton.Click += new System.EventHandler(this.createOrderButton_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(83, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 16);
            this.label12.TabIndex = 61;
            this.label12.Text = "Orders";
            // 
            // reportPage
            // 
            this.reportPage.Controls.Add(this.label13);
            this.reportPage.Controls.Add(this.recentReportsListView);
            this.reportPage.Controls.Add(this.createReportButton);
            this.reportPage.Controls.Add(this.label14);
            this.reportPage.Location = new System.Drawing.Point(4, 22);
            this.reportPage.Name = "reportPage";
            this.reportPage.Padding = new System.Windows.Forms.Padding(3);
            this.reportPage.Size = new System.Drawing.Size(576, 360);
            this.reportPage.TabIndex = 5;
            this.reportPage.Text = "Reports";
            this.reportPage.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(82, 110);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 13);
            this.label13.TabIndex = 66;
            this.label13.Text = "Recent Reports";
            // 
            // recentReportsListView
            // 
            this.recentReportsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader17,
            this.columnHeader18});
            this.recentReportsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem13,
            listViewItem14});
            this.recentReportsListView.Location = new System.Drawing.Point(82, 129);
            this.recentReportsListView.Name = "recentReportsListView";
            this.recentReportsListView.Size = new System.Drawing.Size(225, 187);
            this.recentReportsListView.TabIndex = 65;
            this.recentReportsListView.UseCompatibleStateImageBehavior = false;
            this.recentReportsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Report";
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Name";
            this.columnHeader18.Width = 160;
            // 
            // createReportButton
            // 
            this.createReportButton.Location = new System.Drawing.Point(82, 61);
            this.createReportButton.Name = "createReportButton";
            this.createReportButton.Size = new System.Drawing.Size(75, 23);
            this.createReportButton.TabIndex = 64;
            this.createReportButton.Text = "Create Report";
            this.createReportButton.UseVisualStyleBackColor = true;
            this.createReportButton.Click += new System.EventHandler(this.createReportButton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(82, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 16);
            this.label14.TabIndex = 63;
            this.label14.Text = "Reports";
            // 
            // eStopButton
            // 
            this.eStopButton.Location = new System.Drawing.Point(412, 11);
            this.eStopButton.Name = "eStopButton";
            this.eStopButton.Size = new System.Drawing.Size(97, 23);
            this.eStopButton.TabIndex = 52;
            this.eStopButton.Text = "Emergency Stop";
            this.eStopButton.UseVisualStyleBackColor = true;
            this.eStopButton.Click += new System.EventHandler(this.eStopButton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(543, 12);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 53;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 429);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.eStopButton);
            this.Controls.Add(this.hiddenTabsControl);
            this.Name = "MainForm";
            this.Text = "TMC Management System";
            this.hiddenTabsControl.ResumeLayout(false);
            this.loginPage.ResumeLayout(false);
            this.loginPage.PerformLayout();
            this.statusPage.ResumeLayout(false);
            this.statusPage.PerformLayout();
            this.controlPage.ResumeLayout(false);
            this.controlPage.PerformLayout();
            this.orderPage.ResumeLayout(false);
            this.orderPage.PerformLayout();
            this.reportPage.ResumeLayout(false);
            this.reportPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HiddenTabsControl hiddenTabsControl;
        private System.Windows.Forms.TabPage loginPage;
        private System.Windows.Forms.TabPage statusPage;
        private System.Windows.Forms.TabPage controlPage;
        private System.Windows.Forms.TabPage orderPage;
        private System.Windows.Forms.Button eStopButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox userIDTextBox;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView currentOrdersListView;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.Button createOrderButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage reportPage;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListView recentReportsListView;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.Button createReportButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Button resumeProductionButton;
        private System.Windows.Forms.Button stopProductionButton;
        private System.Data.DataSet dataSet1;
    }
}

