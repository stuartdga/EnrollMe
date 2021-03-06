﻿using System;
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
        private string _organization = System.Configuration.ConfigurationManager.AppSettings["Organization"].ToString();
        private InstructorsDBController controller = new InstructorsDBController();
        private string _value;

        [TestInitialize]
        public void TestInit()
        {
            _value = Helper.GetNewValue();
        }

        [TestMethod]
        public void Instructors_AddRemove()
        {
            var instructor = controller.Add(_value, _value, _value, _organization);
            Assert.IsTrue(instructor.InstructorId > 0);
            var instructor2 = controller.Add(_value, _value, _value, _organization);
            Assert.AreEqual(instructor, instructor2);
            var result = controller.Remove(_value, _value, _value, _organization);
            Assert.AreEqual(result, 1);
            result = controller.Remove(_value, _value, _value, _organization);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Instructors_RemoveById()
        {
            var instructor = controller.Add(_value, _value, _value, _organization);
            Assert.IsTrue(instructor.InstructorId > 0);
            int instructorId = instructor.InstructorId;
            var result = controller.Remove(instructorId);
            Assert.AreEqual(result, 1);
            result = controller.Remove(instructorId);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Instructors_GetByOrganization()
        {
            controller.Add(_value, _value, _value, _organization);
            var instructors = controller.Get(_organization);
            int i = instructors.Count();
            Assert.IsTrue(i > 0);
            int id = instructors[0].InstructorId;
            var instructor = controller.Get(id);
            Assert.AreEqual(id, instructor.InstructorId);
            controller.Remove(_value, _value, _value, _organization);
        }

        [TestMethod]
        public void Instructor_Get()
        {
            controller.Add(_value, _value, _value, _organization);
            var instructors = controller.Get(_organization);
            var instructor1 = instructors.First();
            var instructor2 = controller.Get(instructor1.FirstName, instructor1.MiddleName, instructor1.LastName, _organization);
            Assert.AreEqual(instructor1, instructor2);
            var instructor3 = controller.Get(instructor1.InstructorId);
            Assert.AreEqual(instructor1, instructor3);
            controller.Remove(_value, _value, _value, _organization);
        }
    }
}
