<template>
  <nav>
    <router-link :to="{ name: 'EventList' }">Log In</router-link> |
    <router-link :to="{ name: 'About' }">Cripto</router-link>
    <h1 color="darkviolet">𓆰𓆪GARGO𓆰𓆪</h1>
  </nav>
  <router-view />
</template>

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
