using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrollMeDB.Controller
{
    public class InstructorsDBController
    {
        private EnrollMeModal db = new EnrollMeModal();

        public Instructors Add(string firstName, string middleName, string lastName, string organization)
        {
            
            var instructor = new Instructors();
            instructor.FirstName = firstName;
            instructor.MiddleName = middleName;
            instructor.LastName = lastName;
            instructor.Organization = organization;
            var existingInstructor = db.Instructors.FirstOrDefault(q => q.FirstName == firstName &&
                                                                    q.MiddleName == middleName && 
                                                                    q.LastName == lastName &&
                                                                    q.Organization == organization);
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
        
        public int Remove(string firstName, string middleName, string lastName, string organization)
        {
            int result = 0;
            var instructor = db.Instructors.FirstOrDefault(q => q.FirstName == firstName && 
                                                                    q.MiddleName == middleName &&
                                                                    q.LastName == lastName &&
                                                                    q.Organization == organization);
            if (instructor != null)
            {
                db.Instructors.Remove(instructor);
                result = db.SaveChanges();
            }
            return result;
        }

        public Instructors Get(string firstName, string middleName, string lastName, string organization)
        {
            var instructor = db.Instructors.FirstOrDefault(q => q.FirstName == firstName && 
                                                                q.MiddleName == middleName &&
                                                                q.LastName == lastName &&
                                                                q.Organization == organization);
            return instructor;
        }

        public Instructors Get(int instructorId)
        {
            var instructor = db.Instructors.FirstOrDefault(q => q.InstructorId == instructorId);
            return instructor;
        }

        public IList<Instructors> Get(string organization)
        {
            var instructors = db.Instructors.Where(q => q.Organization == organization);
            return instructors.ToList<Instructors>();
        }
    }
}
