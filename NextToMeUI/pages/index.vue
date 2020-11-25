<template>
  <div>
    <h1>Hello NextToMe</h1>
    <div>
  <commentsList></commentsList>
  </div>
  </div>
</template>

<script>
import commentsList from "@/components/Comments/commentsList";
export default {
  components: {commentsList},
};
</script>
