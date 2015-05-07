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
    public class InstructorsTests
    {
        private InstructorsController controller = new InstructorsController();

        [TestMethod]
        public void Instructors_AddRemove()
        {
            var instructor = controller.Add("asdf", "", "asdf");
            Assert.IsTrue(instructor.InstructorId > 0);
            var instructor2 = controller.Add("asdf", "", "asdf");
            Assert.AreEqual(instructor, instructor2);
            var result = controller.Remove("asdf", "", "asdf");
            Assert.AreEqual(result, 1);
            result = controller.Remove("asdf", "", "asdf");
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Instructors_RemoveById()
        {
            var instructor = controller.Add("asdf", "", "asdf");
            Assert.IsTrue(instructor.InstructorId > 0);
            int instructorId = instructor.InstructorId;
            var result = controller.Remove(instructorId);
            Assert.AreEqual(result, 1);
            result = controller.Remove(instructorId);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Instructors_GetAll()
        {
            controller.Add("asdf", "", "asdf");
            var instructors = controller.GetAll();
            int i = instructors.Count();
            Assert.IsTrue(i > 0);
            controller.Remove("asdf", "", "asdf");
        }

        [TestMethod]
        public void Instructor_Get()
        {
            controller.Add("asdf", "", "asdf");
            var instructors = controller.GetAll();
            var instructor1 = instructors.First();
            var instructor2 = controller.Get(instructor1.FirstName, instructor1.MiddleName, instructor1.LastName);
            Assert.AreEqual(instructor1, instructor2);
            var instructor3 = controller.Get(instructor1.InstructorId);
            Assert.AreEqual(instructor1, instructor3);
            controller.Remove("asdf", "", "asdf");
        }
    }
}
