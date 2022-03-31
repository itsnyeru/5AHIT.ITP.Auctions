using EFCAT.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entity;

namespace Domain.Repository;

public interface ICodeRepository : IRepositoryAsync<Code> {
    Task<T> Create<T>(T code) where T : Code;
    Task<bool> VerifyEmail(string username, string codeId);
    Task<bool> VerifyPasswordReset(string username, string codeId);
    Task<bool> ResetPassword(string username, string codeId, string password);
}

public class CodeRepository : ARepositoryAsync<Code>, ICodeRepository {
    IUserRepository _userRepository;

    public CodeRepository(AuctionDbContext context, IUserRepository userRepository) : base(context) { _userRepository = userRepository; }

    public async Task<T> Create<T>(T code) where T : Code { await _set.AddAsync(code); return code; }

    private async Task<Code?> GetCode(string username, string codeId) => (await _context.Set<User>().Include(u => u.Codes).FirstOrDefaultAsync(u => u.Username.ToUpper() == username.ToUpper()))?.Codes.FirstOrDefault(c => c.Id == Guid.Parse(codeId));

    public async Task<bool> VerifyEmail(string username, string codeId) {
        Code? code = await GetCode(username, codeId);
        if (code == null) return false;
        await DeleteAsync(code);
        return true;
    }

    public async Task<bool> VerifyPasswordReset(string username, string codeId) {
        Code? code = await GetCode(username, codeId);
        return code == null;
    }

    public async Task<bool> ResetPassword(string username, string codeId, string password) {
        Code? code = await GetCode(username, codeId);
        if (code == null) return false;
        User user = code.User;
        user.Password = password;
        await _userRepository.UpdateAsync(user);
        await DeleteAsync(code);
        return true;
    }
}