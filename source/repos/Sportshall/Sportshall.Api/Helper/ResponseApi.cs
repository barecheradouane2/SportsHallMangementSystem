﻿namespace Sportshall.Api.Helper
{
    public class ResponseApi
    {
        public ResponseApi(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageFromStatusCode( statusCode);
        }

        private string GetMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => null,
            };
        }

        public int StatusCode { get; set; }

        public string? Message { get; set; }
    }
}
