namespace Web.Requests;

public class AddPasswordEntryApiRequest
{
    /// <summary>
    /// К чему относится пароль
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Значение пароля
    /// </summary>
    public required string Password { get; set; }
    
    /// <summary>
    /// Дата добавления
    /// </summary>
    public required DateTimeOffset DateAdded { get; set; }
    
    /// <summary>
    /// Почта или сайт
    /// </summary>
    public required bool IsEmail { get; set; }
}