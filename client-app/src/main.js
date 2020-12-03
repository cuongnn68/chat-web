import Vue from 'vue';
import somethingEles from './App.vue';
import router from './router'

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(somethingEles)
}).$mount("#app");
