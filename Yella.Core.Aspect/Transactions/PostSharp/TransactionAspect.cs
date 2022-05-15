using System.Transactions;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Archseptia.Core.Aspect.Transaction.PostSharp
{
    [PSerializable]
    public class TransactionAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
            args.MethodExecutionTag = transactionScope;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var transactionScope = (TransactionScope)args.MethodExecutionTag;
            transactionScope.Complete();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var transactionScope = (TransactionScope)args.MethodExecutionTag;
            transactionScope.Dispose();
        }
    }
}
