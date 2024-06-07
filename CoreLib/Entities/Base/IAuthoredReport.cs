﻿namespace CoreLib.Entities.Base
{
    public interface IAuthoredReport<TReporter, TReporterId> : IReport
    {
        public TReporterId AuthorId { get; set; }
        public TReporter Author { get; set; }
    }
}
