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
  // methods: {
  //     getComments() {
  //       this.$store.dispatch("addComments");
  //     },

  // },

  computed: {
    comments() {
      return this.$store.state.comments;
    }
  },

  mounted() {
    // this.getComments()
  }
};
</script>

<style lang="scss" scoped>
.comments-wraper {
  margin: 60px auto;
  width: 400px;
}

.comments-conteiner {
  border: 1px solid #ccc;
  background-color: #3c3c3c;
  padding: 1rem;
  //   margin-bottom: 1rem;
}
.up-bar {
  display: flex;
  justify-content: space-between;
}
</style>