import APIController from "./ApiController";

class UserController extends APIController {
  constructor() {
    super();
  }

  /**  
    @param {Object} user_info:  
      @param {string} userName
      @param {string} imageBase64
  */
  async sendUserInfo(user_info) {
    const updatedUser = await this.request(
      "post",
      "/user/information/send",
      user_info
    );
    return updatedUser;
  }

  /**  
    @param {Array} user_id - array of ids
  */
  async getUserInfo(users_id) {
    const userInfo = await this.request("post", "/user/information/get", users_id);
    return userInfo;
  }
  /** 
   @param { String } user_id
   */
  async getMyTagsIds() {
    const myTagsIds = await this.request("post", "/messages/get/from/user");
    return myTagsIds;
  }
  /** 
   @param { String } user_id
   */
  async getMyTag(user_id) {
    const myTags = await this.request("post", "/messages/get/from/ids", [user_id]);
    return myTags.data[0];
  }


}

export default new UserController();
