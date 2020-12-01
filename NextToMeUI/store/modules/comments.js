
import { GET_COMMENTS } from "../actions/comments";


export const state = () => ({
    comments: [{ messageId: "4", text: "Банальные, но неопровержимые выводы, а также представители современных.", from: "Victor", createdAt: "2020-11-24T12:18:48.478Z" }, { messageId: "1", text: "Современные технологии достигли такого уровня, что высокое качество позиционных исследований однозначно определяет каждого участника как способного принимать собственные решения касаемо системы массового участия.", from: "Andrey", createdAt: "2020-11-24T12:18:48.478Z" }, { messageId: "2", text: "С другой стороны, выбранный нами инновационный путь прекрасно подходит для реализации форм воздействия. Есть над чем задуматься: элементы политического процесса превращены в посмешище, хотя само их существование приносит несомненную пользу обществу.", from: "Serg", createdAt: "2020-11-24T12:18:48.478Z" }, { messageId: "3", text: "С другой стороны, выбранный нами инновационный путь прекрасно подходит для реализации форм воздействия. Есть над чем задуматься: элементы политического процесса превращены в посмешище, хотя само их существование приносит несомненную пользу обществу.", from: "Oleg", createdAt: "2020-11-24T12:18:48.478Z" }],

    url: 'https://nexttomeapi.azurewebsites.net',

});
export const mutations = {
    ADD_Comments(state, commentsFronServer) {
        state.comments = commentsFronServer;
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
