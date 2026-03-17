using SOA3.Domain.BacklogPatterns;
using SOA3.Domain.ForumPattern;
using SOA3.Domain.PipelinePatterns;
using SOA3.Domain.SCMConfigPatterns;

namespace SOA3.Domain
{
    public class Project
    {
        private Guid _id;
        private string _name;
        private string _description;
        private DateOnly _startDate;
        private DateOnly _endDate;
        private Person _productOwner;
        private SCMConfig _scmConfiguration; // TODO: Prob remove this
        private ISCMService _scmService = new GitSCMAdapter(); // TODO: Prob remove this
        private Forum _forum;
        private string _definitionOfDone;
        private List<BacklogItem> _productBacklog = new List<BacklogItem>();
        private List<DevelopmentPipeline> _developmentPipelines = new List<DevelopmentPipeline>();
    }
}
