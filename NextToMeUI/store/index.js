import Vuex from "vuex";
import auth from "./modules/auth";
import comments from "./modules/currentTag";
import messages from "./modules/messages";

const createStore = () => {
  return new Vuex.Store({
    namespaced: true,
    modules: {
      auth,
      comments,
      messages
    }
  });
};

export default createStore;
