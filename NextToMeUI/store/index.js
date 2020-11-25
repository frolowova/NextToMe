import Vuex from "vuex";
import auth from "./modules/auth";

const createStore = () => {
  return new Vuex.Store({
    namespaced: true,
    modules: {
      auth
    }
  });
};

export default createStore;
