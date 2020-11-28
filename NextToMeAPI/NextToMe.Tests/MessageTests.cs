using NextToMe.API.Controllers;
using NextToMe.Common.DTOs;
using NextToMe.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NextToMe.Common.Models;

namespace NextToMe.Tests
{
    public class Tests : TestBase
    {
        private const string _defaultMessageText = "Test Text";
        private const string _secondMessageText = "Test Text 2";
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
            Assert.AreEqual(TestUserName, messages[0].From);
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