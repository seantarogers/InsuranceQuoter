namespace InsuranceQuoter.Application.Query.Handlers
{
    namespace Cqs.Application.Query.Handlers
    {
        using System.Threading.Tasks;
        using InsuranceQuoter.Application.Query.Results;

        public interface IAsyncQueryHandler<in TQuery, TResult>
            where TResult : QueryResult
        {
            Task<TResult> HandleAsync(TQuery query);
        }
    }
}