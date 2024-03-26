<template>
  <div class="logIn">
    <form @submit.prevent="handleUser" class="id-input" id="form-id">
      <div id="user-error" v-if="error">
        Ingrese un userId para poder ingresar.
      </div>
      <div class="input-container">
        <input
          @keydown.enter="handleUser"
          v-model="userId"
          type="text"
          placeholder="Ingrese su ID de usuario"
          id="inputId"
        />
      </div>

      <button type="submit" id="submit-id">Submit</button>
    </form>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      userId: "",
      error: false,
    };
  },
  methods: {
    ...mapActions(["submitUser"]),

    async handleUser() {
      if (this.userId) {
        this.error = false;
        await this.submitUser(this.userId);
        this.$router.push("/#");
      } else {
        this.error = true;
        this.userId = "";
      }
    },
  },
};
</script>

<style>
.logIn {
  background-color: #1e2750; /* Fondo azul oscuro */
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  padding: 20px;
  width: 300px;
  margin: 0 auto;
}

.id-input {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.input-container {
  margin-bottom: 15px; /* Margen entre los inputs */
}

.input-container input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-sizing: border-box;
  background-color: #fff; /* Fondo blanco */
}

button {
  width: 62%;
  padding: 10px;
  border: none;
  border-radius: 5px;
  background-color: #007bff;
  color: #fff;
  cursor: pointer;
}

button:hover {
  background-color: purple;
}

#user-error {
  width: 59%;
  color: rgba(255, 255, 255, 0.815);
  background-color: purple;
  font-size: 12px;
  margin-bottom: 5px;
  border: 1px solid black;
  border-radius: 10px;
  padding: 5px;
}
</style>
