<template>
  <div v-if="event">
    <h4>{{ event.nombre }}</h4>
    <h4>info???</h4>
  </div>
</template>

<script>
export default {
  props: ["id"],
  data() {
    return {
      event: null,
    };
  },
};
</script>
