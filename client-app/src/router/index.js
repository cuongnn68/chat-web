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
    name: "LOGin", // RE: name not fucking matter again ?? 
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
  //TODO: add other route
  {
    path: "*",
    name: "Home",
    component: Home
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem("token");
  const privatePage = ["/chat", ];
  const publicPage = ["/", "/about", "/login", "/register"]

  const exist = publicPage.includes(to.path);
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
