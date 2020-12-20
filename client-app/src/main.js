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
// TODO * room setting (change name, delete user, leave room, delete room) : new view page
// DONE * chang uesr info ui
// TODO * chang uesr info : function
// TODO * find group, user
// TODO * user chat (db, api, signalr, user stuff like the room one)
// TODO * check input login, register...
// TODO * global floating notification
// TODO * icon for other thing
// TODO * info when hover