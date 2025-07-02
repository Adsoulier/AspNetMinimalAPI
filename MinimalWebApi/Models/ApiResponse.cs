namespace MinimalWebApi.Models
{
    public class ApiResponse<T>
    {
        public class Pagination
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public Pagination? Paging { get; set; }

        public ApiResponse(T? data, bool success, string? message = null, int page = 1, int pageSize = 10)
        {
            Data = data;
            Success = success;
            Message = message;
            Paging = new Pagination { Page = page, PageSize = pageSize };
        }

    }
}
