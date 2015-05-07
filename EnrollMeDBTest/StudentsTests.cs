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
        private StudentsController controller = new StudentsController();
        private ClassesController classesController = new ClassesController();
        private InstructorsController instructorsController = new InstructorsController();
        private int classId;
        private int instructorId;

        [TestInitialize]
        public void TestInitialize()
        {
            var instructor = instructorsController.Add("asdf", "asdf", "asdf");
            instructorId = instructor.InstructorId;
            var classes = classesController.Add("asdf", "asdf", "asdf", "asdf", instructorId);
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
            var student = controller.Add("asdf", "asdf", "asdf", "asdf", "asdf", classId);
            Assert.IsTrue(student.StudentId > 0);
            var student2 = controller.Add("asdf", "asdf", "asdf", "asdf", "asdf", classId);
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
