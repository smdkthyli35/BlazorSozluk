﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories
{
    public class EntryCommentVoteRepository : GenericRepository<EntryCommentVote, BlazorSozlukContext>, IEntryCommentVoteRepository
    {
        public EntryCommentVoteRepository(BlazorSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
