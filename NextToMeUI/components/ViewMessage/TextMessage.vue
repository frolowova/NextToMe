<template>
  <div class="mr-3 my-2">
    <p>
      {{ message }}
    </p>
  </div>
</template>

<script>
export default {
  props: {
    message: String,
  },
};
</script>