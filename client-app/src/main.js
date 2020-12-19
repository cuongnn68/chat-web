import Vue from 'vue';
import somethingEles from './App.vue';
import router from './router'

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(somethingEles)
}).$mount("#app");

localStorage.setItem("domain", "localhost:5001");
document.title = "Discord ripoff";

// TODO * leave room button, setting room
// TODO * room setting (change name, delete user, leave room, delete room) : new view page
// TODO * chang uesr info : new view page
// TODO * sub url???
// TODO * find group, user
// TODO * user chat (db, api, signalr, user stuff like the room one)
// TODO * global floating notification