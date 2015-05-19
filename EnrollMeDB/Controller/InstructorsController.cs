using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrollMeDB.Controller
{
    public class InstructorsController
    {
        private EnrollMeModal db = new EnrollMeModal();

        public Instructors Add(string firstName, string middleName, string lastName)
        {
            
            var instructor = new Instructors();
            instructor.FirstName = firstName;
            instructor.MiddleName = middleName;
            instructor.LastName = lastName;
            var existingInstructor = db.Instructors.FirstOrDefault(q => q.FirstName == firstName &&
                                                                    q.MiddleName == middleName && 
                                                                    q.LastName == lastName);
            if (existingInstructor == null)
            {
                db.Instructors.Add(instructor);
                db.SaveChanges();
            }
            else
                instructor = existingInstructor;

            return instructor;
        }

        public int Remove(int instructorId)
        {
            var result = 0;
            var instructor = db.Instructors.FirstOrDefault(q => q.InstructorId == instructorId);
            if (instructor != null)
            {
                db.Instructors.Remove(instructor);
                result = db.SaveChanges();
            }
            return result;
        }
        
        public int Remove(string firstName, string middleName, string lastName)
        {
            int result = 0;
            var instructor = db.Instructors.FirstOrDefault(q => q.FirstName == firstName && 
                                                                    q.MiddleName == middleName &&
                                                                    q.LastName == lastName);
            if (instructor != null)
            {
                db.Instructors.Remove(instructor);
                result = db.SaveChanges();
            }
            return result;
        }

        public IQueryable<Instructors> GetAll()
        {
            return db.Instructors;
        }

        public Instructors Get(string firstName, string middleName, string lastName)
        {
            var instructor = db.Instructors.FirstOrDefault(q => q.FirstName == firstName && 
                                                                q.MiddleName == middleName &&
                                                                q.LastName == lastName);
            return instructor;
        }

        public Instructors Get(int instructorId)
        {
            var instructor = db.Instructors.FirstOrDefault(q => q.InstructorId == instructorId);
            return instructor;
        }
    }
}
