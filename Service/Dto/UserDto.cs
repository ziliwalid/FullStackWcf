using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Dto
{
    public class UserDto
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<ReservationDto> reservations { get; set; }
        public UserDto()  {          }
        public UserDto(t_user user)
        {
            this.id = user.id; 
            this.fullname = user.fullname;
            this.email = user.email;
            this.password = user.password;  
            if(user.t_reservation != null) 
            {
                reservations = user.t_reservation.
                    Select(r => new ReservationDto(r)).ToList();
            }
        }
    }
}