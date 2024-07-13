namespace Web.Responses;

public class ApiPasswordEntry
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// К чему относится пароль
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Значение пароля
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Дата добавления
    /// </summary>
    public DateTimeOffset DateAdded { get; set; }
    
    /// <summary>
    /// Почта или сайт
    /// </summary>
    public bool IsEmail { get; set; }
}