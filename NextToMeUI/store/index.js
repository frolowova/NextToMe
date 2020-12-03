import Vuex from "vuex";
import auth from "./modules/auth";
import comments from "./modules/comments";
import messages from "./modules/messages";
import userInfo from "./modules/userInfo";

const createStore = () => {
  return new Vuex.Store({
    namespaced: true,
    modules: {
      auth,
      comments,
      messages,
      userInfo
    }
  });
};

export default createStore;
