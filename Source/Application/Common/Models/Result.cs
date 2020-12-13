using System;
using System.Collections.Generic;

namespace MakeMeRich.Application.Common.Models
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public static Result Success()
        {
            return new Result
            {
                Succeeded = true,
                Errors = Array.Empty<string>()
            };
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result
            {
                Errors = errors
            };
        }
    }
}
