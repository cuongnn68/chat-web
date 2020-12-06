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