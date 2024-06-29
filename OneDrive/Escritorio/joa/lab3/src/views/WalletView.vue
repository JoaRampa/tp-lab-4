<template>
  <div class="title">
    <b style="font-size: 30px; margin-bottom: 5px">
      <img src="@/assets/mywallet.png" alt="Logo" width="100" />Billetera
    </b>
  </div>
  <ComponentWallet />
</template>
<script>
import ComponentWallet from "@/components/ComponentWallet.vue";
export default {
  name: "WalletView",
  components: {
    ComponentWallet,
  },
};
</script>

<style>
.title {
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
