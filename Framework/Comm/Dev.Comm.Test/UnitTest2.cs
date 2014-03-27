using System;
using Dev.Comm.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test
{
    [TestClass]
    public class DateUtilTest
    {
        [TestMethod]
        public void UtcToLocal()
        {
            var UtcDate = new DateTime(2013, 1, 1, 1, 1, 1);

            DateTime converted = DateUtil.UtcToLocal(UtcDate);
            Assert.AreEqual(UtcDate.AddHours(8), converted);

        }
    }
}
