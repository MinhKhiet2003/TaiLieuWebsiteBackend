using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return _context.Games.Include(d => d.Category).Include(d => d.User).ToList();

    }

    public async Task<Game> GetGameByIdAsync(int id)
    {
        return _context.Games.Include(v => v.Category)
                .Include(v => v.User)
                .FirstOrDefault(v => v.game_id == id);
    }

    public async Task AddGameAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGameAsync(Game game)
    {
        _context.Entry(game).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGameAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game != null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Game>> SearchGamesAsync(string? name, int? categoryId, int? classId)
    {
        var query = _context.Games
            .Include(g => g.Category)
            .Include(g => g.User)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(g => g.title.Contains(name));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(g => g.category_id == categoryId);
        }

        if (classId.HasValue)
        {
            query = query.Where(g => g.Category.class_id == classId);
        }

        return await query.ToListAsync();
    }
}
