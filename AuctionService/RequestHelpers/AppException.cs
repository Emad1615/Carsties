namespace AuctionService.RequestHelpers
{
    public class AppException(int status, string message, string details)
    {
        public int Status { get; set; } = status;
        public string Message { get; set; } = message;
        public string Details { get; set; } = details;
    }
}
