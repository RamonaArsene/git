using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AuthorizationClass
    {
        public string loggedInAs { get; set; }
        public DateTime issuedAt { get; set; }
        public string username { get; set; }
    }
}