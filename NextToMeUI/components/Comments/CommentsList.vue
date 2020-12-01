<template>
  <div class="comments-wraper">
    <div class="comments-conteiner">
      <div class="up-bar">
        <div>{{comments.length}} комментариев</div>
        <button>скрыть</button>
      </div>
      <v-list-item v-for="(comment, i)  in comments" :key="comment.messageId">
        <CommentCard :commentData="comment" :index="i"></CommentCard>
      </v-list-item>
      <CreateComment></CreateComment>
    </div>
    <div class="create"></div>
  </div>
</template>

<script>
import CommentCard from "@/components/Comments/CommentCard";
import CreateComment from "@/components/Comments/CreateComment";

export default {
  components: { CommentCard, CreateComment },
  // methods: {
  //     getComments() {
  //       this.$store.dispatch("addComments");
  //     },

  // },

  computed: {
    comments() {
      return  this.$store.state.comments.comments;
    }
  },

  mounted() {
    // this.getComments()
  }
};
</script>

<style lang="scss" scoped>
::-webkit-scrollbar {
  width: 0px;
  background: transparent;
}

.comments-wraper {
  margin: 60px auto;
  width: 25rem;
  -ms-overflow-style: none;
  scrollbar-width: none;
}

.comments-conteiner {
  border: 1px solid #ccc;
  background-color: #3c3c3c;
  padding: 1rem;
  max-height: 80vh;
  overflow-x: auto;
}
.up-bar {
  display: flex;
  justify-content: space-between;
  margin: 1rem;
  width: 20rem;
}
</style>
