using Commons.Mediator.Requests;

namespace Commons.Mediator.CQS.Commands;

public interface ICommandHandler<TRequest>: IRequestHandler<TRequest>
    where TRequest : IRequest
{

}

public interface ICommandHandler<TRequest, TResult>: IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{

}
