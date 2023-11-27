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
            var user =  db.t_user.Where(u => u.email == email && u.password == pwd).FirstOrDefault();
            return user == null ? null : new UserDto(user);
        }
    }
}
