namespace Api.Core
{
    public interface IUpdateUserInfoService : IService<IUpdateUserInfoServiceRequest, IUpdateUserInfoServiceResponse, IUpdateUserInfoServiceRow>
    {
        
    }
    public interface IUpdateUserInfoServiceRequest : IServiceRequest
    {
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
    public interface IUpdateUserInfoServiceResponse : IServiceResponse<IUpdateUserInfoServiceRow>
    {
        
    }
    public interface IUpdateUserInfoServiceRow
    {
        string? Message { get; set; }   
    }

}