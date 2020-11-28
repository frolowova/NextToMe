import APIController from "./ApiController";

class UserController extends APIController {
  constructor() {
    super();
  }

  async sendUserInfo(user_info) {
    const updatedUser = this.request(
      "post",
      "/user/information/send",
      user_info
    );
    return updatedUser;
  }

  async getUserInfo(user_id) {
    const userInfo = this.request("post", "/user/information/send", user_id);
    return userInfo;
  }
}

export default new UserController();
