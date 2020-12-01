<template>
  <div class="pa-2 ma-2" position="sticky">
    <v-textarea
      placeholder="Ваш комментарий"
      auto-grow
      solo
      rows="1"
      row-height="15"
      :append-icon="commentText.length > 0 ? 'mdi-send-circle' : ''"
      v-model="commentText"
      background-color="cardBackground"
      color="accent"
      @click:append="sendComment"
      @keyup.esc="commentText = ''"
    ></v-textarea>
  </div>
</template>

<script>
export default {
  data: () => ({
    commentText: ""
  }),
  methods: {
    sendComment() {
      console.log("work");
      console.log(Date);
      const post = "/api/message/comments/send";
      const dataComment = {
        text: this.commentText
      };
      this.$axios
        .$post(`${this.$store.state.url}${post}`, dataComment, {
          headers: { Authorization: `Bearer ${this.$store.state.token}` }
        })
        .then(response => {
          this.$store.dispatch("addMessages");
          this.message = "";
        })
        .catch(error => {
          console.log(error);
        });
    }
  }
};
</script>



<style lang="scss" scoped>
</style>
