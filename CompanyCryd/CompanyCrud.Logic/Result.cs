﻿using System.Collections.Generic;

namespace CompanyCrud.Logic
{
    public class Result
    {
        public bool Success;
        public IEnumerable<ErrorMessage> Errors { get; set; }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>()
            {
                Success = true,
                Value = value
            };
        }

        public static Result Ok()
        {
            return new Result()
            {
                Success = true
            };
        }

        public static Result Error(string message)
        {
            return new Result()
            {
                Success = false,
                Errors = new List<ErrorMessage>()
                {
                    new ErrorMessage()
                    {
                        PropertyName = string.Empty,
                        Message = message
                    }
                }
            };
        }

        public static Result<T> Error<T>(string message)
        {
            return new Result<T>
            {
                Success = false,
                Errors = new List<ErrorMessage>()
                {
                    new ErrorMessage()
                    {
                        PropertyName = string.Empty,
                        Message = message
                    }
                }
            };
        }
    }

    public class Result<T> : Result
    {
        public T Value;
    }

    public class ErrorMessage
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
