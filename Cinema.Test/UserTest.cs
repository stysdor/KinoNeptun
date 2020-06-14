using System;
using Cinema.Infrastructure.Mappers;
using Cinema.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cinema.Test
{
    [TestClass]
    public class UserTest
    {
        private IUserService _service;

        [TestInitialize]
        public void SetUp()
        {
            _service = new UserService(AutoMapperConfig.Initialize());
        }

        [TestMethod]
        public void ValidateUserTests()
        {
            Assert.IsTrue(_service.ValidateUser("AKowalski", "1qaz2wsx"));
        }
    }
}
