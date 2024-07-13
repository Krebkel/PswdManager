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
    /// Получить все записи паролей
    /// </summary>
    Task<PasswordEntry[]> GetAllPasswordEntriesAsync();
    
    /// <summary>
    /// Удалить запись пароля по ID
    /// </summary>
    Task DeletePasswordEntryAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить запись пароля по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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