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
        <p
          class="hide-btn"
          v-if="pressShowComments"
          @click=" (pressShowComments=!pressShowComments)"
        >Скрыть</p>
        <p
          class="show-btn"
          v-if="!pressShowComments"
          @click=" (pressShowComments=!pressShowComments)"
        >Показать все</p>
      </div>
      <v-list-item fluid v-for="(comment, i)  in comments" :key="comment.messageId">
        <CommentCard :commentData="comment" :index="i"></CommentCard>
      </v-list-item>
    </div>
    <CreateComment></CreateComment>
  </div>
</template>

<script>
import CommentCard from "@/components/comments/CommentCard";
import CreateComment from "@/components/comments/CreateComment";
import comments from "../../store/modules/comments";

export default {
  components: { CommentCard, CreateComment },
  data: () => ({
    pressShowComments: false
  }),

  computed: {
    comments() {
      let comments = this.$store.state.comments.comments;
      return this.pressShowComments ? comments : [comments[0]];
    },

    countComments() {
      return this.$store.state.comments.comments.length;
    }
  },

  mounted() {
    // this.getComments();
  }
};
</script>

<style lang="scss" scoped>
::-webkit-scrollbar {
  width: 0px;
  background: transparent;
}

.comments-conteiner {
  max-height: 80vh;
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
