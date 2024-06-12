namespace CoreLib.Interfaces.Contracts;

public interface IViolationAppealReview<TViolationAppeal, TViolationAppealId, TReviewer, TReviewerId>
{
    public TViolationAppealId AppealId { get; set; }
    public TReviewerId ReviewerId { get; set; }
    public bool IsDenied { get; set; } //if true the appeal is nulled
    public TViolationAppeal Appeal { get; set; } //appeal for violation
    public TReviewer Reviewer { get; set; } //reviewing admin which has made the judgement of this review
                                            //(reviewer should if possible always be an admin which
                                            //has not made the original violation decree))
}
