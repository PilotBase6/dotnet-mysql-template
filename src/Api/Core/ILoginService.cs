
namespace Api.Core
{
    public interface ILoginService : IService<ILoginServiceRequest, ILoginServiceResponse, ILoginServiceRow>
    {
        
    }
    public interface ILoginServiceRequest : IServiceRequest
    {
        string Email { get; set; }
        string Password { get; set; }
    }
    public interface ILoginServiceResponse : IServiceResponse<ILoginServiceRow>
    {
        
    }
    public interface ILoginServiceRow
    {
        string? Message { get; set; }   
    }

}