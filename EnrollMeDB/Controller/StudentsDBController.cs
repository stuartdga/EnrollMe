using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrollMeDB.Controller
{
    public class StudentsDBController
    {
        private EnrollMeModal db = new EnrollMeModal();

        public Students Add(string firstName, string middleName, string lastName, string email, string phone, int classId)
        {
            var student = new Students();
            student.FirstName = firstName;
            student.MiddleName = middleName;
            student.LastName = lastName;
            student.Email = email;
            student.Phone = phone;
            student.ClassesId = classId;
            var existingStudent = db.Students.FirstOrDefault(q => q.FirstName == firstName &&
                                                                q.MiddleName == middleName &&
                                                                q.LastName == lastName &&
                                                                q.Email == email &&
                                                                q.Phone == phone &&
                                                                q.ClassesId == classId);
            if (existingStudent == null)
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
            else
                student = existingStudent;

            return student;
        }

        public int Remove(int studentId, int classId)
        {
            var result = 0;
            var student = db.Students.FirstOrDefault(q => q.StudentId == studentId && q.ClassesId == classId);
            if (student != null)
            {
                db.Students.Remove(student);
                result = db.SaveChanges();
            }
            return result;
        }

        public IQueryable<Students> GetStudentsByClassId(int classId)
        {
            var students = db.Students.Where(q => q.ClassesId == classId);
            return students;
        }

        public IQueryable<Students> GetClassesByStudentId(int studentId)
        {
            var students = db.Students.Where(q => q.StudentId == studentId);
            return students;
        }
    }
}
