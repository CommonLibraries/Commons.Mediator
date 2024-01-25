using Commons.Mediator.Requests;

namespace Commons.Mediator.CQS.Queries;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IRequest<TResult>
{

}
