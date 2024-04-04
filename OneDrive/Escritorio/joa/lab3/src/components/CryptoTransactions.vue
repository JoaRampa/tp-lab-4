<template>
  <div class="transactions">
    <div class="purchase">
      <form id="dataP" @submit.prevent="savePurchaseData">
        <div>
          <select id="crypto" v-model="selectedCrypto" required>
            <option
              v-for="crypto in cryptoList"
              :key="crypto.code"
              :value="crypto.code"
            >
              {{ crypto.name }}
            </option>
          </select>
          <p v-if="selectedCrypto">{{ formatNumber(selectedCryptoPrice) }}</p>
        </div>
        <div>
          <label for="amount">Cantidad {{ selectedCrypto }}</label>
          <input
            type="number"
            id="amount"
            v-model.number="amount"
            @input="totalMoney"
            required
          />
        </div>
        <div>
          <label id="money">Total ${{ formatNumber(money) }}</label>
        </div>
        <button type="submit">Submit</button>
      </form>
      <button v-if="saveData" @click="newTransaction(purchaseData)">
        Confirmar
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  data() {
    return {
      purchaseData: null,
      selectedCrypto: null,
      saveData: false,
      money: 0,
      amount: 0,
    };
  },
  computed: {
    ...mapGetters(["userId", "getBTCPrice", "getETHPrice", "getUSDTPrice"]),
    cryptoList() {
      return [
        { code: "btc", name: "BTC", price: this.getBTCPrice },
        { code: "eth", name: "ETH", price: this.getETHPrice },
        { code: "usdt", name: "USDT", price: this.getUSDTPrice },
      ];
    },
    selectedCryptoPrice() {
      const crypto = this.cryptoList.find(
        (crypto) => crypto.code === this.selectedCrypto
      );
      return crypto ? `Precio: ${crypto.price.totalAsk}` : "";
    },
  },
  methods: {
    ...mapActions("transactions", ["createTransaction"]),
    savePurchaseData() {
      if (this.money > 0 && this.amount > 0) {
        this.purchaseData = {
          user_id: this.userId,
          action: "purchase",
          crypto_code: this.selectedCrypto,
          crypto_amount: this.amount,
          money: this.money,
          datetime: new Date(),
        };
        this.saveData = true;
      }
    },
    async newTransaction(purchaseData) {
      try {
        await this.createTransaction(purchaseData);
        this.money = 0;
        this.amount = 0;
      } catch (error) {
        console.error("Error al realizar la nueva transacción:", error);
      }
    },
    totalMoney() {
      const crypto = this.cryptoList.find(
        (crypto) => crypto.code === this.selectedCrypto
      );
      if (this.amount < 0) {
        this.amount = 0;
      }
      this.money = parseFloat((this.amount * crypto.price.totalAsk).toFixed(2));
    },
    formatNumber(number) {
      if (typeof number === "undefined") {
        return "";
      }
      const numStr = number.toString();
      const parts = numStr.split(".");
      parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ".");
      return parts.join(",");
    },
  },
};
</script>

<style>
.transactions {
  color: beige;
}
</style>
