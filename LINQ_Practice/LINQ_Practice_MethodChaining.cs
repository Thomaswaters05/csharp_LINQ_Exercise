using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LINQ_Practice.Models;
using System.Linq;

namespace LINQ_Practice
{
    [TestClass]
    public class LINQ_Practice_MethodChaining
    {
        public List<Cohort> PracticeData { get; set; }
        public CohortBuilder CohortBuilder { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            CohortBuilder = new CohortBuilder();
            PracticeData = CohortBuilder.GenerateCohorts();
        }

        [TestCleanup]
        public void TearDown()
        {
            CohortBuilder = null;
            PracticeData = null;
        }

        [TestMethod]
        public void GetAllCohortsWithZacharyZohanAsPrimaryOrJuniorInstructor()
        {
            var ActualCohorts = PracticeData.Where(zz => zz.PrimaryInstructor.FirstName == "Zachary" && zz.PrimaryInstructor.LastName == "Zohan" || zz.JuniorInstructors.Any(xx => xx.FirstName == "Zachary" && xx.LastName == "Zohan")).ToList();
            CollectionAssert.AreEqual(ActualCohorts, new List<Cohort> { CohortBuilder.Cohort2, CohortBuilder.Cohort3 });
        }

        [TestMethod]
        public void GetAllCohortsWhereFullTimeIsFalseAndAllInstructorsAreActive()
        {
            var ActualCohorts = PracticeData.Where(xx => xx.FullTime == false && xx.PrimaryInstructor.Active == true && xx.JuniorInstructors.All(junior => junior.Active == true)).ToList();
            CollectionAssert.AreEqual(ActualCohorts, new List<Cohort> { CohortBuilder.Cohort1 });
        }

        [TestMethod]
        public void GetAllCohortsWhereAStudentOrInstructorFirstNameIsKate()
        {
            var ActualCohorts = PracticeData.Where(xx => xx.JuniorInstructors.Any(yy => yy.FirstName == "Kate") || xx.PrimaryInstructor.FirstName == "Kate" || xx.Students.Any(yy => yy.FirstName == "Kate")).ToList();
            CollectionAssert.AreEqual(ActualCohorts, new List<Cohort> { CohortBuilder.Cohort1, CohortBuilder.Cohort3, CohortBuilder.Cohort4 });
        }

        [TestMethod]
        public void GetOldestStudent()
        {
            var student = PracticeData.SelectMany(xx => xx.Students).OrderBy(yy => yy.Birthday).First();
            Assert.AreEqual(student, CohortBuilder.Student18);
        }

        [TestMethod]
        public void GetYoungestStudent()
        {
            var student = PracticeData.SelectMany(xx => xx.Students).OrderBy(yyy => yyy.Birthday).Last(); ;
            Assert.AreEqual(student, CohortBuilder.Student3);
        }

        [TestMethod]
        public void GetAllInactiveStudentsByLastName()
        {
            var ActualStudents = PracticeData.SelectMany(xx => xx.Students).Where(yy => yy.Active == false).OrderBy(zz => zz.LastName).ToList();
            CollectionAssert.AreEqual(ActualStudents, new List<Student> { CohortBuilder.Student2, CohortBuilder.Student11, CohortBuilder.Student12, CohortBuilder.Student17 });
        }
    }
}
