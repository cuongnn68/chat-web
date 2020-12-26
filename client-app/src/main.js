import Vue from 'vue';
import somethingEles from './App.vue';
import router from './router';

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(somethingEles)
}).$mount("#app");

console.log(process.env.NODE_ENV === "development");
if(process.env.NODE_ENV === "development") {
  localStorage.setItem("domain", "localhost:5001");
} else {
  localStorage.setItem("domain", "localhost:5001"); // TODO change to domain of azure
}
document.title = "Discord ripoff";

// DONE * sub url???
// DONE * setting room button
// DONE * room setting (change name, delete user, leave room, delete room) : new view page
// DONE * room setting (change name, delete user, leave room, delete room) : function
// DONE * chang uesr info ui
// DONE * chang uesr info : function
// DONE * find group, user ui
// DONE * find group, user function
// TODO * user chat (db, api, signalr, user stuff like the room one)
// TODO * Datetime
// TODO * check input login, register...
// DONE * global floating notification
// DONE * icon for other thing
// TODO * info when hover