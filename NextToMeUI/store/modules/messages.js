import MessageController from "@/api/MessageController";
import { SEND_MESSAGE, GET_MESSAGES } from "../actions/messages";

const state = () => ({
  messages: []
});

const mutations = {
  [GET_MESSAGES](state, messages) {
    state.messages = messages;
  }
};

const actions = {
  [GET_MESSAGES]: async ({ commit }) => {
    const messages = await MessageController.getMessages();
    commit(GET_MESSAGES, messages.data);
    return messages;
  },
  [SEND_MESSAGE]: async ({ commit }, message_info) => {
    const createdMessage = await MessageController.sendMessage(message_info);
    return createdMessage;
  }
};

const getters = {};

export default {
  state,
  getters,
  actions,
  mutations
};
