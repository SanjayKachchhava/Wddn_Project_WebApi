using DemoWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Net.Mail;
using System.Web.UI.WebControls;

namespace DemoWebApi.Controllers
{
    public class UserController : ApiController
    {

        /* [HttpGet]
         public string Greeting(string name)
         {
             return "Hey " + name + "! Welcome!!!";
         }*/

        DatabaseContext db = new DatabaseContext();
        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public User GetUsers(int id)
        {
            return db.Users.Find(id);
        }

        // api/user
        [HttpPost]
        public HttpResponseMessage AddUser(UserModelTemp model)
        {
           
                var tempUser = db.TempUsers.Add(model);
                var code = new Random().Next(1111,9999);
                db.VarificationModel.Add(new VarificationModel(tempUser, code, DateTime.Now));
                db.SaveChanges();


                    string to = model.Email;
                    string from = "xyz@gmail.com";

                    MailMessage message = new MailMessage(from, to, "Varification Code", "code : "+code);

                    SmtpClient client = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(from, "xyz123") // password
                    };
                    client.Send(message);
                    //status.Text = "message was sent successfully";


                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            
        }

        [Route("api/user/validate")]
        [HttpPost]
        public HttpResponseMessage VelidateUser(Validate model)
        {
            var user = db.TempUsers.ToList().Where(m => m.Email == model.Email).FirstOrDefault();
            var validate = db.VarificationModel.ToList().Where(m => m.User == user && m.VarificationCode == model.VerificationCode).FirstOrDefault();
            if (validate != null)
            {
                //db.TempUsers.Remove(user);
                db.Users.Add(user.ConvertToUser());
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateUser(int id,User model)
        {
            try
            {
                if(id == model.ID) {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return response;
                }
            }
            catch (Exception)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        public HttpResponseMessage DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }

        [Route("api/user/login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginModel model)
        {
            var user = db.Users.ToList().Where(m => m.Email == model.Username && m.Password == model.Password).FirstOrDefault();
            if (user != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }

        
    }
}
