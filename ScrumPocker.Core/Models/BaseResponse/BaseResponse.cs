using System.Text.Json.Serialization;

namespace ScrumPocker.Core.Models.BaseResponse
{
    public class BaseResponse
    {
        public BaseResponse()
        {

        }
        public BaseResponse(BaseResponse baseResponse)
        {
            this.Error = baseResponse.Error;
            this.IsSuccess = baseResponse.IsSuccess;
            this.StatusCode = baseResponse.StatusCode;
        }

        #region Preperties
        [JsonIgnore]//Response Body de gorunmesin
        public int StatusCode { get; protected set; }
        public bool IsSuccess { get; set; }
        public ErrorDto Error { get; private set; }

        #endregion

        public static BaseResponse Success()
        {
            return new BaseResponse
            {
                StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK,
                IsSuccess = true,
            };
        }

        public static BaseResponse Success(int statusCode)
        {
            return new BaseResponse
            {
                StatusCode = statusCode,
                IsSuccess = true
            };
        }

        #region Methods 

        public static BaseResponse Fail(string errorMessage)
        {
            return new BaseResponse
            {
                Error = new ErrorDto(errorMessage),
                IsSuccess = false,
                StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest,
            };
        }
        public static BaseResponse Fail(string errorMessage, int statusCode)
        {
            return new BaseResponse
            {
                Error = new ErrorDto(errorMessage),
                IsSuccess = false,
                StatusCode = statusCode,
            };
        }
        public static BaseResponse Fail(ErrorDto errorDto, int statusCode)
        {
            return new BaseResponse
            {
                Error = errorDto,
                StatusCode = statusCode,
                IsSuccess = false
            };
        }

        #endregion

    }
    public class BaseResponse<T> : BaseResponse //where T : class
    {
        public BaseResponse()
        {

        }
        public BaseResponse(BaseResponse baseResponse) : base(baseResponse)
        {

        }
        public T Message { get; private set; }

        #region Methods

        #region Success Methods

        public static BaseResponse<T> Success(T data)
        {
            return new BaseResponse<T>
            {
                Message = data,
                StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK,
                IsSuccess = true
            };
        }

        public static BaseResponse<T> Success(T data, int statusCode)
        {
            return new BaseResponse<T>
            {
                Message = data,
                StatusCode = statusCode,
                IsSuccess = true
            };
        }

        #endregion

        #region Error Methods 

        public static new BaseResponse<T> Fail(string errorMessage)
        {
            return new BaseResponse<T>(BaseResponse.Fail(errorMessage));
        }
        public static new BaseResponse<T> Fail(string errorMessage, int statusCode)
        {
            return new BaseResponse<T>(BaseResponse.Fail(errorMessage, statusCode));
        }
        public static new BaseResponse<T> Fail(ErrorDto errorDto, int statusCode)
        {
            return new BaseResponse<T>(BaseResponse.Fail(errorDto, statusCode));
        }

        #endregion

        #endregion
    }
}
