<template>
  <div class="transactions">
    <div class="purchase">
      <form id="dataP" @submit.prevent="saveTransactionData">
        <div class="crypto select">
          <select id="crypto" class="inputs" v-model="selectedCrypto" required>
            <option
              v-for="crypto in cryptoList"
              :key="crypto.code"
              :value="crypto.code"
            >
              {{ crypto.name }}
            </option>
          </select>
          <p v-if="selectedCrypto" style="margin-top: 15px">
            {{ formatNumber(selectedCryptoPrice) }}
          </p>
        </div>
        <div class="crypto amount" >
          <label for="amount" >Disponible: {{ getWallet[selectedCrypto] }}</label>
          <input
            type="number"
            id="amount"
            class="inputs"
            v-model="amount"
            @input="totalMoney"
            required
          />
        </div>
        <div class="crypto money">
          <label id="money">Total ${{ formatNumber(money) }}</label>
        </div>
        <button type="submit" class="submit-button">
          Vender
          <img
            :src="require(`@/assets/${selectedCrypto}.png`)"
            :alt="selectedCrypto"
            width="25"
          />
        </button>
      </form>
      <button
        v-if="saveData"
        @click="newTransaction(transactionData)"
        class="confirm-button"
      >
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
      transactionData: null,
      selectedCrypto: "btc",
      saveData: false,
      money: 0,
      amount: 0,
      userWallet: {},
      saleAmount: 0,
    };
  },
  computed: {
    ...mapGetters(["userId", "getBTCPrice", "getETHPrice", "getUSDTPrice"]),
    ...mapGetters("transactions", ["getWallet"]),
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
      return crypto ? `Venta: $${crypto.price.totalBid}` : "";
    },
  },
  methods: {
    ...mapActions("transactions", ["createTransaction", "getState"]),
    saveTransactionData() {
      if (this.amount > 0) {
        if (this.saleAmount >= this.amount) {
          this.transactionData = {
            user_id: this.userId,
            action: "sale",
            crypto_code: this.selectedCrypto,
            crypto_amount: this.amount,
            money: this.money,
            datetime: new Date(),
          };
          this.saveData = true;
        } else {
          alert("Insuficiente");
        }
      }
    },
    async newTransaction(transactionData) {
      try {
        await this.createTransaction(transactionData);
        this.getState();
        this.money = 0;
        this.amount = 0;
      } catch (error) {
        console.error("Error al realizar la nueva transacción:", error);
      }
    },
    getSaleAmount(selectedCrypto) {
      if (!this.getWallet) {
        this.getState();
      }

      const wallet = this.getWallet || {};
      const cryptoCode = selectedCrypto.toLowerCase();
      this.saleAmount = wallet[cryptoCode] || 0;
    },
    totalMoney() {
      const crypto = this.cryptoList.find(
        (crypto) => crypto.code === this.selectedCrypto
      );
      if (this.amount < 0) {
        this.amount = 0;
      }
      this.getSaleAmount(this.selectedCrypto);
      this.money = parseFloat((this.amount * crypto.price.totalBid).toFixed(2));
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
    async created() {
    try {
      await this.getState(this.userId);
    } catch (error) {
      console.error("Error:", error);
    }
  },
  },
};
</script>

<style scoped>
.transactions {
  display: flex;
  justify-content: center;
  align-items: center;
  color: beige;
}

.purchase {
  width: 365px;
  background-color: rgb(14, 15, 46);
  padding: 15px;
  border: 1px solid #35314a;
  border-radius: 15px;
}

.crypto {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin: 10px;
  height: 30px;
}

.submit-button,
.confirm-button {
  background-color: #af1b1b;
  color: beige;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.submit-button:hover,
.confirm-button:hover {
  background-color: rgb(103, 15, 15);
}

.inputs {
  background-color: rgb(14, 15, 46);
  border: 1px solid #35314a;
  color: beige;
  border-radius: 5px;
  width: 70px;
}
</style>
