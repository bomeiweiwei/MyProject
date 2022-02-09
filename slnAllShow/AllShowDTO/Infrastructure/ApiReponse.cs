using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowDTO.Infrastructure
{
    public class ApiReponse<T> : BaseReponse where T : class
    {
        public ApiReponse()
        {
        }

        public ApiReponse(T data, bool success = true)
        {
            this.Success = success;
            this.ResultData = data;
        }

        public ApiReponse(string meeesge, T data, bool success = true)
        {
            this.Success = success;
            this.ResultData = data;
            this.Message = meeesge;
        }

        public T ResultData { get; set; }

        /*
        public int TotalCount
        {
            get
            {
                if (typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                {
                    var property = typeof(T).GetProperty("Count");
                    if (this.ResultData != null)
                    {
                        int count = (int)property.GetValue(this.ResultData, null);
                        return count;
                    }
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
        }
        */
        public int TotalDataCount { get; set; }
        public int TotalPageCount { get; set; }
    }
}
