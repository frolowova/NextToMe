import axios from "axios";
export const BASE_URL = "https://nexttomeapi.azurewebsites.net/api";

const axiosModule = axios.create({
  baseURL: BASE_URL,
  responseType: "json"
});

axiosModule.interceptors.response.use(
  response => response,
  error => {
    if (
      error.response.status !== 401 ||
      !error.response.headers["Token-Expired"] === true
    ) {
      return Promise.reject(error);
    }
    const tokens = {
      accessToken: localStorage.getItem("accessToken"),
      refreshToken: localStorage.getItem("refreshToken")
    };
    return axios
      .post(`${BASE_URL}/auth/refresh`, tokens)
      .then(response => {
        localStorage.setItem("accessToken", response.data.accessToken);
        localStorage.setItem("refreshToken", response.data.refreshToken);
        error.response.config.headers[
          "Authorization"
        ] = `Bearer ${response.data.accessToken}`;
        return axios(error.response.config);
      })
      .catch(error => {
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        return Promise.reject(error);
      });
  }
);

export default axiosModule;
