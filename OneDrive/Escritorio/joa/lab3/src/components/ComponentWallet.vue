<template>
  <div>
    <h2>Billetera de {{ userId }}</h2>
    <div class="row">
      <div v-if="Object.keys(getWallet).length === 0">
        <p>No hay elementos en la billetera.</p>
      </div>
      <div
        v-else
        class="col"
        v-for="(amount, cryptoCode) in getWallet"
        :key="cryptoCode"
      >
        <b>{{ cryptoCode.toUpperCase() }}</b>
        <p>{{ amount }}</p>
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
  },
  methods: {
    ...mapActions("transactions", ["getState"]),
  },
  async created() {
    try {
      await this.getState(this.userId);
    } catch (error) {
      console.error("Error:", error);
    }
  },
};
</script>
