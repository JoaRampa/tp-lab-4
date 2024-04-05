<template>
  <h1 style="color: beige">Compra Crypto</h1>
  <div class="trade">
    <CryptoPurchase style="margin-right: 15px" />
    <CryptoSale style="margin-left: 15px" />
  </div>
  <CryptoRequestPrice />
</template>
<script>
import CryptoPurchase from "@/components/CryptoPurchase.vue";
import CryptoRequestPrice from "@/components/CryptoRequestPrice.vue";
import CryptoSale from "@/components/CryptoSale.vue";
export default {
  name: "viewPucharse",
  components: {
    CryptoPurchase,
    CryptoRequestPrice,
    CryptoSale,
  },
};
</script>

<style scoped>
.trade {
  display: flex;
  justify-content: center;
}
</style>
