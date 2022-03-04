namespace BikiniBottomMall
{
    partial class ProductsManagement
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
            this.Search = new System.Windows.Forms.Label();
            this.txtSearch1 = new System.Windows.Forms.TextBox();
            this.backtosign = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Myproducts = new System.Windows.Forms.Label();
            this.txtproductid = new System.Windows.Forms.TextBox();
            this.Update = new System.Windows.Forms.Button();
            this.txtinventory = new System.Windows.Forms.TextBox();
            this.Inventory = new System.Windows.Forms.Label();
            this.txtSearch2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtsales = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Search
            // 
            this.Search.AutoSize = true;
            this.Search.BackColor = System.Drawing.Color.White;
            this.Search.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Search.Location = new System.Drawing.Point(96, 400);
            this.Search.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(131, 19);
            this.Search.TabIndex = 0;
            this.Search.Text = "Products Search";
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // txtSearch1
            // 
            this.txtSearch1.Location = new System.Drawing.Point(238, 398);
            this.txtSearch1.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch1.Name = "txtSearch1";
            this.txtSearch1.Size = new System.Drawing.Size(239, 27);
            this.txtSearch1.TabIndex = 1;
            this.txtSearch1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // backtosign
            // 
            this.backtosign.BackColor = System.Drawing.Color.White;
            this.backtosign.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.backtosign.Location = new System.Drawing.Point(797, 40);
            this.backtosign.Name = "backtosign";
            this.backtosign.Size = new System.Drawing.Size(148, 29);
            this.backtosign.TabIndex = 6;
            this.backtosign.Text = "SignOut";
            this.backtosign.UseVisualStyleBackColor = false;
            this.backtosign.Click += new System.EventHandler(this.backtosign_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(93, 524);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.Size = new System.Drawing.Size(842, 179);
            this.dataGridView3.TabIndex = 7;
            this.dataGridView3.Text = "dataGridView1";
            this.dataGridView3.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            this.dataGridView3.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView3_RowHeaderMouseDoubleClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(97, 187);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(317, 155);
            this.dataGridView2.TabIndex = 10;
            this.dataGridView2.Text = "dataGridView1";
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick_1);
            // 
            // Myproducts
            // 
            this.Myproducts.AutoSize = true;
            this.Myproducts.BackColor = System.Drawing.Color.White;
            this.Myproducts.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Myproducts.Location = new System.Drawing.Point(139, 452);
            this.Myproducts.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Myproducts.Name = "Myproducts";
            this.Myproducts.Size = new System.Drawing.Size(90, 19);
            this.Myproducts.TabIndex = 11;
            this.Myproducts.Text = "Product ID";
            // 
            // txtproductid
            // 
            this.txtproductid.Location = new System.Drawing.Point(241, 450);
            this.txtproductid.Margin = new System.Windows.Forms.Padding(2);
            this.txtproductid.Name = "txtproductid";
            this.txtproductid.ReadOnly = true;
            this.txtproductid.Size = new System.Drawing.Size(127, 27);
            this.txtproductid.TabIndex = 12;
            this.txtproductid.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // Update
            // 
            this.Update.BackColor = System.Drawing.Color.White;
            this.Update.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Update.Location = new System.Drawing.Point(628, 446);
            this.Update.Margin = new System.Windows.Forms.Padding(2);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(104, 28);
            this.Update.TabIndex = 14;
            this.Update.Text = "Update";
            this.Update.UseVisualStyleBackColor = false;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // txtinventory
            // 
            this.txtinventory.Location = new System.Drawing.Point(484, 450);
            this.txtinventory.Margin = new System.Windows.Forms.Padding(2);
            this.txtinventory.Name = "txtinventory";
            this.txtinventory.Size = new System.Drawing.Size(111, 27);
            this.txtinventory.TabIndex = 16;
            // 
            // Inventory
            // 
            this.Inventory.AutoSize = true;
            this.Inventory.BackColor = System.Drawing.Color.White;
            this.Inventory.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Inventory.Location = new System.Drawing.Point(398, 452);
            this.Inventory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Inventory.Name = "Inventory";
            this.Inventory.Size = new System.Drawing.Size(83, 19);
            this.Inventory.TabIndex = 15;
            this.Inventory.Text = "Inventory";
            // 
            // txtSearch2
            // 
            this.txtSearch2.Location = new System.Drawing.Point(209, 156);
            this.txtSearch2.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch2.Name = "txtSearch2";
            this.txtSearch2.Size = new System.Drawing.Size(207, 27);
            this.txtSearch2.TabIndex = 18;
            this.txtSearch2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(97, 158);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "Order Search";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(97, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "History Order";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(535, 187);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(400, 155);
            this.dataGridView1.TabIndex = 28;
            this.dataGridView1.Text = "dataGridView1";
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(693, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 19);
            this.label6.TabIndex = 27;
            this.label6.Text = "Top 3 Sales";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(688, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 19);
            this.label3.TabIndex = 17;
            this.label3.Text = "Annual Sales";
            // 
            // txtsales
            // 
            this.txtsales.Location = new System.Drawing.Point(818, 119);
            this.txtsales.Margin = new System.Windows.Forms.Padding(2);
            this.txtsales.Name = "txtsales";
            this.txtsales.ReadOnly = true;
            this.txtsales.Size = new System.Drawing.Size(127, 27);
            this.txtsales.TabIndex = 12;
            this.txtsales.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // ProductsManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.txtsales);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearch2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtinventory);
            this.Controls.Add(this.Inventory);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.txtproductid);
            this.Controls.Add(this.Myproducts);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.backtosign);
            this.Controls.Add(this.txtSearch1);
            this.Controls.Add(this.Search);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ProductsManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductsPage";
            this.Load += new System.EventHandler(this.ProductsManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSearch1;
        private System.Windows.Forms.Button backtosign;
        private System.Windows.Forms.Label Search;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label Myproducts;
        private System.Windows.Forms.TextBox txtsales;
        private System.Windows.Forms.Button Update;
        private System.Windows.Forms.TextBox txtInventory;
        private System.Windows.Forms.Label Inventory;
        private System.Windows.Forms.TextBox txtSearch2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BusinessCenter;
        private System.Windows.Forms.TextBox txtproductid;
        private System.Windows.Forms.TextBox txtinventory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
    }
}