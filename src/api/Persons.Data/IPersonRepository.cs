using Persons.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Data
{
    public interface IPersonRepository
    {
        public IEnumerable<Person> GetAllPersons();
        public Person GetPersonById(int id);

    }
}
