using NextToMe.API.Controllers;
using NextToMe.Common.DTOs;
using NextToMe.Common.Models;
using NextToMe.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextToMe.Tests
{
    public class MessageCommentsTests : TestBase
    {
        private const string _defaultMessageText = "Test MessageText";
        private const string _defaultCommentsText = "Test CommentText";
        private const string _secondCommentsText = "Test CommentText";
        private readonly Location _zeroLocation = new Location(0, 0);

        [Test]
        public async Task AddOneCommentToOneMessage()
        {
            MessagesController messagesController = GetMessagesController();
            MessageCommentsController messageCommentsController = GetMessageCommentsController();

            await messagesController.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation
            });
            List<MessageResponse> messages = await messagesController.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            await messageCommentsController.SendComment(new AddMessageCommentRequest
            {
                Text = _defaultCommentsText,
                MessageId = messages[0].Id
            });

            List<MessageCommentResponse> comments = await messageCommentsController.GetComments(messages[0].Id);
            Assert.AreEqual(1, comments.Count);
            Assert.AreEqual(_defaultCommentsText, comments[0].Text);
            Assert.AreEqual(TestUserName, comments[0].From);
        }

        [Test]
        public async Task MessageCommentsOrderTest()
        {
            int messageCommentsCount = 10;
            MessagesController messagesController = GetMessagesController();
            MessageCommentsController messageCommentsController = GetMessageCommentsController();

            await messagesController.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation
            });
            List<MessageResponse> messages = await messagesController.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            await SendCommentsWithNumbers(messageCommentsController, messageCommentsCount, messages[0].Id);
            List<MessageCommentResponse> comments = await messageCommentsController.GetComments(messages[0].Id);

            Assert.AreEqual(messageCommentsCount, comments.Count);
            for (int i = 0; i < messageCommentsCount; i++)
            {
                Assert.AreEqual(i.ToString(), comments[i].Text);
            }
        }

        [Test]
        public async Task AddCommentToAnotherComment()
        {
            MessagesController messagesController = GetMessagesController();
            MessageCommentsController messageCommentsController = GetMessageCommentsController();

            await messagesController.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation
            });
            List<MessageResponse> messages = await messagesController.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            await messageCommentsController.SendComment(new AddMessageCommentRequest
            {
                Text = _defaultCommentsText,
                MessageId = messages[0].Id
            });
            List<MessageCommentResponse> comments = await messageCommentsController.GetComments(messages[0].Id);
            await messageCommentsController.SendComment(new AddMessageCommentRequest
            {
                Text = _defaultCommentsText,
                MessageId = comments[0].MessageId
            });
            comments = await messageCommentsController.GetComments(messages[0].Id);

            Assert.AreEqual(2, comments.Count);
            Assert.AreEqual(_defaultCommentsText, comments[0].Text);
            Assert.AreEqual(_secondCommentsText, comments[1].Text);
        }

        //[Test]
        //public async Task EmptyCommentsCollectionFromDeletedMessage()
        //{
        //    var deleteDate = DateTime.UtcNow.AddSeconds(DeleteMessageDelayInSeconds);
        //    MessagesController messagesController = GetMessagesController();
        //    MessageCommentsController messageCommentsController = GetMessageCommentsController();
        //    await messagesController.SendMessage(new AddMessageRequest
        //    {
        //        Text = _defaultMessageText,
        //        Location = _zeroLocation,
        //        DeleteAt = deleteDate
        //    });
        //    List<MessageResponse> messages = await messagesController.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
        //    string messageId = messages[0].Id;
        //    await messageCommentsController.SendComment(new AddMessageCommentRequest
        //    {
        //        Text = _defaultCommentsText,
        //        MessageId = messageId
        //    });
        //    await Task.Delay(TimeSpan.FromSeconds(DeleteMessageDelayInSeconds * 2));
        //    messages = await messagesController.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation }); // Empty
        //    List<MessageCommentResponse> comments = await messageCommentsController.GetComments(messageId); // must throw exception because messageID not exist
        //    Assert.Throws<BadRequestException>(async () => await messageCommentsController.GetComments(messageId));
        //}

        private static async Task SendCommentsWithNumbers(MessageCommentsController controller, int count, Guid messageId)
        {
            for (var i = 0; i < count; ++i)
            {
                await controller.SendComment(new AddMessageCommentRequest
                {
                    Text = i.ToString(),
                    MessageId = messageId
                });
            }
        }
    }
}
