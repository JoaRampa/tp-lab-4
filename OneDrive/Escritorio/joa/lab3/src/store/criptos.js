import EventService from "@/services/EventService";

export default {
  namespaced: true,
  state: {
    evento: [],
  },
  getters: {
    evento(state) {
      return state.evento;
    },
  },
  mutations: {
    MostrarCriptos(state, data) {
      state.evento = data;
    },
  },
  actions: {
    cargarCriptos: async function ({ commit }) {
      try {
        const response = await EventService.getApis();
        commit("MostrarCriptos", response.data);
      } catch (error) {
        console.log(error);
      }
    },
  },
};
