using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    public class Service1 : IService1
    {
        myDBEntities1 db = new myDBEntities1();

        public UserDto Login(string email, string pwd)
        {
            var user = db.t_user.Where(u => u.email == email && u.password == pwd).FirstOrDefault();
            return user == null ? null : new UserDto(user);
        }
        /*Reservation process*/
        public ReservationDto AddReservation(int userId, int bookId)
        {
            try
            {

                var user = db.t_user.Find(userId);
                var book = db.t_book.Find(bookId);

                if (user == null || book == null)
                    return null;


                var reservation = new t_reservation
                {
                    t_user = user,
                    t_book = book
                };


                user.t_reservation.Add(reservation);


                db.SaveChanges();

                return new ReservationDto(reservation);
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool DeleteReservation(int reservationId)
        {
            try
            {
                var reservation = db.t_reservation.Find(reservationId);

                if (reservation == null)
                    return false;

                db.t_reservation.Remove(reservation);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return false;
            }
        }


        public List<BookDto> GetAllBooks()
        {
            var allBooks = db.t_book.ToList(); // Materialize the query
            var bookDtos = allBooks.Select(book => new BookDto(book)).ToList();
            return bookDtos;
        }

        public UserDto refresh(int userId)
        {
            return new UserDto(db.t_user.Find(userId));
        }
    }
}
