using System;
using System.Collections.Generic;
using Cinema.Infrastructure.Dto;
using Cinema.Infrastructure.Mappers;
using Cinema.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cinema.Test
{
    /// <summary>
    /// Tests Showing service, including repository and connecting with datebase.
    /// </summary>
    [TestClass]
    public class ShowingTest
    {
        IShowingService _services;
        int addedShowingId = 0;

        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _services = new ShowingService(AutoMapperConfig.Initialize());
        }

        /// <summary>
        /// Tests getting showing by id.
        /// </summary>
        [TestMethod]
        public void GetShowingTest()
        {
            var showing = _services.Get(1);
            Assert.IsNotNull(showing);
            Assert.AreEqual(showing.TheatreId,1);
            Assert.AreEqual(showing.MovieId, 21);
        }

        /// <summary>
        /// Tests getting actual showings.
        /// </summary>
        [TestMethod]
        public void GetActuallShowingsTest()
        {
            var showings = _services.GetActuall(14);
            Assert.IsNotNull(showings);
        }

        /// <summary>
        /// Tests adding showing.
        /// </summary>
        [TestMethod]
        public void AddShowingTest()
        {
            var showingDto = new ShowingDto()
            {
                MovieId = 20,
                MovieTitle = "Lewiatan",
                TheatreId = 1,
                ShowingDateTime = new DateTime(2021, 8, 15, 20, 30, 0)
            };

            addedShowingId = _services.InsertOrUpdate(showingDto);
            var showingDb = _services.Get(addedShowingId);
            Assert.ReferenceEquals(showingDb, showingDto);
        }

        /// <summary>
        /// Removes added showing.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            if (addedShowingId > 0)
            {
                _services.Remove(addedShowingId);
            }
        }
    }

   
}
