

using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Exceptions;
using static ChatApp.Contract.Services.V1.Upload.Command;
using static ChatApp.Contract.Services.V1.Upload.Respone;

namespace ChatApp.Application.Usecases.V1.Commands.Upload
{
    public sealed class UploadFileCommandHandler : ICommandHandler<ParamUploadFileCommand, UploadFileRespone>
    {
        private readonly ICloudinaryService _cloudinaryService;
        public UploadFileCommandHandler(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        public async Task<Result<UploadFileRespone>> Handle(ParamUploadFileCommand request, CancellationToken cancellationToken)
        {
            switch (request.Type)
            {
                case TypeMessage.Audio:
                    {
                        var result = await _cloudinaryService.UploadAudio(request.File);
                        return Result.Success(new UploadFileRespone { Url = result.Url.ToString() });
                    }
                case TypeMessage.Image:
                    {
                        var result = await _cloudinaryService.UploadImage(request.File);
                        return Result.Success(new UploadFileRespone { Url = result.Url.ToString() });
                    }
                default:    throw new Exception("Type File is not supported !");
            }
        }
    }
}
