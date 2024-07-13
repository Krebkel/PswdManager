using Contracts;

namespace Passwords.Services;

public interface IPasswordEntryService
{
    /// <summary>
    /// Создать запись пароля
    /// </summary>
    Task<PasswordEntryCreationResult> CreatePasswordEntryAsync(CreatePasswordEntryRequest passwordEntry, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить запись пароля
    /// </summary>
    Task<PasswordEntryUpdateResult> UpdatePasswordEntryAsync(UpdatePasswordEntryRequest passwordEntry, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить все запись пароляы
    /// </summary>
    Task<PasswordEntry[]> GetAllPasswordEntriesAsync();
    
    /// <summary>
    /// Удалить запись пароля
    /// </summary>
    Task DeletePasswordEntryAsync(int id, CancellationToken cancellationToken);

    Task<PasswordEntry> GetPasswordEntryAsync(int id);

}

public class UpdatePasswordEntryRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public bool IsEmail { get; set; }
}

public class CreatePasswordEntryRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public bool IsEmail { get; set; }
}