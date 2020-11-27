import axiosModule from "./axiousModule";

export default class APIController {
  constructor() {
    this.axios = axiosModule;
    this.updateAuthHeader(localStorage.getItem('accessToken'))
  }

  async request(method, url, data = {}) {
    const response = await this.axios[method](url, data);
    if (response.status != 200) {
      throw new Error(response.status);
    }
    return response;
  }

  updateAuthHeader(accessToken) {
    this.axios.defaults.headers["Authorization"] = `Bearer ${accessToken}`;
  }
}
