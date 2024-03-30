<template>
  <body>
    <nav v-if="isAuthenticated">
      <img src="@/assets/logo.png" alt="Logo" width="35" height="30" /><b
        >CRYPCREATE</b
      >
      <router-link style="text-decoration: none" :to="{ name: 'LogOut' }"
        >Cerrar sesión</router-link
      >
      <router-link style="text-decoration: none" :to="{ name: 'viewPucharse' }"
        >Compra</router-link
      >
    </nav>
    <router-view />
  </body>
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
  text-align: center;
}

nav {
  padding: 10px;
  margin-bottom: 10px;
  text-align: left;
  background-color: rgb(14, 15, 46);
}

nav a {
  margin-left: 18px;
  font-weight: bold;
  color: BEIGE;
}

nav a:hover {
  color: #0f5b9a;
}

body {
  background-image: url("@/assets/fondo.jpg");
  background: cover;
  background-size: cover;
  background-repeat: no-repeat;
}

b {
  background: -webkit-linear-gradient(rgb(71, 8, 71), #0f5b9a);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
  font-size: 18px;
  font-weight: bold;
}
</style>
