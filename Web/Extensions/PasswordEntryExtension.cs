using Passwords.Services;
using Web.Requests;

namespace Web.Extensions;

public static class PasswordEntryExtension
{
    public static CreatePasswordEntryRequest ToAddPasswordEntryRequest(this AddPasswordEntryApiRequest apiRequest)
    {
        return new CreatePasswordEntryRequest
        {
            Name = apiRequest.Name,
            Password = apiRequest.Password,
            DateAdded = apiRequest.DateAdded,
            IsEmail = apiRequest.IsEmail
        };
    }

    public static UpdatePasswordEntryRequest ToUpdatePasswordEntryRequest(this UpdatePasswordEntryApiRequest apiRequest, int id)
    {
        return new UpdatePasswordEntryRequest
        {
            Id = id,
            Name = apiRequest.Name,
            Password = apiRequest.Password,
            DateAdded = apiRequest.DateAdded,
            IsEmail = apiRequest.IsEmail
        };
    }
}