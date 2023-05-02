
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
    user: {}
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
    isLoggedIn(state) {
      return state.jwtToken != null;
    }
  },
  actions:{}
});

export default store;