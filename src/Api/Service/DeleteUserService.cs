using Infrastructure.Repositories;
using Api.Core;

namespace Api.Service
{
    public class DeleteUserService : IDeleteUserService
    {
        private readonly UserRepository _userRepository;

        public DeleteUserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async ValueTask<IDeleteUserServiceResponse> ExecuteAsync(IDeleteUserServiceRequest request)
        {
            var validations = await ValidateAsync(request);
            if (validations.Any())
            {
                return new DeleteUserServiceResponse
                {
                    Success = false,
                    Errors = validations
                };
            }

            var userExist = await _userRepository.GetById(request.Id);

            await _userRepository.Delete(userExist);

            return new DeleteUserServiceResponse
            {
                Success = true,
                Data = new DeleteUserServiceRow
                {
                    Message = "User successfully deleted."
                }
            };
        }

        public async ValueTask<IEnumerable<string>> ValidateAsync(IDeleteUserServiceRequest request)
        {
            var errors = new List<string>();

            var userExist = await _userRepository.GetById(request.Id);
            if (request.Id == Guid.Empty)
            {
                errors.Add("A valid user ID must be provided.");
            }
            else if (userExist == null)
            {
                errors.Add("The user does not exist.");
            }

            return errors;
        }
    }

    public class DeleteUserServiceRequest : IDeleteUserServiceRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserServiceResponse : IDeleteUserServiceResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public IDeleteUserServiceRow? Data { get; set; }
        public string? Message { get; set; }
    }

    public class DeleteUserServiceRow : IDeleteUserServiceRow
    {
        public string? Message { get; set; }
    }
}