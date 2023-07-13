<template>
    <link rel="icon" href="/favicon.ico"><link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.3.0/css/all.min.css" integrity="sha512-SzlrxWUlpfuzQ+pcUCosxcglQRNAq/DZjVsC0lE40xsADsfeQoEypE+enwcOiGjk/bSuGGKHEyjSoQ1zVisanQ==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <div class="modal modal-lg" tabindex="-1" id="MyProfileModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col"></div>
                        <div class="col text-center h20-modal-header">
                            My Profile
                        </div>
                        <div class="col text-right text-end">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    </div>
                    <div class="row h20-modal-spacing">
                        <div class="col-xl-6">
                            <label>First Name</label>
                            <div class="input-group mb-3">
                                <input :value="this.store.getters.getUser ? this.store.getters.getUser.FirstName : ''" disabled type="text" class="form-control">
                            </div>
                        </div>
                        <div class="col-xl-6">
                            <label>Last Name</label>
                            <div class="input-group mb-3">
                                <input :value="this.store.getters.getUser ? this.store.getters.getUser.LastName : ''" disabled type="text" class="form-control">
                            </div>
                        </div>
                        <div class="col-xl-6">
                            <label>Email</label>
                            <div class="input-group mb-3">
                                <input :value="this.store.getters.getUser ? this.store.getters.getUser.Email : ''" type="text" disabled class="form-control">
                            </div>
                        </div>
                        <div class="col-xl-6">
                            <label>Role</label>
                            <div class="input-group mb-3">
                                <input :value="this.store.getters.getUserRole" disabled type="text" class="form-control">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-5"></div>
                        <div class="col text-center">
                            <div class="h20-blue-hover" v-on:click="signOut()"  data-bs-dismiss="modal">
                                <i class="fa-solid fa-sign-out" style="font-size:28px; margin-top:2rem;cursor:pointer"></i>
                                <h4 class="h20-blue-hover" style="font-size:20px;text-decoration:underline;cursor:pointer">Sign Out</h4>
                            </div>
                        </div>
                        <div class="col-lg-5"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
  import { inject } from 'vue'
  import { useStore } from "vuex";
  export default {
    data() {
        return {
            store: useStore(),
        }
    },
    created () {
        const emitter = inject('emitter');
        emitter.on('openProfileModal', (value) => {
            console.log('event fired', `value: ${value}`);
        });
    },
     methods: {
        signOut() {
            this.store.commit('removeJWT');
            this.$router.push("/Login")
        }
     }
  }
</script>