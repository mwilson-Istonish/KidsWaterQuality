<script setup>
import { RouterLink, RouterView } from 'vue-router'
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
  <RouterView />
  <footer class="navbar navbar-expand-sm footer mt-auto h20-footer">
      <div class="container-fluid">
          <div class="col">
            <div>
              &copy; <span v-text="currentYear"></span> State of Colorado
            </div>
          </div>
          <div class="col text-center"><span v-on:click="goHome()"><i class="fa-solid fa-house-chimney h20-footer-icon"></i></span></div>
          <div class="col text-end">
            <RouterLink to="/ContactUs">Contact Us</RouterLink>
          </div>
      </div>
    </footer>
</template>

<script>
  export default {
    data () {
      return {
        currentYear: new Date().getFullYear()
      }
    },
      
    methods: {
      getCurrentYear () {
        return new Date().getFullYear()
      },
      goHome () {
        this.$router.push("/Dashboard")
      }
    }
  }
</script>
