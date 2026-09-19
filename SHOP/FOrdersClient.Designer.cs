namespace SHOP
{
    partial class FOrdersClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOrdersClient));
            this.panel1 = new System.Windows.Forms.Panel();
            this.IdUser = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvOrdersClient = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.отменитьЗаказToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.возратToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersClient)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.IdUser);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(903, 110);
            this.panel1.TabIndex = 2;
            // 
            // IdUser
            // 
            this.IdUser.AutoSize = true;
            this.IdUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.IdUser.Location = new System.Drawing.Point(607, 52);
            this.IdUser.Name = "IdUser";
            this.IdUser.Size = new System.Drawing.Size(19, 15);
            this.IdUser.TabIndex = 5;
            this.IdUser.Text = "Id";
            this.IdUser.Visible = false;
            this.IdUser.Click += new System.EventHandler(this.IdUser_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(33, 36);
            this.btnExit.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 39);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Назад";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(353, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ваши заказы";
            // 
            // dgvOrdersClient
            // 
            this.dgvOrdersClient.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrdersClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdersClient.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvOrdersClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdersClient.Location = new System.Drawing.Point(0, 110);
            this.dgvOrdersClient.Name = "dgvOrdersClient";
            this.dgvOrdersClient.Size = new System.Drawing.Size(903, 364);
            this.dgvOrdersClient.TabIndex = 3;
            this.dgvOrdersClient.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdersClient_CellValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отменитьЗаказToolStripMenuItem,
            this.возратToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 48);
            // 
            // отменитьЗаказToolStripMenuItem
            // 
            this.отменитьЗаказToolStripMenuItem.Name = "отменитьЗаказToolStripMenuItem";
            this.отменитьЗаказToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.отменитьЗаказToolStripMenuItem.Text = "Отменить заказ";
            this.отменитьЗаказToolStripMenuItem.Click += new System.EventHandler(this.отменитьЗаказToolStripMenuItem_Click);
            // 
            // возратToolStripMenuItem
            // 
            this.возратToolStripMenuItem.Name = "возратToolStripMenuItem";
            this.возратToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.возратToolStripMenuItem.Text = "Оформить возрат";
            this.возратToolStripMenuItem.Click += new System.EventHandler(this.возратToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(726, 43);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 32);
            this.button1.TabIndex = 8;
            this.button1.Text = "Обновить список";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FOrdersClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 474);
            this.Controls.Add(this.dgvOrdersClient);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FOrdersClient";
            this.Text = "История заказов";
            this.Load += new System.EventHandler(this.FOrdersClient_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersClient)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label IdUser;
        private System.Windows.Forms.DataGridView dgvOrdersClient;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem отменитьЗаказToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem возратToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}