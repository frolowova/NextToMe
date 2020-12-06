import UserController from "@/api/UserController";
import { SEND_USER_INFO, GET_USER_INFO, GET_USER_ID, CHANGE_THEME} from "../actions/userInfo";
import {AUTH_LOGOUT,} from "../actions/auth";
  
  const state = () => ({
    id: localStorage.getItem('nextId') || null,
    userInfo:{
      userName: "",
      imageBase64: "",
      userId: "",
    },
    // email: localStorage.getItem('firstSeenSlider'),
    darkTheme: JSON.parse(localStorage.getItem('isDark') || false),
  });
  
  const mutations = {

    [GET_USER_INFO](state, userInfo) {
      state.userInfo = userInfo;
    },
    [SEND_USER_INFO](state, updatedUser) {
      state.userInfo = updatedUser;
    },

    [AUTH_LOGOUT]() {
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
      localStorage.removeItem("nextId");
    }
  };
  
  const actions = {

    [GET_USER_INFO]: async ({ commit, state}) => {
      const userInfo = await UserController.getUserInfo([state.id]);
      commit(GET_USER_INFO, userInfo.data[0]);
      console.log(userInfo.data[0]);
    },

    [SEND_USER_INFO]: async ({ commit }, userName) => {
      console.log(userName)
      const updatedUser = await UserController.sendUserInfo(userName);
      commit(SEND_USER_INFO, updatedUser);
      console.log(updatedUser);
    },

    [AUTH_LOGOUT]: async ({ commit }) => {
      return commit(AUTH_LOGOUT);
    }
  };
  
  const getters = {
    userId: state => {
      return state.id;
    },
    userName: state => {
      return state.userInfo.userName;
    },
    darkTheme: state => {
      return state.darkTheme;
    },
    // email: state => {
    //   return state.email;
    // }
  };
  
  export default {
    state,
    getters,
    actions,
    mutations
  };


  // mounted() {
  //   setTimeout(() => {
  //   this.$vuetify.theme.dark = this.$store.getters.darkTheme
  //   }, 0);
  // },