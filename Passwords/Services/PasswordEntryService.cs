using Contracts;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Passwords.Services;

public class PasswordEntryService : IPasswordEntryService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<PasswordEntryService> _logger;

    public PasswordEntryService(AppDbContext dbContext, ILogger<PasswordEntryService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PasswordEntryCreationResult> CreatePasswordEntryAsync(CreatePasswordEntryRequest request,
        CancellationToken cancellationToken)
    {
        var createdPasswordEntry = new PasswordEntry
        {
            Name = request.Name,
            Password = request.Password,
            DateAdded = request.DateAdded,
            IsEmail = request.IsEmail
        };

        await _dbContext.PasswordEntries.AddAsync(createdPasswordEntry, cancellationToken);

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
            when (e.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            _dbContext.PasswordEntries.Remove(createdPasswordEntry);
            var reason = $"Не удалось добавить новую запись. Документ №{createdPasswordEntry.Name} уже существует!";
            return PasswordEntryCreationResult.Error(reason);
        }

        return PasswordEntryCreationResult.Success(createdPasswordEntry);
    }

    public async Task<PasswordEntryUpdateResult> UpdatePasswordEntryAsync(UpdatePasswordEntryRequest request, CancellationToken cancellationToken)
    {
        var existingPasswordEntry = await _dbContext.PasswordEntries.FirstOrDefaultAsync(i => i.Id == request.Id);

        if (existingPasswordEntry == null)
        {
            _logger.LogWarning("Запись с Id: {Id} не найдена.", request.Id);
            return PasswordEntryUpdateResult.Error($"Не найдена запись с Id: {request.Id}");
        }

        if (!string.IsNullOrEmpty(request.Name))
            existingPasswordEntry.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Password))
            existingPasswordEntry.Password = request.Password;
        
        existingPasswordEntry.IsEmail = request.IsEmail;

        existingPasswordEntry.DateAdded = request.DateAdded;

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
            when (e.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            await _dbContext.PasswordEntries.Entry(existingPasswordEntry).ReloadAsync(cancellationToken);
            var reason = $"Не удалось обновить запись с Id:{existingPasswordEntry.Id}. " +
                         $"Произведена попытка поменять номер записи на существующий в базе: {request.Id}";
            return PasswordEntryUpdateResult.Error(reason);
        }
        
        _logger.LogInformation("Запись успешно обновлен: {@PasswordEntryName}", existingPasswordEntry.Name);

        return PasswordEntryUpdateResult.Success(existingPasswordEntry);
    }

    public async Task<PasswordEntry> GetPasswordEntryAsync(int id)
    {
        return await _dbContext.PasswordEntries.FirstAsync(i => i.Id == id);
    }

    public async Task<PasswordEntry[]> GetAllPasswordEntriesAsync()
    {
        return await _dbContext.PasswordEntries.ToArrayAsync();
    }

    public async Task DeletePasswordEntryAsync(int id, CancellationToken cancellationToken)
    {
        var existingPasswordEntry = await _dbContext.PasswordEntries.FirstOrDefaultAsync(i => i.Id == id);

        if (existingPasswordEntry == null)
        {
            _logger.LogWarning("Запись с номером {PasswordEntryName} не найдена.", id);
            return;
        }

        _dbContext.PasswordEntries.Remove(existingPasswordEntry);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Запись успешно удалена: {@PasswordEntryName}", existingPasswordEntry.Name);
    }
}