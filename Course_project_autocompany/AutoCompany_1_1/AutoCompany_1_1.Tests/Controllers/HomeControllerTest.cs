using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoCompany_1_1;
using AutoCompany_1_1.Controllers;
using AutoCompany_1_1.Models;

namespace AutoCompany_1_1.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CarCreate()
        {
            var res = CarDAO.GetCars();
            Assert.IsNotNull(res);
        }
    }
}
