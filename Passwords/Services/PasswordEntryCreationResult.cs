using Contracts;

namespace Passwords.Services;

public class PasswordEntryCreationResult
{
    public required bool IsSuccess { get; init; }
    
    public string? StatusMessage { get; init; }

    public PasswordEntry Result { get; init; }

    public static PasswordEntryCreationResult Error(string message)
    {
        return new PasswordEntryCreationResult
        {
            IsSuccess = false,
            StatusMessage = message
        };
    }
    
    public static PasswordEntryCreationResult Success(PasswordEntry result, string? message = null)
    {
        return new PasswordEntryCreationResult
        {
            IsSuccess = true,
            Result = result,
            StatusMessage = message
        };
    }
}

public class PasswordEntryUpdateResult
{
    public required bool IsSuccess { get; init; }
    
    public string? StatusMessage { get; init; }

    public PasswordEntry Result { get; init; }

    public static PasswordEntryUpdateResult Error(string message)
    {
        return new PasswordEntryUpdateResult
        {
            IsSuccess = false,
            StatusMessage = message
        };
    }
    
    public static PasswordEntryUpdateResult Success(PasswordEntry result, string? message = null)
    {
        return new PasswordEntryUpdateResult
        {
            IsSuccess = true,
            Result = result,
            StatusMessage = message
        };
    }
}