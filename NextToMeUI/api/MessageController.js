import APIController from "./ApiController";

class MessageController extends APIController {
  constructor() {
    super();
  }

  /* 
    message_info: 
      text: "string",
      location: {
        latitude: 0,
        longitude: 0
      },
      place: "string"
  */
  async sendMessage(message_info) {
    const createdMessage = this.request("post", "/messages/send", message_info);
    return createdMessage;
  }

  /* 
    message_info: 
      currentLocation: {
        latitude: 0,
        longitude: 0
      },
      skip: 0,
      take: 0,
      gettingMessagesRadiusInMeters: 0
  */
  async getMessages(message_info) {
    const messages = this.request("post", "/messages/get", message_info);
    return messages;
  }
}

export default new MessageController();
