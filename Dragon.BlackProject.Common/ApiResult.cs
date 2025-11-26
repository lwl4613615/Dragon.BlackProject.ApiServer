namespace Dragon.BlackProject.Common
{
    public enum ApiCode
    {
        Success = 0,
        Error = 1,
        NotFound = 404,
        Unauthorized = 401,
        Forbidden = 403,
        BadRequest = 400,
        InternalServerError = 500,
        InvalidUserNameOrPassword=1001
    }
    public static class ApiMessageCatalog
    {
        private static readonly IReadOnlyDictionary<ApiCode, string> Message = new Dictionary<ApiCode, string>
        {
            [ApiCode.Success] = "请求成功",
            [ApiCode.Error] = "请求失败",
            [ApiCode.NotFound] = "资源未找到",
            [ApiCode.Unauthorized] = "未授权",
            [ApiCode.Forbidden] = "禁止访问",
            [ApiCode.BadRequest] = "错误的请求",
            [ApiCode.InternalServerError] = "服务器内部错误",
            [ApiCode.InvalidUserNameOrPassword]= "无效的用户名或密码"

        };
        public static string ResolveMessage(ApiCode code)
        {
            return Message.TryGetValue(code, out var message) ? message : "未知错误";
        }

    }
    public record ApiResult
    {
        public ApiCode code { get; init; }
        public string? message { get; init; }

    }

    public record ApiResult<T> : ApiResult
    {
        public T? data { get; init; }

        public Object? OValue { get; init; }

        public static ApiResult<T> Ok(T data, object? extra = null) =>
         new ApiResult<T>
         {
             code = ApiCode.Success,
             message = ApiMessageCatalog.ResolveMessage(ApiCode.Success),
             data = data,
             OValue = extra
         };
        public static ApiResult<T> Fail(ApiCode code = ApiCode.Error, object? extra=null) =>
            new ApiResult<T>
            {
                code = code,
                message = ApiMessageCatalog.ResolveMessage(code),
                data = default,
                OValue = extra
            };

        public static ApiResult<T> Form(ApiCode code, T? data=default,object? extra = null) =>
            new ApiResult<T>
            {
                code =code,
                message = ApiMessageCatalog.ResolveMessage(code),
                data = default,
                OValue = extra
            };
    }



}
