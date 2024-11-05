using WebBlog.Domain.Entities;
using WebBlog.Domain.Enums;

namespace WebBlog.Application.UnitTests.Users;

public class UserTests
{
    [Fact]
    public void User_DefaultConstructor_SetsDefaultValues()
    {
        // Arrange & Act
        var user = new User();

        // Assert
        Assert.Equal(string.Empty, user.Fullname);
        Assert.Null(user.Describe);
        Assert.Null(user.Email);
        Assert.Null(user.Password);
        Assert.Null(user.Avatar);
        Assert.Equal(Status.Inactive, user.IsActive);
        Assert.NotNull(user.Comments);
        Assert.NotNull(user.FollowerFollowerUsers);
        Assert.NotNull(user.FollowerUsers);
        Assert.NotNull(user.Messages);
        Assert.NotNull(user.Posts);
        Assert.NotNull(user.Tokens);
        Assert.NotNull(user.Votes);
    }

    [Fact]
    public void User_SetProperties_ReturnsExpectedValues()
    {
        // Arrange
        var user = new User
        {
            Fullname = "John Doe",
            Describe = "A regular user",
            Email = "john.doe@example.com",
            Password = "password123",
            Avatar = "avatar.png",
            IsActive = Status.Active
        };

        // Act & Assert
        Assert.Equal("John Doe", user.Fullname);
        Assert.Equal("A regular user", user.Describe);
        Assert.Equal("john.doe@example.com", user.Email);
        Assert.Equal("password123", user.Password);
        Assert.Equal("avatar.png", user.Avatar);
        Assert.Equal(Status.Active, user.IsActive);
    }

    [Fact]
    public void User_AddComment_AddsCommentToList()
    {
        // Arrange
        var user = new User();
        var comment = new Comment();

        // Act
        user.Comments.Add(comment);

        // Assert
        Assert.Contains(comment, user.Comments);
    }

    [Fact]
    public void User_AddFollower_AddsFollowerToList()
    {
        // Arrange
        var user = new User();
        var follower = new Follower();

        // Act
        user.FollowerFollowerUsers.Add(follower);

        // Assert
        Assert.Contains(follower, user.FollowerFollowerUsers);
    }

    [Fact]
    public void User_AddMessage_AddsMessageToList()
    {
        // Arrange
        var user = new User();
        var message = new Message();

        // Act
        user.Messages.Add(message);

        // Assert
        Assert.Contains(message, user.Messages);
    }

    [Fact]
    public void User_AddPost_AddsPostToList()
    {
        // Arrange
        var user = new User();
        var post = new Post();

        // Act
        user.Posts.Add(post);

        // Assert
        Assert.Contains(post, user.Posts);
    }

    [Fact]
    public void User_AddToken_AddsTokenToList()
    {
        // Arrange
        var user = new User();
        var token = new Token();

        // Act
        user.Tokens.Add(token);

        // Assert
        Assert.Contains(token, user.Tokens);
    }

    [Fact]
    public void User_AddVote_AddsVoteToList()
    {
        // Arrange
        var user = new User();
        var vote = new Vote();

        // Act
        user.Votes.Add(vote);

        // Assert
        Assert.Contains(vote, user.Votes);
    }
}