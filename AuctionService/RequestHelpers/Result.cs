namespace AuctionService.RequestHelpers
{
    public class Result<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public int Status { get; set; }
        public string Error { get; set; }
        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value, Status = 200 };
        public static Result<T> Failure(string error, int status) => new Result<T> { IsSuccess = false, Error = error, Status = status };
    }
}
