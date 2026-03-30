using SOA3.Domain;

namespace SOA3.DomainServices
{
    public class PersonRepository
    {
        public List<Person> Personen { get; } = [new() {Name = "Renze Westerink", Email = "renze@010203.net"}];
        public Guid CreateNewPerson(string name, string email)
        {
            Person person = new()
            {
                Name = name,
                Email = email
            };
            Personen.Add(person);
            return person.Id;
        }
        public List<Person> GetAllPersonen() => Personen;
    }
}
