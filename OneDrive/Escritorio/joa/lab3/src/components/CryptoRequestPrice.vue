<template>
  <div class="contain">
    <div class="cryptos">
      <div class="header">
        <p>Moneda</p>
        <p>Precio de la moneda</p>
        <p>Operar</p>
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
        <a class="row trade" href="/pucharse">Operar</a>
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
.contain {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 15px;
}

.cryptos {
  background-color: rgb(14, 15, 46);
  width: 50%;
  border: 1px solid #35314a;
  border-radius: 15px;
  color: beige;
  padding: 15px;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 15px;
  text-align: center;
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
