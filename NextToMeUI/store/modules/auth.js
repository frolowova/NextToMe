import AuthController from "@/api/AuthController";
import {
  AUTH_SIGNUP,
  LOGIN_SUCCESS,
  AUTH_LOGIN,
  AUTH_LOGOUT,
  SIGNUP_CONFIRM
} from "../actions/auth";

const state = () => ({
  username: null
});

const mutations = {
  [LOGIN_SUCCESS](state, data) {
    localStorage.setItem("accessToken", data.accessToken);
    localStorage.setItem("refreshToken", data.refreshToken);
    state.username = data.username;
  },
  [AUTH_LOGOUT]() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
  }
};

const actions = {
  [AUTH_LOGIN]: async ({ commit }, user_info) => {
    const authResponse = await AuthController.login(user_info);
    commit(LOGIN_SUCCESS, authResponse);
  },
  [AUTH_SIGNUP]: async ({ commit }, { name, email: login }) => {
    const signupStatus = await AuthController.signup({
      login,
      redirectUrl: "http://localhost:3000/NextToMeUI/dist/confirm"
    });
    return signupStatus;
  },
  [AUTH_LOGOUT]: async ({ commit }) => {
    return commit(AUTH_LOGOUT);
  },
  [SIGNUP_CONFIRM]: async ({ commit }, user_info) => {
    const confirmStatus = await AuthController.confirm(user_info);
    return confirmStatus;
  }
};

const getters = {};

export default {
  state,
  getters,
  actions,
  mutations
};
