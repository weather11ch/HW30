using HW30.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW30.Builders
{
    internal class RegisterRequestBuilder
    {
        private readonly RegisterRequest _request;

        public RegisterRequestBuilder()
        {
            _request = new RegisterRequest();
        }

        public RegisterRequestBuilder Email(string email)
        {
            _request.Email = email;
            return this;
        }

        public RegisterRequestBuilder Password(string password)
        {
            _request.Password = password;
            return this;
        }

        public RegisterRequest Build()
        {
            return _request;
        }
    }
}
