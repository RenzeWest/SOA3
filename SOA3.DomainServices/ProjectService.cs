using SOA3.Domain;

namespace SOA3.DomainServices
{
    public class ProjectService
    {
        private static ProjectRepository _projectRepository = new ProjectRepository();
        public static Project? CreateProject(string name, string description, DateTime startDate, DateTime endDate, string productOwnerName, string definitionOfDone)
        {
            // Validation logic
            // Create Project
            Project project = new Project()
            {
                Name = name,
                Description = description,
                StartDate = DateOnly.FromDateTime(startDate),
                EndDate = DateOnly.FromDateTime(endDate),
                ProductOwner = PersonService.GetPersonByName(productOwnerName),
                DefinitionOfDone = definitionOfDone
            };
            _projectRepository.Create(project);

            // return if correct
            return project;
        }

        public static Project? GetProject() => _projectRepository.GetProject();
    }
}
