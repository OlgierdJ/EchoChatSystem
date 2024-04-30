using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseNote<TAuthor, TAuthorId, TSubject, TSubjectId>
    {
        public TAuthorId AuthorId { get; set; }
        public TSubjectId SubjectId { get; set; }

        public string Note { get; set; }

        public TAuthor Author { get; set; }
        public TSubject Subject { get; set; }
    }
}
