<template>
  <div>
    <nav v-if="isAuthenticated">
      <div class="brand">
        <img src="@/assets/logo.png" alt="Logo" width="35" />
        <b>CRYPCREATE</b>
        <router-link :to="{ name: 'Home' }">Inicio </router-link>
        <router-link :to="{ name: 'viewPucharse' }" style="margin-left: 18px">
          Tradear</router-link
        >
        <router-link
          :to="{ name: 'investmentsCrypto' }"
          style="margin-left: 18px"
        >
          Analisis Inversiones</router-link
        >
      </div>
      <div
        class="user-menu"
        @mouseover="showMenu = true"
        @mouseleave="showMenu = false"
      >
        <div class="brand">
          <img
            src="@/assets/user.png"
            alt="User"
            width="35"
            height="33"
            :class="{ hovered: showMenu }"
          />
          <b :class="{ hovered: showMenu }">{{ userId }}</b>
        </div>
        <div v-show="showMenu" class="menu">
          <div class="user-info">
            <img src="@/assets/logo.png" alt="Logo" width="35" />
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
              <a href="/history">
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
import "bootstrap/dist/js/bootstrap.bundle.min.js";

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
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  background-color: rgb(14, 15, 46);
  min-height: 100vh;
}

nav {
  display: flex;
  justify-content: space-between;
  padding: 8px;
  border-bottom: 1px solid #35314a;
  margin-bottom: 15px;
}

nav a {
  font-weight: bold;
  font-size: 15px;
  color: BEIGE;
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
  font-size: 20px;
  font-weight: bold;
}

.hovered {
  filter: brightness(150%);
}
</style>
