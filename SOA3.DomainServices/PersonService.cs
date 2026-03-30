using SOA3.Domain;

namespace SOA3.DomainServices
{
    public class PersonService
    {
        private static readonly PersonRepository _personRepository = new PersonRepository();

        public static List<Person> GetAllPersons() => _personRepository.GetAllPersonen();
        public static Person? GetPersonByName(string name) => _personRepository.GetAllPersonen().FirstOrDefault(p => p.Name == name);

    }
}
