using System.Collections.Generic;
using CommonDomain;

namespace Hospital.Domain.Specs
{
    public class NullConflictDetector : IDetectConflicts
    {
        public void Register<TUncommitted, TCommitted>(ConflictDelegate handler) where TUncommitted : class where TCommitted : class
        {
        }

        public bool ConflictsWith(IEnumerable<object> uncommittedEvents, IEnumerable<object> committedEvents)
        {
            return false;
        }
    }
}