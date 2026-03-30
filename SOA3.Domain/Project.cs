using SOA3.Domain.BacklogPatterns;
using SOA3.Domain.ForumPattern;
using SOA3.Domain.PipelinePatterns;
using SOA3.Domain.SCMConfigPatterns;

namespace SOA3.Domain
{
    public class Project
    {
        public Guid Id;
        public required string Name;
        public required string Description;
        public DateOnly StartDate;
        public DateOnly EndDate;
        public required Person ProductOwner;
        public SCMConfig? SCMConfiguration; // Should only set this with the sprint or smth
        public ISCMService SCMService = new GitSCMAdapter(); // TODO: Prob remove this
        public Forum Forum = new Forum();
        public required string DefinitionOfDone;
        private List<BacklogItem> _productBacklog = new List<BacklogItem>();
        private List<DevelopmentPipeline> _developmentPipelines = new List<DevelopmentPipeline>();
    }
}
