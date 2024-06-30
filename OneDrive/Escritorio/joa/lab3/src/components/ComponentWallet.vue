<template>
  <div class="contain">
    <div class="cryptos">
      <div class="contain">
        <img src="@/assets/logo.png" style="width: 50px" />
        <b style="font-size: 40px">{{ userId }}</b>
      </div>
      <div class="header p">
        <p><img src="@/assets/b.png" />Coins</p>
        <p>Amount</p>
        <p>Cash in</p>
      </div>
      <div v-if="Object.keys(getWallet).length === 0">
        <p>No hay elementos en la billetera.</p>
      </div>
      <div
        v-else
        v-for="(amount, cryptoCode) in getWallet"
        :key="cryptoCode"
        class="header"
      >
        <div class="coin">
          <img :src="require(`@/assets/${cryptoCode}.png`)" :alt="cryptoCode" />
          {{ cryptoCode.toUpperCase() }}
        </div>
        <div class="amount">{{ amount }}</div>
        <div>${{ calculateCash(amount, cryptoCode) }}</div>
      </div>
      <div class="header p" style="align-items: right">
        <p>Total: ${{ totalCash }}</p>
      </div>
    </div>
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
    mounted() {
      // Iniciar la actualización cada 10 segundos
      setInterval(async () => {
        await this.fetchData();
      }, 10000); // 10000 milisegundos = 10 segundos
    },
  },
};
</script>

<style scoped>
.contain {
  display: flex;
  justify-content: center;
  align-items: center;
}

.cryptos {
  width: 50%;
  border: 1px solid #35314a;
  border-radius: 15px;
  padding: 15px;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 15px;
  text-align: left;
}

.header > * {
  flex: 1;
}

.header p {
  background-color: #35314a;
  padding: 10px;
  height: 40px;
  display: flex;
  align-items: center;
}

.header p image {
  width: 35px;
}

img {
  padding: 5px;
  width: 35px;
}

.coin {
  display: flex;
  align-items: center;
}
</style>
