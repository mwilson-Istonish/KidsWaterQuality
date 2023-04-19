<script setup>
import { RouterLink, RouterView } from 'vue-router'
import MyProfile from '@/components/modals/MyProfile.vue'
</script>

<template>
  <header>
    <nav class="navbar navbar-expand-sm h20-navbar">
      <div class="container-fluid">
        <div class="col text-center">
          <img src="src/assets/img/navbar-logo.png" id="navbar-logo" style="max-height:50px">
        </div>
      </div>
    </nav>
  </header>
  <MyProfile />
  <RouterView />
  <footer class="navbar navbar-expand-sm footer mt-auto h20-footer">
      <div class="container-fluid">
          <div class="col">
            <div>
              &copy; <span v-text="currentYear"></span> State of Colorado
            </div>
          </div>
          <div class="col text-center">
            <div v-show="this.store.getters.isLoggedIn">
              <span v-on:click="navigateToPage('/Dashboard')" title="Dashboard"><i class="fa-solid fa-house-chimney h20-footer-icon h20-footer-icon-spacing"></i></span>
              <span class="h20-footer-icon-spacing h20-footer-divider"></span>
              <span data-bs-toggle="modal" data-bs-target="#MyProfileModal" title="My Profile"><i class="fa-solid fa-user-gear h20-footer-icon h20-footer-icon-spacing"></i></span>
            </div>
          </div>
          <div class="col text-end">
            <RouterLink to="/ContactUs">Contact Us</RouterLink>
          </div>
      </div>
    </footer>
</template>

<script>
  import { inject } from 'vue'
  import { useStore } from "vuex";
  export default {
    components: {
      MyProfile
    },
    data () {
      return {
        currentYear: new Date().getFullYear(),
        emitter: inject('emitter'),    
        store: useStore()
      }
    },
    methods: {
      getCurrentYear () {
        return new Date().getFullYear()
      },
      navigateToPage(page) {
          this.$router.push(page)
      },
      openProfileModal() {
        this.emitter.emit('openProfileModal', true);
      }
    }
  }
</script>