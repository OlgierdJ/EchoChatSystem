﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseViolationAppealReview<TId, TViolationAppeal, TViolationAppealId, TReviewer, TReviewerId> : BaseEntity<TId>
    {
        public TViolationAppealId AppealId { get; set; }
        public TReviewerId ReviewerId { get; set; }
        public bool IsDenied { get; set; } //if true the appeal is nulled
        public TViolationAppeal Appeal { get; set; } //appeal for violation
        public TReviewer  Reviewer { get; set; } //reviewing admin which has made the judgement of this review
                                                 //(reviewer should if possible always be an admin which
                                                 //has not made the original violation decree))
    }
}
