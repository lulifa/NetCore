using System;
using System.Collections.Generic;
using System.Text;

namespace Richard.Core.Model
{
    public class ApiResult
    {
        public ApiResult(bool success)
        {
            this.success = success;
        }
        public ApiResult(bool success,object data) : this(success)
        {
            this.data = data;
        }
        public ApiResult(bool success, object data, string msg) : this(success,data)
        {
            this.msg = msg;
        }
        public bool success { get; set; }
        public int code
        {
            get
            {
                if (success)
                {
                    return 200;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string msg { get; set; }
        public object data { get; set; }
    }
}
