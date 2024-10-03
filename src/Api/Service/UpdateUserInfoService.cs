using Infrastructure.Entities;
using Infrastructure.Repositories;
using Api.Core;

namespace Api.Service
{
    public class UpdateUserInfoService : IUpdateUserInfoService
    {
        private readonly UserRepository UserRepository;
        public UpdateUserInfoService(UserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public async ValueTask<IUpdateUserInfoServiceResponse> ExecuteAsync(IUpdateUserInfoServiceRequest request)
        {
            var validations = await ValidateAsync(request);
            if (validations.Any())
            {
                return new UpdateUserInfoServiceResponse
                {
                    Success = false,
                    Errors = validations
                };
            }

            var userExist = await UserRepository.GetByEmailAsync(request.Email);

            userExist.Name = request.Name;
            userExist.Email = request.Email;
            userExist.SetPassword(request.Password);
            userExist.UpdatedAt = DateTime.Now;

            await UserRepository.Update(userExist);

            return new UpdateUserInfoServiceResponse
            {
                Success = true,
                Message = "User updated successfully"
            };
        }
        public async ValueTask<IEnumerable<string>> ValidateAsync(IUpdateUserInfoServiceRequest request)
        {
            var errors = new List<string>();
            var user = await UserRepository.GetByEmailAsync(request.Email);

            if (string.IsNullOrEmpty(request.Name))
            {
                errors.Add("Name is required");
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                errors.Add("Email is required");
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                errors.Add("Password is required");
            }
            if (user == null)
            {
                errors.Add("User does not exist");
            }
            try
            {
                User.ValidateName(request.Name);
            }
            catch (ArgumentException e)
            {
                errors.Add(e.Message);
            }
            try
            {
                User.ValidateEmail(request.Email);
            }
            catch (ArgumentException e)
            {
                errors.Add(e.Message);
            }
            try
            {
                User.ValidatePassword(request.Password);
            }
            catch (ArgumentException e)
            {
                errors.Add(e.Message);
            }



            return errors;
        }
    }
    public class UpdateUserInfoServiceRequest : IUpdateUserInfoServiceRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class UpdateUserInfoServiceResponse : IUpdateUserInfoServiceResponse
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public IUpdateUserInfoServiceRow? Data { get; set; }
    }
    public class UpdateUserInfoServiceRow : IUpdateUserInfoServiceRow
    {
        public string? Message { get; set; }
    }
}