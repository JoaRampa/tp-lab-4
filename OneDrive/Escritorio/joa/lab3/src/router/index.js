import { createRouter, createWebHistory } from "vue-router";
import EventList from "../views/EventList.vue";
import About from "@/views/AboutView.vue";
import store from "@/store";

const routes = [
  {
    path: "/",
    name: "EventList",
    component: EventList,
    meta: { requiereGuest: true },
  },
  {
    path: "/About",
    name: "About",
    component: About,
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
