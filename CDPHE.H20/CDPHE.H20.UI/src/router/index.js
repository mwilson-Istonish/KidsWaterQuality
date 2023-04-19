import { createRouter, createWebHistory } from 'vue-router'
import store from '@/store.js';
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      redirect: '/Login'
    },
    {
      path: '/Login',
      name: 'Login',
      component: () => import('../views/Login.vue')
    },
    {
      path: '/ContactUs',
      name: 'ContactUs',
      component: () => import('../views/ContactUs.vue')
    },
    {
      path: '/Dashboard',
      name: 'Dashboard',
      meta: {
        authRequired: false,
      },
      component: () => import('../views/Dashboard.vue')
    },
    {
      path: '/Requests',
      name: 'Requests',
      component: () => import('../views/Requests.vue')
    },
    {
      path: '/UserManagement',
      name: 'UserManagement',
      meta: {
        authRequired: true,
      },
      component: () => import('../views/UserManagement.vue')
    },
    {
      path: '/WaterForKids',
      name: 'WaterForKids',
      meta: {
        authRequired: true,
      },
      component: () => import('../views/WaterForKids.vue')
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'Any',
      redirect: '/Login'
    }
  ]
});

function unauthorized() {
  router.push('/Dashboard')
}

router.beforeEach((to, from, next) => {
    console.log(to);
    if(to.meta.authRequired) {
      if (to.name == "UserManagement" && !store.getters.userManagementAccess) {
        unauthorized()
      }
      if (to.name == "WaterForKids" && !store.getters.waterForKidsAccess) {
        unauthorized()
      }
    }
    return next()
});

export default router