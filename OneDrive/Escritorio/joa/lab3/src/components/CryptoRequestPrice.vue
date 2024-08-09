<template>
  <div>
    <div class="cryptos">
      <div class="header p">
        <p><img src="@/assets/b.png" />Coins</p>
      </div>
      <div v-for="crypto in cryptoList" :key="crypto.code" class="header">
        <div class="coin">
          <img
            :src="require(`@/assets/${crypto.code}.png`)"
            :alt="crypto.name"
          />{{ crypto.name }}
        </div>
        <div v-if="crypto.price" class="row price">
          ${{ formatNumber(crypto.price.totalAsk) }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  computed: {
    ...mapGetters([
      "isAuthenticated",
      "getBTCPrice",
      "getETHPrice",
      "getUSDTPrice",
    ]),
    cryptoList() {
      return [
        { code: "btc", name: "BTC", price: this.getBTCPrice },
        { code: "eth", name: "ETH", price: this.getETHPrice },
        { code: "usdt", name: "USDt", price: this.getUSDTPrice },
      ];
    },
  },
  methods: {
    ...mapActions(["CryptoRequestPrice"]),
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
        await this.CryptoRequestPrice();
      } catch (error) {
        console.error("Error:", error);
      }
    },
  },
  async created() {
    await this.fetchData();

    // Actualiza los datos
    setInterval(async () => {
      await this.fetchData();
    }, 1000);
  },
};
</script>

<style scoped>
.cryptos {
  border: 1px solid #35314a;
  border-radius: 15px;
  padding: 15px;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  text-align: center;
  padding: 5px;
  margin-bottom: 10px;
}
.header p {
  background-color: #35314a;
  padding: 10px;
  width: 100%;
  height: 40px;
  display: flex;
  align-items: center;
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
