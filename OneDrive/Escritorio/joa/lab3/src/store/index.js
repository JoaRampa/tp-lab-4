import { createStore } from "vuex";
import criptos from "./criptos";
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
