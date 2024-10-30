using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentregistreringsprogram
{
    internal class DatabaseHandler
    {
        public StudentDbContext DbCtx { get; set; }

        public DatabaseHandler(StudentDbContext dbCtx)
        {
            this.DbCtx = dbCtx;
        }
        public string AddNewStudent(string firstName, string lastName, string city)
        {
            string returnMessage = "";
            Student student = new Student(firstName, lastName, city);
            DbCtx.Add(student);
            DbCtx.SaveChanges();
            returnMessage = $"The new student {firstName} {lastName} has been added to the registry.";

            return returnMessage ;
        }

        public string ChangeStudentData(Student student, string? newFirstName , string? newLastName , string? newCity)
        {
            string returnString = "";

            if (student == null)
            {
                returnString = "The Student was not found.";
                return returnString;
            }

            // The data will only be changed if the incoming parameter is not null

            if (newFirstName != null) student.FirstName = newFirstName;
            if (newLastName != null) student.LastName = newLastName;
            if (newCity != null) student.City = newCity;
            

            returnString = "The student was updated successfully.";
            DbCtx.SaveChanges();

            

                return returnString;
        }
        public Student GetStudent(int id)
        {
            
            var student = DbCtx.Students.Where(s => s.StudentId == id).FirstOrDefault<Student>();

            return student;
        }
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            if (DbCtx.Students != null)
            {
                foreach (var student in DbCtx.Students)
                {
                    students.Add(student);
                }
            }

            return students;
        }

      
    }
}
