import axios from "axios";
import axiosModule from "./axiousModule";
const API_KEY_GEO = "45ccaf60-7908-468d-adde-66b6dca72de5";

export default class APIController {
  constructor() {
    this.axios = axiosModule;
    this.updateAuthHeader(localStorage.getItem("accessToken"));
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

  async getLocationInfo() {
    if (!navigator.geolocation) {
      throw new Error("У вас не разрешен доступ к геолокации!");
    } else {
      const {
        coords: { latitude, longitude }
      } = await this._getPosition();
      const location = await axios.get(
        `https://geocode-maps.yandex.ru/1.x/?apikey=${API_KEY_GEO}&format=json&geocode=${longitude},${latitude}&kind=metro&results=1`
      );
      return {
        location: {
          latitude,
          longitude
        },
        place:
          location.data.response.GeoObjectCollection.featureMember[0].GeoObject
            .name
      };
    }
  }

  _getPosition(options = {}) {
    return new Promise((resolve, reject, options) => {
      navigator.geolocation.getCurrentPosition(resolve, reject, options);
    });
  }
}
