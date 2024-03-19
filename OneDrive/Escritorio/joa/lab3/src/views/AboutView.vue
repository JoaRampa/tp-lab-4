<template>
  <div
    class="About"
    v-for="(event, index) in evento"
    :key="event.time"
    :event="event"
  >
    <h4>{{ index }}</h4>
    <h4>Precio en ARS$ {{ event.ask }}</h4>
  </div>
</template>

<script>
import EventService from "@/services/EventService";
export default {
  /* props: {
    coin: "btc",
    fiat: "usdt"
  },*/
  data() {
    return {
      evento: null,
    };
  },
  created() {
    EventService.getApis()
      .then((response) => {
        this.evento = response.data;
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
</script>

<style>
.About {
  cursor: pointer;
  border: 1px solid #39495c;
  display: flex;
  flex-direction: column;
  align-items: center;
}
.About:hover {
  transform: scale(1.01);
  box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0, 2);
}
</style>
