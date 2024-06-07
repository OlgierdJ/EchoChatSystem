﻿namespace CoreLib.Entities.Base
{
    public interface IPunishableReport<TSubject, TSubjectId, TPunishment, TPunishmentId> : ITargetedReport<TSubject, TSubjectId>
    {
        public TPunishmentId? ViolationId { get; set; }
        public TPunishment? Violation { get; set; }
    }
}
