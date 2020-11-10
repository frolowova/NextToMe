using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NextToMe.API.Controllers;
using NextToMe.Common.DTOs;
using NextToMe.Common.Models;
using NextToMe.Tests.Helpers;
using NUnit.Framework;

namespace NextToMe.Tests
{
    public class Tests : TestBase
    {
        private const string _defaultMessageText = "Test Text";
        private const string _secondMessageText = "Test Text 2";

        [Test]
        public async Task MessageAndUserLocationAreEqual()
        {
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = new Location(0, 0)
            });

            List<MessageResponse> messages = await controller.GetMessages(currentLocation: new Location(0, 0));
            Assert.AreEqual(1, messages.Count);
        }

        [Test]
        public async Task MessageLocationMoreThanGettingMessageRadius()
        {
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                Location = new Location(0, 0.005)
            });

            List<MessageResponse> messages = await controller.GetMessages(currentLocation: new Location(0, 0));
            Assert.AreEqual(0, messages.Count);
        }


        [Test]
        public async Task OneMessageTest()
        {
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText
            });
            List<MessageResponse> messages = await controller.GetMessages();
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual(_defaultMessageText, messages[0].Text);
            Assert.AreEqual(TestUserName, messages[0].From);
        }

        [Test]
        public async Task EmptyMessageCollectionTest()
        {
            MessagesController controller = GetMessagesController();
            List<MessageResponse> messages = await controller.GetMessages();
            Assert.IsEmpty(messages);
        }

        [Test]
        public async Task MessagesOrderTest()
        {
            int messagesCount = 10;
            MessagesController controller = GetMessagesController();
            await SendMessagesWithNumbers(controller, messagesCount);

            List<MessageResponse> messages = await controller.GetMessages();
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

            var messages = await controller.GetMessages(skip: messagesToSkip);
            Assert.AreEqual(messagesCount - messagesToSkip, messages.Count);
        }

        [Test]
        public async Task TakeTest()
        {
            int messagesCount = 10;
            int messagesToTake = 4;
            MessagesController controller = GetMessagesController();
            await SendMessagesWithNumbers(controller, messagesCount);

            List<MessageResponse> messages = await controller.GetMessages(take: messagesToTake);
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

            List<MessageResponse> messages = await controller.GetMessages(messagesToSkip, messagesToTake);
            Assert.AreEqual(messagesToTake, messages.Count);
            for (int i = 0; i < messagesToTake; ++i)
            { 
                Assert.AreEqual((messagesToSkip + i).ToString(), messages[i].Text);
            }
        }

        [Test]
        public async Task DeleteMessageTest()
        {
            var deleteDate = DateTime.UtcNow.AddSeconds(DeleteMessageDelayInSeconds);
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                DeleteAt = deleteDate
            });
            await Task.Delay(TimeSpan.FromSeconds(DeleteMessageDelayInSeconds * 2));
            List<MessageResponse> messages = await controller.GetMessages();
            Assert.IsEmpty(messages);
        }

        [Test]
        public async Task DeleteOneOfTwoMessagesTest()
        {
            var deleteDate = DateTime.UtcNow.AddSeconds(DeleteMessageDelayInSeconds);
            MessagesController controller = GetMessagesController();
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _defaultMessageText,
                DeleteAt = deleteDate
            });
            await controller.SendMessage(new AddMessageRequest
            {
                Text = _secondMessageText
            });
            await Task.Delay(TimeSpan.FromSeconds(DeleteMessageDelayInSeconds * 2));
            List<MessageResponse> messages = await controller.GetMessages();
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual(_secondMessageText, messages[0].Text);
        }

        private async Task SendMessagesWithNumbers(MessagesController controller, int count)
        {
            for (var i = 0; i < count; ++i)
            {
                await controller.SendMessage(new AddMessageRequest
                {
                    Text = i.ToString()
                });
            }
        }
    }
}