<template>
  <div class="contain">
    <div class="crypto mx-auto">
      <div class="crypto-header">
        <p>Moneda</p>
        <p>Precio de la moneda</p>
        <p>Operar</p>
      </div>
      <div v-for="crypto in cryptoList" :key="crypto.code" class="crypto-row">
        <div>
          <img
            :src="require(`@/assets/${crypto.code}.png`)"
            :alt="crypto.name"
          />{{ crypto.name }}
        </div>
        <div v-if="crypto.price" class="price">
          ${{ formatNumber(crypto.price.totalAsk) }}
        </div>
        <a href="/">Operar</a>
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
  },
  created() {
    this.CryptoRequestPrice();
  },
};
</script>

<style scoped>
.contain {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 80vh;
}

.crypto {
  background-color: rgb(14, 15, 46);
  width: 50%;
  border: 1px solid #35314a;
  border-radius: 15px;
}

.crypto-header {
  display: flex;
  justify-content: space-between;
  padding: 15px;
  color: gray;
}

.crypto-row {
  display: flex;
  padding: 15px;
  color: beige;
  justify-content: space-between;
}

p:hover {
  color: beige;
}

img {
  padding: 5px;
  width: 35px;
  height: 30x;
}
</style>
