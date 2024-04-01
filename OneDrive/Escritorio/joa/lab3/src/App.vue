<template>
  <body>
    <nav v-if="isAuthenticated">
      <div>
        <img src="@/assets/logo.png" alt="Logo" width="35" height="30" /><b
          >CRYPCREATE</b
        >
      </div>
      <div>
        <router-link style="text-decoration: none" :to="{ name: 'LogOut' }"
          >Cerrar sesión</router-link
        >
        <router-link
          style="text-decoration: none"
          :to="{ name: 'viewPucharse' }"
          >Compra</router-link
        >
      </div>
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
  display: flex;
  justify-content: space-between;
  padding: 10px;
  text-align: left;
  background-color: rgb(14, 15, 46);
}

nav a {
  margin-right: 18px;
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
  background: -webkit-linear-gradient(rgb(71, 8, 71), #a9c6de);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
  font-size: 18px;
  font-weight: bold;
}
</style>
