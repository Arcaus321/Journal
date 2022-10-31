using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Group { get; set; }
        public int SpecializationId { get; set; }
        public string Specialization { get; set; }
        public User(int id, string name, int groupId, string group, int specializationId, string specialization)
        {
            Id = id;
            Name = name;
            Group = group;
            GroupId = groupId;
            Specialization = specialization;
            SpecializationId = specializationId;

        }

        public static User GetUser(int id)
        {
            string query = $@"SELECT u.id, (FirstName || ' ' || LastName || ' ' || MiddleName), UserGroup, g.GroupName, g.SpecializationId, s.SpecializationName 
                                                                       FROM Users u LEFT JOIN 
                                                                       Groups g on g.id = u.UserGroup LEFT JOIN
                                                                       Specialization s on s.Id = g.SpecializationId 
                                                                       WHERE u.id = {id} AND u.isDelete = 0";

            var userData = WorkWithData.ExecuteSqlQueryAsEnumerable(query);
            return new User(Convert.ToInt32(userData.First()[0]), userData.First()[1].ToString(), Convert.ToInt32(userData.First()[2]), userData.First()[3].ToString(), Convert.ToInt32(userData.First()[4]), userData.First()[5].ToString());
        }

        public DataTable GetSubjects()
        {
            string query = $"select Distinct id, subjectName from subjects Where Groupid = {GroupId}";
            DataTable subjects = WorkWithData.ExecuteSqlQueryAsDataTable(query);
            return subjects;
        }

        public DataTable GetSemesters()
        {
            string query = $@"SELECT DISTINCT Semester FROM Subjects s
                              WHERE GroupId = {GroupId}";
            DataTable subjects = WorkWithData.ExecuteSqlQueryAsDataTable(query);
            return subjects;
        }
    }
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Subject(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
