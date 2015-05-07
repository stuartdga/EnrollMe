using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrollMeDB.Controller
{
    public class ClassesController
    {
        private EnrollMeModal db = new EnrollMeModal();

        public Classes Add(string className, string dayOfClass, string timeOfClass, string location, int instructorId)
        {
            var classes = new Classes();
            classes.ClassName = className;
            classes.DayOfClass = dayOfClass;
            classes.TimeOfClass = timeOfClass;
            classes.Location = location;
            classes.InstructorId = instructorId;
            var existingClass = db.Classes.FirstOrDefault(q => q.ClassName == className &&
                                                                q.DayOfClass == dayOfClass &&
                                                                q.TimeOfClass == timeOfClass &&
                                                                q.Location == location);
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

        public int Remove(string className, string dayOfClass, string timeOfClass, string location)
        {
            int result = 0;
            var classes = db.Classes.FirstOrDefault(q => q.ClassName == className &&
                                                                q.DayOfClass == dayOfClass &&
                                                                q.TimeOfClass == timeOfClass &&
                                                                q.Location == location);
            if (classes != null)
            {
                db.Classes.Remove(classes);
                result = db.SaveChanges();
            }
            return result;
        }

        public IQueryable<Classes> GetAll()
        {
            return db.Classes;
        }

        public Classes Get(string className, string dayOfClass, string timeOfClass, string location)
        {
            var classes = db.Classes.FirstOrDefault(q => q.ClassName == className &&
                                                                q.DayOfClass == dayOfClass &&
                                                                q.TimeOfClass == timeOfClass &&
                                                                q.Location == location);
            return classes;
        }
    }
}
