namespace BuberBreakfast.Exceptions
{
    public static class ServiceErrors
    {
        public static HttpException BadRequestException(string? message=null) => new HttpException(400, message ?? "Bad Request.");
        public static HttpException NotFoundException(string? message=null) => new HttpException(404, message ?? "The requested resource could not be found.");
        public static HttpException ConflictException(string? message=null) => new HttpException(409, message ?? "The request could not be completed because of a conflict in the request.");
    }
}
