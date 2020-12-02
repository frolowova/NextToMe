
import { GET_COMMENTS } from "../actions/comments";


export const state = () => ({
    comments: [{ messageId: "4", src: "https://avatars0.githubusercontent.com/u/9064066?v=4&s=460", text: "Банальные, но неопровержимые выводы, а также представители современных.", from: "Victor", createdAt: "2020-12-01T10:55:48.478Z" }, { messageId: "7", src: "https://avatars0.githubusercontent.com/u/9064047?v=4&s=460", text: "Банальные, но неопровержимые выводы, а также представители современных.", from: "Max", createdAt: "2020-11-30T13:00:00.478Z" }, { messageId: "6", src: "https://avatars0.githubusercontent.com/u/9064076?v=4&s=460", text: "Банальные, но неопровержимые выводы, а также представители современных.", from: "Alex", createdAt: "2020-12-01T03:18:48.478Z" }, { messageId: "1", src: "https://avatars0.githubusercontent.com/u/9064068?v=4&s=460", text: "Современные технологии достигли такого уровня, что высокое качество позиционных исследований однозначно определяет каждого участника как способного принимать собственные решения касаемо системы массового участия.", from: "Andrey", createdAt: "2020-12-01T18:55:00.478Z" }, { messageId: "2", src: "https://avatars0.githubusercontent.com/u/9064065?v=4&s=460", text: "С другой стороны, выбранный нами инновационный путь прекрасно подходит для реализации форм воздействия. Есть над чем задуматься: элементы политического процесса превращены в посмешище, хотя само их существование приносит несомненную пользу обществу.", from: "Serg", createdAt: "2020-11-30T12:18:48.478Z" }, { messageId: "3", src: "https://avatars0.githubusercontent.com/u/9064064?v=4&s=460", text: "С другой стороны, выбранный нами инновационный путь прекрасно подходит для реализации форм воздействия. Есть над чем задуматься: элементы политического процесса превращены в посмешище, хотя само их существование приносит несомненную пользу обществу.", from: "Oleg", createdAt: "2020-11-24T12:18:48.478Z" }],

    url: 'https://nexttomeapi.azurewebsites.net',

});
export const mutations = {
    ADD_Comments(state, commentsFromServer) {
        state.comments = commentsFromServer;
    },


};

export const actions = {

    addComments({ commit, state }) {
        const get = "/api/message/comments/get";
        this.$axios.$get(`${state.url}${get}`)
            .then((response) => {
                commit("ADD_Comments", response);
            })
            .catch(error => {
                console.log(error);
            });
    },
};

export const getters = {
    comments: state => {
        return state.comments;
    }

};

export default {
    state,
    getters,
    actions,
    mutations
};
