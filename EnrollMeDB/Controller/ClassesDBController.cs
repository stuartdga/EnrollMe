using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrollMeDB.Controller
{
    public class ClassesDBController
    {
        private EnrollMeModal db = new EnrollMeModal();

        public Classes Add(string className, string dayOfClass, string timeOfClass, string location, int instructorId, string organization)
        {
            var classes = new Classes();
            classes.ClassName = className;
            classes.DayOfClass = dayOfClass;
            classes.TimeOfClass = timeOfClass;
            classes.Location = location;
            classes.InstructorId = instructorId;
            classes.Organization = organization;
            var existingClass = db.Classes.FirstOrDefault(q => q.ClassName == className &&
                                                                q.DayOfClass == dayOfClass &&
                                                                q.TimeOfClass == timeOfClass &&
                                                                q.Location == location &&
                                                                q.Organization == organization);
            if (existingClass == null)
            {
                db.Classes.Add(classes);
                db.SaveChanges();
            }
            else
                classes = existingClass;

            return classes;
        }

        public int Remove(int classId)
        {
            var result = 0;
            var classes = db.Classes.FirstOrDefault(q => q.ClassId == classId);
            if (classes != null)
            {
                db.Classes.Remove(classes);
                result = db.SaveChanges();
            }
            return result;
        }

        public int Remove(string className, string dayOfClass, string timeOfClass, string location, string organization)
        {
            int result = 0;
            var classes = db.Classes.FirstOrDefault(q => q.ClassName == className &&
                                                                q.DayOfClass == dayOfClass &&
                                                                q.TimeOfClass == timeOfClass &&
                                                                q.Location == location &&
                                                                q.Organization == organization);
            if (classes != null)
            {
                db.Classes.Remove(classes);
                result = db.SaveChanges();
            }
            return result;
        }

        public Classes Get(string className, string dayOfClass, string timeOfClass, string location, string organization)
        {
            var classObject = db.Classes.FirstOrDefault(q => q.ClassName == className &&
                                                                q.DayOfClass == dayOfClass &&
                                                                q.TimeOfClass == timeOfClass &&
                                                                q.Location == location &&
                                                                q.Organization == organization);
            return classObject;
        }

        public Classes Get(int id)
        {
            var classObject = db.Classes.FirstOrDefault(q => q.ClassId == id);
            return classObject;
        }

        public IList<Classes> Get(string organization)
        {
            var classes = db.Classes.Where(q => q.Organization == organization);
            return classes.ToList();
        }
    }
}
