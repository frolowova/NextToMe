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
import { SET_USER_ID } from "@/store/actions/userInfo";
const state = () => ({
  username: null,
  login: localStorage.getItem("login") || null
});

const mutations = {
  [LOGIN_SUCCESS](state, { response, login }) {
    localStorage.setItem("accessToken", response.accessToken);
    localStorage.setItem("refreshToken", response.refreshToken);
    localStorage.setItem("nextId", response.id);
    localStorage.setItem("login", login);
    state.username = response.username;
  },
  [AUTH_LOGOUT]() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("nextId");
    localStorage.removeItem("login");
  }
};

const actions = {
  [AUTH_LOGIN]: async ({ commit, dispatch }, user_info) => {
    const authResponse = await AuthController.login(user_info);
    commit(LOGIN_SUCCESS, { response: authResponse, login: user_info.login });
    dispatch(SET_USER_ID, authResponse.id, { root: true });
    return authResponse;
  },
  [AUTH_SIGNUP]: async ({ commit }, { name, email: login }) => {
    const signupStatus = await AuthController.signup({
      login,
      redirectUrl: "http://nexttome.ru/auth-confirm"
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
  [RESET_PASSWORD_ATTEMPT]: async ({ commit }, { login }) => {
    const resetStatus = await AuthController.resetPassword({
      login,
      redirectUrl: "http://nexttome.ru/reset-confirm"
    });
    return resetStatus;
  },
  [RESET_PASSWORD_CONFIRM]: async ({ commit }, { code, password, userId }) => {
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
