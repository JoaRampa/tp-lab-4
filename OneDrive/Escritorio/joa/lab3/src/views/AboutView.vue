<template>
  <div class="About">
    <CryptoRequestPrice />
  </div>
</template>

<script>
import CryptoRequestPrice from "../components/CryptoRequestPrice.vue";

export default {
  name: "AboutView",
  components: {
    CryptoRequestPrice,
  },
};
</script>

<style>
.About {
  cursor: pointer;
  border: 1px solid #39495c;
  display: flex;
  flex-direction: column;
  align-items: center;
}
.About:hover {
  transform: scale(1.01);
  box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0, 2);
}
</style>
