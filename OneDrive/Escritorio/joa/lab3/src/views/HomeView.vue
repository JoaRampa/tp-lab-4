<template>
  <div class="home">
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
  name: "HomeView",
  components: {
    LogIn,
  },
  computed: {
    ...mapGetters(["userId", "isAuthenticated"]),
  },
};
</script>

<style>
.home {
  display: flex;
  flex-direction: column;
  align-items: center;
  color: beige;
}
</style>
