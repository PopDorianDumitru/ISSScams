namespace ISSProject.GraphAnalyser.Domain
{
    internal interface IGraphAnalyserLog
    {
        string GeneratedMessage { get; }
        int LogId { get; }
        DateTime LogTime { get; }
        int Score { get; }
        int SourceUserId { get; }
        int TargetUserId { get; }

        object Clone();
        int GetId();
    }
}