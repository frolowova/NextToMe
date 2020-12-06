<template>
  <div class="create-tag-form">
    <v-form ref="form" v-model="valid">
      <span class="ml-6 font-weight-bold">Тег</span>
      <v-textarea
        solo
        auto-grow
        v-model="tagText"
        class="rounded-xl"
        :rules="[rules.required]"
      ></v-textarea>
      <div class="images-block">
        <tagImages :arrayOfPic="arrayOfPic" @remove-image="removeImage" />
      </div>
      <v-alert dense text type="error" v-if="errorMessage">{{
        errorMessage
      }}</v-alert>
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
          class="mb-6 rounded-lg"
          :disabled="loading"
          @click.prevent="loadImage"
          >Добавить изображение</v-btn
        >
      </div>
      <v-btn
        class="rounded-lg"
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
import tagImages from "@/components/Tags/TagImages.vue";
import { SEND_MESSAGE } from "@/store/actions/messages";
export default {
  components: {
    tagImages
  },
  data: () => ({
    valid: true,
    loading: false,
    tagText: "",
    selectedFiles: [],
    attachedFiles: [],
    rules: {
      required: v => !!v || "Напишите-что нибудь"
    },
    errorMessage: ""
  }),
  computed: {
    totalFiles() {
      return this.selectedFiles.length + this.attachedFiles.length;
    },
    arrayOfPic() {
      return this.attachedFiles.map(img => img.url);
    }
  },
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    createTag() {
      this.validate();
      if (this.valid) {
        this.loading = true;
        this.$store
          .dispatch(SEND_MESSAGE, {
            text: this.tagText,
            photos: [...this.arrayOfPic]
          })
          .then(res => {
            this.loading = false;
            this.$router.push(`/tag?id=${res.data.id}`);
          })
          .catch(err => {
            this.loading = false;
            this.errorMessage = "Что-то пошло не так, попробуйте позже";
            setTimeout(() => {
              this.errorMessage = "";
            }, 3000);
          });
      }
    },
    removeImage(index) {
      this.attachedFiles = this.attachedFiles.filter((item, i) => i !== index);
    },
    loadImage() {
      const fileInput = this.$refs.fileInput.$children[0].$el;
      fileInput.click();
    },
    onChangeFile() {
      if (!this.selectedFiles.length) {
        return;
      }
      if (this.totalFiles > 8) {
        alert("Вы не можете прикрепить больше 8 файлов!");
        this.selectedFiles = [];
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
</style>
