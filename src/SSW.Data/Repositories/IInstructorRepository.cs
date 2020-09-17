using SSW.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Repositories
{
    public interface IInstructorRepository : IAsyncRepository<Instructor>
    {
        Task<Instructor> GetByIdAsync(int id, bool includeCourses = true);
        Task<IReadOnlyCollection<Instructor>> GetAllAsync(bool includeCourses = true);
        //Task<Instructor> GetByEmailAsync(string email);
        //Task<bool> IsInstructorExists(string email);
    }
}
