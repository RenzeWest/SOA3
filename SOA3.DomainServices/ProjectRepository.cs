using SOA3.Domain;

namespace SOA3.DomainServices
{
    public class ProjectRepository
    {

        public List<Project> Projects { get; } = new List<Project>();
        public void Add(Project project) => Projects.Add(project);
    }
}
