using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository: IRepositoryBase<Account>
    {
        IEnumerable<Account> AccountsByOwner(Guid ownerId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task CreateAccountAsync(Account account);
    }
}
