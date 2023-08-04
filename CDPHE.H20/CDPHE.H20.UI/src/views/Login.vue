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
                        <form id="loginForm" @submit="submitEmail()">
                            <div style="margin-top:2rem; font-size: 18px">
                                <label>Enter your email to log in or request an account:</label>
                                <div class="input-group mb-3">
                                    <input v-model="email" type="text" class="form-control" placeholder="example@email.com">
                                </div>
                            </div>
                            <div style="display: none; margin-bottom:1rem" class="text-danger" id="emailError"></div>
                            <div>
                                <button class="btn btn-md h20-btn" id="EmailSubmitBtn" type="button" v-on:click="submitEmail()">Submit</button>
                            </div>
                        </form>
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
                            Please provide the login code that was sent to your email.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 text-center">
                            By signing in, you agree to the <a href="#" v-on:click="toggleTOSVisibility()">Terms of Service</a>.
                        </div>
                        <div class="col-xl-12">
                            <div id="tos" class="tosDisplay" style="display: none">

                            </div>
                        </div>
                    </div>
                    <div class="row h20-modal-spacing">
                        <div class="col-12">
                            <form @submit="login()" id="loginFormCode">
                                <label>Code:</label>
                                <span id="codeError" class="h20-error-text"></span> 
                                <div class="h20-input-group input-group mb-3">
                                    <input v-model="loginCode" type="text" class="form-control">
                                </div>    
                                <div>
                                    <button class="btn btn-md h20-btn" id="CodeSubmitBtn" v-on:click="login()">Submit</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal modal-md" tabindex="-1" id="LoginNoUserModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col text-center">
                            <form id="accountRequestForm" @submit="requestAccount()">
                                An account could not be found for the provided email.
                                <div class="row">
                                    <div class="col-12" style="margin-bottom:1rem">
                                        Would you like to request an account from CDPHE?<br>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <input v-model="firstName" type="text" class="form-control" placeholder="First Name"/>
                                        </div>    
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <input v-model="lastName" type="text" class="form-control" placeholder="Last Name"/>
                                        </div>    
                                    </div>
                                    <div class="col">
                                        <div class="input-group mb-3">
                                            <input v-model="email" type="text" class="form-control" placeholder="Email">
                                        </div>           
                                    </div>
                                </div>
                                <div id="RequestAccountError" class="text-danger" style="display: none"></div>
                                <div id="RequestAccountSuccess" class="text-success" style="display: none">An account creation request has been sent. <br>You will receive an email upon approval.</div>
                                <br>
                                <button type="button" class="btn btn-primary" id="requestAccountBtn" v-on:click="requestAccount()">Request Account</button>&nbsp;
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" aria-label="Close">Close</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import LoginMixin from '../mixins/LoginMixin'
import { useStore } from "vuex";
export default {
    mixins: [LoginMixin],
    data() {
        return {
            email: "",
            firstName: "",
            lastName: "",
            loginCode: "",
            store: useStore()
        };
    },
    async created() {
        await this.getTOS();
        $("#tos").html(this.tos)
    },
    methods: {
        async submitEmail() {
            event.preventDefault();
            var emailError = $("#emailError");
            if(this.email && this.validateEmail(this.email)){
                emailError.hide()
                await this.submitLoginEmail(this.email)
                if(this.isCurrentUser) {
                    $("#Login2FAModal").modal('show')
                }
                else {
                    $("#LoginNoUserModal").modal('show')
                }
            }
            else {
                emailError.text("Please provide a valid email")
                emailError.show()
            }
        },
        async login() {
            event.preventDefault();
            var buttonEle = document.getElementById("CodeSubmitBtn");
            var errorMessageEle = document.getElementById("codeError");
            buttonEle.disabled = true;
            var loginSuccess = false;
            var errorMsg = "Please provide a valid login code";

            if (this.loginCode) {
                await this.submitToken(this.loginCode);
                loginSuccess = this.store.getters.isLoggedIn
            }

            if (loginSuccess) {    
                $("#Login2FAModal").modal('hide')
                this.$router.push("/Dashboard");
            }
            else {
                errorMessageEle.text(errorMsg);
                buttonEle.disabled = false;
            }
        },
        toggleTOSVisibility() {
            var tos = $("#tos");
            tos.slideToggle()
        },
        async requestAccount() {  
            event.preventDefault();   
            var buttonEle = document.getElementById("requestAccountBtn");
            buttonEle.disabled = true;
            var errorMsg = $("#RequestAccountError");
            var successMsg = $("#RequestAccountSuccess");
            if(this.firstName && this.lastName && this.email && this.validateEmail(this.email)) {
                await this.requestUserAccount(this.firstName, this.lastName, this.email);
                successMsg.show()
                errorMsg.hide()
            }
            else {
                errorMsg.text("Please provide a valid first name, last name, and email address.")
                errorMsg.show() 
                buttonEle.disabled = false;
            }
        },
        validateEmail(supposedEmail) {
            var emailRegex = /^[^@]+@[^@]+\.[^@]+$/
            return emailRegex.test(supposedEmail)
        }
    },
}
</script>