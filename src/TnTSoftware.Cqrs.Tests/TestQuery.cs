////namespace TnTSoftware.Cqrs.Tests
////{
////    using System.Threading;
////    using System.Threading.Tasks;
////    using Query;

////    public class TestQueryHandler : IQueryHandler<TestQuery>
////    {
////        public string SettableValue { get; }

////        public TestQueryHandler(string settableValue = null)
////        {
////            SettableValue = settableValue;
////        }
        
////        public Task<ExecutionResponse> Handle(QueryContext<TestQuery> request, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResponse> next)
////        {
////            throw new System.NotImplementedException();
////        }
////    }
    
////    public class TestQuery : IQuery
////    {
////    }
////}