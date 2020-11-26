<template>
  <div class="create-comment-conteiner">
    <div class="create-comment">
      <v-textarea class="new-comment-text" v-model="comment" rows="1"></v-textarea>
      <v-btn class="mx-2" fab dark large color="#F55454" @click="sendComment">
        <v-icon>mdi-telegram</v-icon>
      </v-btn>
    </div>
  </div>
</template>

<script>
export default {
  data: () => ({
    comment: ""
  })
};
</script>>



<style lang="scss" scoped>
.create-comment-conteiner {
  position: sticky;
  bottom: 0;
  width: 20.5rem;
  background-color: inherit;
}
.create-comment {
  border: 1px solid grey;
  border-radius: 5px;
  position: relative;
  left: 5%;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.new-comment-text {
  outline: none;
  margin-bottom: 0px;
  resize: none;
}
.mx-2 {
  margin-bottom: 0;
  width: 2.5rem;
  height: 2.5rem;
}
</style>
