import Vuex from "vuex";
import auth from "./modules/auth";
import messages from "./modules/messages";
import userInfo from "./modules/userInfo";
import currentTag from "./modules/currentTag";

const createStore = () => {
  return new Vuex.Store({
    namespaced: true,
    modules: {
      auth,

      messages,
      userInfo,
      currentTag
    }
  });
};

export default createStore;
