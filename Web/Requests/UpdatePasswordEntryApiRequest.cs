namespace Web.Requests;

public class UpdatePasswordEntryApiRequest
{
    /// <summary>
    /// К чему относится пароль
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Значение пароля
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Дата добавления
    /// </summary>
    public required DateTimeOffset DateAdded { get; set; }
    
    /// <summary>
    /// Почта или сайт
    /// </summary>
    public required bool IsEmail { get; set; }
}