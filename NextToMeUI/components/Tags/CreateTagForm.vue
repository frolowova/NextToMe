<template>
  <div class="create-tag-form">
    <v-form ref="form" v-model="valid">
      <span class="ml-6 font-weight-bold">Тег</span>
      <v-textarea
        solo
        auto-grow
        v-model="tagText"
        class="rounded-lg"
        :rules="[rules.required]"
      ></v-textarea>
      <div class="images-block">
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
        :loading="loading"
        >Разместить</v-btn
      >
    </v-form>
  </div>
</template>

<script>
import picturesOfMessage from "@/components/ViewMessage/PicturesOfMessage.vue";
import { SEND_MESSAGE } from "@/store/actions/messages";
export default {
  components: {
    picturesOfMessage
  },
  data: () => ({
    valid: true,
    loading: false,
    tagText: "",
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
        this.$store
          .dispatch(SEND_MESSAGE, { text: this.tagText })
          .then(res => console.log(res));
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
        if (file.type !== "image/jpeg") {
          return;
        }
        const reader = new FileReader();
        reader.addEventListener("load", ({ target: { result } }) => {
          this.compressImage(result, file);
        });
        reader.readAsDataURL(file);
      });
    },
    compressImage(base64, file) {
      const canvas = document.createElement("canvas");
      const img = document.createElement("img");

      img.addEventListener("load", () => {
        let width = img.width;
        let height = img.height;
        const MAX_WIDTH = 1200;
        const MAX_HEIGHT = 900;

        if (width > height) {
          if (width > MAX_WIDTH) {
            height = Math.round((height *= MAX_WIDTH / width));
            width = MAX_WIDTH;
          }
        } else {
          if (height > MAX_HEIGHT) {
            width = Math.round((width *= MAX_HEIGHT / height));
            height = MAX_HEIGHT;
          }
        }
        canvas.width = width;
        canvas.height = height;

        const ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0, width, height);
        const compressedData = canvas.toDataURL("image/jpeg", 0.8);
        this.attachedFiles.push({
          url: compressedData,
          file: file
        });
      });
      img.addEventListener("error", () => {
        throw new Error("Compressed img error!");
      });
      img.src = base64;
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
