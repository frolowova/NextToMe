import MessageController from "@/api/MessageController";
import UserController from "@/api/UserController";
import {
  SEND_MESSAGE,
  GET_MESSAGES,
  LOAD_AVATARS,
  GET_TOP_MESSAGES,
  CHANGE_LIST_TITLE,
  GET_MY_MESSAGES
} from "../actions/messages";

const state = () => ({
  messages: [],
  avatars: [],
  title: "Рядом"
});

const mutations = {
  [GET_MESSAGES](state, messages) {
    state.messages = messages;
  },
  [LOAD_AVATARS](state, avatars) {
    state.avatars = avatars;
  },
  [CHANGE_LIST_TITLE](state, title) {
    state.title = title;
  },
  [GET_MY_MESSAGES](state, messages) {
    state.message = messages;
  }
};

const actions = {
  [GET_MESSAGES]: async ({
    commit
  }) => {
    const messages = await MessageController.getMessages(0, 100);
    commit(GET_MESSAGES, messages.data);
    return messages;
  },
  [GET_MY_MESSAGES]: ({
    commit
  }, messages) => {
    commit(GET_MESSAGES, messages);
    return null;
  },
  [SEND_MESSAGE]: async ({
    commit
  }, message_info) => {
    const createdMessage = await MessageController.sendMessage(message_info);
    return createdMessage;
  },
  [LOAD_AVATARS]: async ({
    commit,
    state
  }) => {
    const users_id = state.messages.map(message => message.from);
    const avatars = await UserController.getUserInfo(users_id);
    commit(LOAD_AVATARS, avatars.data);
    return avatars;
  },
  [GET_TOP_MESSAGES]: async ({
    commit
  }) => {
    const messages = await MessageController.getTopMessages();
    commit(GET_MESSAGES, messages.data);
    return messages;
  },
  [CHANGE_LIST_TITLE]: ({
    commit
  }, title) => {
    commit(CHANGE_LIST_TITLE, title);
  }
};

const getters = {};

export default {
  state,
  getters,
  actions,
  mutations
};
