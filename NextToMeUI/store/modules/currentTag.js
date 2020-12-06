import { GET_COMMENTS, SEND_COMMENTS, GET_IMAGES } from "../actions/currentTag";
import CommentsController from "@/api/CommentsController";
import MessageController from "@/api/MessageController";

export const state = () => ({
  comments: [],
  images: []
});
export const mutations = {
  [GET_COMMENTS](state, comments) {
    state.comments = comments;
  },
  [GET_IMAGES](state, images) {
    state.images = images;
  }
};

export const actions = {
  [GET_COMMENTS]: async ({ commit }, messageId) => {
    const comments = await CommentsController.getComments(messageId);
    commit(GET_COMMENTS, comments.data);
    return comments;
  },
  [SEND_COMMENTS]: async ({ commit }, comment_info) => {
    const newComment = await CommentsController.sendComment(comment_info);
    return newComment;
  },
  [GET_IMAGES]: async ({ commit }, messageImageId) => {
    const images = await MessageController.getImages(messageImageId);
    commit(GET_IMAGES, images.data);
    return images;
  }
};

export const getters = {};

export default {
  state,
  getters,
  actions,
  mutations
};
