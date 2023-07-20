using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ModelResponse<T>
    {
        public ModelResponse(string token)
        {
            StatusCode = (int)EnumStatus.Success;
            NewToken = token;
        }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string NewToken { get; set; }
        public T Data { get; set; }
    }
}
