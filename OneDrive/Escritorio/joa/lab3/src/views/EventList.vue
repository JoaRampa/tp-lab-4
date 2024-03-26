<template>
  <div class="events">
    <LogIn v-if="!isAuthenticated" />
    <div v-else>
      <h1>Bienvenido, {{ userId }}!</h1>
    </div>
  </div>
</template>

<script>
import LogIn from "@/components/LoginForm.vue";
import { mapGetters } from "vuex";

export default {
  name: "EvenList",
  components: {
    LogIn,
  },
  computed: {
    ...mapGetters(["userId", "isAuthenticated"]),
  },
};
</script>

<style>
.gargo {
  background-color: rgb(10, 2, 28);
}
.events {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
