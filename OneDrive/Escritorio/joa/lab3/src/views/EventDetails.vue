<template>
  <div v-if="event">
    <h4>{{ event.nombre }}</h4>
    <h4>info???</h4>
  </div>
</template>

<script>
import EventService from "@/services/EventService";
export default {
  props: ['id'],
  data() {
    return {
      event: null
    };
  },
  created() {
    EventService.getEvent(this.id)
      .then(response => {
        this.event = response.data
      })
      .catch(error => {
        console.log(error);
      });
  }
};
</script>