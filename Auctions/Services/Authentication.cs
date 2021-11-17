using Domain.Repository;
using Forms.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Model.Entity;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Auctions.Services {
    public class Authentication : AuthenticationStateProvider {
        private const string _storage = "identity";

        private readonly ProtectedLocalStorage _protectedLocalStorage;
        [Inject]
        private IUserRepository _userRepository { get; set; }

        public Authentication(ProtectedLocalStorage protectedLocalStorage) {
            _protectedLocalStorage = protectedLocalStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            var principal = new ClaimsPrincipal();

            try {
                var storedPrincipal = await _protectedLocalStorage.GetAsync<string>(_storage);

                if (storedPrincipal.Success) {
                    var user = JsonConvert.DeserializeObject<User>(storedPrincipal.Value);
                    var (_, isLookUpSuccess) = LookUpUser(user.Username, user.Password);

                    if (isLookUpSuccess) {
                        var identity = CreateIdentityFromUser(user);
                        principal = new(identity);
                    }
                }
            } catch {

            }

            return new AuthenticationState(principal);
        }

        public async Task LoginAsync(Login loginFormModel) {
            var (userInDatabase, isSuccess) = LookUpUser(loginFormModel.Email, loginFormModel.Password);
            var principal = new ClaimsPrincipal();

            if (isSuccess) {
                var identity = CreateIdentityFromUser(userInDatabase);
                principal = new ClaimsPrincipal(identity);
                await _protectedLocalStorage.SetAsync(_storage, JsonConvert.SerializeObject(userInDatabase));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public async Task LogoutAsync() {
            await _protectedLocalStorage.DeleteAsync(_storage);
            var principal = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        private static ClaimsIdentity CreateIdentityFromUser(User user) {
            return new ClaimsIdentity(new Claim[] {
                new (ClaimTypes.Name, user.Username),
                new (ClaimTypes.Hash, user.Password)
            });
        }

        private (User, bool) LookUpUser(string username, string password) {
            var result = _userRepository.FilterAsync(u => username == u.Username && password == u.Password).Result.First();
            return (result, result is not null);
        }
    }
}
