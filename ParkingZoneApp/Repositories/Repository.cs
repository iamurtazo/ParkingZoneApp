
using Microsoft.EntityFrameworkCore;
using ParkingZoneApp.Data;

namespace ParkingZoneApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            Save();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int? Id)
        {
            return _dbSet.Find(Id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            _context.Update(entity);    
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
