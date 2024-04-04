import axios from "axios";
const API_BASE_URL = "https://labor3-d60e.restdb.io/rest/transactions";
const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: { "x-apikey": "64a2e9bc86d8c525a3ed8f63" },
});

const state = {
  wallet: {},
};

const getters = {
  getWallet: (state) => state.wallet,
};

const mutations = {
  updateCryptoAmount(state, { cryptoCode, amount, action }) {
    if (!state.wallet[cryptoCode]) {
      state.wallet[cryptoCode] = 0;
    }

    state.wallet[cryptoCode] += action === "purchase" ? amount : -amount;

    if (state.wallet[cryptoCode] < 0) {
      state.wallet[cryptoCode] = 0;
    }
  },

  setWallet(state, newWallet) {
    state.wallet = { ...newWallet };
  },
};

const actions = {
  async createTransaction({ commit }, purchaseData) {
    try {
      console.log("Transaction Data:", purchaseData);
      const response = await apiClient.post("", purchaseData);

      commit("updateCryptoAmount", {
        cryptoCode: purchaseData.crypto_code,
        amount: purchaseData.crypto_amount,
        action: purchaseData.action,
      });

      return response.data;
    } catch (error) {
      console.error("Error al crear la transacción:", error);
    }
  },
  async getState({ commit }, userId) {
    try {
      const response = await apiClient.get(
        `${API_BASE_URL}?q={"user_id":"${userId}"}`
      );
      const userCriptos = {};

      for (const transaction of response.data) {
        if (transaction.crypto_code && transaction.action) {
          const cryptoCode = transaction.crypto_code.toLowerCase();

          if (!userCriptos[cryptoCode]) {
            userCriptos[cryptoCode] = 0;
          }
          userCriptos[cryptoCode] +=
            transaction.action === "purchase"
              ? parseFloat(transaction.crypto_amount)
              : -parseFloat(transaction.crypto_amount);
          if (userCriptos[cryptoCode] === 0) {
            delete userCriptos[cryptoCode];
          }
        }
      }
      commit("setWallet", userCriptos);
    } catch (error) {
      console.error("Error al devolver el estado de la cuenta:", error);
    }
  },
};

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions,
};
