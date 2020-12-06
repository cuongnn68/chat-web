<template>
  <div id="app">
    <NavBar class="nav"/>
    <router-view class="content"/>
  </div>
</template>

<script>
import NavBar from "./components/NavigationBar.vue";
export default {
  name: "App", // dont fukcing matter
  components: {
    NavBar
  }
}
</script>

<style>
/*
 màu chính: #b15ebb
 màu phụ:  #221d63
 */

* { /* remove default margin */
    margin: 0;
    /* transition-duration: 0.3s; */
}

body {
  height: 100%;
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
}
.content{
  flex: 1;
  overflow: auto;
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
  /* border-color: white;
  color: white;
  background-color:#b15ebb; */
  box-shadow: 1px 1px 4px black;
}
button:active {
  box-shadow: 2px 2px 4px#665069;
}

input {
  font-size: 18px;
  border:solid;
  border-color: #b15ebb;
  border-width: 0.15rem;
  border-radius: 0.4rem;
  box-shadow: 2px 2px 4px#665069;

  padding: 0 0.2rem 0 0.2rem; 
  /* WHY: does this matter??? */

  outline:none;

  min-width:15px; 
  /* FIXME: fix on mobile */
}


.test-scope { /* can inject into component*/
  background-color: red;
  
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

</style>
