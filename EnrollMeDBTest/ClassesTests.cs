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
        private string _value;

        [TestInitialize]
        public void TestInitialize()
        {
            _value = Helper.GetNewValue();
            var instructor = instructorsController.Add(_value, _value, _value);
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
            var classes = controller.Add(_value, _value, _value, _value, instructorId);
            Assert.IsTrue(classes.ClassId > 0);
            var classes2 = controller.Add(_value, _value, _value, _value, instructorId);
            Assert.AreEqual(classes, classes2);
            var result = controller.Remove(_value, _value, _value, _value);
            Assert.AreEqual(result, 1);
            result = controller.Remove(_value, _value, _value, _value);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Classes_Get()
        {
            var result = controller.Remove(_value, _value, _value, _value);
            var classes = controller.Add(_value, _value, _value, _value, instructorId);
            Assert.AreEqual(classes, controller.Get(_value, _value));
            Assert.AreEqual(classes, controller.Get(_value, _value, _value, _value));
            Assert.AreEqual(classes, controller.Get(classes.ClassId));
            var classes2 = controller.GetAll();
            Assert.AreEqual(classes, classes2.FirstOrDefault(q => q.ClassId == classes.ClassId));
            result = controller.Remove(_value, _value, _value, _value);
        }
    }
}
