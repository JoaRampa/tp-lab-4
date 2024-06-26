<template>
  <div class="component">
    <h2>Resumen de Inversiones</h2>
    <div>Gastos por compras y ventas: {{ calculateInvestment() }}</div>
    <div>Poseciones: {{ totalCash }}</div>
    <div>Total Ganancias/Perdidas= {{ calculateTotalProfit() }}</div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  data() {
    return {
      transactions: [],
      money: 0,
    };
  },
  computed: {
    ...mapGetters(["userId"]),
    ...mapGetters("transactions", ["getWallet"]),
    ...mapGetters(["getBTCPrice", "getETHPrice", "getUSDTPrice"]),
    totalCash() {
      let total = 0;
      for (let cryptoCode in this.getWallet) {
        total += this.calculateCash(this.getWallet[cryptoCode], cryptoCode);
      }
      return this.formatNumber(total);
    },
  },
  methods: {
    ...mapActions("transactions", ["getState"]),
    ...mapActions("transactions", ["getHistory"]),
    calculateInvestment() {
      let buys = 0;
      let sales = 0;

      for (let index = 0; index < this.transactions.length; index++) {
        let transaction = this.transactions[index];

        if (transaction.action === "purchase") {
          buys -= transaction.money;
        } else if (transaction.action === "sale") {
          sales += transaction.money;
        }
      }

      let invTotal = buys + sales;
      return this.formatNumber(invTotal);
    },
    calculateCash(amount, cryptoCode) {
      const code = cryptoCode.toUpperCase();
      const cryptoGetter = `get${code}Price`;
      const cryptoPrice = this[cryptoGetter];
      if (cryptoPrice) {
        const cash = parseFloat(amount * cryptoPrice.totalBid);
        return cash;
      } else {
        console.error(`Getter ${cryptoGetter} no encontrado`);
        return 0;
      }
    },
    calculateTotalProfit() {
      let totalInvestments = parseFloat(
        this.calculateInvestment().replace(".", "").replace(",", ".")
      );
      let totalCashValue = parseFloat(
        this.totalCash.replace(".", "").replace(",", ".")
      );

      let totalProfit = totalInvestments + totalCashValue;
      return this.formatNumber(totalProfit);
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
    async fetchData() {
      try {
        await this.getState(this.userId);
      } catch (error) {
        console.error("Error:", error);
      }
    },
  },
  async created() {
    try {
      this.transactions = await this.getHistory(this.userId);
      this.fetchData();
    } catch (error) {
      console.error("Error al obtener el historial:", error);
    }
  },
};
</script>

<style scoped>
.component {
  color: beige;
}
</style>
