import APIController from "./ApiController";

class MessageController extends APIController {
  constructor() {
    super();
  }

  /**  
    @param {Object} message_info:  
      @param {string} text - текст сообщения
      @param {Array} images - пока не добавлено
  */
  async sendMessage(message_info) {
    const location = await this.getLocationInfo();
    message_info = { ...message_info, ...location };
    const createdMessage = await this.request(
      "post",
      "/messages/send",
      message_info
    );
    return createdMessage;
  }

  /**  
    @param {Number} skip - сколько пропустить сообщение  
    @param {Number} take - сколько взять сообщений
    @param {Number} gettingMessagesRadiusInMeters - радиус
  */
  async getMessages(skip = 0, take = 10, gettingMessagesRadiusInMeters = 0) {
    const { location } = await this.getLocationInfo();
    const location_params = {
      currentLocation: {
        latitude: location.latitude,
        longitude: location.longitude
      },
      skip,
      take,
      gettingMessagesRadiusInMeters
    };
    const messages = await this.request(
      "post",
      "/messages/get",
      location_params
    );
    return messages;
  }

  /**  
    @param {String} message_id
  */
  async likeMessage(message_id) {
    const likeStatus = this.request(
      "post",
      `/messages/like?messageId=${message_id}`
    );
    return likeStatus;
  }

  /**  
    @param {String} message_id
  */
  async unlikeMessage(message_id) {
    const unLikeStatus = this.request(
      "post",
      `/messages/like/remove?messageId=${message_id}`
    );
    return unLikeStatus;
  }
}

export default new MessageController();
