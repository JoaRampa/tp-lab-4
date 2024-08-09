<template>
  <div class="container">
    <div class="logIn">
      <h4>
        <img
          src="@/assets/logo.png"
          alt="Logo Crypto"
          width="35"
          height="30"
        /><b>CRYPCREATE</b>
      </h4>
      <h3>Inicio de sesión</h3>
      <form @submit.prevent="handleUser" class="id-input" id="form-id">
        <div id="user-error" v-if="error">
          Error, Introduzca un userID valido.
        </div>
        <div class="input-container">
          <input
            @keydown.enter="handleUser"
            v-model="userId"
            type="text"
            id="inputId"
            placeholder="Ingrese UserID"
          />
        </div>
        <button type="submit" id="submit-id">Siguiente</button>
      </form>
    </div>
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
.container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 80vh;
}
.logIn {
  text-align: left;
  background-color: rgb(14, 15, 46);
  border: 1px solid #35314a;
  border-radius: 25px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 28px;
  width: 35%;
}

h4 {
  font-size: 40px;
  font-weight: bold;
}

h3 {
  font-size: 24px;
  margin-bottom: 20px;
}

.input-container {
  margin: 25px 0px 25px;
}

.input-container input {
  width: 100%;
  padding: 10px;
  color: beige;
  border: 1px solid #35314a;
  border-radius: 10px;
  box-sizing: border-box;
  background-color: rgb(14, 15, 46);
}

.input-container:hover {
  border: 1px solid #0f5b9a;
  border-radius: 10px;
  box-sizing: border-box;
}

button {
  color: beige;
  background-color: #0f5b9a;
  border: 20px;
  padding: 12px 20px;
  font-size: 16px;
  cursor: pointer;
  width: 100%;
}

button:hover {
  background-color: rgb(19, 20, 97);
}

#submit-id {
  border-radius: 10px;
}

#user-error {
  color: rgb(255, 0, 0);
  background-color: rgb(14, 15, 46);
  font-size: 12px;
  margin-bottom: 5px;
  padding: 5px;
}
</style>
