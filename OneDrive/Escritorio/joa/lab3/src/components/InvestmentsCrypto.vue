<template>
  <div>
    <h2>Resumen de Inversiones</h2>
    <div>Total: ${{ totalCash }}</div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
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
  created() {
    this.fetchData(); // Llama a fetchData al crear el componente para obtener los datos iniciales
  },
};
</script>

<style scoped>
/* Estilos según necesidades */
</style>
