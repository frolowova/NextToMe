<template>
  <div class="create-tag-form mx-6 mt-4">
    <v-form ref="form" v-model="valid">
      <span class="ml-2">Тег</span>
      <v-textarea
        background-color="cardBackground"
        solo
        auto-grow
        v-model="tagText"
        class="rounded-xl mt-2"
        :rules="[rules.required]"
        placeholder="Текст тега"
        autofocus
      />

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
        />
        <ntm-button
          title="Добавить изображение"
          outlined
          :disabled="loading"
          class="mb-6"
          @click.prevent="loadImageClickHandler"
        />
      </div>

      <ntm-button
        title="Разместить"
        :disabled="loading || !tagText.length"
        :loading="loading"
        class="mb-6"
        @click.prevent="createTag"
      />
    </v-form>
  </div>
</template>

<script>
import loadAndPreviewImages from "@/mixins/loadAndPreviewImages";

import tagImages from "@/components/Tags/TagImages.vue";
import { SEND_MESSAGE } from "@/store/actions/messages";
import NTMButton from "@/components/Common/NTMButton.vue";

export default {
  components: {
    tagImages,
    NTMButton,
  },
  mixins: [loadAndPreviewImages],

  data: () => ({
    valid: true,
    loading: false,
    tagText: "",
    selectedFiles: [],
    attachedFiles: [],
    rules: {
      required: (v) => !!v || "Напишите-что нибудь",
    },
    errorMessage: "",
  }),

  computed: {
    totalFiles() {
      return this.selectedFiles.length + this.attachedFiles.length;
    },
    arrayOfPic() {
      return this.attachedFiles.map((img) => img.url);
    },
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
            photos: [...this.arrayOfPic],
          })
          .then((res) => {
            this.loading = false;
            this.$router.push(`/home`);
          })
          .catch((err) => {
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
    loadImageClickHandler() {
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
      this.loadAndPreviewImages(selectedFiles, {
        width: 1200,
        height: 900,
      });
    },
  },
};
</script>

<style scoped>
.create-tag-form {
  margin: auto;
}
</style>
