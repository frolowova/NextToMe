import MessageController from "@/api/MessageController";
import UserController from "@/api/UserController";
import { SEND_MESSAGE, GET_MESSAGES, LOAD_AVATARS } from "../actions/messages";

const state = () => ({
  messages: [],
  avatars: []
});

const mutations = {
  [GET_MESSAGES](state, messages) {
    state.messages = messages;
  },
  [LOAD_AVATARS](state, avatars) {
    state.avatars = avatars;
  }
};

const actions = {
  [GET_MESSAGES]: async ({ commit }) => {
    const messages = await MessageController.getMessages(0, 100);
    commit(GET_MESSAGES, messages.data);
    return messages;
  },
  [SEND_MESSAGE]: async ({ commit }, message_info) => {
    const createdMessage = await MessageController.sendMessage(message_info);
    return createdMessage;
  },
  [LOAD_AVATARS]: async ({ commit, state }) => {
    const users_id = state.messages.map(message => message.from);
    const avatars = await UserController.getUserInfo(users_id);
    commit(LOAD_AVATARS, avatars.data);
    return avatars;
  }
};

const getters = {};

export default {
  state,
  getters,
  actions,
  mutations
};
