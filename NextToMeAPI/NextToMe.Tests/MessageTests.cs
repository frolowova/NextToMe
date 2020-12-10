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
            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
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
        public async Task GetIdsOfUserMessages()
        {
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation
            });
            List<Guid> messagesGuid = await controller.GetIdsOfUserMessages();
            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            Assert.AreEqual(1, messagesGuid.Count);
            Assert.AreEqual(_defaultMessageText, messages.First(x => x.Id == messagesGuid[0]).Text);
        }

        [Test]
        public async Task GetMessagesFromIds()
        {
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation
            });
            List<Guid> messagesGuid = await controller.GetIdsOfUserMessages();
            List<MessageResponse> messages = await controller.GetMessagesFromId(messagesGuid);
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual(_defaultMessageText, messages[0].Text);
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
        public async Task SkipTest()
        {
            int messagesCount = 10;
            int messagesToSkip = 3;
            MessagesController controller = GetMessagesController();
            await SendMessagesWithNumbers(controller, messagesCount);

            var messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation, Skip = messagesToSkip });
            Assert.AreEqual(messagesCount - messagesToSkip, messages.Count);
        }

        [Test]
        public async Task TakeTest()
        {
            int messagesCount = 10;
            int messagesToTake = 4;
            MessagesController controller = GetMessagesController();
            await SendMessagesWithNumbers(controller, messagesCount);

            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation, Take = messagesToTake });
            Assert.AreEqual(messagesToTake, messages.Count);
        }

        [Test]
        public async Task SkipTakeTest()
        {
            int messagesCount = 10;
            int messagesToSkip = 4;
            int messagesToTake = 2;
            MessagesController controller = GetMessagesController();
            await SendMessagesWithNumbers(controller, messagesCount);

            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation, Skip = messagesToSkip, Take = messagesToTake });
            Assert.AreEqual(messagesToTake, messages.Count);
            for (int i = 0; i < messagesToTake; ++i)
            {
                Assert.AreEqual((messagesToSkip + i).ToString(), messages[i].Text);
            }
        }

        [Test]
        public async Task AddMessageWithPhotos()
        {
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = _zeroLocation,
                Photos = new List<string>() { _image1, _image2 }
            });
            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            string image1 = await controller.GetMessageImage(messages[0].Photos.First());
            string image2 = await controller.GetMessageImage(messages[0].Photos.Last());

            Assert.AreEqual(2, messages[0].Photos.Count());
            Assert.AreEqual(_image1, image1);
            Assert.AreEqual(_image2, image2);
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

        [Test]
        public async Task ViewsTest()
        {
            int messagesCount = 10;
            int messageToView = 4;
            MessagesController controller = GetMessagesController();
            await SendMessagesWithNumbers(controller, messagesCount);

            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            await controller.AddViewToMessage(new List<Guid> { messages[messageToView].Id });
            messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            Assert.AreEqual(1, messages[messageToView].Views);
            Assert.AreEqual(0, messages[0].Views);
        }

        [Test]
        public async Task TopViewsTest()
        {
            int messagesCount = 10;
            MessagesController controller = GetMessagesController();
            await SendMessagesWithNumbers(controller, messagesCount);

            List<MessageResponse> messages = await controller.GetMessages(new GetMessageRequest { CurrentLocation = _zeroLocation });
            var topMessageToView = messages[4].Id;
            var secondMessageToView = messages[7].Id;
            await controller.AddViewToMessage(new List<Guid> { topMessageToView });
            await controller.AddViewToMessage(new List<Guid> { topMessageToView });
            await controller.AddViewToMessage(new List<Guid> { topMessageToView, secondMessageToView });
            messages = await controller.GetTopViewedMessages(new SkipTakeMessagesRequest());
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual(messages[0].Id, topMessageToView);
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