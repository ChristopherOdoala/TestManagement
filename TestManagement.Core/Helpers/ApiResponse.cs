using TestManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestManagement.Core.Helpers
{
    public class ApiResponse
    {
        public ApiResponseCodes Code { get; set; }
        public string Description { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Payload { get; set; } = default(T);
        public int TotalCount { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors == null ? false : Errors.Any();

        public ApiResponse(T data = default, string message = "",
           ApiResponseCodes codes = ApiResponseCodes.OK, int? totalCount = 0, params string[] errors)
        {
            Payload = data;
            Errors = errors.ToList();
            Code = !errors.Any() ? codes : codes == ApiResponseCodes.OK ? ApiResponseCodes.ERROR : codes;
            Description = message;
            TotalCount = totalCount ?? 0;
        }
    }
}
