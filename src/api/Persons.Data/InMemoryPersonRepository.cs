using Persons.Core;

namespace Persons.Data
{
    public class InMemoryPersonRepository : IPersonRepository
    {
        private readonly List<Person> _persons = new();
        private int _nextId = 6;

        public InMemoryPersonRepository()
        {
            _persons.Add(new Person { Id = 1, FirstName = "Timothy", LastName = "Lopez" });
            _persons.Add(new Person { Id = 2, FirstName = "George", LastName = "Vlachodimitropoulos" });
            _persons.Add(new Person { Id = 3, FirstName = "Kevin", LastName = "Dzousa" });
            _persons.Add(new Person { Id = 4, FirstName = "Rodel", LastName = "Reyes" });
            _persons.Add(new Person { Id = 5, FirstName = "Peter", LastName = "Hildebrandt" });

            _nextId = _persons.Count > 0 ? _persons.Max(p => p.Id) + 1 : 1; //get max(Id) + 1
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _persons;
        }

        public Person GetPersonById(int id)
        {
            return _persons.FirstOrDefault(p => p.Id == id);
        }

        public Person Add(Person person)
        {
            person.Id = _nextId++;
            _persons.Add(person);
            return person;
        }
    }
}
