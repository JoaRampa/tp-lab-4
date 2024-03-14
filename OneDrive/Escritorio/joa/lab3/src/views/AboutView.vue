<template>
    <div class="About">
      <EventCard v-for="event in coins" :key="event.title" :event="event"/>
    </div>
</template>
<script>
import EventCard from '@/components/EventCard.vue'
export default {
  name: 'About',
  components: {
    EventCard
  },
  data(){
    return {
      coins: [
      { 
        title: "argenbtc",
        ask: 76328103.67,
        totalAsk: 76328103.67,
        bid: 73201070,
        totalBid: 73201070,
        time: 1710272453
      },{
        title: "binancep2p", 
        ask: 84765241.62,
        totalAsk: 84765241.62,
        bid: 75760165.97,
        totalBid: 75760165.97,
        time: 1710352637}]   
    }
  }
}
</script>
<style scoped>
.About{
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>