using Microsoft.EntityFrameworkCore;
using Yella.Domain.Entities;
using Yella.Utilities.Results;

namespace Yella.EntityFrameworkCore;

public interface IUnitOfWork : IDisposable
{
    bool Commit(bool state = true);
}


