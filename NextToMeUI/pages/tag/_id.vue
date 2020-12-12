<template>
  <div>
    <page-message></page-message>
    <comments-list></comments-list>
  </div>
</template>

<script>
import CommentsList from "@/components/Comments/CommentsList";
import PageMessage from "~/components/ViewMessage/PageMessage.vue";
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
};
</script> 