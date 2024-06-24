import axios from "axios";
const API_BASE_URL = "https://laboratorio-afe2.restdb.io/rest/transactions";
const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: { "x-apikey": "650b53356888544ec60c00bf" },
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
  async getHistory(_, userId) {
    try {
      const response = await apiClient.get(
        `${API_BASE_URL}?q={"user_id":"${userId}"}`
      );
      return response.data;
    } catch (error) {
      console.error("Error al devolver el historial:", error);
    }
  },

  async deleteTransaction({ commit }, transactionId) {
    try {
      const response = await apiClient.delete(
        `${API_BASE_URL}/${transactionId}`
      );
      const { crypto_code, crypto_amount, action } = response.data;
      commit("updateCryptoAmount", {
        cryptoCode: crypto_code,
        amount: crypto_amount,
        action: action === "purchase" ? "sale" : "purchase",
      });
    } catch (error) {
      console.error("Error al eliminar la transacción:", error);
    }
  },
  async editTransaction({ commit }, { transactionId, newValues }) {
    try {
      const response = await apiClient.patch(
        `${API_BASE_URL}/${transactionId}`,
        newValues
      );
      const { crypto_code, crypto_amount, money, action } = response.data;

      const newAmount = action === "purchase" ? -crypto_amount : crypto_amount;
      const newMoney = action === "purchase" ? -money : money;

      commit("updateCryptoAmount", {
        cryptoCode: crypto_code,
        amount: newAmount,
        money: newMoney,
        action,
      });
    } catch (error) {
      console.error("Error al editar la transacción:", error);
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
