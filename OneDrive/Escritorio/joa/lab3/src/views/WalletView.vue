<template>
  <h1 style="color: beige">My Assets</h1>
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
