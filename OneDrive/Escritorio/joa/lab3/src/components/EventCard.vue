<template>
    <div class="event-card">
      <h4>{{ event.nombre }}</h4>
      <h4>{{ event.title }}</h4>
      <span>${{ event.ask}} on {{ event.time }}</span>
    </div>
</template>

<script>
export default {
  name: "EventCard",
  props:{
    event: Object
  }
};
</script>

<style scoped>
.event-card {
  padding: 20px;
  width: 250px;
  cursor: pointer;
  border: 1px solid #39495c;
  margin-bottom: 18px;
}
.event-card:hover {
  transform: scale(1.01);
  box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0, 2);
}
</style>
