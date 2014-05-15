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
            this.button_Add = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderListViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
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
            this.numberOfProductsDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.orderListViewBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(28, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(501, 378);
            this.dataGridView1.TabIndex = 0;
            // 
            // button_Add
            // 
            this.button_Add.AllowDrop = true;
            this.button_Add.Location = new System.Drawing.Point(565, 87);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(101, 23);
            this.button_Add.TabIndex = 1;
            this.button_Add.Text = "Add New Order";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(565, 130);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(101, 23);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "Cancel Order";
            this.button_Cancel.UseVisualStyleBackColor = true;
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
            // Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Order";
            this.Size = new System.Drawing.Size(713, 430);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderListViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_Cancel;
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
        private System.Windows.Forms.BindingSource orderListViewBindingSource;
    }
}
