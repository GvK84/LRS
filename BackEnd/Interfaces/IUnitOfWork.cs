using System;

namespace BackEnd.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        ITypeRepository Types { get; }
        ITitleRepository Titles { get; }
        int Complete();
    }
}
