<template>
  <div class="parrent">
    <div class="child">
      <h2>Register</h2>
      <div class="line">
        <label for="username">Username</label>
        <input name="username" type="text" v-model="username">
      </div>
      <div class="line">
        <label for="password">Password</label>
        <input name="password" type="password" v-model="password">
      </div>
      <!-- <div class="line">
        <label for="password2">Repeat password</label>
        <input name="password2" type="text" v-model="password2">
      </div> -->
      <div class="line">
        <label for="email">Email</label>
        <input name="email" type="text" v-model="email">
      </div>
      <div class="line">
        <label for="phone">Phone</label>
        <input name="phone" type="text" v-model="phone">
      </div>
      <!-- <p>Date of Birth</p> -->
      <button v-on:click="register">Register</button>
      <div class="error">
        <p>{{error}}</p>
      </div>
    </div>
  </div>
</template>

<script>
import * as api from "../api/userAPI.js";
export default {
  name: "Register",
  data() { // short hand for data: function() { //RM
    return {
      username: "",
      password: "",
      password2: "",
      email: "",
      phone: "",
      error: "",
    }
  },
  methods: {
    register() {
      if(!this.username) {
        this.error = "Username is empty";
        return;
      }
      if(!this.password) {
        this.error = "Password is empty";
        return;
      }
      let reInfo = {
        username: this.username,
        password: this.password,
        email: this.email || null,
        phone: this.phone || null,
      }
      console.log(reInfo);
      api.registerUser(reInfo)
            .then(response => {
              if(response.ok) {
                this.$router.push("/login");
              } else {
                // console.log(response.text());
                response.json()
                        .then(value => {
                  if(value["error"]) {
                    this.error = value["error"];
                  } else {
                    this.error = response.statusText;
                  }
                });
              }
            });
      // api.testAPI();


      // console.log("register methods");
      // this.$router.push({name: "About"});
    },

  }

}
</script>
  
<style scoped>
  input{
    width: 300px;
    max-width: 400px;
    padding: 0.35rem;
  }

  .parrent{
    display: flex;
    justify-content: center;
    align-items: center;
  }
  .child {
    /* border-radius: 24px; */
    /* border-width: 10px; */
    /* border-color: black; */
  }
  .line {
    /* width: 250px; */
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    justify-content: flex-start;
    /* justify-content: space-evenly; */
    margin: 15px;
  }
  .inline1 {
    /* width: 85px; */

    /* flex-basis: 1; */
  }
  .inline2 {
    /* flex-basis: 4; */
  }
  .mrg-h {
    margin: 20px 0px 0px 0px;
  }

  .error {
    margin: 15px;
    color: red;
    font-size: 13px;
  }
</style>