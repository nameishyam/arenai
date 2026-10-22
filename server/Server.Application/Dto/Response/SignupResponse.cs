using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Application.Dto.Response
{
    public class SignupResponse
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
    }
}
