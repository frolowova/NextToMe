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
      !error.response.headers["token-expired"]
    ) {
      return Promise.reject(error.response.status);
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
        axiosModule.defaults.headers["Authorization"] = `Bearer ${accessToken}`;
        error.response.config.headers[
          "Authorization"
        ] = `Bearer ${response.data.accessToken}`;
        return axios(error.response.config);
      })
      .catch(error => {
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        delete axiosModule.defaults.headers["Authorization"];
        return Promise.reject(error.response.status);
      });
  }
);

export default axiosModule;
