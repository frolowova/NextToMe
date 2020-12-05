import UserController from "@/api/UserController";
import { SEND_USER_INFO, GET_USER_INFO, GET_USER_ID, CHANGE_THEME} from "../actions/userInfo";
import {AUTH_LOGOUT,} from "../actions/auth";
  
  const state = () => ({
    id: localStorage.getItem('nextId') || null,
    userInfo:{
      userName: "",
      imageBase64: "",
    },
    darkTheme: localStorage.getItem('isDark') || false,
  });
  
  const mutations = {
    [GET_USER_ID](state, id) {
      state.id = id;
    },
    [GET_USER_INFO](state, userInfo) {
      state.userInfo = userInfo;
    },
    [SEND_USER_INFO](state, updatedUserInfo) {
      state.userInfo = updatedUserInfo;
    },
     
    [CHANGE_THEME](state, darkTheme){
      localStorage.setItem('isDark', darkTheme);
      state.darkTheme = darkTheme;
    },
    [AUTH_LOGOUT]() {
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
      localStorage.removeItem("nextId");
    }
  };
  
  const actions = {
    [GET_USER_ID]: async({commit, state}) => {
      const id = localStorage.getItem("nextId");
      state.id = id;
      commit(GET_USER_ID, id);
      console.log(id);
    },
    [GET_USER_INFO]: async ({ commit, state}) => {
      const userInfo = await UserController.getUserInfo([state.id]);
      commit(GET_USER_INFO, userInfo);
      return userInfo;
      
    },
    [SEND_USER_INFO]: async ({ commit }, user_info ) => {
      const updatedUserInfo = await UserController.sendUserInfo(user_info);
      return updatedUserInfo ;
    },
    [AUTH_LOGOUT]: async ({ commit }) => {
      return commit(AUTH_LOGOUT);
    }
    
  };
  
  const getters = {
    userId: state => {
      return state.id;
    },
    userInfo: state => {
      return state.userInfo;
    }
  };
  
  export default {
    state,
    getters,
    actions,
    mutations
  };