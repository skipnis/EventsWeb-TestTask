using Application.Commands.UserCommands;
using Application.Dtos;
using Application.Handlers.CommandHandlers;
using Application.Interfaces;
using Core.Enities;
using Core.Interfaces;
using MapsterMapper;
using Moq;

namespace Application.Tests;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        
        _unitOfWorkMock.Setup(uow => uow.UserRepository).Returns(_userRepositoryMock.Object);
        
        _handler = new UpdateUserCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task Handle_UserFound_UpdatesUser()
    {
        // Arrange
        CancellationToken cancellationToken = new CancellationToken();
        var userId = Guid.NewGuid();
        var user = new User { Id = userId };
        var userUpdateDto = new UserUpdateDto { Email = "test@test.com" };
        var command = new UpdateUserCommand(userId, userUpdateDto);

        _userRepositoryMock.Setup(repo => repo.GetById(userId, cancellationToken)).ReturnsAsync(user);
        _mapperMock.Setup(m => m.Map(userUpdateDto, It.IsAny<User>())).Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map(userUpdateDto, user), Times.Once);
        _userRepositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
        Assert.Equal(userId, result);
    }
    
    [Fact]
    public async Task Handle_UserNotFound_ThrowsException()
    {
        // Arrange
        CancellationToken cancellationToken = new CancellationToken();
        var userId = Guid.NewGuid();
        var userUpdateDto = new UserUpdateDto { Email = "test@test.com" };
        var command = new UpdateUserCommand(userId, userUpdateDto);

        _userRepositoryMock.Setup(repo => repo.GetById(userId, cancellationToken)).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}