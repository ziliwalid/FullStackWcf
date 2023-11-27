using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Dto
{
    public class ReservationDto
    {
        public int id { get; set; }
        public BookDto book { get; set; }
        public ReservationDto()
        {
            book = new BookDto();
        }
        public ReservationDto(t_reservation reservation)
        {
            this.id = reservation.id;
            this.book = new BookDto(reservation.t_book);            
        }
    }
}