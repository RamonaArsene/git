using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsersController : ApiController
    {
        byte[] secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };
        User user = new User { Username = "John", Password = "1234" };
        string authorizationString = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJsb2dnZWRJbkFzIjoiVXNlciIsImlzc3VlZEF0IjoiXC9EYXRlKDE1MDU5ODk2MzYzMDgpXC8iLCJ1c2VybmFtZSI6IkpvaG4ifQ.QQrbBhA6q0zkv0eawZyuKPNW7POVSVY5Ie91BpNCdLg";    

        [System.Web.Http.HttpGet]
        public User GetAllUsers()
        {
            return user;
        }

        [Route("api/Users/Authenticate")]
        [System.Web.Http.HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Autheticate([FromBody]User reqUser)
        {
            //var user = users.FirstOrDefault((p) => p.Username.Equals(reqUser.Username) && p.Password.Equals(reqUser.Password));
            if (user.Username.Equals(reqUser.Username) && user.Password.Equals(reqUser.Password))
            {
                AuthorizationClass auth = new AuthorizationClass();
                auth.loggedInAs = "User";
                auth.username = reqUser.Username;
                auth.issuedAt = DateTime.Now;

                //var secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };

                string token = Jose.JWT.Encode(auth, secretKey, JwsAlgorithm.HS256);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, token);
                response.Headers.Add("Authentication", "Autorizat");

                return new ResponseMessageResult(response);
            }
            else
            {

                var fail = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return new ResponseMessageResult(fail);
               
            }
        }

        [Route("api/Users/Authorize")]
        [System.Web.Http.HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Authorize ()
        {
            var token = Request.Headers.GetValues("token").ToList()[0].ToString();
            var auth = Jose.JWT.Decode<AuthorizationClass>(token, secretKey, JwsAlgorithm.HS256);
            if (auth.loggedInAs.Equals("User"))
            {
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.OK,auth));
            }
            else
            {
                var fail = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return new ResponseMessageResult(fail);
            }
        }
        }




    }
