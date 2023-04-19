<template>
    <div class="h20-router-view">
        <div class="row">
            <div class="col-lg-4 col-md-3"></div>
            <div class="col-lg-4 col-md-6">
                <div class="card">
                    <div class="card-body" style="padding:2rem">
                        <div class="text-center" style="font-weight:600; font-size:22px">
                            Welcome to the Water Quality Portal
                        </div>
                        <div style="margin-top:2rem; font-size: 18px">
                            <label>Enter your email to log in or create an account:</label>
                            <div class="input-group mb-3">
                                <input v-model="email" type="text" class="form-control" placeholder="example@email.com">
                            </div>
                        </div>
                        <div>
                            <button class="btn btn-md h20-btn" id="EmailSubmitBtn" data-bs-toggle="modal" data-bs-target="#Login2FAModal">Submit</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-3"></div>
        </div>
    </div>
    <div class="modal modal-md" tabindex="-1" id="Login2FAModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style="">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xl-2"></div>
                        <div class="col text-center h20-modal-header">
                            Confirm Your Identity
                        </div>
                        <div class="col-xl-2 text-right text-end">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col text-center">
                            Please provide the login code that was sent to your email
                        </div>
                    </div>
                    <div class="row h20-modal-spacing">
                        <div class="col-12">
                            <label>Code:</label>
                            <span id="codeError" class="h20-error-text"></span> 
                            <div class="h20-input-group input-group mb-3">
                                <input v-model="loginCode" type="text" class="form-control">
                            </div>    
                            <div>
                                <button class="btn btn-md h20-btn" id="CodeSubmitBtn" v-on:click="login()">Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import LoginMixin from '../mixins/LoginMixin'
import bootstrap from 'bootstrap/dist/js/bootstrap.bundle.js'
import { useStore } from "vuex";
export default {
    mixins: [LoginMixin],
    data() {
        return {
            email: "",
            loginCode: "",
            store: useStore()
        };
    },
    created() {
        this.getWeatherForecast();
    },
    methods: {
        login() {
            var buttonEle = document.getElementById("CodeSubmitBtn");
            var errorMessageEle = document.getElementById("codeError");
            buttonEle.disabled = true;
            var loginSuccess = false;
            var errorMsg = "";
            if (this.loginCode) {
                if (this.loginCode == "j") {
                    loginSuccess = true;
                }
                else {
                    errorMsg = "This login code is incorrect"
                }
            }
            else {
                errorMsg = "Please provide a valid login code"
            }
            if (loginSuccess) {
                var myModalEl = document.getElementById('Login2FAModal');
                var modal = bootstrap.Modal.getInstance(myModalEl)
                modal.hide();   
                this.store.commit('changeLoggedInStatus', true)
                this.$router.push("/Dashboard");
            }
            else {
                errorMessageEle.innerText = errorMsg;
                buttonEle.disabled = false;
            }
        }
    },
}
</script>