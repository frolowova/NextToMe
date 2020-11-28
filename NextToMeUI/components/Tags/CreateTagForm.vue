<template>
  <div class="create-tag-form">
    <v-form ref="form" v-model="valid" lazy-validation>
      <span class="ml-6 font-weight-bold">Тег</span>
      <v-textarea
        solo
        auto-grow
        v-model="tagText"
        class="rounded-lg"
        :rules="[rules.required]"
      ></v-textarea>
      <div class="d-flex justify-space-between align-center">
        <label class="font-weight-bold" for="switch">Анонимное сообщение</label>
        <v-switch id="switch" v-model="anonymMessageFlag" inset></v-switch>
      </div>
      <div class="images-block d-flex flex-row flex-wrap">
        <!--- HERE IS picturesOfMessage COMPONENT --->
      </div>
      <div class="input-file-btn">
        <v-file-input
          accept="image/x-png,image/jpeg,image/gif"
          ref="fileInput"
          multiple
          v-show="false"
          v-model="selectedFiles"
          @change="onChangeFile"
        ></v-file-input>
        <v-btn
          block
          color="primary"
          elevation="2"
          x-large
          outlined
          class="mb-6"
          :disabled="loading"
          @click.prevent="loadImage"
          >Добавить изображение</v-btn
        >
      </div>

      <v-btn
        block
        color="primary"
        elevation="2"
        x-large
        @click.prevent="createTag"
        :disabled="loading"
        >Разместить</v-btn
      >
    </v-form>
  </div>
</template>

<script>
import picturesOfMessage from "@/components/ViewMessage/PicturesOfMessage.vue";

export default {
  components: {
    picturesOfMessage
  },
  data: () => ({
    valid: false,
    loading: false,
    tagText: "",
    anonymMessageFlag: false,
    selectedFiles: [],
    attachedFiles: [],
    rules: {
      required: v => !!v || "Напишите-что нибудь"
    }
  }),
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    createTag() {
      this.validate();
      if (this.valid) {
        this.loading = true;
        //here is converting form data with images to body post
        this.loading = false;
      }
    },
    loadImage() {
      const fileInput = this.$refs.fileInput.$children[0].$el;
      fileInput.click();
    },
    onChangeFile() {
      if (!this.selectedFiles.length) {
        return;
      }
      const selectedFiles = Array.from(this.selectedFiles);
      this.selectedFiles = [];
      selectedFiles.forEach(file => {
        const reader = new FileReader();
        reader.addEventListener("load", ({ target: { result } }) => {
          this.attachedFiles.push({
            url: result,
            file: file
          });
        });
        reader.readAsDataURL(file);
      });
    }
  }
};
</script>

<style scoped>
.create-tag-form {
  max-width: 650px;
  margin: auto;
}
.img {
  height: 80px;
}
</style>
