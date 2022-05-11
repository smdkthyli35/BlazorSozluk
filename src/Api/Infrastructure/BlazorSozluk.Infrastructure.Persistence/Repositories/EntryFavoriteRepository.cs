using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories
{
    public class EntryFavoriteRepository : GenericRepository<EntryFavorite>, IEntryFavoriteRepository
    {
        public EntryFavoriteRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
