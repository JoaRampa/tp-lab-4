import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import LogOut from "../views/LogOut.vue";
import PucharseView from "../views/ViewPucharse.vue";
import store from "@/store";

const routes = [
  {
    path: "/",
    name: "Home",
    component: HomeView,
    meta: { requiereGuest: true },
  },
  {
    path: "/logOut",
    name: "LogOut",
    component: LogOut,
    meta: { requiresAuth: true },
  },
  {
    path: "/pucharse",
    name: "viewPucharse",
    component: PucharseView,
    meta: { requiresAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth && !store.state.isAuthenticated) {
    alert("Ingrese un userId");
    next("/");
  } else if (to.meta.requiresGuest && store.state.isAuthenticated) {
    next("/#");
  } else {
    next();
  }
});

export default router;
