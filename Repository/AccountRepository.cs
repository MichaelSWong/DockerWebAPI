using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository: RepositoryBase<Account>, IAccountRepository        
    {
        public AccountRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {

        }

        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        {
            return FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList();
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await FindAll()
                .OrderBy(ac => ac.DateCreated)
                .ToListAsync();
        }

        public async Task CreateAccountAsync(Account account)
        {
            account.Id = Guid.NewGuid();
            Create(account);
            await SaveAsync();
        }
    }
}
