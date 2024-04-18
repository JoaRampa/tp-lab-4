import axios from "axios";

const state = {
  criptos: {
    btc: null,
    eth: null,
    usdt: null,
  },
};

const getters = {
  ...Object.keys(state.criptos).reduce((acc, criptoRequest) => {
    acc[`get${criptoRequest.toUpperCase()}Price`] = (state) =>
      state.criptos[criptoRequest];
    return acc;
  }, {}),
};

const mutations = {
  criptoPrice(state, { criptoRequest, data }) {
    state.criptos[criptoRequest] = data;
  },
};

const actions = {
  async CryptoRequestPrice({ commit, state }) {
    try {
      for (const criptoRequest in state.criptos) {
        const criptoResponse = await axios.get(
          `https://criptoya.com/api/argenbtc/${criptoRequest}/ars/1`
        );
        commit("criptoPrice", { criptoRequest, data: criptoResponse.data });
      }
    } catch (error) {
      console.log("Error fetching cryptocurrency prices:", error);
    }
  },
};

export default {
  state,
  getters,
  mutations,
  actions,
};
