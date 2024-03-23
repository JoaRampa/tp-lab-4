import { createStore } from "vuex";
import criptos from "./criptos.js";
import logIn from "./logIn.js";

export default createStore({
  state: {},
  getters: {},
  mutations: {},
  actions: {},
  modules: {
    criptos,
    logIn,
  },
});
