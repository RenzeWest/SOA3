using SOA3.Domain;

namespace SOA3.DomainServices
{
    public class ProjectService
    {
        private ProjectRepository repository = new ProjectRepository();

        public bool AddProject()
        {
            // validation logic
            // Create Project
            Project project = new Project();

            // return if correct
            return true;
        }
    }
}
