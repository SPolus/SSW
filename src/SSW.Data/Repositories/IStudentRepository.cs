using SSW.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Repositories
{
    public interface IStudentRepository : IAsyncRepository<Student>
    {
        Task<Student> GetByIdAsync(int id, bool includeOptions = true);
        Task<IReadOnlyCollection<Student>> GetAllAsync(bool includeOptions = true);
        //Task<Student> GetByEmailAsync(string email);
        //Task<bool> IsStudentExists(string email);
    }
}
