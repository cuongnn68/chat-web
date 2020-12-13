import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: "/login",
    name: "Login", // RM: name used to .$router.push("name-of-view") to change view //TODO is it?
    component: () => import("../views/Login.vue")
  },
  {
    path: "/register",
    name: "Register",
    component: () => import("../views/Register.vue")
  },
  {
    path: "/chat",
    name: "Chat",
    component: () => import("../views/Chat.vue")
  },
  {
    path: "/test",
    name: "Test",
    component: () => import("../views/Test.vue")
  },
  //TODO: add other route
  {
    path: "*",
    name: "Any",
    component: Home
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to, from, next) => {
  const user = JSON.parse(localStorage.getItem("userInfo"));
  const token = user && user["token"];
  
  const privatePage = ["/chat", "/search"];
  const publicPage = ["/", "/about", "/login", "/register", "/test"]

  const exist = publicPage.concat(privatePage).includes(to.path);
  // console.log(publicPage);
  if(!exist) {
    next("/");
  }
  const requiredAuth = privatePage.includes(to.path);
  if(requiredAuth && !token) {
    next("/login");
  }
  next();
});

export default router;
