import Vue from 'vue';
import somethingEles from './App.vue';
import router from './router';

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(somethingEles)
}).$mount("#app");

localStorage.setItem("domain", "localhost:5001");
document.title = "Discord ripoff";

// DONE * sub url???
// DONE * setting room button
// DONE * room setting (change name, delete user, leave room, delete room) : new view page
// TODO * room setting (change name, delete user, leave room, delete room) : function
// DONE * chang uesr info ui
// TODO * chang uesr info : function
// DONE * find group, user ui
// TODO * find group, user function
// TODO * user chat (db, api, signalr, user stuff like the room one)
// TODO * check input login, register...
// DONE * global floating notification
// DONE * icon for other thing
// TODO * info when hover