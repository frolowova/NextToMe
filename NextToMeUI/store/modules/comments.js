
import { GET_COMMENTS, SEND_COMMENTS } from "../actions/comments";
import CommentsController from "@/api/CommentsController";


export const state = () => ({
    comments: []



});
export const mutations = {
    [GET_COMMENTS](state, comments) {
        state.comments = comments
    },

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

};

export const getters = {

};

export default {
    state,
    getters,
    actions,
    mutations
};
