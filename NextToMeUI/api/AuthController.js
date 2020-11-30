import APIController from "./ApiController";

class AuthController extends APIController {
  constructor() {
    super();
  }

  /**  
    @param {Object} user_info
      @param {String} login
      @param {String} password
  */
  async login(user_info) {
    const loginResponse = await this.request("post", "/auth/login", user_info);
    this.updateAuthHeader(loginResponse.data.accessToken);
    const { data } = loginResponse;
    return data;
  }

  /**  
    @param {Object} user_info
      @param {String} login
      @param {String} redirectUrl - url, с окном для ввода пароля
  */
  async signup(user_info) {
    const signupResponse = await this.request(
      "post",
      "/auth/register",
      user_info
    );
    return signupResponse;
  }

  /**  
    @param {Object} user_info
      @param {String} userId
      @param {String} code 
      @param {String} password 
  */
  async confirm(user_info) {
    const confirmResponse = await this.request(
      "post",
      "/auth/confirm",
      user_info
    );
    return confirmResponse;
  }

  /**  
    @param {Object} user_info
      @param {String} login
      @param {String} redirectUrl
  */
  async resetPassword(user_info) {
    const resetStatus = await this.request(
      "post",
      "/auth/password/reset",
      user_info
    );
    return resetStatus;
  }

  /**  
    @param {Object} user_info
      @param {String} userId
      @param {String} code
      @param {String} newPassword
  */
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
