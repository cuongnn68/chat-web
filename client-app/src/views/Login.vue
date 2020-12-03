<template>
  <div class="parrent">
    <div class="child">
      <form v-on:submit.prevent="nameOfMethod($event)" class="">
        <h1 class="" >Login</h1>
        <div class="mrg-h line">
          <label class="inline1" for="username">Username</label> 
          <!-- TODO: add space between inline1 & inline2 -->
          <input class="inline2" type="text" name="username" v-model="username">
        </div>
        <div class="mrg-h line">
          <label class="inline1" for="password">Password</label>
          <input class="inline2" type="password" v-model="password">
        </div>
        <button class="mrg-h" >Login</button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "Login",
  data: function() {
    return {
      username: "",
      password: ""
    }
  },
  methods: {
    nameOfMethod(e) {
      // e.preventDefault();
      console.log({
        username: this.username,
        password: this.password
      });
      //TODO: call api (post method) to athenticate, then $emit
    },
  }
}
</script>

<style scoped>
  .parrent {
    display: flex;
    justify-content: center;
    align-items: center;
  }
  .child {
    width: 300px;
    padding: 40px;
    background-color: #b15ebb;
    color: white;
    border-radius: 20px;
  }
  .line {
    /* width: 250px; */
    display: flex;
    flex-direction: row;
    justify-content: center;
    /* justify-content: space-evenly; */
  }
  .inline1 {
    width: 85px;
    flex-basis: 1;
  }
  .inline2 {
    flex-basis: 4;
  }
  .mrg-h {
    margin: 20px 0px 0px 0px;
  }
  button {
    border: none;
  }
</style>