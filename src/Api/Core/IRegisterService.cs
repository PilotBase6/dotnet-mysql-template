namespace Api.Core
{
    public interface IRegisterService : IService<IRegisterServiceRequest, IRegisterServiceResponse, IRegisterServiceRow>
    {
        
    }
    public interface IRegisterServiceRequest : IServiceRequest
    {
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
    public interface IRegisterServiceResponse : IServiceResponse<IRegisterServiceRow>
    {

    }
    public interface IRegisterServiceRow
    {
        string? Message { get; set; }   
    }

}