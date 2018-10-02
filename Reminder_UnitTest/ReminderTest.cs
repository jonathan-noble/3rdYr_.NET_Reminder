using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPF_Reminder;

namespace Reminder_UnitTest
{
    /// <summary>
    ///Test Class with its test methods for unit testing
    /// </summary>
    [TestClass]
    public class ReminderTest
    {
        [TestMethod]
        public void Test_Serialize()
        {
            //arranging
            String expected_name = "Mona Lisa";
            String name = Reminder.UserName;

            //Act
            String actual_name = name;

            //Assert
            Assert.AreEqual(expected_name, actual_name);

        }   // failed test

        [TestMethod]
        public void Test_AddNote()
        {
            Reminder r = new Reminder();
            Assert.IsTrue(r.AddNote(new Note("Studies", 15)), "note");
        }   // passed test


        [TestMethod]
        public void Test_DayClick()
        {
            Reminder r = new Reminder();
            List<string> expected_days = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            List<string> actual_days = r.GetDays();

            CollectionAssert.AreEqual(expected_days, actual_days);
        }   // passed test
    }
}
