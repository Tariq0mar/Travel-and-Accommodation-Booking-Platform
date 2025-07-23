using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;

    public UserService(
        IUserRepository userRepository,
        IValidator<User> userValidator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException($"User with Id = {id} not found.");
        }

        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync(UserFilter queryFilter)
    {
        return await _userRepository.GetAllAsync(queryFilter);
    }

    public async Task<User> AddAsync(User user)
    {
        var validation = await _userValidator.ValidateAsync(user);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid user: {errors}");
        }

        var addedUser = await _userRepository.AddAsync(user);

        if (addedUser is null)
        {
            throw new CreationException($"User could not be created.");
        }

        await _userRepository.SaveChangesAsync();

        return addedUser;
    }

    public async Task UpdateAsync(User user)
    {
        var validation = await _userValidator.ValidateAsync(user);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid user update: {errors}");
        }

        var success = await _userRepository.UpdateAsync(user);
        if (!success)
        {
            throw new NotFoundException($"User with Id = {user.Id} not found.");
        }

        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _userRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"User with Id = {id} not found.");
        }

        await _userRepository.SaveChangesAsync();
    }
}
