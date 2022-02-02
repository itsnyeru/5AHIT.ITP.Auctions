using EFCAT.Service.Authentication;
using EFCAT.Service.Storage;
using Model.Configuration;
using Model.Entity;

namespace Services;

public class AuctionAuthentication : AuthenticationService<User> {
    IMailService _mailService;

    public AuctionAuthentication(AuctionDbContext dbContext, ILocalStorage storage, IMailService mailService) : base(dbContext, storage) { _mailService = mailService; }

    protected async override Task OnRegisterSuccess(User account) {
        EmailVerificationCode code = new EmailVerificationCode() { User = account };
        await _dbContext.Set<Code>().AddAsync(code);
        await _dbContext.SaveChangesAsync();
        await _mailService.SendEmailAsync(account.Email, "Email Verification", $"https://localhost:7038/emailverification/{account.Username}/{code.Id}");
    }

}