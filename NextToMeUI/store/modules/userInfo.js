import UserController from "@/api/UserController";
import { SEND_USER_INFO, GET_USER_INFO, GET_USER_ID} from "../actions/userInfo";
  
  const state = () => ({
    id: localStorage.getItem('nextId') || null,
    userInfo: {
      image: " ",
      userName: " ",
    }
  });
  
  const mutations = {
    [GET_USER_ID](state, id) {
      state.id = id;
    },
    [GET_USER_INFO](state, userInfO) {
      state.userInfo = userInfO;
    }

    // [CHANGE_THEME](state, theme) {
    //   localStorage.setItem('dark');

    // },
  };
  
  const actions = {
    [GET_USER_ID]: async({commit, state}) => {
      const id = localStorage.getItem("nextId");
      state.id = id;
      commit(GET_USER_ID, id);
    },


    [GET_USER_INFO]: async ({ commit, state}) => {
      const userInfo = await UserController.getUserInfo(state.id);
      commit(GET_USER_INFO , userInfo);
      console.log(userInfo);
      return userInfo;
      
    },
    // [SEND_USER_INFO]: async ({ commit }, user_info ) => {
    //   const newUserInfo = await UserController.sendUserInfo(user_info);
    //   return newUserInfo ;
    // }
  };
  
  const getters = {
    // userName: state => {
    //   return state.userInfo.userName;
    // }
  };
  
  export default {
    state,
    getters,
    actions,
    mutations
  };