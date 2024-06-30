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
        <div class="crypto amount">
          <label for="amount"
            >Cantidad de -{{ selectedCrypto.toLocaleUpperCase() }}</label
          >
          <input
            type="text"
            id="amount"
            class="inputs"
            v-model="amount"
            @input="
              totalMoney();
              validateInput();
            "
            required
          />
        </div>
        <div class="crypto money">
          <label id="money">Total ${{ formatNumber(money) }}</label>
        </div>
        <div class="btn-save">
          <div class="divPurchase">
            <button
              class="btn btn-outline-light"
              type="submit"
              :disabled="amount === 0"
              data-bs-target="#confirmPurchase"
              data-bs-toggle="modal"
            >
              Comprar
              <img
                :src="require(`@/assets/${selectedCrypto}.png`)"
                :alt="selectedCrypto"
                width="25"
              />
            </button>
          </div>

          <div class="modal" id="confirmPurchase">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title">Confirmar Compra</h5>
                  <button
                    type="button"
                    class="btn-close"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                  ></button>
                </div>
                <div class="modal-body">
                  <p>¿Quiere confirmar la compra?</p>
                </div>
                <div class="modal-footer">
                  <button
                    type="button"
                    class="btn btn-secondary"
                    data-bs-dismiss="modal"
                  >
                    Cerrar
                  </button>
                  <button
                    type="button"
                    class="btn btn-primary"
                    data-bs-dismiss="modal"
                    @click="newTransaction(transactionData)"
                  >
                    Confirmar
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </form>
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
      decimalPart: true,
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
      return crypto ? `Compra: $${crypto.price.totalAsk}` : "";
    },
  },
  methods: {
    ...mapActions("transactions", ["createTransaction"]),
    saveTransactionData() {
      if (this.money > 0 && this.amount > 0) {
        this.transactionData = {
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
    async newTransaction(transactionData) {
      try {
        await this.createTransaction(transactionData);
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
    validateInput() {
      const regex = /^\d+(\.\d{0,6})?$/;
      if (!regex.test(this.amount)) {
        this.decimalPart = false;
      } else {
        this.decimalPart = true;
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
}

.purchase {
  width: 100%;
  padding: 15px;
}

.crypto {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 35px;
  height: 30px;
}

.divPurchase button {
  background-color: #10ac4c;
  color: beige;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.divPurchase button:hover {
  background-color: rgb(11, 118, 45);
}

.inputs {
  background-color: rgb(14, 15, 46);
  border: 1px solid #35314a;
  color: beige;
  border-radius: 5px;
  width: 70px;
}

.modal-content {
  color: black;
}
</style>
