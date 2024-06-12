namespace CoreLib.Interfaces.Contracts;

public interface IViolationAppeal<TViolation, TViolationId, TViolationAppealReview>
//no need to add appealer cause appealer will always be the one who appeals
{
    public TViolationId ViolationId { get; set; }
    public TViolation Violation { get; set; } //original violation which has been appealed by the subject
    public string Message { get; set; }
    public TViolationAppealReview? Review { get; set; } //review which symbolises whether or not the violation has been nulled or not
}
