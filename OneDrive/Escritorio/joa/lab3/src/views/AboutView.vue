<template>
  <div class="About" v-for="evento in coins" :key="evento.id" :evento="evento">
    <p>{{ evento.title }} on {{ evento.bid }}</p>
  </div>
</template>

<script>
export default {
  name: 'About',
  props:{
    coins: Object
  },
  data(){
    return {
      coins: [
      { 
        id:1234,
        title: "argenbtc",
        ask: 76328103.67,
        totalAsk: 76328103.67,
        bid: 73201070,
        totalBid: 73201070,
        time: 1710272453
      },{
        id:5668,
        title: "binancep2p", 
        ask: 84765241.62,
        totalAsk: 84765241.62,
        bid: 75760165.97,
        totalBid: 75760165.97,
        time: 1710352637
      }]   
    }
  }
}
</script>

<style >
.About{
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