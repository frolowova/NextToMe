<template>
  <div class="pa-2 ma-2" position="absolute">
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
import { SEND_COMMENTS } from "@/store/actions/comments";

export default {
  data: () => ({
    commentText: ""
  }),
  methods: {
    sendComment() {
      this.$store.dispatch(SEND_COMMENTS, {
        text: this.commentText,
        messageId: this.messageId
      });
    }
  },

  computed: {
    messageId() {
      return this.$route.query.id;
    }
  }
};
</script>



<style lang="scss" scoped>
</style>
