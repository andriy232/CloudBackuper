using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using DataGuardian.Controls;
using DataGuardian.Impl;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Tests
{
    [TestClass]
    public class TestBackupSchedule
    {
        [TestMethod]
        public void NextPerformDateOneTimeTest()
        {
            var backup = new BackupStep
            {
                Period = BackupPeriod.OneTime,
                PeriodParameters = new[] { DayOfWeek.Monday.ToString(), DayOfWeek.Friday.ToString() }.ToList(),
                RecurEvery = 1,
                LastPerformTime = DateTime.MinValue,
                StartDate = new DateTime(2020, 01, 01),
            };

            Assert.AreEqual(new DateTime(2020, 01, 01), backup.NextPerformDate);
        }

        [TestMethod]
        public void NextPerformDateOneTimePastTest()
        {
            var backup = new BackupStep
            {
                Period = BackupPeriod.OneTime,
                PeriodParameters = new[] { DayOfWeek.Monday.ToString(), DayOfWeek.Friday.ToString() }.ToList(),
                RecurEvery = 1,
                LastPerformTime = new DateTime(2020, 01, 01),
            };

            Assert.AreEqual(DateTime.MaxValue, backup.NextPerformDate);
        }

        [TestMethod]
        public void NextPerformDateDailyTest()
        {
            var backup = new BackupStep
            {
                Period = BackupPeriod.Daily,
                PeriodParameters = new[] { DayOfWeek.Monday.ToString(), DayOfWeek.Friday.ToString() }.ToList(),
                RecurEvery = 1,
                LastPerformTime = DateTime.MinValue,
                StartDate = new DateTime(2020, 01, 01),
            };

            Assert.AreEqual(new DateTime(2020, 01, 01), backup.NextPerformDate);
        }

        [TestMethod]
        public void NextPerformDateDailyPastTest()
        {
            var backup = new BackupStep
            {
                Period = BackupPeriod.Daily,
                PeriodParameters = new[] { DayOfWeek.Monday.ToString(), DayOfWeek.Friday.ToString() }.ToList(),
                RecurEvery = 2,
                LastPerformTime = new DateTime(2020, 01, 01),
            };

            Assert.AreEqual(new DateTime(2020, 01, 03), backup.NextPerformDate);
        }

        [TestMethod]
        public void NextPerformDateWeeklyTest()
        {
            var backup = new BackupStep
            {
                Period = BackupPeriod.Weekly,
                PeriodParameters = new[] { DayOfWeek.Monday.ToString(), DayOfWeek.Friday.ToString() }.ToList(),
                RecurEvery = 1,
                LastPerformTime = new DateTime(2020, 01, 05),
                StartDate = new DateTime(2020, 01, 01),
            };

            Assert.AreEqual(new DateTime(2020, 01, 06), backup.NextPerformDate);
        }

        [TestMethod]
        public void NextPerformDateWeeklyPastTest()
        {
            var backup = new BackupStep
            {
                Period = BackupPeriod.Weekly,
                PeriodParameters = new[] { DayOfWeek.Monday.ToString(), DayOfWeek.Friday.ToString() }.ToList(),
                RecurEvery = 1,
                LastPerformTime = new DateTime(2020, 01, 07),
                StartDate = new DateTime(2020, 01, 01),
            };

            Assert.AreEqual(new DateTime(2020, 01, 10), backup.NextPerformDate);
        }

        [TestMethod]
        public void NextPerformDateMonthlyTest()
        {
            var backup = new BackupStep
            {
                Period = BackupPeriod.Monthly,
                PeriodParameters = new[] { Month.January.ToString(), Month.April.ToString() }.ToList(),
                RecurEvery = 1,
                LastPerformTime = new DateTime(2020, 01, 05),
                StartDate = new DateTime(2020, 01, 01),
            };

            Assert.AreEqual(new DateTime(2020, 04, 05), backup.NextPerformDate);
        }
    }
}
