<template>
  <div class="comments-wraper">
    <div class="up-bar">
      <div>{{comments.length}} комментариев</div>
      <button>скрыть</button>
    </div>
    <div class="comments-conteiner">
      <v-list-item v-for="(comment, i)  in comments" :key="comment.messageId">
        <CommentCard :commentData="comment" :index="i"></CommentCard>
      </v-list-item>
    </div>
  </div>
</template>

<script>
import CommentCard from "@/components/comments/CommentCard";

export default {
  components: { CommentCard },

  computed: {
    comments() {
      return this.$store.state.comments;
    }
  },

};
</script>

<style lang="scss" scoped>
.comments-wraper {
  margin: 60px auto;
  width: 400px;
}

.comments-conteiner {
  border: 1px solid #ccc;
  background-color: #F9F9F9;
  padding: 1rem;
}
.up-bar {
  display: flex;
  justify-content: space-between;
}
</style>
