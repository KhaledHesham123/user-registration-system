using MediatR;

namespace User_Registration_System.Shared.Interfaces
{
    public interface ICommand<out TResponse>:IRequest<TResponse>;
    
    
}
