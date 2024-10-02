namespace Api.Core
{
    public interface IService<TReponse, TDataResponse>
    where TReponse : IServiceResponse<TDataResponse>
    {
        ValueTask<TReponse> ExecuteAsync();

        ValueTask<IEnumerable<string>> ValidateAsync();
    }
    public interface IService<TRequest, TReponse, TDataResponse>
    where TRequest : IServiceRequest
    where TReponse : IServiceResponse<TDataResponse>
    {
        ValueTask<TReponse> ExecuteAsync(TRequest request);

        ValueTask<IEnumerable<string>> ValidateAsync(TRequest request);
    }

    public interface IServiceResponse<T>
    {
        string? Message { get; set; }
        IEnumerable<string>? Errors { get; set; }
        bool Success { get; set; }
        abstract T? Data { get; set; }
    }

    public interface IServiceRequest
    {
    }
}