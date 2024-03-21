<template>
  <div class="events">
    <LogIn />
  </div>
</template>

<script>
import LogIn from "@/components/LoginForm.vue";
import EventService from "@/services/EventService";

export default {
  name: "EvenList",
  components: {
    LogIn,
  },
  data() {
    return {
      events: null,
    };
  },
  created() {
    EventService.getEvents()
      .then((response) => {
        this.events = response.data;
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
</script>

<style>
.gargo {
  background-color: rgb(10, 2, 28);
}
.events {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
