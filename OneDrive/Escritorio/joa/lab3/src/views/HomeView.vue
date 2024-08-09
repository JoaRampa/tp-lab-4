<template>
  <div class="home">
    <LogIn v-if="!isAuthenticated" />
  </div>
  <div v-if="isAuthenticated">
    <div class="view">
      <img src="@/assets/bchain.png" alt="image" style="width: 100px" />
      <b style="font-size: 40px">Trading</b>
    </div>
    <div class="view">
      <div class="cripto"><CryptoRequestPrice /></div>
      <div class="operation">
        <div class="tabs">
          <button
            style="border-top-left-radius: 15px"
            :class="{ active: currentTab === 'purchase' }"
            @click="currentTab = 'purchase'"
          >
            Compra Crypto
          </button>
          <button
            style="border-top-right-radius: 15px"
            :class="{ active: currentTab === 'sale' }"
            @click="currentTab = 'sale'"
          >
            Venta Crypto
          </button>
        </div>
        <div class="tab-content">
          <component :is="currentTabComponent" style="margin: 0 15px" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import LogIn from "@/components/LoginForm.vue";
import CryptoRequestPrice from "@/components/CryptoRequestPrice.vue";
import CryptoPurchase from "@/components/CryptoPurchase.vue";
import CryptoSale from "@/components/CryptoSale.vue";
import { mapGetters } from "vuex";
export default {
  name: "HomeView",
  components: {
    CryptoPurchase,
    CryptoSale,
    LogIn,
    CryptoRequestPrice,
  },
  data() {
    return {
      currentTab: "purchase",
    };
  },
  computed: {
    ...mapGetters(["isAuthenticated"]),
    currentTabComponent() {
      return this.currentTab === "purchase" ? "CryptoPurchase" : "CryptoSale";
    },
  },
};
</script>

<style scoped>
.view {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 5px;
}
.cripto {
  width: 35%;
  margin-right: 30px;
}
.operation {
  width: 45%;
  border: 1px solid #35314a;
  border-radius: 15px;
  margin-left: 35px;
}
.tabs {
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
}

.tabs button {
  background-color: rgb(14, 15, 46);
  cursor: pointer;
  transition: background-color 0.8s;
}

.tabs button.active,
.tabs button:hover {
  background-color: rgb(15, 19, 78);
}
</style>
