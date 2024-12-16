namespace Lab05.GUI
{
    partial class frmRegister
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

        private void InitializeComponent()
        {
            this.cmbFaculty = new System.Windows.Forms.ComboBox();
            this.cmbMajor = new System.Windows.Forms.ComboBox();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.btnRegister = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clMssv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clHoten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clKhoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDtb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbFaculty
            // 
            this.cmbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFaculty.FormattingEnabled = true;
            this.cmbFaculty.Location = new System.Drawing.Point(441, 105);
            this.cmbFaculty.Name = "cmbFaculty";
            this.cmbFaculty.Size = new System.Drawing.Size(200, 24);
            this.cmbFaculty.TabIndex = 0;
            this.cmbFaculty.DropDown += new System.EventHandler(this.cmbFaculty_DropDown);
            this.cmbFaculty.SelectedIndexChanged += new System.EventHandler(this.cmbFaculty_SelectedIndexChanged);
            // 
            // cmbMajor
            // 
            this.cmbMajor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMajor.FormattingEnabled = true;
            this.cmbMajor.Location = new System.Drawing.Point(441, 154);
            this.cmbMajor.Name = "cmbMajor";
            this.cmbMajor.Size = new System.Drawing.Size(200, 24);
            this.cmbMajor.TabIndex = 1;
            // 
            // dgvStudents
            // 
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clChon,
            this.clMssv,
            this.clHoten,
            this.clKhoa,
            this.clDtb});
            this.dgvStudents.Location = new System.Drawing.Point(38, 230);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.RowHeadersWidth = 51;
            this.dgvStudents.Size = new System.Drawing.Size(989, 313);
            this.dgvStudents.TabIndex = 2;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(76, 549);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(93, 50);
            this.btnRegister.TabIndex = 3;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(364, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Khoa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Chuyên Ngành";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(342, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(372, 38);
            this.label2.TabIndex = 54;
            this.label2.Text = "Đăng ký chuyên ngành";
            // 
            // clChon
            // 
            this.clChon.HeaderText = "Chọn";
            this.clChon.MinimumWidth = 6;
            this.clChon.Name = "clChon";
            // 
            // clMssv
            // 
            this.clMssv.HeaderText = "MSSV";
            this.clMssv.MinimumWidth = 6;
            this.clMssv.Name = "clMssv";
            this.clMssv.ReadOnly = true;
            // 
            // clHoten
            // 
            this.clHoten.HeaderText = "Họ tên";
            this.clHoten.MinimumWidth = 6;
            this.clHoten.Name = "clHoten";
            this.clHoten.ReadOnly = true;
            // 
            // clKhoa
            // 
            this.clKhoa.HeaderText = "Khoa";
            this.clKhoa.MinimumWidth = 6;
            this.clKhoa.Name = "clKhoa";
            this.clKhoa.ReadOnly = true;
            // 
            // clDtb
            // 
            this.clDtb.HeaderText = "ĐTB";
            this.clDtb.MinimumWidth = 6;
            this.clDtb.Name = "clDtb";
            // 
            // frmRegister
            // 
            this.ClientSize = new System.Drawing.Size(1086, 611);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.dgvStudents);
            this.Controls.Add(this.cmbMajor);
            this.Controls.Add(this.cmbFaculty);
            this.Name = "frmRegister";
            this.Text = "Register Major";
            this.Load += new System.EventHandler(this.frmRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox cmbFaculty;
        private System.Windows.Forms.ComboBox cmbMajor;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMssv;
        private System.Windows.Forms.DataGridViewTextBoxColumn clHoten;
        private System.Windows.Forms.DataGridViewTextBoxColumn clKhoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDtb;
    }
}
