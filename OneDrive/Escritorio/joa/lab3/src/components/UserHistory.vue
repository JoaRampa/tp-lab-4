<template>
  <div class="history-page">
    <h1>Historial de transacciones de {{ userId }}</h1>
    <div class="container-xs">
      <ul v-if="transactions.length > 0">
        <li
          v-for="(transaction, index) in transactions"
          :key="transaction.datetime"
        >
          <div class="transaction">
            <p>Transacción {{ transactions.length - index }}</p>
            <div
              class="transaction-box"
              :class="{ purchase: transaction.action === 'purchase' }"
            >
              <p>Tipo de cripto: {{ transaction.crypto_code }}</p>
              <p>Acción realizada: {{ transaction.action }}</p>
            </div>
          </div>
          <p>Cantidad de cripto: {{ transaction.crypto_amount }}</p>
          <p>Dinero en pesos: {{ transaction.money }}</p>
          <p>Id de la transacción: {{ transaction._id }}</p>
          <p>Fecha: {{ transaction.datetime }}</p>
        </li>
      </ul>
      <h3 v-else>Historial vacío</h3>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  data() {
    return {
      transactions: [],
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

<style></style>
