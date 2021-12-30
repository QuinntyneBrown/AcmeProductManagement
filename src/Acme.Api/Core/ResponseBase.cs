using System.Collections.Generic;

namespace Acme.Api.Core
{
    public class ResponseBase
    {
        public List<string> ValidationErrors { get; set; }
    }
}
