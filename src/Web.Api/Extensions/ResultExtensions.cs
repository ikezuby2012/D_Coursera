using SharedKernel;

namespace Web.Api.Extensions;

public static class ResultExtensions
{
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }

    public static IResult Match<TIn>(
     this Result<TIn> result,
     Func<TIn, IResult> onSuccess,
     Func<Error, IResult> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
    }

    //public static IResult Match(
    //    this Result result,
    //    Func<IResult> onSuccess,
    //    Func<Error, IResult> onFailure)
    //{
    //    return result.IsSuccess
    //        ? onSuccess()
    //        : onFailure(result.Error);
    //}

}
