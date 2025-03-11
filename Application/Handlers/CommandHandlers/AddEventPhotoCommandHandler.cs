using Application.Commands.EventCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class AddEventPhotoCommandHandler : IRequestHandler<AddEventImageCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public AddEventPhotoCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(AddEventImageCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.EventRepository.GetById(request.EventId);
        
        if(eventEntity == null) throw new Exception($"Event with id: {request.EventId} not found");
        
        if (request.ImageDto.Image == null)
            throw new Exception("No photo file provided");
        
        var photoPath = await _fileStorageService.SaveFile(request.ImageDto.Image);
        
        eventEntity.SetImageUrl(photoPath);
        
        await _unitOfWork.EventRepository.UpdateAsync(eventEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return photoPath;
    }
}