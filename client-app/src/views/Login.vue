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
        <p class="error">{{error}}</p>
      </form>
    </div>
  </div>
</template>

<script>
import {userAuth} from "../api/userAPI.js";
export default {
  name: "Login",
  data: function() {
    return {
      username: "",
      password: "",
      error: "",
    }
  },
  methods: {
    nameOfMethod(e) {
      // e.preventDefault();
      // console.log(e.target);
      // api.testAPI("nncuong", 123);
      // console.log({
      //   username: this.username,
      //   password: this.password
      // });
      if(!this.username) {
        this.error = "Username cant be empty";
        return;
      }
      if(!this.password) {
        this.error = "Password cant be empty";
        return;
      }
      const info = {
        username: this.username,
        password: this.password
      };

      userAuth(info).then(res => {
        let value;

        if(res.ok) {
          value = res.json()
                      .catch(e => {
                          console.log(e);
                          this.error=e;
                      });
          value.then(val => {
            if(!val["token"]) throw "Not Logined";
            localStorage.setItem("userInfo", JSON.stringify(val));
            this.$emit("loged-in");
            this.$router.push("/chat");
          });
        } else {
          value.then(err => {
            if(err["error"]) {this.error = err["error"];}
            else {this.error = res.text;}
          }).catch(e => {
            console.log(e);
            this.error = "Error"
          })
        }
      }).catch(e => console.log(e));

      // this.$router.push({name: "About"});
    },
  }
}
</script>

<style scoped>
  input{
    width: 300px;
    padding: 0.35rem;
    max-width: 400px;
  }

  /* button {
    border: none;
  } */

  .parrent {
    display: flex;
    justify-content: center;
    align-items: center;
  }
  .child {
    /* width: 300px;
    padding: 40px; */
    /* background-color: #b15ebb;
    color: white; */
    /* border-radius: 20px; */
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
    /* width: 85px;
    flex-basis: 1; */
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
  .error {
    margin: 15px;
    color: red;
    font-size: 13px;
  }
</style>