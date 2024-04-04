<template>
  <div class="home">
    <LogIn v-if="!isAuthenticated" />
    <div v-else>
      <h1>Bienvenido, {{ userId }}!</h1>
    </div>
    <CryptoRequestPrice v-if="isAuthenticated" />
  </div>
</template>

<script>
import LogIn from "@/components/LoginForm.vue";
import CryptoRequestPrice from "@/components/CryptoRequestPrice.vue";
import { mapGetters } from "vuex";
export default {
  name: "HomeView",
  components: {
    LogIn,
    CryptoRequestPrice,
  },
  computed: {
    ...mapGetters(["userId", "isAuthenticated"]),
  },
};
</script>

<style>
.home {
  color: beige;
}
</style>
