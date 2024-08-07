using HW30.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW30.Builders
{
    internal class UserRequestBuilder
    {
        private readonly UserRequest _request;

        public UserRequestBuilder()
        {
            _request = new UserRequest();
        }

        public UserRequestBuilder Name(string name)
        {
            _request.Name = name;
            return this;
        }

        public UserRequestBuilder Job(string job)
        {
            _request.Job = job;
            return this;
        }

        public UserRequest Build()
        {
            return _request;
        }
    }
}
