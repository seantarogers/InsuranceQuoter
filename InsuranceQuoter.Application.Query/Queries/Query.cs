namespace InsuranceQuoter.Application.Query.Queries
{
    using InsuranceQuoter.Application.Query.Results;

    public abstract record Query<TResult>
        where TResult : QueryResult;
}