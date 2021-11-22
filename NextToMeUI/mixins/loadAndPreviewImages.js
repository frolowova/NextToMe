export default {
  /**
   * @function loadAndPreviewImages
   * @param {Array} selectedFiles массив выбранных файлов
   * @param {Object} size объект с полями высоты и ширины картинки
   * @discription Метод загружает файлы и отображает в preview
   */
  methods: {
    loadAndPreviewImages(selectedFiles = [], size = {}) {
      const compressImage = (base64, file) => {
        const canvas = document.createElement("canvas");
        const img = document.createElement("img");

        img.addEventListener("load", () => {
          let width = img.width;
          let height = img.height;
          const MAX_WIDTH = size?.width || 200;
          const MAX_HEIGHT = size?.height || 200;

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
            file: file,
          });
        });
        img.addEventListener("error", () => {
          throw new Error("Compressed img error!");
        });
        img.src = base64;
      };

      selectedFiles.forEach((file) => {
        if (file.type !== "image/jpeg" && file.type !== "image/png") {
          return;
        }
        const reader = new FileReader();
        reader.addEventListener("load", ({ target: { result } }) => {
          compressImage(result, file);
        });
        reader.readAsDataURL(file);
      });
    },
  },
};
