namespace Api.Core
{
    public interface IGetAllUserService : IService<IGetAllUserServiceResponse, IGetAllUserServiceRow>
    {
        
    }
    public interface IGetAllUserServiceResponse : IServiceResponse<IGetAllUserServiceRow>
    {
        
    }
    public interface IGetAllUserServiceRow
    {
        IEnumerable<IUsersInfo> Users { get; set; }   
    }

    public interface IUsersInfo
    {
        Guid UserId { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}