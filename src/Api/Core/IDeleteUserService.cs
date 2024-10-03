namespace Api.Core
{
    public interface IDeleteUserService : IService<IDeleteUserServiceRequest, IDeleteUserServiceResponse, IDeleteUserServiceRow>
    {
        
    }
    public interface IDeleteUserServiceRequest : IServiceRequest
    {
        Guid Id { get; set; }
    }
    public interface IDeleteUserServiceResponse : IServiceResponse<IDeleteUserServiceRow>
    {

    }
    public interface IDeleteUserServiceRow
    {
        string? Message { get; set; }   
    }

}