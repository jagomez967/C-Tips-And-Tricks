using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tips.Entities;

namespace Tips
{
    [TestClass]
    public class LazyInitializationTest
    {


        [TestMethod]
        public void Example()
        {
            var nonLazyReport = new Report();

            var lazyReport = new Lazy<Report>();

            lazyReport.Value.DoSomething();
        }










        [TestMethod]
        public void SomeObjectsMightNotBeUsed()
        {
            var nonLazyReports = new Report[10];

            var sw = Stopwatch.StartNew();

            for (var i = 0; i < 10; i++)
            {
                nonLazyReports[i] = new Report();

                if (i % 2 == 0)
                {
                    nonLazyReports[i].DoSomething();
                }
            }

            sw.Stop();

            var nonLazyTotalCreationTime = sw.ElapsedMilliseconds;



            var lazyReports = new Lazy<Report>[10];

            sw = Stopwatch.StartNew();

            for (var i = 0; i < 10; i++)
            {
                lazyReports[i] = new Lazy<Report>();

                if (i % 2 == 0)
                {
                    lazyReports[i].Value.DoSomething();
                }
            }

            sw.Stop();

            var lazyTotalCreationTime = sw.ElapsedMilliseconds;
        }







        [TestMethod]
        public void Init()
        {
            var defaultCtor = new Lazy<Report>();
            var defaultRepName = defaultCtor.Value.ReportName;

            var specificCtor = new Lazy<Report>(() => new Report("Sales Report"));
            var specificRepName = specificCtor.Value.ReportName;
        }






        [TestMethod]
        public void ValueIsReadOnly()
        {
            var r = new Lazy<Report>();

            // r.Value = new Report(); 


            r.Value.ReportName = "Sales Report";
        }






        [TestMethod]
        public void NoExceptionCaching()
        {
            var r = new Lazy<ExceptionReport>();

            try
            {
                r.Value.DoSomething();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }

            try
            {
                r.Value.DoSomething();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }




        [TestMethod]
        public void WithExceptionCaching()
        {
            var r = new Lazy<ExceptionReport>(() => new ExceptionReport());

            try
            {
                r.Value.DoSomething();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                r.Value.DoSomething();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

    }
}