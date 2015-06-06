using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnrollMeDB;
using EnrollMeDB.Controller;

namespace EnrollMeDBTest
{
    [TestClass]
    public class StudentsTests
    {
        private string _organization = System.Configuration.ConfigurationManager.AppSettings["Organization"].ToString();
        private StudentsDBController controller = new StudentsDBController();
        private ClassesDBController classesController = new ClassesDBController();
        private InstructorsDBController instructorsController = new InstructorsDBController();
        private int classId;
        private int instructorId;
        private string _value;

        [TestInitialize]
        public void TestInitialize()
        {
            _value = Helper.GetNewValue();
            var instructor = instructorsController.Add(_value, _value, _value, _organization);
            instructorId = instructor.InstructorId;
            var classes = classesController.Add(_value, _value, _value, _value, instructorId, _organization);
            classId = classes.ClassId;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            classesController.Remove(classId);
            instructorsController.Remove(instructorId);
        }

        [TestMethod]
        public void Students_AddGetRemove()
        {
            var student = controller.Add(_value, _value, _value, _value, _value, classId);
            Assert.IsTrue(student.StudentId > 0);
            var student2 = controller.Add(_value, _value, _value, _value, _value, classId);
            Assert.AreEqual(student, student2);
            var students = controller.GetStudentsByClassId(student.ClassesId);
            Assert.AreEqual(student, students.First());
            students = controller.GetClassesByStudentId(student.StudentId);
            Assert.AreEqual(student, students.First());
            var result = controller.Remove(student.StudentId, student.ClassesId);
            Assert.AreEqual(result, 1);
            result = controller.Remove(student.StudentId, student.ClassesId);
            Assert.AreEqual(result, 0);
        }
    }
}
