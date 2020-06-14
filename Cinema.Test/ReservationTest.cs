using System;
using System.Collections.Generic;
using Cinema.Infrastructure.Dto;
using Cinema.Infrastructure.Mappers;
using Cinema.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cinema.Test
{
    /// <summary>
    /// Tests Reservation Service, including repository and connecting with datebase.
    /// </summary>
    [TestClass]
    public class ReservationTest
    {
        IReservationService _services;
        int addReservationId;

        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _services = new ReservationService(AutoMapperConfig.Initialize());
            addReservationId = 0;
        }


        /// <summary>
        /// Tests getting reservation by showing.
        /// </summary>
        [TestMethod]
        public void GetReservationByShowing()
        {
            int showingId = 1;
            IList<ReservationDto> reservations = _services.GetByShowing(showingId);
            Assert.IsNotNull(reservations);
        }

        /// <summary>
        /// Tests getting seats, including reserved one, for the showing.
        /// </summary>
        [TestMethod]
        public void GetSeats()
        {
            int showingId = 1;
            IList<RowSeatDto> seats = _services.GetSeats(showingId);
            Assert.IsNotNull(seats);
            Assert.AreEqual(seats.Count, 30);
            Assert.AreEqual(seats[1].SeatStatus, RowSeatDto.Status.Free);
        }

        /// <summary>
        /// Tests adding reservation to datebase.
        /// </summary>
        [TestMethod]
        public void AddReservation()
        {
            var reservationDto = new ReservationDto()
            {
                RowSeatId = 20,
                ShowingId = 1,
                CustomerId = 1
            };

            addReservationId = _services.InsertOrUpdate(reservationDto);
            var reservationDb = _services.Get(addReservationId);
            Assert.ReferenceEquals(reservationDb, reservationDto);
        }

        /// <summary>
        /// Removes added in test reservation.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            if (addReservationId > 0)
            {
                _services.Remove(addReservationId);
            }
        }   
    }
}
