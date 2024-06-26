﻿using System.Net;

namespace SSPRExternalApiExample.Api.Dtos
{
    public class ResultItem
    {
        public bool IsOk { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public string ProcessCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ResultItem()
        {
            IsOk = false;
            Data = null;
            Message = string.Empty;
            ProcessCode = string.Empty;
            StatusCode = HttpStatusCode.NoContent;
        }

        public ResultItem(bool isOk = true, object data = null, string message = "", HttpStatusCode httpStatusCode = HttpStatusCode.OK, string processCode = "")
        {
            IsOk = isOk;
            Data = data;
            Message = message;
            StatusCode = httpStatusCode;
            ProcessCode = processCode;
        }
    }
}
