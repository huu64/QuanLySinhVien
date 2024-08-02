using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                List<Faculty> listFalcultys = context.Faculties.ToList(); //lấy các khoa
                List<Student> listStudent = context.Students.ToList(); //lấy sinh viên
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cmbFaculty.DataSource = listFalcultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }
        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                dgvStudent.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }
        private void AddStudent(string studentID, string fullName, double? averageScore, int? facultyID)
        {
            using (var context = new Model1())
            {
                Student newStudent = new Student
                {
                    StudentID = studentID,
                    FullName = fullName,
                    AverageScore = averageScore,
                    FacultyID = facultyID
                };
                context.Students.Add(newStudent);
                context.SaveChanges();
            }
        }

        private void DeleteStudent(string studentID)
        {
            using (var context = new Model1())
            {
                Student studentToDelete = context.Students.FirstOrDefault(p => p.StudentID == studentID);
                if (studentToDelete != null)
                {
                    context.Students.Remove(studentToDelete);
                    context.SaveChanges();
                }
            }
        }

        private void UpdateStudent(string studentID, string fullName, double? averageScore, int? facultyID)
        {
            using (var context = new Model1())
            {
                Student studentToUpdate = context.Students.FirstOrDefault(p => p.StudentID == studentID);
                if (studentToUpdate != null)
                {
                    studentToUpdate.FullName = fullName;
                    studentToUpdate.AverageScore = averageScore;
                    studentToUpdate.FacultyID = facultyID;
                    context.SaveChanges();
                }
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            string studentID = txtStudentID.Text;
            string fullName = txtFullName.Text;
            double? averageScore = string.IsNullOrEmpty(txtAverageScore.Text) ? (double?)null : Convert.ToDouble(txtAverageScore.Text);
            int? facultyID = cmbFaculty.SelectedValue as int?;

            AddStudent(studentID, fullName, averageScore, facultyID);
            RefreshData();
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            string studentID = txtStudentID.Text;
            string fullName = txtFullName.Text;
            double? averageScore = string.IsNullOrEmpty(txtAverageScore.Text) ? (double?)null : Convert.ToDouble(txtAverageScore.Text);
            int? facultyID = cmbFaculty.SelectedValue as int?;

            UpdateStudent(studentID, fullName, averageScore, facultyID);
            RefreshData();
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            string studentID = txtStudentID.Text;

            DeleteStudent(studentID);
            RefreshData();
        }
        private void RefreshData()
        {
            using (var context = new Model1())
            {
                List<Student> listStudent = context.Students.ToList();
                BindGrid(listStudent);
            }
        }
    }
}
