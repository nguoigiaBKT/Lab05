using Lab05.DAL;
using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using Lab05.BUS;
using System.Drawing;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity.Infrastructure;



namespace Lab05.GUI
{
    public partial class frmStudent : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private string avatarFilePath = string.Empty;

        public frmStudent()
        {
            InitializeComponent();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dgvStudents);
                var listFacultys = facultyService.GetAll();
                var listStudents = studentService.GetAll();
                FillFalcultyCombobox(listFacultys);
                BindGrid(listStudents);
                picAvatar.Image = null;
                picAvatar.SizeMode = PictureBoxSizeMode.Zoom; 
                cmbFaculty.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
         
            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }

        private void BindGrid(List<Student> listStudents)
        {
            dgvStudents.Rows.Clear();

            foreach (var item in listStudents)
            {
                int index = dgvStudents.Rows.Add();

                if (index >= 0 && index < dgvStudents.Rows.Count)
                {
                    dgvStudents.Rows[index].Cells["clMssv"].Value = item.StudentID;
                    dgvStudents.Rows[index].Cells["clHoten"].Value = item.FullName;

                    if (item.Faculty != null)
                    {
                        dgvStudents.Rows[index].Cells["clKhoa"].Value = item.Faculty.FacultyName;
                    }
                    else
                    {
                        dgvStudents.Rows[index].Cells["clKhoa"].Value = string.Empty;
                    }

                    dgvStudents.Rows[index].Cells["clDtb"].Value = item.AverageScore.ToString(CultureInfo.InvariantCulture);

                    if (item.Major != null)
                    {
                        dgvStudents.Rows[index].Cells["clChuyennganh"].Value = item.Major.Name;
                    }
                    else
                    {
                        dgvStudents.Rows[index].Cells["clChuyennganh"].Value = string.Empty;
                    }

                    ShowAvatar(item.StudentID);
                }
                else
                {
                    MessageBox.Show($"Index out of range: {index}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void DeleteAvatar(string studentID)
        {
            try
            {
                string folderPath = @"D:\DataDownloaded\LapTrinhWindows\Projects\Lab05\Images";
                string[] fileExtensions = { ".jpg", ".jpeg", ".png" };

                foreach (var extension in fileExtensions)
                {
                    string filePath = Path.Combine(folderPath, $"{studentID}{extension}");
                    if (File.Exists(filePath))
                    {
                        if (picAvatar.Image != null)
                        {
                            picAvatar.Image.Dispose();
                            picAvatar.Image = null;
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa avatar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string SaveAvatar(string sourceFilePath, string studentID)
        {
            try
            {
                string folderPath = @"D:\DataDownloaded\LapTrinhWindows\Projects\Lab05\Images";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileExtension = Path.GetExtension(sourceFilePath);
                string targetFilePath = Path.Combine(folderPath, $"{studentID}{fileExtension}");

                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Không tìm thấy file: {sourceFilePath}");
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Copy(sourceFilePath, targetFilePath, true);
                return $"{studentID}{fileExtension}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu avatar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void ShowAvatar(string studentID)
        {
            string folderPath = @"D:\DataDownloaded\LapTrinhWindows\Projects\Lab05\Images";

            var student = studentService.FindById(studentID);

            if (student != null && !string.IsNullOrEmpty(student.Avatar))
            {
                string avatarFilePath = Path.Combine(folderPath, student.Avatar);

                if (File.Exists(avatarFilePath))
                {
                    using (var stream = new FileStream(avatarFilePath, FileMode.Open, FileAccess.Read))
                    {
                        picAvatar.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    picAvatar.Image = null;
                    picAvatar.BackColor = Color.White;
                }
            }
            else
            {
                picAvatar.Image = null;
                picAvatar.BackColor = Color.White;
            }
        }


        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avatarFilePath = openFileDialog.FileName;
                    picAvatar.Image = Image.FromFile(avatarFilePath);
                }
            }
        }

        private void btnThemSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtStudentId.Text) || string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtAverageScore.Text) || cmbFaculty.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sinh viên", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!float.TryParse(txtAverageScore.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float averageScore) || averageScore < 0.0f || averageScore > 10.0f)
                {
                    MessageBox.Show("Điểm trung bình phải là số từ 0.0 đến 10.0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtFullName.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Họ tên không được chứa số", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var student = studentService.FindById(txtStudentId.Text);
                if (student == null)
                {
                    student = new Student
                    {
                        StudentID = txtStudentId.Text,
                        FullName = txtFullName.Text,
                        AverageScore = Math.Round(averageScore, 1),
                        FacultyID = int.Parse(cmbFaculty.SelectedValue.ToString())
                    };

                    if (!string.IsNullOrEmpty(avatarFilePath))
                    {
                        student.Avatar = SaveAvatar(avatarFilePath, student.StudentID);
                    }

                    studentService.InsertOrUpdate(student);
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    student.FullName = txtFullName.Text;
                    student.AverageScore = Math.Round(averageScore, 1);
                    int newFacultyID = int.Parse(cmbFaculty.SelectedValue.ToString());

       
                    if (student.FacultyID != newFacultyID)
                    {
                        student.FacultyID = newFacultyID;
                        student.MajorID = null; 
                    }

                    if (!string.IsNullOrEmpty(avatarFilePath))
                    {
                      
                        if (picAvatar.Image != null)
                        {
                            picAvatar.Image.Dispose();
                            picAvatar.Image = null;
                        }

                        // Delete the previous avatar
                        DeleteAvatar(student.StudentID);

                        student.Avatar = SaveAvatar(avatarFilePath, student.StudentID);
                    }

                    studentService.InsertOrUpdate(student);
                    MessageBox.Show("Chỉnh sửa thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                BindGrid(studentService.GetAll());
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    MessageBox.Show($"Lỗi khi lưu dữ liệu: {innerException.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    innerException = innerException.InnerException;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var student = studentService.FindById(txtStudentId.Text);
                if (student != null)
                {
                    DeleteAvatar(student.StudentID); 
                    studentService.Delete(student);
                    BindGrid(studentService.GetAll());
                    MessageBox.Show("Sinh viên đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sinh viên không tìm thấy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkFaculty_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<Student> students;
                if (checkFaculty.Checked)
                {
                    students = studentService.GetAll().Where(s => s.MajorID == null).ToList();
                }
                else
                {
                    students = studentService.GetAll();
                }

                if (students == null || students.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                BindGrid(students);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
                txtStudentId.Text = row.Cells["clMssv"].Value.ToString();
                txtFullName.Text = row.Cells["clHoten"].Value.ToString();
                txtAverageScore.Text = row.Cells["clDtb"].Value.ToString();

                var facultyName = row.Cells["clKhoa"].Value.ToString();
                var faculty = facultyService.GetAll().FirstOrDefault(f => f.FacultyName == facultyName);
                if (faculty != null)
                {
                    cmbFaculty.SelectedValue = faculty.FacultyID;
                }
                else
                {
                    cmbFaculty.SelectedIndex = -1;
                }

                ShowAvatar(txtStudentId.Text);
            }
        }


        private void DKCNToolStripMenuItem_Click(object sender, EventArgs e)
        {
   
            frmRegister frm = new frmRegister();
            frm.ShowDialog();
    }
    }
}






















