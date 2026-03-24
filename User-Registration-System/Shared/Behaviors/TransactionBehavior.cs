using Azure;
using MediatR;
using User_Registration_System.Shared.Interfaces;
using User_Registration_System.Shared.Respones;
using User_Registration_System.Shared.UnitOfWork;

namespace User_Registration_System.Shared.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> logger;

        public TransactionBehavior(IUnitOfWork unitOfWork, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
               await unitOfWork.BeginTransactionAsync();

                var respone = await next();

                if (respone is IRequestResponse result && !result.IsSuccess)
                {
                    await unitOfWork.RollbackTransactionAsync();
                }
                else
                {
                    await unitOfWork.CommitTransactionAsync();
                }
                return respone;

            }
            catch (Exception ex)
            {

                await unitOfWork.RollbackTransactionAsync();
                logger.LogError(ex, "Transaction failed for {Request}", typeof(TRequest).Name);

                throw;

            }
        }
    }
}
