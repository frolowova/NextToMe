import Vuex from "vuex";
import auth from "./modules/auth";
import comments from "./modules/comments"

const createStore = () => {
  return new Vuex.Store({
    namespaced: true,
    modules: {
      auth,
      comments,
    }
  });
};

export default createStore;
