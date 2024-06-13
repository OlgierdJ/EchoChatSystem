namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface ITargetedReport<TSubject, TSubjectId> : IReport
{
    public TSubjectId SubjectId { get; set; }
    public TSubject Subject { get; set; }
}
