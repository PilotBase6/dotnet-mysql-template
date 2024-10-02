
namespace Api.Core
{
    public interface ICreateNewUserService : IService<ICreateNewUserServiceRequest, ICreateNewUserServiceResponse, ICreateNewUserServiceRow>
    {
        
    }
    public interface ICreateNewUserServiceRequest : IServiceRequest
    {
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
    public interface ICreateNewUserServiceResponse : IServiceResponse<ICreateNewUserServiceRow>
    {
        
    }
    public interface ICreateNewUserServiceRow
    {
        string Message { get; set; }   
    }

}