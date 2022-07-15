using Microsoft.AspNetCore.Mvc;
using Skinet.Controllers;
using System.Collections.Generic;

namespace Skinet.Errors
{
    public class APIValidationErrorResponce : APIResponce
    {
        public APIValidationErrorResponce() : base(400)
        {

        }
        public IEnumerable<string> Errors { get; set; }

    }
}
