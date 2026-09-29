using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStudentManagementSystem.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }

        public Result(bool isSuccess, string? message, T? data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
    }
}
