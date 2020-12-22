<template>
  <div class="parent">
    <div class="child">

      <div class="info">
        <h2>User info</h2>
        <div class="in">
          <label for="id">ID</label>
          <input name="id" type="text" disabled v-model="user.id">
        </div>
        <div class="in">
          <label for="username">Username</label>
          <input name="username" type="text" disabled v-model="user.username">
        </div>
        <div class="in">
          <label for="dob">Date of Birth</label>
          <input name="dob" type="text" disabled v-model="user.dob">
        </div>
        <div class="in">
          <label for="full-name">Full name</label>
          <input name="full-name" type="text" v-model="user.fullName" v-bind:disabled="!isOwner">
        </div>
        <div class="in">
          <label for="email">Email</label>
          <input name="email" type="text" v-model="user.email" v-bind:disabled="!isOwner">
        </div>
        <div class="in">
          <label for="phone">Phone</label>
          <input name="phone" type="text" v-model="user.phone" v-bind:disabled="!isOwner">
        </div>
        <button class="btn" v-if="isOwner" v-on:click="updateUser">Update</button>
      </div>


      <!-- TODO wait when can logout other token on server -->
      <div v-if="false&&isOwner" class="pass">
        <h2>Change password</h2>
        <div class="in">
          <label for="old-pass">Old password</label>
          <input name="old-pass" type="password">
        </div>
        <div class="in">
          <label for="new-pass">New password</label>
          <input name="new-pass" type="password">
        </div>
        <div class="in">
          <label for="re-new-pass">Repeat new passwrod</label>
          <input name="re-new-pass" type="password">
        </div>
        <button class="btn">Update</button>
      </div>

    </div>
  </div>
</template>

<script>
import * as st from "../utils/storage.js";
import * as userAPI from "../api/userAPI.js";
export default {
  name: "UserInfo",
  created() {
    this.userId = this.$route.params.id;
    this.getUserInfo()
  },
  methods: {
    getUserInfo() {
      userAPI.getInfo(this.$route.params.id)
            .then(res => {
              if(res.ok) {
                res.json().then(val => {
                    console.log(val);
                    this.user.id = val.id;
                    this.user.username = val.username;
                    this.user.fullName = val.fullName;
                    this.user.email = val.email;
                    this.user.phone = val.phone;
                    this.user.dob = val.dateOfBirth;
                    this.isOwner = st.getUser().id === val.id;
                  }).catch(e => console.log(e));
              } else {
                this.$router.push("/home");
                this.newNoti("Cant load user", "red");
              }
            })
            .catch(e => {
              console.log(e);
              this.newNoti("ERROR", "red");
            });
    },
    updateUser() {
      userAPI.updateUser(this.user.id, this.user.fullName||"", this.user.email||"", this.user.phone||"")
            .then(res => {
              if(res.ok) {
                this.newNoti("Updated user info", "green");
              } else {
                res.json().then(val => this.newNoti(val.error, "red"));
              }
            })
            .catch(e => {
              console.log(e);
              this.newNoti("ERROR cant update", "red");
            });
    },
    newNoti(content, color) {
      this.$emit("newNoti", {content, type: color});
    }
  },
  data: function() {
    return {
      user: {
        id: 0,
        username: "",
        fullName: "",
        email: "",
        phone: "",
        dob: "",
      },
      isOwner: false,
    }
  }
}
</script>

<style scoped>
  .parent {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100%;
    width: 100%;
  }
  .child {
    display: flex;  
    flex-direction: row;
    justify-content: center;
    align-items: center;
    flex-wrap: wrap;
    overflow-y: auto;
  }
  .child > div {
    padding: 30px;
  }
  .btn {
    align-self: center;
    margin: 0.4rem;
  }
  .info {
    display: flex;
    flex-direction: column;
    
  }
  .pass {
    display: flex;
    flex-direction: column;
  }
  .in {
    margin: 0.4rem;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
  }
</style>