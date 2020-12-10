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
  async getMessages(skip = 0, take = 10, gettingMessagesRadiusInMeters = 1000) {
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
    @param {Number} skip - сколько пропустить сообщение  
    @param {Number} take - сколько взять сообщений
  */
  async getTopMessages(skip = 0, take = 10) {
    const top_params = {
      skip,
      take
    };
    const messages = await this.request("post", "/messages/top", top_params);
    return messages;
  }

  /**  
    @param {String} message_id
  */
  async likeMessage(message_id) {
    const likeStatus = await this.request(
      "post",
      `/messages/like?messageId=${message_id}`
    );
    return likeStatus;
  }

  /**  
    @param {String} message_id
  */
  async unlikeMessage(message_id) {
    const unLikeStatus = await this.request(
      "post",
      `/messages/like/remove?messageId=${message_id}`
    );
    return unLikeStatus;
  }

  /** 
  @param {String} image_id
  */
  async getImage(image_id) {
    const images = await this.request(
      "post",
      `/messages/image/get?messageImageId=${image_id}`
    );
    return images;
  }

  async getIDsMessages() {
    const ids = await this.request("post", "/messages/get/from/user");
    return ids;
  }

  /** 
  @param {Array} messages_ids
  */
  async getUserMessages(messages_ids) {
    const messages = await this.request(
      "post",
      "/messages/get/from/user",
      messages_ids
    );
    return messages;
  }

  /** 
  @param {String} message_id
  */
  async updateViews(message_id) {
    const viewStatus = await this.request("post", "/messages/views", [
      message_id
    ]);
    return viewStatus;
  }
}

export default new MessageController();
