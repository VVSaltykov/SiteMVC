using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using SiteMVC.ViewModels;

namespace SiteMVC.Repositories
{
    public class AccountRepository
    {
        private readonly ApplicationContext appContext;

        public AccountRepository(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }
        public async Task AddNewAccount(Account account)
        {
            appContext.Accounts.Add(account);
            await appContext.SaveChangesAsync();
        }
        public async Task<Account> GetAccountByLoginModelAsync(LoginViewModel loginViewModel)
        {
            Account account = await appContext.Accounts.FirstOrDefaultAsync(a => a.Login == loginViewModel.Login && a.Password == loginViewModel.Password);
            return account;
        }
        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            var account = await appContext.FindAsync<Account>(accountId);
            return account;
        }
        public async Task<Account> GetAccountByLoginAsync(LoginViewModel loginViewModel)
        {
            Account account = await appContext.Accounts.FirstOrDefaultAsync(a => a.Login == loginViewModel.Login);
            return account;
        }
        public async Task<Account> GetAccountByLoginAsync(string login)
        {
            Account account = await appContext.Accounts.FirstOrDefaultAsync(a => a.Login == login);
            return account;
        }
    }
}
