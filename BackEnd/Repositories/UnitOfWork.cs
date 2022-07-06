using BackEnd.Data;
using BackEnd.Interfaces;

namespace BackEnd.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LRS_DBContext _context;

        public UnitOfWork(LRS_DBContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Titles = new TitleRepository(_context);
            Types = new TypeRepository(_context);
        }
        public IUserRepository Users { get; private set; }
        public ITitleRepository Titles { get; private set; }
        public ITypeRepository Types { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
