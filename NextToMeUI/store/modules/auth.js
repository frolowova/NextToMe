import AuthController from "@/api/AuthController";
import {
  AUTH_SIGNUP,
  LOGIN_SUCCESS,
  AUTH_LOGIN,
  AUTH_LOGOUT,
  SIGNUP_CONFIRM,
  RESET_PASSWORD_ATTEMPT,
  RESET_PASSWORD_CONFIRM
} from "../actions/auth";

const state = () => ({
  username: null
});

const mutations = {
  [LOGIN_SUCCESS](state, data) {
    localStorage.setItem("accessToken", data.accessToken);
    localStorage.setItem("refreshToken", data.refreshToken);
    localStorage.setItem("nextId", data.refreshToken);
    state.username = data.username;
  },
  [AUTH_LOGOUT]() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("nextId");
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
      redirectUrl: "http://localhost:3000/NextToMeUI/dist/auth-confirm"
    });
    return signupStatus;
  },
  [AUTH_LOGOUT]: async ({ commit }) => {
    return commit(AUTH_LOGOUT);
  },
  [SIGNUP_CONFIRM]: async ({ commit }, user_info) => {
    const confirmStatus = await AuthController.confirm(user_info);
    return confirmStatus;
  },
  [RESET_PASSWORD_ATTEMPT]: async ({ commit }, {login}) => {
    const resetStatus = await AuthController.resetPassword({
      login,
      redirectUrl: "http://localhost:3000/NextToMeUI/dist/reset-confirm"
    });
    return resetStatus;
  },
  [RESET_PASSWORD_CONFIRM]:  async ({ commit }, {code, password, userId}) => {
    const resetStatus = await AuthController.setNewPassword({
      code,
      NewPassword: password,
      userId
    });
    return resetStatus;
  }
};

const getters = {};

export default {
  state,
  getters,
  actions,
  mutations
};
