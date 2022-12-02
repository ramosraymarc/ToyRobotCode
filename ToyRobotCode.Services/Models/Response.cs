using System;
using System.Collections.Generic;
using System.Linq;
using ToyRobotCode.Services.Enums;

namespace ToyRobotCode.Services.Models
{
    public sealed class Response
    {
        public ResponseCode Code { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }

        private Response(ResponseCode responseCode, IEnumerable<string> errors)
        {
            Code = responseCode;
            Errors = errors.ToArray();
        }

        private Response(ResponseCode responseCode, string message)
        {
            Code = responseCode;
            Message = message;
        }

        public static Response Success(string message)
        {
            Console.WriteLine($"{ResponseCode.Ok}: {message}");
            return new Response(ResponseCode.Ok, message);
        }

        public static Response Invalid(IEnumerable<string> errors)
        {
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    Console.WriteLine($"{ResponseCode.Invalid}: {error}");
                }
            }

            return new Response(ResponseCode.Invalid, errors);
        }
    }
}
