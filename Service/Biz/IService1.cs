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
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        UserDto Login(string email, string password);

        [OperationContract]
        ReservationDto AddReservation(int userId, int bookId);
        [OperationContract]
        List<BookDto> GetAllBooks();
        [OperationContract]
        bool DeleteReservation(int reservationId);
        [OperationContract]
        UserDto refresh(int userId);


    }

}
