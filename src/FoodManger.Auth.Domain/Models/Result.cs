namespace FoodManager.Auth.Domain.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        private Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        public T Data
        {
            get
            {
                if (!IsSuccess)
                {
                    throw new InvalidOperationException("No value available for failed result.");
                }

                return _value;
            }
        }

        public static Result<T> Success(T value) => new(value, true, Error.None);

        public static new Result<T> Failure(Error error) => new(default!, false, error);
    }

    public sealed record Error(string code, string message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}