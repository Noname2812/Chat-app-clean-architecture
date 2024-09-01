

using MediatR;

namespace ChatApp.Application.Behaviors
{
    //public sealed class TransactionPielineBehavior<TRequest, TResponne> :
    //    IPipelineBehavior<TRequest, TResponne> where TRequest : notnull
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly ApplicationDbContext _context;
    //    public TransactionPielineBehavior(IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext)
    //    {
    //        _context = applicationDbContext;
    //        _unitOfWork = unitOfWork;
    //    }
    //    public async Task<TResponne> Handle(TRequest request, RequestHandlerDelegate<TResponne> next, CancellationToken cancellationToken)
    //    {
    //        if (!IsCommand())
    //        {
    //            return await next();
    //        }
    //    }
    //    private bool IsCommand() => typeof(TRequest).Name.EndsWith("Command");
    //}
}
