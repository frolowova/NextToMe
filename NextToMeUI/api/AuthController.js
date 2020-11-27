import APIController from "./ApiController";

class AuthController extends APIController {
  constructor() {
    super();
  }

  async login(user_info) {
    const loginResponse = await this.request("post", "/auth/login", user_info);
    this.updateAuthHeader(loginResponse.data.accessToken);
    const {data} = loginResponse
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
}

export default new AuthController();
