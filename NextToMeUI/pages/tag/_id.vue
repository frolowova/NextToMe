<template>
  <div>
    <page-message />
    <CommentsList></CommentsList>
  </div>
</template>

<script>
import CommentsList from "@/components/Comments/CommentsList";
import PageMessage from "~/components/ViewMessage/PageMessage.vue";
export default {
  components: {
    CommentsList,
    PageMessage,
  },
};
</script> 