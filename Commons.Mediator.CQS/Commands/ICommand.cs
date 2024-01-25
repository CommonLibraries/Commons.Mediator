using Commons.Mediator.Requests;

namespace Commons.Mediator.CQS.Commands;

public interface ICommand: IRequest
{

}

public interface ICommand<TResult>: IRequest<TResult> {

}
