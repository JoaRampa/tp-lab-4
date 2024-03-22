export default {
  state: {
    userId: null,
  },
  getters: {
    userId: (state) => state.userId,
  },
  mutations: {
    submitUser(state, userId) {
      state.userId = userId;
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
};
