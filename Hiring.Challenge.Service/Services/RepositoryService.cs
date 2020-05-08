using Hiring.Challenge.Domain.Interfaces;
using Hiring.Challenge.Domain.Models;
using Hiring.Challenge.Domain.ViewModels;
using Hiring.Challenge.Infrastructure.Extensions;
using Hiring.Challenge.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiring.Challenge.Business.Services
{
    public class RepositoryService : IRepositoryService
    {
        readonly ApplicationContext _context;
        readonly GitHubRepository _gitHubRepository;
        public RepositoryService(ApplicationContext context, GitHubRepository gitHubRepository)
        {
            _context = context;
            _gitHubRepository = gitHubRepository;
        }

        public async ValueTask<RepositoryViewModel> GetAsync(Guid id)
        {
            var repository = await _context.Repositories
                .FirstOrDefaultAsync(x => x.Id == id);

            return repository?.ToViewModel();
        }

        public async ValueTask<IEnumerable<string>> GetLanguagesAsync()
        {
            var languages = _gitHubRepository.GetLanguages();
            
            return languages;
        }

        public async ValueTask<IEnumerable<RepositoryViewModel>> GetTopRepositoriesFromLanguageAsync(string language)
        {
            var repositories = await _context.Repositories
                .Where(x => x.Language == language)
                .ToListAsync();

            if(!(repositories?.Any() ?? false))
            {
                repositories = await _gitHubRepository.GetTopRepositoriesByLanguageAsync(language);
                
                await _context.Repositories.AddRangeAsync(repositories);
                await _context.SaveChangesAsync();
            }

            return repositories.Select(x => x.ToViewModel());
        }

    }
}
