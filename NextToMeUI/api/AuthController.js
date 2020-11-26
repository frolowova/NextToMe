import APIController from "./ApiController";

class AuthController extends APIController {
  constructor() {
    super();
  }

  async login(user_info) {
    const loginResponse = await this.request("post", "/auth/login", user_info);
    this.updateAuthHeader(loginResponse.data.accessToken);
    const { data } = loginResponse;
    return data;
  }

  async signup(user_info) {
    const signupResponse = await this.request(
      "post",
      "/auth/register",
      user_info
    );
    return signupResponse;
  }

  async confirm(user_info) {
    const confirmResponse = await this.request(
      "post",
      "/auth/confirm",
      user_info
    );
    return confirmResponse;
  }

  async resetPassword(user_info) {
    const resetStatus = await this.request(
      "post",
      "/auth/password/reset",
      user_info
    );
    return resetStatus;
  }

  async setNewPassword(user_info) {
    const setStatus = await this.request(
      "post",
      "/auth/password/set",
      user_info
    );
    return setStatus;
  }
}

export default new AuthController();
