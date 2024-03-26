<template>
  <nav>
    <router-link v-if="!isAuthenticated" :to="{ name: 'EventList' }">Log In</router-link> 
    <router-link v-if="isAuthenticated" :to="{ name: 'About' }">Cripto</router-link>
    <h1>𓆰𓆪GARGO𓆰𓆪</h1>
  </nav>
  <router-view />
</template>
<script>
import { mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters(["isAuthenticated"]),
  },
};
</script>
<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: darkviolet;
}
</style>
