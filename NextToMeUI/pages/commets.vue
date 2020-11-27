<template>
  <div>
    <h1>Hello NextToMe</h1>
    <div>
      <CommentsList></CommentsList>
    </div>
  </div>
</template>

<script>
import CommentsList from "@/components/Comments/CommentsList";

export default {
  components: { CommentsList }
};
</script>