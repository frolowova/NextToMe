<template>
  <v-text-field
    class="pa-0 ma-0"
    :label = labelC
    type="text"
    single-line
    reverse
    loading="0"
  > </v-text-field>
</template>

<script>
export default {
  props: {
    labelC: String,
  }
}
</script>

