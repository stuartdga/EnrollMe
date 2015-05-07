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
    public class ClassesTests
    {
        private ClassesController controller = new ClassesController();
        private InstructorsController instructorsController = new InstructorsController();
        private int instructorId;

        [TestInitialize]
        public void TestInitialize()
        {
            var instructor = instructorsController.Add("asdf", "asdf", "asdf");
            instructorId = instructor.InstructorId;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            instructorsController.Remove(instructorId);
        }

        [TestMethod]
        public void Classes_AddRemove()
        {
            var classes = controller.Add("asdf", "asdf", "asdf", "asdf", instructorId);
            Assert.IsTrue(classes.ClassId > 0);
            var classes2 = controller.Add("asdf", "asdf", "asdf", "asdf", instructorId);
            Assert.AreEqual(classes, classes2);
            var result = controller.Remove("asdf", "asdf", "asdf", "asdf");
            Assert.AreEqual(result, 1);
            result = controller.Remove("asdf", "asdf", "asdf", "asdf");
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Classes_Get()
        {
        }

        [TestMethod]
        public void Classes_GetAll()
        {
        }
    }
}
