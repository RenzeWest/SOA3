using SOA3.Domain.NotificationPatterns;
using SOA3.Domain.PipelinePatterns.CompositePattern.Leafs;

namespace SOA3.Domain.PipelinePatterns.VisitorPattern
{
    public class RunVisitor : IPipelineVisitor
    {
        private NotificationPublisher _publisher = new NotificationPublisher();

        public void PipelineFailed(Exception exception)
        {
            var message = $"[Run] Failed with message:{exception.Message}";
            Console.WriteLine(message);
            _publisher.NotifySubscribers(new Notification(DateTime.UtcNow, message));
        }

        public void VisitAnalysePipelineAction(AnalysePipelineAction action) => Console.WriteLine($"[Run] Analyse action called: {action.Name}");

        public void VisitBuildPipelineAction(BuildPipelineAction action) => Console.WriteLine($"[Run] Build action called: {action.Name}");

        public void VisitDeployPipelineAction(DeployPipelineAction action) => Console.WriteLine($"[Run] Deploy action called: {action.Name}");

        public void VisitPackagePipelineAction(PackagePipelineAction action) => Console.WriteLine($"[Run] Package action called: {action.Name}");

        public void VisitSourcePipelineAction(SourcePipelineAction action) => Console.WriteLine($"[Run] Source action called: {action.Name}");

        public void VisitTestPipelineAction(TestPipelineAction action) => Console.WriteLine($"[Run] Test action called: {action.Name}");

        public void VisitUtilityPipelineAction(UtilityPipelineAction action) => Console.WriteLine($"[Run] Utility action called: {action.Name}");
    }
}
