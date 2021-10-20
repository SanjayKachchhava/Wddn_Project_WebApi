using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWebApi.Models
{
    public class VarificationModel
    {
        public VarificationModel() { }
        public VarificationModel(UserModelTemp user, int code, DateTime time)
        {
            this.User = user;
            this.VarificationCode = code;
            this.CreateDate = time;
        }
        public int Id { get; set; }
        public UserModelTemp User { get; set; }
        public int VarificationCode { get; set; }
        public DateTime CreateDate { get; set; }

    }
}