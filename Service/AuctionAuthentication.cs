using EFCAT.Service.Authentication;
using EFCAT.Service.Storage;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entity;

namespace Services;

public class AuctionAuthentication : AuthenticationService<User> {
    IMailService _mailService;

    public AuctionAuthentication(AuctionDbContext dbContext, ILocalStorage storage, IMailService mailService) : base(dbContext, storage) { _mailService = mailService; }

    protected async override Task<bool> OnLogin(object obj, User account, string token) {
        User? user = await _dbSet.Include(u => u.Codes).FirstOrDefaultAsync(u => u.Id == account.Id);
        if (user == null) return false;
        return !user.Codes.Any(c => c.GetType() == typeof(EmailVerificationCode));
    }

    protected async override Task OnRegisterSuccess(User account) {
        EmailVerificationCode code = new EmailVerificationCode() { User = account };
        await _dbContext.Set<Code>().AddAsync(code);
        await _dbContext.SaveChangesAsync();
        await _mailService.SendEmailAsync(account.Email, "Email Verification", $"https://localhost:7038/emailverification/{account.Username}/{code.Id}");
    }

}