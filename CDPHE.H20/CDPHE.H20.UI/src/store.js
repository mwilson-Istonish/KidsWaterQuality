
import Vuex from 'vuex'

const store = new Vuex.Store({
  state: {
    title: "Vue Store",
    isLoggedIn: false,
    userRole: "Admin",
    waterForKidsRoles: ["WQ Infrastructure Staff", "WQ Fiscal Staff", "Facility User"],
    userManagementRoles: ["User Approver"],
    count: 1,
    jwtToken: JSON.parse(localStorage.getItem("jwt")),
    user: JSON.parse(localStorage.getItem("user"))
  },
  mutations: {
    increment (state, incrementNum) {
      state.count = state.count + incrementNum;
    },
    changeLoggedInStatus (state, status) {
        state.isLoggedIn = status;
    },
    updateCount (state, newCount) {
        state.count = newCount;
    },
    updateJWT (state, jwtToken) {
      //have to also set state to make it reactive in other components, but it pulls from localstorage in the get method
      state.jwtToken = jwtToken;
      localStorage.setItem('jwt', JSON.stringify(jwtToken));

      //decodes user info from jwt
      var base64Url = jwtToken.split('.')[1];
      var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      var jsonUser = decodeURIComponent(window.atob(base64).split('').map(function(c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      }).join(''));
      
      state.user = null
      localStorage.setItem('user', jsonUser);
    },
    removeJWT (state) {
      //have to also set state to make it reactive in other components, but it pulls from localstorage in the get method
      state.jwtToken = null   
      localStorage.removeItem('jwt');
    }
  },
  getters: {
    isAdmin(state) {
        return state.userRole == "Admin"
    },
    waterForKidsAccess(state) {
        return state.waterForKidsRoles.includes(state.userRole);
    },
    userManagementAccess(state, getters) {
        return state.userManagementRoles.includes(state.userRole) || getters.isAdmin
    },
    getCount(state) {
        return state.count;
    },
    getJWT(state) {
      return state.jwtToken
    },
    getUser(state) {
      return state.user
    },
    getUserRole(state) {
      return state.user['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    },
    isLoggedIn(state) {
      return state.jwtToken != null;
    }
  },
  actions:{}
});

export default store;