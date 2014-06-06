namespace Tmc.Scada.App
{
    partial class Order
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.orderIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blackDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.redDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.greenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whiteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberOfProductsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderListViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button_Add = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.numericUpDown_black = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_blue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_green = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_red = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_white = new System.Windows.Forms.NumericUpDown();
            this.label_black = new System.Windows.Forms.Label();
            this.label_blue = new System.Windows.Forms.Label();
            this.label_green = new System.Windows.Forms.Label();
            this.label_red = new System.Windows.Forms.Label();
            this.label_white = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_showCancelled = new System.Windows.Forms.CheckBox();
            this.label_addOrderError = new System.Windows.Forms.Label();
            this.label_invalidOrderReason = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderListViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_black)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_white)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderIDDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.blackDataGridViewTextBoxColumn,
            this.blueDataGridViewTextBoxColumn,
            this.redDataGridViewTextBoxColumn,
            this.greenDataGridViewTextBoxColumn,
            this.whiteDataGridViewTextBoxColumn,
            this.startTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn,
            this.numberOfProductsDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.orderListViewBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(41, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(474, 222);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // orderIDDataGridViewTextBoxColumn
            // 
            this.orderIDDataGridViewTextBoxColumn.DataPropertyName = "OrderID";
            this.orderIDDataGridViewTextBoxColumn.HeaderText = "OrderID";
            this.orderIDDataGridViewTextBoxColumn.Name = "orderIDDataGridViewTextBoxColumn";
            this.orderIDDataGridViewTextBoxColumn.Width = 50;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.Width = 60;
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
            this.startTimeDataGridViewTextBoxColumn.Width = 110;
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            this.endTimeDataGridViewTextBoxColumn.Width = 110;
            // 
            // numberOfProductsDataGridViewTextBoxColumn
            // 
            this.numberOfProductsDataGridViewTextBoxColumn.DataPropertyName = "NumberOfProducts";
            this.numberOfProductsDataGridViewTextBoxColumn.HeaderText = "NumberOfProducts";
            this.numberOfProductsDataGridViewTextBoxColumn.Name = "numberOfProductsDataGridViewTextBoxColumn";
            // 
            // orderListViewBindingSource
            // 
            this.orderListViewBindingSource.DataSource = typeof(TmcData.OrderListView);
            // 
            // button_Add
            // 
            this.button_Add.AllowDrop = true;
            this.button_Add.Location = new System.Drawing.Point(16, 182);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(84, 23);
            this.button_Add.TabIndex = 1;
            this.button_Add.Text = "Add New Order";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(41, 281);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(101, 23);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "Cancel Order";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // numericUpDown_black
            // 
            this.numericUpDown_black.Location = new System.Drawing.Point(65, 37);
            this.numericUpDown_black.Name = "numericUpDown_black";
            this.numericUpDown_black.Size = new System.Drawing.Size(35, 20);
            this.numericUpDown_black.TabIndex = 3;
            // 
            // numericUpDown_blue
            // 
            this.numericUpDown_blue.Location = new System.Drawing.Point(65, 63);
            this.numericUpDown_blue.Name = "numericUpDown_blue";
            this.numericUpDown_blue.Size = new System.Drawing.Size(35, 20);
            this.numericUpDown_blue.TabIndex = 4;
            // 
            // numericUpDown_green
            // 
            this.numericUpDown_green.Location = new System.Drawing.Point(65, 115);
            this.numericUpDown_green.Name = "numericUpDown_green";
            this.numericUpDown_green.Size = new System.Drawing.Size(35, 20);
            this.numericUpDown_green.TabIndex = 6;
            // 
            // numericUpDown_red
            // 
            this.numericUpDown_red.Location = new System.Drawing.Point(65, 89);
            this.numericUpDown_red.Name = "numericUpDown_red";
            this.numericUpDown_red.Size = new System.Drawing.Size(35, 20);
            this.numericUpDown_red.TabIndex = 5;
            // 
            // numericUpDown_white
            // 
            this.numericUpDown_white.Location = new System.Drawing.Point(65, 141);
            this.numericUpDown_white.Name = "numericUpDown_white";
            this.numericUpDown_white.Size = new System.Drawing.Size(35, 20);
            this.numericUpDown_white.TabIndex = 7;
            // 
            // label_black
            // 
            this.label_black.AutoSize = true;
            this.label_black.Location = new System.Drawing.Point(13, 39);
            this.label_black.Name = "label_black";
            this.label_black.Size = new System.Drawing.Size(34, 13);
            this.label_black.TabIndex = 8;
            this.label_black.Text = "Black";
            // 
            // label_blue
            // 
            this.label_blue.AutoSize = true;
            this.label_blue.Location = new System.Drawing.Point(13, 65);
            this.label_blue.Name = "label_blue";
            this.label_blue.Size = new System.Drawing.Size(28, 13);
            this.label_blue.TabIndex = 9;
            this.label_blue.Text = "Blue";
            // 
            // label_green
            // 
            this.label_green.AutoSize = true;
            this.label_green.Location = new System.Drawing.Point(13, 117);
            this.label_green.Name = "label_green";
            this.label_green.Size = new System.Drawing.Size(36, 13);
            this.label_green.TabIndex = 11;
            this.label_green.Text = "Green";
            // 
            // label_red
            // 
            this.label_red.AutoSize = true;
            this.label_red.Location = new System.Drawing.Point(13, 91);
            this.label_red.Name = "label_red";
            this.label_red.Size = new System.Drawing.Size(27, 13);
            this.label_red.TabIndex = 10;
            this.label_red.Text = "Red";
            // 
            // label_white
            // 
            this.label_white.AutoSize = true;
            this.label_white.Location = new System.Drawing.Point(13, 148);
            this.label_white.Name = "label_white";
            this.label_white.Size = new System.Drawing.Size(35, 13);
            this.label_white.TabIndex = 12;
            this.label_white.Text = "White";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label_blue);
            this.panel1.Controls.Add(this.label_white);
            this.panel1.Controls.Add(this.button_Add);
            this.panel1.Controls.Add(this.numericUpDown_black);
            this.panel1.Controls.Add(this.label_green);
            this.panel1.Controls.Add(this.numericUpDown_blue);
            this.panel1.Controls.Add(this.label_red);
            this.panel1.Controls.Add(this.numericUpDown_red);
            this.panel1.Controls.Add(this.numericUpDown_green);
            this.panel1.Controls.Add(this.label_black);
            this.panel1.Controls.Add(this.numericUpDown_white);
            this.panel1.Location = new System.Drawing.Point(521, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(115, 222);
            this.panel1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Add New Order";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(209, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "Order List";
            // 
            // checkBox_showCancelled
            // 
            this.checkBox_showCancelled.AutoSize = true;
            this.checkBox_showCancelled.Location = new System.Drawing.Point(160, 285);
            this.checkBox_showCancelled.Name = "checkBox_showCancelled";
            this.checkBox_showCancelled.Size = new System.Drawing.Size(138, 17);
            this.checkBox_showCancelled.TabIndex = 15;
            this.checkBox_showCancelled.Text = "Show Cancelled Oorder";
            this.checkBox_showCancelled.UseVisualStyleBackColor = true;
            this.checkBox_showCancelled.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label_addOrderError
            // 
            this.label_addOrderError.AutoSize = true;
            this.label_addOrderError.ForeColor = System.Drawing.Color.Red;
            this.label_addOrderError.Location = new System.Drawing.Point(518, 278);
            this.label_addOrderError.Name = "label_addOrderError";
            this.label_addOrderError.Size = new System.Drawing.Size(67, 13);
            this.label_addOrderError.TabIndex = 16;
            this.label_addOrderError.Text = "Invalid Order";
            this.label_addOrderError.Visible = false;
            // 
            // label_invalidOrderReason
            // 
            this.label_invalidOrderReason.AutoSize = true;
            this.label_invalidOrderReason.ForeColor = System.Drawing.Color.Red;
            this.label_invalidOrderReason.Location = new System.Drawing.Point(534, 291);
            this.label_invalidOrderReason.MaximumSize = new System.Drawing.Size(100, 0);
            this.label_invalidOrderReason.Name = "label_invalidOrderReason";
            this.label_invalidOrderReason.Size = new System.Drawing.Size(0, 13);
            this.label_invalidOrderReason.TabIndex = 17;
            this.label_invalidOrderReason.Visible = false;
            // 
            // Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_invalidOrderReason);
            this.Controls.Add(this.label_addOrderError);
            this.Controls.Add(this.checkBox_showCancelled);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Order";
            this.Size = new System.Drawing.Size(639, 364);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderListViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_black)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_white)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.BindingSource orderListViewBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blackDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn redDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn greenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn whiteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberOfProductsDataGridViewTextBoxColumn;
        private System.Windows.Forms.NumericUpDown numericUpDown_black;
        private System.Windows.Forms.NumericUpDown numericUpDown_blue;
        private System.Windows.Forms.NumericUpDown numericUpDown_green;
        private System.Windows.Forms.NumericUpDown numericUpDown_red;
        private System.Windows.Forms.NumericUpDown numericUpDown_white;
        private System.Windows.Forms.Label label_black;
        private System.Windows.Forms.Label label_blue;
        private System.Windows.Forms.Label label_green;
        private System.Windows.Forms.Label label_red;
        private System.Windows.Forms.Label label_white;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_showCancelled;
        private System.Windows.Forms.Label label_addOrderError;
        private System.Windows.Forms.Label label_invalidOrderReason;
    }
}
