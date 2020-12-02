<template>
  <div>
    <div class="comments-conteiner">
      <div class="up-bar px-4">
        <div>
          {{countComments}}
          <span v-if="countComments ==0 || countComments>=5">комментариев</span>
          <span v-if="countComments ==1">комментарий</span>
          <span v-if="countComments>1 && countComments<=4">комментария</span>
        </div>
        <v-btn
          class="text-none"
          small
          text
          v-if="pressShowComments"
          @click=" (pressShowComments=!pressShowComments)"
        >Скрыть</v-btn>
        <v-btn
          class="text-none"
          small
          text
          :disabled="!comments.length"
          v-if="!pressShowComments"
          @click=" (pressShowComments=!pressShowComments)"
        >Показать все</v-btn>
      </div>
      <div class="d-flex align-top justify-center" v-if="!comments.length">
        <p>Здесь пока нет комментариев. Будьте Первым!</p>
      </div>
      <div v-if="comments.length">
        <v-list-item fluid v-for="(comment, i)  in comments" :key="comment.messageId">
          <CommentCard :commentData="comment" :index="i"></CommentCard>
        </v-list-item>
      </div>
    </div>
    <CreateComment></CreateComment>
  </div>
</template>

<script>
import CommentCard from "@/components/comments/CommentCard";
import CreateComment from "@/components/comments/CreateComment";
import { GET_COMMENTS } from "@/store/actions/comments";

export default {
  components: { CommentCard, CreateComment },
  data: () => ({
    pressShowComments: false
  }),

  computed: {
    comments() {
      let comments = this.$store.state.comments.comments;

      return this.pressShowComments || comments.length == 0
        ? comments
        : [comments[0]];
    },

    countComments() {
      return this.$store.state.comments.comments.length;
    },
    messageId() {
      return this.$route.query.id;
    }
  },

  mounted() {
    this.$store.dispatch(GET_COMMENTS, this.messageId);
  }
};
</script>

<style lang="scss" scoped>
::-webkit-scrollbar {
  width: 0px;
  background: transparent;
}

.comments-conteiner {
  max-height: 40vh;
  overflow-x: auto;
  -ms-overflow-style: none;
  scrollbar-width: none;
  position: relative;
}
.up-bar {
  display: flex;
  justify-content: space-between;
  width: 100%;
}
.hide-btn,
.show-btn {
  cursor: pointer;
}
</style>
