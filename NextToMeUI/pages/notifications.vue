<template>
  <v-container style="padding-left: 0px; padding-right: 0px">
    <div style="font-size: 20px; text-align: center">Страница в разработке</div>
    <!-- <v-card class="pa-md-4 mb-4 mx-lg-auto">
      <div class="d-flex align-top justify-space-between" style="padding: 16px">
        <div class="d-flex align-start">
          <v-avatar size="40px" style="margin-right: 12px">
            <img
              alt="Avatar"
              src="data:image/png;base64,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"
            />
          </v-avatar>
          <div class="mx-2">
            <div>Виктор</div>
            <div>оставил(а) комментарий:</div>
            <v-card-text style="padding: 0px"
              >Банальные, но неопровержимые выводы, а также представители
              современных.</v-card-text
            >
            <v-card-actions style="padding: 0px">
              <v-list-item
                class="grow"
                style="padding: 0px; color: color: #FFFFFF; opacity: 0.38;"
              >
                <span>1 минуту назад</span>
              </v-list-item>
            </v-card-actions>
          </div>
        </div>
      </div>
    </v-card>
    <v-card class="pa-md-4 mb-4 mx-lg-auto">
      <div class="d-flex align-top justify-space-between" style="padding: 16px">
        <div class="d-flex align-start">
          <v-avatar size="40px" style="margin-right: 12px">
            <img
              alt="Avatar"
              src="data:image/png;base64,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"
            />
          </v-avatar>
          <div class="mx-2">
            <div>Ольга</div>
            <div>оставил(а) комментарий:</div>
            <v-card-text style="padding: 0px"
              >Современные технологии достигли такого уровня, что высокое
              качество позиционных исследований однозначно
              определяет...</v-card-text
            >
            <v-card-actions style="padding: 0px">
              <v-list-item
                class="grow"
                style="padding: 0px; color: color: #FFFFFF; opacity: 0.38;"
              >
                <span>15 минут назад</span>
              </v-list-item>
            </v-card-actions>
          </div>
        </div>
      </div>
    </v-card> -->
  </v-container>
</template>

<script>
export default {
  data: () => ({}),
};
</script>