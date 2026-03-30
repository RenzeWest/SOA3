using SOA3.Domain;

namespace SOA3.DomainServices
{
    public class ProjectRepository
    {

        public Project MainProject { get; private set; }

        public Project? GetProject() => MainProject;

        public void Create(Project project) => MainProject = project;
    }
}
