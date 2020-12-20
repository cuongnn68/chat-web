<template>
  <div id="app">
    <NavBar v-on:logout="logOut" v-bind:login="login" class="nav"/>
    <router-view v-on:loged-in="logedIn" class="content"/>
  </div>
</template>

<script>
import NavBar from "./components/NavigationBar.vue";
export default {
  name: "App", // dont fukcing matter
  components: {
    NavBar
  },
  data: function() {
    return {
      login: false,
    }
  },
  methods: {
    logedIn: function() {
      this.login = true;
    },
    logOut: function() {
      this.login = false;
    }
  },
  created() {
    if(localStorage.getItem("userInfo")) this.login = true;
  }
}
</script>

<style>
  /*
  màu chính: #b15ebb
  - alter chinh: #b676b6
  2nd: #5f566d
  màu phụ:  #221d63
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
    padding: 0.35rem;
    outline:none;

    min-width:15px; 
    /* FIXME: fix on mobile */
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
