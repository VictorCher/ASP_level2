using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _Controller;

        //private class TestLogger : ILogger<HomeController>
        //{
        //    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) { TODO_IMPLEMENT_ME(); }

        //    public bool IsEnabled(LogLevel logLevel) { return TODO_IMPLEMENT_ME; }

        //    public IDisposable BeginScope<TState>(TState state) { return TODO_IMPLEMENT_ME; }
        //}

        [TestInitialize]
        public void Initialize()
        {
            //_Controller = new HomeController(new TestLogger());

            var logger_moq = new Mock<ILogger<HomeController>>();
            //logger_moq.Setup()
            _Controller = new HomeController(logger_moq.Object);
        }

        [TestCleanup]
        public void Cleanup() { }

        [TestMethod]
        public void Index_Returns_View()
        {
            var result = _Controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var result = _Controller.ContactUs();
            Assert.IsType<ViewResult>(result);
        } 

        [TestMethod]
        public void Blog_Returns_View()
        {
            var result = _Controller.ContactUs();
            Assert.IsType<ViewResult>(result);
        }   

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var result = _Controller.ContactUs();
            Assert.IsType<ViewResult>(result);
        }  

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void ThrowException_Throw_ApplicationException()
        {
            _ = _Controller.ThrowException();
        }

        [TestMethod]
        public void NotFoundPage_Returns_View()
        {
            var result = _Controller.NotFoundPage();
            Assert.IsType<ViewResult>(result);
        }    

        [TestMethod]
        public void ErrorStatus_404_Redirect_to_NotFoundPage()
        {
            var result = _Controller.ErrorStatus("404");
            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect_to_action.ControllerName);
            Assert.Equal(nameof(HomeController.NotFoundPage), redirect_to_action.ActionName);
        }
    }
}
