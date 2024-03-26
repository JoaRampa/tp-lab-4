import { createStore } from "vuex";
import criptos from "./criptos.js";
import VuexPersistence from "vuex-persist";

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
  },
  plugins: [
    new VuexPersistence({
      storage: window.localStorage,
    }).plugin,
  ],
});
