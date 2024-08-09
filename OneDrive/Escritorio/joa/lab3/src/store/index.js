import { createStore } from "vuex";
import criptos from "./criptos.js";
import transactions from "./transactions.js";
import VuexPersistence from "vuex-persist";
import router from "../router";

export default createStore({
  state: {
    userId: null,
    isAuthenticated: false,
  },
  getters: {
    userId: (state) => state.userId,
    isAuthenticated: (state) => state.isAuthenticated,
  },
  mutations: {
    submitUser(state, userId) {
      state.userId = userId;
      state.isAuthenticated = true;
    },
    logout(state) {
      state.isAuthenticated = false;
      state.userId = null;
      router.push("/");
    },
  },
  actions: {
    submitUser({ commit }, userId) {
      try {
        commit("submitUser", userId);
      } catch (error) {
        console.error("Error durante el inicio de sesión:", error);
      }
    },
  },
  modules: {
    criptos,
    transactions,
  },
  plugins: [
    new VuexPersistence({
      storage: window.localStorage,
    }).plugin,
  ],
});
