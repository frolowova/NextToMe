<template>
  <div>
    <div
      class="up-bar px-4 d-flex justify-space-between"
      width="100%"
      position="sticky"
    >
      <div>
        {{ countComments }}
        <span v-if="countComments % 10 == 1 && countComments % 100 != 11"
          >КОММЕНТАРИЙ</span
        >
        <span
          v-else-if="
            countComments % 10 >= 2 &&
              countComments % 10 <= 4 &&
              (countComments % 100 < 10 || countComments % 100 >= 20)
          "
          >КОММЕНТАРИЯ</span
        >
        <span v-else>КОММЕНТАРИЕВ</span>
      </div>
      <v-btn
        class="text-none"
        small
        text
        v-if="pressShowComments"
        @click="pressShowComments = !pressShowComments"
        >Скрыть</v-btn
      >
      <v-btn
        class="text-none"
        small
        text
        :disabled="!comments.length"
        v-if="!pressShowComments"
        @click="showComments(), pressShowComments = !pressShowComments"
        >Показать все</v-btn
      >
    </div>
    <div  class="comments-conteiner" ref="listComment"  >
      <div class="d-flex align-top justify-center pt-4" v-if="!comments.length">
        <p>Здесь пока нет комментариев. Будьте Первым!</p>
      </div>
      <div v-if="comments.length">
         <div v-if="skeletonLoad">
          <CommentLoading />
        </div>
        <v-list-item fluid v-for="(comment, i) in comments" :key="i" >
          <CommentCard
            v-if="!skeletonLoad"
            :commentData="comment"
            :avatarLoading="avatarLoading"
          ></CommentCard>
        </v-list-item>
      </div>
    </div>
    <CreateComment :load="avatarLoading" @sendComment = "scrollDown"></CreateComment>
  </div>
</template>

<script>
import CommentCard from "@/components/Comments/CommentCard";
import CommentLoading from "@/components/Comments/CommentLoading";
import CreateComment from "@/components/Comments/CreateComment";
import { GET_COMMENTS, LOAD_COMMENT_AVATARS } from "@/store/actions/currentTag";

export default {
  components: { CommentCard, CreateComment, CommentLoading },
  data: () => ({
    pressShowComments: false,
    avatarLoading: false,
    skeletonLoad: false,
  }),

  methods: {
   
    scrollDown() { 
      
      setTimeout(() => {
        let list = this.$refs.listComment;
        list.scrollTop = list.scrollHeight;
      }, 0); 
    },
    showComments() {
      
      setTimeout(() => {
        let list = this.$refs.listComment;
        let top = window.scrollY + list.getBoundingClientRect().y;
        window.scrollTo(0, top);
      }, 0); 

    },
  },

  computed: {
    comments() {
      let comments = this.$store.state.currentTag.comments;

      return this.pressShowComments || comments.length == 0
        ? comments
        : [comments[0]];
    },

    countComments() {
      return this.$store.state.currentTag.comments.length;
    },
    messageId() {
      return this.$route.query.id;
    },
     
  },

  mounted() {
    this.skeletonLoad = true;
    this.avatarLoading = true;
    this.$store
      .dispatch(GET_COMMENTS, this.messageId)
      .then(result => {
        this.skeletonLoad = false;
        return this.$store.dispatch(LOAD_COMMENT_AVATARS);
      })
      .then(res => (this.avatarLoading = false));
      
     
  },
 
};
</script>

<style lang="scss" scoped>
::-webkit-scrollbar {
  width: 0px;
  background: transparent;
}

.comments-conteiner {
  max-height: 62vh;
  overflow-x: auto;
  -ms-overflow-style: none;
  scrollbar-width: none;
  position: relative;
}
</style>
