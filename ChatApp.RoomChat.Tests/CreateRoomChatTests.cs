using ChatApp.Application.Usecases.V1.Commands.RoomChat;
using ChatApp.Domain.Abstractions;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Moq;
using static ChatApp.Contract.Services.V1.RoomChat.Command;

namespace ChatApp.RoomChat.Tests
{
    public class CreateRoomChatTests
    {
        //private readonly Mock<IRepositoryBase<Domain.Entities.RoomChat, Guid>> _mockRoomChatRepository;
        //private readonly Mock<IRepositoryBase<ConversationParticipant, Guid>> _mockConversationParticipantRepository;
        //private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        //private readonly Mock<UserManager<AppUser>> _mockUserManager;

        //public CreateRoomChatTests()
        //{
        //    _mockRoomChatRepository = new Mock<IRepositoryBase<Domain.Entities.RoomChat, Guid>>();
        //    _mockConversationParticipantRepository = new Mock<IRepositoryBase<ConversationParticipant, Guid>>();
        //    _mockUnitOfWork = new Mock<IUnitOfWork>();
        //    _mockUserManager = MockUserManager<AppUser>();
        //}
        //private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        //{
        //    var store = new Mock<IUserStore<TUser>>();
        //    var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        //    mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        //    mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
        //    return mgr;
        //}
        //[Fact]
        //public async Task ValidRequest_CreatesRoomChatGroup()
        //{
        //    // Arrange
        //    var handler = new CreateRoomChatCommandHandler(
        //        _mockRoomChatRepository.Object,
        //        _mockUnitOfWork.Object,
        //    );

        //    var command = new CreateRoomChatCommand
        //    {
        //        IsGroup = true,
        //        Name = "Test Group",
        //        Members = new List<Member>
        //        {
        //           new Member(Guid.Parse("431A8A45-CA9A-4345-68FF-08DCC7FFA669"),"User1",""),
        //           new Member(Guid.Parse("BE2EEE61-78EE-406A-4776-08DCCA07C35F"),"User2",""),
        //           new Member(Guid.Parse("72D787C2-56C0-4182-570D-08DCCA127351"),"User3","")
        //        }
        //    };

        //    _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
        //        .ReturnsAsync(new AppUser());

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    _mockRoomChatRepository.Verify(x => x.Add(It.IsAny<Domain.Entities.RoomChat>()), Times.Once);
        //    _mockConversationParticipantRepository.Verify(x => x.Add(It.IsAny<ConversationParticipant>()), Times.Exactly(3));
        //    _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}
        //[Fact]
        //public async Task ValidRequest_CreatesRoomChatPrivate()
        //{
        //    // Arrange
        //    var handler = new CreateRoomChatCommandHandler(
        //        _mockRoomChatRepository.Object,
        //        _mockUnitOfWork.Object,
        //        _mockConversationParticipantRepository.Object,
        //        _mockUserManager.Object
        //    );

        //    var command = new CreateRoomChatCommand
        //    {
        //        IsGroup = false,
        //        Name = "Test Group",
        //        Members = new List<Member>
        //        {
        //           new Member(Guid.Parse("431A8A45-CA9A-4345-68FF-08DCC7FFA669"),"User1",""),
        //           new Member(Guid.Parse("BE2EEE61-78EE-406A-4776-08DCCA07C35F"),"User2","")
        //        }
        //    };

        //    _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
        //        .ReturnsAsync(new AppUser());

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    _mockRoomChatRepository.Verify(x => x.Add(It.IsAny<Domain.Entities.RoomChat>()), Times.Once);
        //    _mockConversationParticipantRepository.Verify(x => x.Add(It.IsAny<ConversationParticipant>()), Times.Exactly(2));
        //    _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}
        //[Fact]
        //public async Task InvalidRequest_LessThanTwoMembers()
        //{
        //    // Arrange
        //    var handler = new CreateRoomChatCommandHandler(
        //        _mockRoomChatRepository.Object,
        //        _mockUnitOfWork.Object
        //    );

        //    var command = new CreateRoomChatCommand
        //    {
        //        IsGroup = false,
        //        Members = new List<Member>
        //        {
        //           new Member(Guid.NewGuid(),"User1",""),
        //        }
        //    };

        //    // Act & Assert
        //    await Assert.ThrowsAsync<RoomChatException.RoomChatMustHaveAtLeastTwoMembers>(
        //        () => handler.Handle(command, CancellationToken.None)
        //    );
        //}

        //[Fact]
        //public async Task InvalidRequest_GroupChatWithLessThanThreeMembers()
        //{
        //    // Arrange
        //    var handler = new CreateRoomChatCommandHandler(
        //        _mockRoomChatRepository.Object,
        //        _mockUnitOfWork.Object,
        //    );

        //    var command = new CreateRoomChatCommand
        //    {
        //        IsGroup = true,
        //        Members = new List<Member>
        //        {
        //            new Member(Guid.NewGuid(),"User1",""),
        //            new Member(Guid.NewGuid(),"User2","")
        //        }
        //    };

        //    // Act & Assert
        //    await Assert.ThrowsAsync<RoomChatException.GroupChatMustHaveAtLeastThreeMembers>(
        //        () => handler.Handle(command, CancellationToken.None)
        //    );
        //}

        //[Fact]
        //public async Task InvalidRequest_InvalidMemberId()
        //{
        //    // Arrange
        //    var handler = new CreateRoomChatCommandHandler(
        //        _mockRoomChatRepository.Object,
        //        _mockUnitOfWork.Object,
        //        _mockConversationParticipantRepository.Object,
        //        _mockUserManager.Object
        //    );

        //    var command = new CreateRoomChatCommand
        //    {
        //        IsGroup = false,
        //        Members = new List<Member>
        //        {
        //            new Member(Guid.Parse("431A8A45-CA9A-4345-68FF-08DCC7FFA622"),"User1",""),
        //            new Member(Guid.NewGuid(),"User2","")
        //        }
        //    };

        //    _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
        //        .ReturnsAsync((AppUser)null);

        //    // Act & Assert
        //    await Assert.ThrowsAsync<RoomChatException.MemberIdIsInvalid>(
        //        () => handler.Handle(command, CancellationToken.None)
        //    );
        //}
    }
}