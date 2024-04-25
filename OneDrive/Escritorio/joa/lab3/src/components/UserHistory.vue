<template>
  <div class="history-page">
    <h1>
      Historial de transacciones de <b style="font-size: 35px">{{ userId }}</b>
    </h1>
    <div class="container-xs">
      <div
        class="transaction"
        v-for="(transaction, index) in transactions"
        :key="transaction.datetime"
      >
        <button
          class="btn btn-primary"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#collapseExample"
          aria-expanded="false"
          aria-controls="collapseExample"
        >
          <p>Transacción {{ index + 1 }} {{ transaction.crypto_code }}</p>
        </button>

        <div class="collapse" id="collapseExample">
          <div
            class="transaction-box"
            :class="{ purchase: transaction.action === 'purchase' }"
          >
            <p>Acción: {{ transaction.action }}</p>
          </div>
          <p>Cantidad: {{ transaction.crypto_amount }}</p>
          <p>Pesos ${{ transaction.money }}</p>
          <p>Id: {{ transaction._id }}</p>
          <p>Fecha: {{ transaction.datetime }}</p>
          <button
            @click="saveTransactionId(transaction._id)"
            class="btn btn-primary"
            data-bs-toggle="modal"
            data-bs-target="#confirmEdit"
          >
            Editar Nº{{ index + 1 }}
          </button>
          <button
            @click="saveTransactionId(transaction._id)"
            class="btn btn-danger"
            data-bs-toggle="modal"
            data-bs-target="#confirmDelete"
          >
            Eliminar Nº{{ index + 1 }}
          </button>
        </div>
      </div>
      <div class="modal" id="confirmDelete">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title">Eliminar transacción</h5>
              <button class="btn-close" data-bs-dismiss="modal"></button>
            </div>

            <div class="modal-body">
              <h6>Confirme que está seguro de eliminar esta transacción.</h6>
            </div>

            <div class="modal-footer">
              <button class="btn btn-secondary" data-bs-dismiss="modal">
                Cancelar
              </button>
              <button
                @click="deleteTransactionLocal(deleteTransactionId)"
                data-bs-dismiss="modal"
                class="btn btn-danger"
              >
                Eliminar
              </button>
            </div>
          </div>
        </div>
      </div>
      <!-- -->
      <div class="modal" id="confirmEdit">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title">Editar transacción</h5>
              <button class="btn-close" data-bs-dismiss="modal"></button>
            </div>

            <div class="modal-body">
              <h6>Ingrese el nuevo monto de cripto:</h6>
              <input
                class="input"
                type="number"
                id="crypto_amount"
                v-model="crypto_amount"
                min="0"
                step="0.01"
              />

              <h6>Ingrese el nuevo monto de ARS:</h6>
              <input
                class="input"
                type="number"
                id="money"
                v-model="money"
                min="0"
                step="0.01"
              />
            </div>

            <div class="modal-footer">
              <button class="btn btn-secondary" data-bs-dismiss="modal">
                Cancelar
              </button>
              <button
                @click="
                  editTransactionLocal(editTransactionId, crypto_amount, money)
                "
                class="btn btn-primary"
                data-bs-dismiss="modal"
              >
                Editar
              </button>
            </div>
          </div>
        </div>
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
      deleteTransactionId: null,
      editTransactionId: null,
      crypto_amount: 0,
      money: 0,
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
    ...mapActions("transactions", ["getHistory", "deleteTransaction"]),
    async deleteTransactionLocal(transactionId) {
      try {
        await this.deleteTransaction(transactionId);

        this.transactions = this.transactions.filter(
          (transaction) => transaction._id !== transactionId
        );
        this.updateHistory();
      } catch (error) {
        console.error("Error al eliminar la transacción:", error);
      }
    },
    async updateHistory() {
      try {
        const response = await this.getHistory(this.userId);
        console.log(response);
        this.transactions = response.sort(
          (a, b) => new Date(b.datetime) - new Date(a.datetime)
        );
      } catch (error) {
        console.error("Error al obtener el historial:", error);
      }
    },
    async editTransactionLocal(transactionId, crypto_amount, money) {
      try {
        const newValues = {};

        if (crypto_amount > 0) {
          newValues.crypto_amount = crypto_amount;
        }

        if (money > 0) {
          newValues.money = money;
        }

        if (Object.keys(newValues).length > 0) {
          await this.$store.dispatch("transactions/editTransaction", {
            transactionId,
            newValues,
          });
          this.updateHistory();
        } else {
          console.log(
            "No se realizaron cambios ya que los valores no son mayores a 0."
          );
        }
      } catch (error) {
        console.error("Error al editar la transacción:", error);
      }
    },
    saveTransactionId(transactionId) {
      this.deleteTransactionId = transactionId;
      this.editTransactionId = transactionId;
    },
  },
};
</script>

<style>
.history-page {
  color: beige;
}
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
