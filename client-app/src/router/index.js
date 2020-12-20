import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    alias: "/home"
  },
  {
    path: '/about',
    name: 'About',
    // RM //? route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: "/login",
    name: "Login",
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
  {
    path: "/user",
    name: "UserInfo",
    component: () => import("../views/UserInfo.vue")
  },
  {
    path: "/room/:id",
    name: "RoomManager",
    component: () => import("../views/RoomManager.vue")
  },
  //TODO: add other route
  {
    path: "*",
    name: "Any",
    component: () => import("../views/Error404.vue")
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
  const publicPage = ["/", "/about", "/login", "/register", "/user", "/test", "/room"] // TODO: create error page and put this shit away

  // const exist = publicPage.concat(privatePage).findIndex(path => path.startsWith(to.path));
  
  // if(!exist) {
  //   console.log(to.path);
  //   next("/");
  // }
  const requiredAuth = privatePage.includes(to.path);
  if(requiredAuth && !token) {
    next("/login");
  }
  next();
});

export default router;
