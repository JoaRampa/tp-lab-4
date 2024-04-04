<template>
  <div>
    <nav v-if="isAuthenticated">
      <div class="brand">
        <img src="@/assets/logo.png" alt="Logo" width="35" />
        <b>CRYPCREATE</b>
        <router-link :to="{ name: 'viewPucharse' }">Comprar</router-link>
      </div>
      <div
        class="user-menu"
        @mouseover="showMenu = true"
        @mouseleave="showMenu = false"
      >
        <img
          src="@/assets/user.png"
          alt="User"
          width="35"
          :class="{ hovered: showMenu }"
        />
        <div v-show="showMenu" class="menu">
          <div class="user-info">
            <img src="@/assets/logo.png" alt="Logo" width="25" />
            <b>{{ userId }}</b>
          </div>
          <div class="options-menu">
            <div class="option wallet">
              <a href="/wallet">
                <img
                  src="@/assets/wallet.png"
                  alt="wallet"
                  width="25"
                  height="22"
                />
                Billetera
              </a>
            </div>
            <div class="option historial">
              <a href="/">
                <img
                  src="@/assets/history.png"
                  alt="historial"
                  width="25"
                  height="22"
                />
                Historial
              </a>
            </div>
            <div class="option logout">
              <a href="/#" @click="$store.commit('logout')">
                <img
                  src="@/assets/logout.png"
                  alt="logout"
                  width="25"
                  height="22"
                />
                Cerrar Sesión
              </a>
            </div>
          </div>
        </div>
      </div>
    </nav>
    <router-view />
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  data() {
    return {
      showMenu: false,
    };
  },
  computed: {
    ...mapGetters(["userId", "isAuthenticated"]),
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
  align-items: center;
  padding: 10px;
  text-align: left;
  background-color: rgb(14, 15, 46);
}

nav a {
  font-weight: bold;
  font-size: 15px;
  color: BEIGE;
}

body {
  background-image: url("@/assets/fondo.jpg");
  background: cover;
  background-size: cover;
  background-repeat: no-repeat;
}

.brand {
  display: flex;
  align-items: center;
}

.brand b {
  margin-right: 18px;
}

.user-menu {
  margin-right: 18px;
}

.menu {
  position: absolute;
  right: 10px;
  background-color: rgb(14, 15, 46);
  border: 1px solid #35314a;
  border-radius: 5px;
  z-index: 1000;
  font-size: 12px;
  width: 250px;
}

.user-info {
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 20px 0px 1px;
}

.options-menu {
  margin-top: 10px;
}

.option a {
  display: flex;
  align-items: center;
  text-decoration: none;
  color: BEIGE;
  padding: 10px;
}

.option a:hover {
  background-color: #181945;
}

.option img {
  margin-right: 10px;
}

b {
  background: -webkit-linear-gradient(#49e0bd, rgb(157, 32, 149));
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
  font-size: 18px;
  font-weight: bold;
}

.hovered {
  filter: brightness(130%);
}
</style>
