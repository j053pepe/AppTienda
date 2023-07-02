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
        public ModelResponse()
        {
            StatusCode = (int)EnumStatus.Success;
        }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
