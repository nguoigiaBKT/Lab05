using Lab05.DAL.Entities;
using Lab05.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

namespace Lab05.GUI
{
    public partial class frmRegister : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private readonly MajorService majorService = new MajorService();

        public frmRegister()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dgvStudents.Columns.Clear();
            DataGridViewCheckBoxColumn clChon = new DataGridViewCheckBoxColumn();
            clChon.Name = "clChon";
            clChon.HeaderText = "Chọn";
            dgvStudents.Columns.Add(clChon);
            dgvStudents.Columns.Add("clMssv", "MSSV");
            dgvStudents.Columns.Add("clHoten", "Họ Tên");
            dgvStudents.Columns.Add("clKhoa", "Khoa");
            dgvStudents.Columns.Add("clDtb", "Điểm Trung Bình");
        }


        private void BindGrid(List<Student> listStudents)
        {
            dgvStudents.Rows.Clear();

            foreach (var item in listStudents)
            {
                int index = dgvStudents.Rows.Add();
                dgvStudents.Rows[index].Cells["clChon"].Value = false;
                dgvStudents.Rows[index].Cells["clMssv"].Value = item.StudentID;
                dgvStudents.Rows[index].Cells["clHoten"].Value = item.FullName;
                dgvStudents.Rows[index].Cells["clKhoa"].Value = item.Faculty?.FacultyName ?? string.Empty;
                dgvStudents.Rows[index].Cells["clDtb"].Value = item.AverageScore;
            }
        }


        private void cmbFaculty_DropDown(object sender, EventArgs e)
        {

            if (cmbFaculty.DataSource == null)
            {
                try
                {
                    var listFaculties = facultyService.GetAll();
                    FillFacultyCombobox(listFaculties);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FillFacultyCombobox(List<Faculty> listFaculties)
        {
            this.cmbFaculty.DataSource = listFaculties;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
            this.cmbFaculty.SelectedIndex = -1;
        }

        private void FillMajorCombobox(List<Major> listMajors)
        {
            this.cmbMajor.DataSource = listMajors;
            this.cmbMajor.DisplayMember = "Name";
            this.cmbMajor.ValueMember = "MajorID";
            this.cmbMajor.SelectedIndex = -1;
        }

        private readonly StudentModel context = new StudentModel();


        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMajor.SelectedValue == null)
                {
                    MessageBox.Show("Chưa chọn chuyên ngành.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int majorID = (int)cmbMajor.SelectedValue;
                bool studentSelected = false;

                foreach (DataGridViewRow row in dgvStudents.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["clChon"].Value))
                    {
                        studentSelected = true;
                        string studentID = row.Cells["clMssv"].Value.ToString();
                        var student = studentService.FindById(studentID);
                        if (student != null)
                        {
                            student.MajorID = majorID;
                            studentService.InsertOrUpdate(student);
                        }
                    }
                }

                if (!studentSelected)
                {
                    MessageBox.Show("Chưa chọn sinh viên nào.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Đăng ký chuyên ngành thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

             
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đăng ký chuyên ngành {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataGridView()
        {
            if (cmbFaculty.SelectedValue != null && int.TryParse(cmbFaculty.SelectedValue.ToString(), out int facultyID))
            {
                var students = context.Student.Include(s => s.Faculty)
                                              .Include(s => s.Major)
                                              .Where(s => s.FacultyID == facultyID)
                                              .ToList();
                BindGrid(students);
            }
        }

        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFaculty.SelectedValue != null && int.TryParse(cmbFaculty.SelectedValue.ToString(), out int facultyID))
            {
                var students = context.Student.Include(s => s.Faculty)
                                              .Include(s => s.Major)
                                              .Where(s => s.FacultyID == facultyID)
                                              .ToList();
                if (students == null || !students.Any())
                {
                    MessageBox.Show("Không tìm thấy.");
                }
                else
                {
                    BindGrid(students);
                }

                var majors = majorService.GetByFacultyId(facultyID);
                FillMajorCombobox(majors);
            }
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            cmbFaculty.DataSource = null;
            cmbMajor.DataSource = null;
            dgvStudents.Rows.Clear();
            cmbFaculty.SelectedIndex = -1;
            cmbFaculty.SelectedIndexChanged += cmbFaculty_SelectedIndexChanged;
        }


    }
}
