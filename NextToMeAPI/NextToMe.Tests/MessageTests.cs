using System;
using NextToMe.API.Controllers;
using NextToMe.Common.DTOs;
using NextToMe.Common.Models;
using NextToMe.Tests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace NextToMe.Tests
{
    public class Tests : TestBase
    {
        private const string _defaultMessageText = "Test Text";
        private const string _secondMessageText = "Test Text 2";
        private const string _image1 = "base64_1";
        private const string _image2 = "base64_2";
        private readonly Location _zeroLocation = new Location(0, 0);


        [Test]
        public async Task OneMessageTest()
        {
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation
            });
            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest{ CurrentLocation = _zeroLocation });
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual(_defaultMessageText, messages[0].Text);
            Assert.AreEqual(TestUserId, messages[0].From);
        }

        [Test]
        public async Task EmptyMessageCollectionTest()
        {
            MessagesController controller = GetMessagesController();
            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            Assert.IsEmpty(messages);
        }

        [Test]
        public async Task MessagesOrderTest()
        {
            int messagesCount = 10;
            MessagesController controller = GetMessagesController();
            await SendMessagesWithNumbers(controller, messagesCount);

            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            Assert.AreEqual(messagesCount, messages.Count);
            for (var i = 0; i < messagesCount; ++i)
            {
                Assert.AreEqual(i.ToString(), messages[i].Text);
            }
        }

        [Test]
        public async Task OrderByTest()
        {
            int messagesCount = 10;
            MessagesController controller = GetMessagesController();
            MessageCommentsController commentsController = GetMessageCommentsController();
            await SendMessagesWithNumbers(controller, messagesCount);

            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            Guid topCommentsMessageId = messages[3].Id;
            Guid secondCommentsMessageId = messages[4].Id;
            await commentsController.SendComment(new AddMessageCommentRequest { MessageId = topCommentsMessageId, Text = _defaultMessageText});
            await commentsController.SendComment(new AddMessageCommentRequest { MessageId = topCommentsMessageId, Text = _defaultMessageText });
            await commentsController.SendComment(new AddMessageCommentRequest { MessageId = secondCommentsMessageId, Text = _defaultMessageText });
            messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation, Filter = new MessageFilter{ OrderBy = OrderBy.Comments, OrderType = OrderType.Desc }});
            Assert.AreEqual(topCommentsMessageId, messages[0].Id);
            Assert.AreEqual(secondCommentsMessageId, messages[1].Id);
        }
        
        [Test]
        public async Task AddMessageWithPhotos()
        {
            MessagesController controller = GetMessagesController();
            var imagesList = new List<string> {_image1, _image2};
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation,
                Photos = imagesList
            });
            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            List<Guid> photoIds = messages[0].Photos.ToList();
            Dictionary<Guid, string> images = await controller.GetMessageImages(photoIds);

            Assert.AreEqual(2, messages[0].Photos.Count());
            Assert.Contains(_image1, imagesList);
            Assert.Contains(_image2, imagesList);
            Assert.AreNotEqual(_image2, _image1);
        }

        [Test]

        public async Task LikeOneMessage()
        {
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation
            });
            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            await controller.LikeMessage(messages[0].Id);
            messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            Assert.AreEqual(1, messages[0].LikesCount);
        }

        [Test]

        public async Task LikeThenRemoveLikeFromMessage()
        {
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation
            });
            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            await controller.LikeMessage(messages[0].Id);
            await controller.RemoveLike(messages[0].Id);
            messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            Assert.AreEqual(0, messages[0].LikesCount);
        }

        private async Task SendMessagesWithNumbers(MessagesController controller, int count)
        {
            for (var i = 0; i < count; ++i)
            {
                await controller.SendMessage(new AddMessageRequest
                {
                    Text = i.ToString(),
                    Location = _zeroLocation
                });
            }
        }
    }
}