using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace templatebase.src.Infraestructure.Adapters.In.Dto
{
    public class ResponseAPI
    {
        public ResponseAPI()
        {
            ErrorMessages = [];
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSucess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}