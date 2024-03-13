<template>
  <div class="events">
    <h1>Alumno</h1>
    <form @submit.prevent="ingresarDatos">
      <label>ID:</label>
      <input type="text" v-model="id">
      <br>
      <label>Nombre:</label>
      <input type="text" v-model="nombre">
      <br>
      <button type="submit">Guardar</button>
    </form>
    <ul>
      <li v-for="(persona, index) in personas" :key="index">
        ID: {{ persona.id }} - Nombre: {{ persona.nombre }}
      </li>
    </ul>

    <EventCard v-for="event in alumnos" :key="event.id" :event="event" />
  </div>
</template>

<script>

import EventCard from "@/components/EventCard.vue";
import EventService from '@/services/EventService.js';

export default {
  name: "EvenList",
  data() {
    return {
      id: '',
      nombre: '',
      alumnos: []
    };
  },
  methods: {
    ingresarDatos() {
      const nuevoAlumno = {
        id: this.id,
        nombre: this.nombre
      };
      this.alumnos.push(nuevoAlumno);
      this.id = '';
      this.nombre = '';
      alert("Datos ingresados correctamente.");
    }
  },
  created() {
    EventService.getEvents()
      .then(response => {
        this.alumnos = response.data;
      })
      .catch(error => {
        console.log(error);
      });
  }
};

</script>

