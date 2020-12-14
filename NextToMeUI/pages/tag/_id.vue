<template>
  <div>
    <page-message></page-message>
    <comments-list></comments-list>
  </div>
</template>

<script>
import CommentsList from "@/components/Comments/CommentsList";
import PageMessage from "~/components/ViewMessage/PageMessage.vue";
// import { RESET_IMAGES, RESET_COMMENTS } from "~/store/actions/currentTag";
export default {
  headerData: {
    title: "Рядом",
  },
  data: () => ({
    text: "",
  }),
  components: {
    CommentsList,
    PageMessage,
  },
  mounted() {},

  // beforeRouteUpdate(to, from, next) {
  //   this.$store.dispatch(RESET_IMAGES);
  //   this.$store.dispatch(RESET_COMMENTS);
  //   next();
  // },
};
</script> 