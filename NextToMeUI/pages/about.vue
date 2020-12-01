<template>
  <div class="about-page d-flex align-center">
    <slider class="slider" :slides="slides" />
  </div>
</template>

<script>
import slider from "@/components/Auth/Slider";
import successIcon from "@/assets/flame-success.svg";
import waitingIcon from "@/assets/flame-waiting.svg";
import messageIcon from "@/assets/flame-no-messages.svg";

export default {
  middleware: ["auth", "about"],
  layout: "auth",
  data: () => ({
    slides: [
      {
        image: successIcon,
        title: "Будь в топе!",
        text:
          "Ставь теги в интересных местах, возможно, именно твой тег попадет в ТОП"
      },
      {
        image: waitingIcon,
        title: "Успевай удивлять",
        text:
          "Тег существует 24 часа. Просматривай и комментриуй, продлевая жизнь тега"
      },
      {
        image: messageIcon,
        title: "Будь в курсе",
        text: "Включай push-уведомления и отслеживай коментарии к своему тегу"
      }
    ]
  }),
  components: {
    slider
  }
};
</script>

<style scoped>
.about-page {
  position: fixed;
  top: 0;
  height: 100%;
}


</style>
