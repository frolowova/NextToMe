<template>
  <nuxt-link :to="to" class="py-4 link-button">{{text.toUpperCase()}}</nuxt-link>
</template>

<script>
export default {
  props: {
    text: String,
    to: String
  }
}
</script>

<style style="scss" scoped>
.link-button {
  font-size: 1rem;
  font-weight: 500;
  height: 52px;
  text-decoration: none;
  text-align: center;
  display: block;
  border: 1px solid #FFFFFF;
  color: #FFFFFF;
  border-radius: 4px;
  cursor: pointer;
}
</style>