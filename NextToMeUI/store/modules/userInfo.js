import UserController from "@/api/UserController";
import {
  SEND_USER_INFO,
  GET_USER_INFO,
  SET_AUTH_DATA
} from "../actions/userInfo";
import { AUTH_LOGOUT } from "../actions/auth";

const state = () => ({
  id: localStorage.getItem("nextId") || null,
  userInfo: {
    userName: "",
    imageBase64: "",
    userId: ""
  },

  email: localStorage.getItem("login"),
  darkTheme: JSON.parse(localStorage.getItem("isDark") || false)
});

const mutations = {
  [GET_USER_INFO](state, userInfo) {
    state.userInfo = userInfo;
  },
  [SEND_USER_INFO](state, updUserInfo) {
    console.log(updUserInfo);
    state.userInfo = updUserInfo;
  },

  [AUTH_LOGOUT]() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("nextId");
  },
  [SET_AUTH_DATA](state, { id, login }) {
    state.id = id;
    state.login = login;
  }
};

const actions = {
  [GET_USER_INFO]: async ({ commit, state }) => {
    const userInfo = await UserController.getUserInfo([state.id]);
    commit("GET_USER_INFO", userInfo.data[0]);
  },

  [SEND_USER_INFO]: async ({ commit }, user_info) => {
    const updUserInfo = await UserController.sendUserInfo(user_info);
    return updUserInfo;
  },

  [AUTH_LOGOUT]: async ({ commit }) => {
    return commit(AUTH_LOGOUT);
  },
  [SET_AUTH_DATA]: ({ commit }, id) => {
    commit(SET_AUTH_DATA, id);
  }
};

const getters = {
  userId: state => {
    return state.id;
  },
  userName: state => {
    return state.userInfo.userName;
  },
  imageBase64: state => {
    return state.userInfo.imageBase64;
  },
  darkTheme: state => {
    return state.darkTheme;
  },
  email: state => {
    return state.email;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
