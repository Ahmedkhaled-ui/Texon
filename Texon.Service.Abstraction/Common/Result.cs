namespace Texon.Service.Abstraction.Common
{
    public class Result
    {
        protected readonly List<Error> errors = [];
        public IReadOnlyList<Error> Errors => errors;
        public bool IsSuccess => errors.Count == 0;
        public bool IsFailure => !IsSuccess;

        protected Result() { }

        protected Result(List<Error> errors)
        {

            errors.AddRange(errors);
        }


        protected Result(Error error)
        {

            errors.Add(error);
        }


        public static Result Ok() => new Result();
        public static Result Failure(Error error) => new Result(error);
        public static Result Failure(List<Error> errors) => new Result(errors);


    }

    public class Result<TValue> : Result
    {
        private readonly TValue values;
        public TValue Value => IsSuccess ? values
            : throw new InvalidOperationException("Can Not access the value of  a faild result");


        private Result(TValue value) : base()
        {
            values = value;

        }

        private Result(Error error) : base(error)
        {

            values = default!;
        }


        private Result(List<Error> errors) : base(errors)
        {


            values = default!;
        }



        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        public static Result<TValue> Fail(Error error) => new Result<TValue>(error);
        public static Result<TValue> Fail(List<Error> error) => new Result<TValue>(error);


        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);
        public static implicit operator Result<TValue>(List<Error> error) => Fail(error);

    }

}
