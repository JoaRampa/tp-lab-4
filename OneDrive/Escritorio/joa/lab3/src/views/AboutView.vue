<template>
  <div
    class="About"
    v-for="(event, index) in evento"
    :key="event.index"
    :event="event"
  >
    <h4>{{ index }}</h4>
    <h4>Precio en ARS$ {{ event.ask }}</h4>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
//import EventService from "@/services/EventService";
export default {
  data() {
    return {};
  },
  methods: {
    ...mapActions("criptos", ["cargarCriptos"]),
  },
  computed: {
    ...mapGetters("criptos", ["evento"]),
  },
  created() {
    this.cargarCriptos();
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
