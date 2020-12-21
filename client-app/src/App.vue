<template>
  <div id="app">
    <NavBar v-on:newNoti="addNoti" v-on:logout="logOut" v-bind:login="login" class="nav"/>
    <router-view v-on:newNoti="addNoti" v-on:loged-in="logedIn" class="content"/>
    <div class="noti">
      <!-- <p class="nor-noti red-noti">hello</p> -->
      <div class="nor-noti" 
        v-for="noti in notis" 
        v-bind:key="noti.time"
        v-bind:class="{'red-noti': noti.type==='red', 'grn-noti': noti.type==='green'}">
        <!-- <i class="fas fa-exclamation-triangle"></i> -->
        <i class="fas fa-bell" v-bind:class="{'fa-exclamation': noti.type==='red', 'fa-rocket': noti.type==='green'}"></i>
        <p class="space-side">
          {{noti.content}}
        </p>
        <i v-on:click="deleteNoti(noti.time)" class="fas fa-times-circle del-noti"></i>
        
      </div>
    </div>
  </div>
</template>

<script>
import NavBar from "./components/NavigationBar.vue";
export default {
  name: "App", //RM //? dont fkcng matter
  components: {
    NavBar //RM import bf use
  },
  data: function() {
    return {
      login: false,
      notis: []
    }
  },
  methods: {
    logedIn: function() {
      this.login = true;
    },
    logOut: function() {
      this.login = false;
    },
    addNoti(value) {
      value.time = window.performance.now();
      this.notis.push(value);
      if(this.notis.length > 3) this.notis.splice(0,1);
      this.notis.sort((a,b) => a.time < b.time);
      window.setTimeout(() => this.deleteNoti(value.time), 6000);
      // this.rm(value.time, this.deleteNoti);
    },
    deleteNoti(time) {
      this.notis = this.notis.filter(nt => nt.time !== time);
    },
    // rm: (time, del) => {
    //   window.setTimeout(() => del(time) ,6000) //RM do task ones after amount of time
    // }
  },
  created() {
    if(localStorage.getItem("userInfo")) this.login = true;
    // setInterval(() => { //RM repeatly run after amount of time, time not guaranteed right
    //   this.notis.splice(-1,1);
    // }, 7000)
  },
  mounted() {
    //? when mount
  }
}
</script>

<style>
  /*
  màu chính: #b15ebb
  - alter chinh: #b676b6
  2nd: #5f566d
  màu phụ:  #221d63

  red: #ff5959
  green: #43bb43
  */

  * { /* remove default margin */
      margin: 0;
      /* transition-duration: 0.3s; */
  }

  body {
    height: 100%;
    /* color: rgb(129, 115, 129); */
  }

  #app {
    margin: 0;
    background-color: white;
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    /* color: #2c3e50; */
    color: #221d63;

    /* background-color: #2c3e50; */
    height: 100%;
    display: flex;
    flex-direction: column;
  }



  button {
    border: solid;
    border-color: #b15ebb;
    border-width: 0.15rem;
    border-radius: 0.4rem;
    color: #b15ebb;
    background-color:white;
    text-align: center;
    padding: 0.4rem;
    font-size: 18px;
    font-weight: 700;

    box-shadow: 2px 2px 4px#665069;

    outline:none;
    cursor:pointer;
  }
  button:hover {
    color: white;
    background-color:#b15ebb;
    box-shadow: 1px 1px 4px black;
  }
  button:active {
    box-shadow: 2px 2px 4px#665069;
  }

  .red-btn {
    background-color: white;
    border-color: #ff5959;
    color: #ff5959;
  }
  .red-btn:hover {
    color: white;
    background-color: #ff5959;
  }

  .grn-btn {
    background-color: white;
    border-color: #43bb43;
    color: #43bb43;
  }
  .grn-btn:hover{
    background-color: #43bb43;
    color: white;
  }


  label {
    font-size: 0.9rem;
    font-weight: 550;
    color: #5f566d;
    /* color: #817381; */
  }

  input {
    font-size: 18px;
    border:solid;
    border-color: #5f566d;
    border-width: 0.12rem;
    border-radius: 0.4rem;
    box-shadow: 2px 2px 4px gray;

    /* padding: 0 0.2rem 0 0.2rem;  */
    /* ?: does this matter??? */

    width: 300px;
    padding: 0.4rem;
    outline:none;

    min-width:15px; 
    /* FIXME: fix on mobile */
  }

  input:disabled {
    background-color: lightgray;
  }

  .test-scope { /* can inject into component*/
    background-color: red;
    
  }

  .error {
    margin: 15px;
    color: red;
    font-size: 13px;
  }

  /* width */
  ::-webkit-scrollbar {
    width: 5px;
  }

  /* Track */
  ::-webkit-scrollbar-track {
    background: #f1f1f1;
  }

  /* Handle */
  ::-webkit-scrollbar-thumb {
    background: #888;
  }

  /* Handle on hover */
  ::-webkit-scrollbar-thumb:hover {
    background: #555;
  }

  table {
    border-collapse: collapse;
    empty-cells: show;
  }
  table > tr {
    /* border-top: 1px solid;
    border-bottom: 1px solid; */
  }
  table > tr > th,td {
    /* background-color: red; */
    padding: 0.8rem;
    /* border: 1px solid; */
    border-top: 2px solid;
    border-bottom: 2px solid;
  }
  /* table > tr {
    border: solid;
    border-color: black;
    border-width: 1px;
  } */
  .my-input {
    /* width: 250px; */
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    justify-content: flex-start;
    /* justify-content: space-evenly; */
    margin: 15px;
  }
</style>

<style scoped>
  .noti {
    /* background: blue; */
    /* height: 100px;
    width: 100px; */
    display: flex;
    flex-direction: column;
    align-items: flex-end;
    z-index: 500;
    position: absolute;
    top: 65px;
    right: 5px;
  }
  .nor-noti {
    display: flex;
    flex-direction: row;
    /* opacity: 0.65; */
    max-width: 250px;
    max-height: 50px;
    overflow: hidden;
    margin: 5px;
    padding: 5px;
    border-style: solid;
    border-width: 0.1rem;
    border-radius: 5px;
    /* border-color: gray;
    background-color: lightgray;
    color: black; */
    /* box-shadow: 1px 1px 1px 0px darkgray; */
    box-shadow: 1.5px 1.5px 2.5px 0px gray;

  }
  .nor-noti:hover {
    /* opacity: 0.8; */
    box-shadow: 1.5px 1.5px 2.5px 0px black;
  }
  .red-noti {
    /* border-color: #9e3939; */
    background-color: white;
    color: #ff5959;
  }
  .grn-noti {
    /* border-color: #2a7c2a; */
    background-color: white;
    color: #43bb43;
  }
  .nav {
    overflow: hidden;
    z-index: 200;
    color: #5f566d;
  }
  .content{
    flex: 1;
    overflow: auto;
    color: #5f566d;
  }
  .space-side {
    margin: 0 10px 0 10px;
  }
  .del-noti{
    cursor: pointer;
  }
</style>