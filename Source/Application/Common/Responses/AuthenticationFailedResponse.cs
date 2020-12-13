using System.Collections.Generic;

namespace MakeMeRich.Application.Common.Responses
{
    public class AuthenticationFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
