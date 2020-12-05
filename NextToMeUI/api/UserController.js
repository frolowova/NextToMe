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
    const updatedUser = this.request(
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
    const userInfo = this.request("post", "/user/information/get", users_id);
    return userInfo;
  }
}

export default new UserController();
