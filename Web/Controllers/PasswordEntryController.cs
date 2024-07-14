using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Passwords.Services;
using Web.Extensions;
using Web.Requests;
using Web.Responses;

namespace Web.Controllers;

[ApiController]
[Route("api/passwordEntries")]
public class PasswordEntryController : ControllerBase
{
    private readonly ILogger<PasswordEntryController> _logger;
    private readonly IPasswordEntryService _passwordEntryService;

    public PasswordEntryController(ILogger<PasswordEntryController> logger, 
        IPasswordEntryService passwordEntryService)
    {
        _logger = logger;
        _passwordEntryService = passwordEntryService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPasswordEntry([FromBody] AddPasswordEntryApiRequest apiRequest, CancellationToken ct)
    {
        try
        {
            var addPasswordEntryRequest = apiRequest.ToAddPasswordEntryRequest();
            var result = await _passwordEntryService.CreatePasswordEntryAsync(addPasswordEntryRequest, ct);
            
            if (result.IsSuccess)
            {
                var apiResult = new ApiPasswordEntry
                {
                    Id = result.Result.Id,
                    Name = apiRequest.Name,
                    Password = apiRequest.Password,
                    DateAdded = apiRequest.DateAdded,
                    IsEmail = apiRequest.IsEmail
                };
                return Ok(apiResult);
            }
            
            return BadRequest(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при добавлении записи");
            return BadRequest($"Ошибка при добавлении записи {e.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePasswordEntry(int id, [FromBody] UpdatePasswordEntryApiRequest request, CancellationToken ct)
    {
        try
        {
            var updatePasswordEntryRequest = request.ToUpdatePasswordEntryRequest(id);
            var result = await _passwordEntryService.UpdatePasswordEntryAsync(updatePasswordEntryRequest, ct);
            
            if (result.IsSuccess)
            {
                var apiResult = new ApiPasswordEntry
                {
                    Id = result.Result.Id,
                    Name = result.Result.Name,
                    Password = result.Result.Password,
                    DateAdded = result.Result.DateAdded,
                    IsEmail = result.Result.IsEmail
                };
                return Ok(apiResult);
            }

            return BadRequest(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обновлении записи");
            return BadRequest($"Ошибка при обновлении записи {e.Message}");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPasswordEntry(int id)
    {
        try
        {
            var passwordEntry = await _passwordEntryService.GetPasswordEntryAsync(id);
            var result = new ApiPasswordEntry
            {
                Id = passwordEntry.Id,
                Name = passwordEntry.Name,
                Password = passwordEntry.Password,
                DateAdded = passwordEntry.DateAdded,
                IsEmail = passwordEntry.IsEmail
            };

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при получении записи");
            return BadRequest($"Ошибка при получении записи {e.Message}");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPasswordEntries()
    {
        try
        {
            var passwordEntries = await _passwordEntryService.GetAllPasswordEntriesAsync();
            var result = new List<ApiPasswordEntry>();

            foreach (var passwordEntry in passwordEntries)
            {
                result.Add(new ApiPasswordEntry()
                {
                    Id = passwordEntry.Id,
                    Name = passwordEntry.Name,
                    Password = passwordEntry.Password,
                    DateAdded = passwordEntry.DateAdded,
                    IsEmail = passwordEntry.IsEmail
                });
            }
            
            return Ok(result.ToArray());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при получении записи");
            return BadRequest($"Ошибка при получении записи {e.Message}");
        }
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeletePasswordEntry(int id, CancellationToken ct)
    {
        try
        {
            await _passwordEntryService.DeletePasswordEntryAsync(id, ct);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при удалении записи");
            return BadRequest($"Ошибка при удалении записи {e.Message}");
        }
    }
}