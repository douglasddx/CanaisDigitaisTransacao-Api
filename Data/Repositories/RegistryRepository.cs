using System.Collections.Generic;
using canalTransacao.Entities;

namespace canalTransacao.Data.Repositories
{
    public class RegistryRepository
    {
        private readonly AppDataContext _context;
        public RegistryRepository(AppDataContext context)
        {
            _context = context;
        }

        public void AddRegistrys(IEnumerable<Registry> registrys)
        {
            _context.Registry.AddRange(registrys);
        }

        public void Salve()
        {
            _context.SaveChanges();
        }
    }
}