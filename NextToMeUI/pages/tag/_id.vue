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
  headerData: { 
    title: "Тег"
  },
  components: {
    CommentsList,
    PageMessage,
  },
};
</script> 