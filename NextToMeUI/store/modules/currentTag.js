import {
  GET_COMMENTS,
  SEND_COMMENTS,
  GET_IMAGES,
  LOAD_COMMENT_AVATARS,
  RESET_IMAGES
} from "../actions/currentTag";
import CommentsController from "@/api/CommentsController";
import UserController from "@/api/UserController";
import MessageController from "@/api/MessageController";

export const state = () => ({
  comments: [],
  images: [],
  commentsAvatars: []
});
export const mutations = {
  [GET_COMMENTS](state, comments) {
    state.comments = comments;
  },
  [GET_IMAGES](state, images) {
    state.images.push(images);
  },
  [RESET_IMAGES](state) {
    state.images = [];
  },
  [LOAD_COMMENT_AVATARS](state, avatars) {
    state.commentsAvatars = avatars;
  }
};

export const actions = {
  [GET_COMMENTS]: async ({
    commit
  }, messageId) => {
    const comments = await CommentsController.getComments(messageId);
    commit(GET_COMMENTS, comments.data);
    return comments;
  },
  [SEND_COMMENTS]: async ({
    commit
  }, comment_info) => {
    const newComment = await CommentsController.sendComment(comment_info);
    return newComment;
  },
  [GET_IMAGES]: async ({
    commit
  }, messageImageId) => {
    const images = await MessageController.getImage(messageImageId);
    commit(GET_IMAGES, images.data);
    return images;
  },
  [RESET_IMAGES]: ({
    commit
  }) => {
    commit(RESET_IMAGES);
    return null;
  },
  [LOAD_COMMENT_AVATARS]: async ({
    commit,
    state
  }) => {
    const users_id = state.comments.map(comment => comment.from);
    const avatars = await UserController.getUserInfo(users_id);
    commit(LOAD_COMMENT_AVATARS, avatars.data);
    return avatars;
  }
};

export const getters = {};

export default {
  state,
  getters,
  actions,
  mutations
};
