using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain;
using Cinema.Core.Repositories;
using Dapper;

namespace Cinema.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for reservation object
    /// </summary>
    public class ReservationRepository : IReservationRepository
    {
        /// <summary>
        /// Gets reservation by id
        /// </summary>
        /// <param name="id">id of the reservation</param>
        /// <returns>Reservation object</returns>
        public Reservation Get(int id)
        {
            Reservation reservationObj = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                reservationObj = db.Query<Reservation, RowSeat, Customer, Showing, Theatre, Movie, Reservation>(@"SELECT " +
                                       @"R.Id, R.ReservationDate, R.Status, R.RowSeatId, " +
                                       @"RS.Id, RS.RowNumber, RS.SeatNumber, R.CustomerId, " +
                                       @"C.Id, C.CustomerName, C.CustomerSurname, C.Phone, C.Email, R.ShowingId,  " +
                                       @"S.Id, S.ShowingDateTime, S.TheatreId, " +
                                       @"T.Id, T.TheatreName, S.MovieId, " +
                                       @"M.Id, M.MovieTitle, M.MovieDescription,M.YearOfProduction " +
                                       @"FROM Reservation AS R INNER JOIN RowSeat AS RS ON R.RowSeatId = RS.Id " +
                                       @"INNER JOIN Showing AS S ON R.ShowingId = S.Id " +
                                       @"INNER JOIN Movie AS M ON S.MovieId = M.Id " +
                                       @"INNER JOIN Theatre AS T ON S.TheatreId = T.Id " +
                                       @"LEFT JOIN Customer AS C ON R.CustomerId = C.Id " +
                                       $"WHERE R.Id = {id} ",
                                       (reservation, rowseat, customer, showing, theatre, movie) =>
                                       {
                                           reservation.RowSeatId = rowseat;
                                           reservation.ShowingId = showing;
                                           showing.MovieId = movie;
                                           showing.TheatreId = theatre;
                                           reservation.CustomerId = customer;
                                           return reservation;
                                       }, splitOn: "RowSeatId, CustomerId, ShowingId, TheatreId, MovieId "
                                       ).Distinct()
                                       .SingleOrDefault();
            }
            return reservationObj;
        }

        /// <summary>
        /// Gets all reservations.
        /// </summary>
        /// <returns>List of all reservations</returns>
        public IList<Reservation> GetAll()
        {
            IList<Reservation> reservations = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                string actuallDate = (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                reservations = db.Query<Reservation, RowSeat, Customer, Showing, Theatre, Movie, Reservation>(@"SELECT " +
                                       @"R.Id, R.ReservationDate, R.Status, R.RowSeatId, " +
                                       @"RS.Id, RS.RowNumber, RS.SeatNumber, R.CustomerId, " +
                                       @"C.Id, C.CustomerName, C.CustomerSurname, C.Phone, C.Email, R.ShowingId,  " +
                                       @"S.Id, S.ShowingDateTime, S.TheatreId, " +
                                       @"T.Id, T.TheatreName, S.MovieId, " +
                                       @"M.Id, M.MovieTitle, M.MovieDescription,M.YearOfProduction "  +
                                       @"FROM Reservation AS R INNER JOIN RowSeat AS RS ON R.RowSeatId = RS.Id " +
                                       @"INNER JOIN Showing AS S ON R.ShowingId = S.Id " +
                                       @"INNER JOIN Movie AS M ON S.MovieId = M.Id " +
                                       @"INNER JOIN Theatre AS T ON S.TheatreId = T.Id " +
                                       @"LEFT JOIN Customer AS C ON R.CustomerId = C.Id " +
                                       $"WHERE S.ShowingDateTime > '{actuallDate}'"+
                                       @"ORDER BY S.ShowingDateTime;",
                                       (reservation, rowseat, customer, showing, theatre, movie) =>
                                       {
                                           reservation.RowSeatId = rowseat; 
                                           reservation.ShowingId = showing;
                                           showing.MovieId = movie;
                                           showing.TheatreId = theatre;
                                           reservation.CustomerId = customer;
                                           return reservation; 
                                       }, splitOn: "RowSeatId, CustomerId, ShowingId, TheatreId, MovieId "
                                       ).Distinct()
                                       .ToList();
            }
            return reservations;
        }

        /// <summary>
        /// Insterts or Updates a reservation depending on setting id. If id isn't set (the value equals 0) the reservation is added. Otherwise the reservation is modified.
        /// </summary>
        /// <param name="item">model of reservation to add or edit.</param>
        /// <returns>id of inserted reservation or number of affected rows.</returns>
        public int InsertOrUpdate(Reservation item)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                if (item.Id > 0)
                    return Update(item, db);
                else
                    return Insert(item, db);
            }
        }

        private int Insert(Reservation item, IDbConnection db)
        {
            string status = ((int)item.Status).ToString();
            string date = (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
            var customerId = (item.CustomerId == null) ? "NULL" : item.CustomerId.Id.ToString();
            string sql = @"INSERT INTO Reservation (ShowingId, RowSeatId, Status, ReservationDate, CustomerId)"
                + $" Values ({item.ShowingId.Id}, {item.RowSeatId.Id}, '{status}', '{date}',  {customerId});"
                + @" SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql).Single();

            return id;
        }

        private int Update(Reservation item, IDbConnection db)
        {
            int reservationId = item.Id;
            int showingId = item.ShowingId.Id;
            int seatId = item.RowSeatId.Id;
            string status = ((int)item.Status).ToString();
            var customerId = (item.CustomerId == null) ? "NULL": item.CustomerId.Id.ToString() ; 
         
            string sql = $"UPDATE Reservation SET ShowingId = {showingId}, RowSeatId = {seatId}, " +
                $"Status = {status},  ReservationDate = '{item.ReservationDate}', CustomerId = {customerId} " +
                $"WHERE Id = {reservationId};";

            var affectedRows = db.Execute(sql);
            return affectedRows;
        }

        /// <summary>
        /// Removes the custom reservation
        /// </summary>
        /// <param name="id">id of the reservation to remove.</param>
        public void Remove(int id)
        {
            string sql = "DELETE FROM Reservation WHERE Id=@Id";
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                var affectedRows = db.Execute(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Gets reservations by showing.
        /// </summary>
        /// <param name="showing">object of showing</param>
        /// <returns>List of reservation</returns>
        public IList<Reservation> GetReservationByShowing(Showing showing)
        {
            IList<Reservation> reservations = null;
            int showingId = showing.Id;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                reservations = db.Query<Reservation, RowSeat, Reservation>(@"SELECT R.Id, R.ReservationDate, R.Status, R.RowSeatId, " +
                                       @"RS.Id, RS.RowNumber, RS.SeatNumber FROM Reservation AS R " +
                                       @"INNER JOIN Showing AS S ON R.ShowingId = S.Id " +
                                       @"INNER JOIN RowSeat AS RS ON R.RowSeatId = RS.Id " +
                                       $"WHERE S.Id = {showingId}",
                                       (reservation, rowseat) =>
                                       {
                                           reservation.RowSeatId = rowseat;
                                           reservation.ShowingId = showing;
                                           return reservation;
                                       }, splitOn: "RowSeatId")
                                       .Distinct()
                                       .ToList();
            }
            return reservations;
        }

        #region Members of future functionality

        /// <summary>
        /// Gets list of reservations by customer
        /// </summary>
        /// <param name="customer">object of customer</param>
        /// <returns>List of reservations.</returns>
        public IList<Reservation> GetReservationByCustomer(Customer customer)
        {
            IList<Reservation> reservations = null;
            int customerId = customer.Id;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                reservations = db.Query<Reservation, RowSeat, Showing, Reservation>(@"SELECT R.Id, R.ReservationDate, R.Status, R.RowSeatId, " +
                                       @"RS.Id, RS.RowNumber, RS.SeatNumber, " +
                                       @"R.ShowingId, S.Id, S.MovieId, S.TheatreId, S.ShowingDateTime FROM Reservation AS R " +
                                       @"INNER JOIN Showing AS S ON R.ShowingId = S.Id " +
                                       @"INNER JOIN RowSeat AS RS ON R.RowSeatId = RS.Id " +
                                       @"INNER JOIN Customer AS C ON R.CustomerId = C.Id " +
                                       $"WHERE C.Id = {customerId}",
                                       (reservation, rowseat, showing) =>
                                       {
                                           reservation.RowSeatId = rowseat;
                                           reservation.ShowingId = showing;
                                           return reservation;
                                       }, splitOn: "RowSeatId, R.ShowingId")
                                       .Distinct()
                                       .ToList();
            }
            return reservations;
        }
        #endregion
    }
}
