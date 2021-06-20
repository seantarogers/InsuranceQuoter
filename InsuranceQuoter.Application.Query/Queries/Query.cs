namespace InsuranceQuoter.Application.Query.Queries
{
    using InsuranceQuoter.Application.Query.Results;

    public abstract class Query<TResult>
        where TResult : QueryResult
    {
    }
}