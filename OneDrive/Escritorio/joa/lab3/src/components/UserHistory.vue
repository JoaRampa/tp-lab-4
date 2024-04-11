<template>
  <div class="history-page">
    <h1>Historial de transacciones de {{ userId }}</h1>
    <div class="container-xs">
      <div
        class="transaction"
        v-for="(transaction, index) in transactions"
        :key="transaction.datetime"
      >
        <button @click="showMenu = true" class="transaction">
          <p>Transacción {{ transactions.length - index }}</p>
        </button>

        <div
          v-show="showMenu"
          class="transaction"
          v-if="transactions.length > 0"
        >
          <div
            class="transaction-box"
            :class="{ purchase: transaction.action === 'purchase' }"
          >
            <p>Tipo: {{ transaction.crypto_code }}</p>
            <p>Acción: {{ transaction.action }}</p>
          </div>
          <p>Cantidad: {{ transaction.crypto_amount }}</p>
          <p>Pesos ${{ transaction.money }}</p>
          <p>Id: {{ transaction._id }}</p>
          <p>Fecha: {{ transaction.datetime }}</p>
        </div>
        <h3 v-else>Historial vacío</h3>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  data() {
    return {
      transactions: [],
      showMenu: false,
    };
  },

  computed: {
    ...mapGetters(["userId"]),
  },

  async created() {
    try {
      this.transactions = await this.getHistory(this.userId);
    } catch (error) {
      console.error("Error al obtener el historial:", error);
    }
  },

  methods: {
    ...mapActions("transactions", ["getHistory"]),
  },
};
</script>

<style>
.container-xs {
  display: flex;
  flex-wrap: wrap;
}

.transaction {
  background-color: rgb(14, 15, 46);
  border: 1px solid #35314a;
  border-radius: 5px;
  flex: 0 0 20%;
  box-sizing: border-box;
}
</style>
