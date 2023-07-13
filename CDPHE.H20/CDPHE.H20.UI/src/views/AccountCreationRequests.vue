<template>
    <div class="h20-dashboard-view h20-full-height">
        <div class="row h20-full-height">
            <div class="col">
                <div class="card h20-full-height">
                    <div class="card-body" style="padding:2rem;">
                        <table class="table" id="RequestsTable">
                            <thead>
                                <tr>
                                    <th scope="col">Email</th>
                                    <th scope="col">First Name</th>
                                    <th scope="col">Last Name</th>
                                    <th scope="col">Request Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="request in this.accountCreationRequests" class="h20-request-row" v-on:click="getAccountRequestDetails(request)">
                                    <td>{{ request.Email }}</td>
                                    <td>{{ request.FirstName }}</td>
                                    <td>{{ request.LastName }}</td>
                                    <td>{{ new Date(request.RequestDate).toLocaleDateString('en-US') }}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal modal-md" tabindex="-1" id="AccountCreationRequestModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-6" style="margin-bottom:1rem; font-size:20px">
                            User Information<br>
                        </div>
                        <div class="col-sm-6 text-end" style="margin-bottom:1rem; font-size:20px">
                            {{ new Date(selectedRequest.RequestDate).toLocaleDateString('en-US') }}
                        </div>
                        <div class="col-md-6">
                            <label>First Name</label>
                            <div class="input-group mb-3">
                                <input v-model="selectedRequest.FirstName" type="text" class="form-control" placeholder="First Name"/>
                            </div>    
                        </div>
                        <div class="col-md-6">
                            <label>Last Name</label>
                            <div class="input-group mb-3">
                                <input v-model="selectedRequest.LastName" type="text" class="form-control" placeholder="Last Name"/>
                            </div>    
                        </div>
                        <div class="col-md-12">
                            <label>Email</label>
                            <div class="input-group mb-3">
                                <input v-model="selectedRequest.Email" type="text" class="form-control" disabled placeholder="Email">
                            </div>           
                        </div>
                        <div class="col-md-12">
                            <label>Role</label>
                            <div class="input-group mb-3">
                                <select class="form-select" v-model="this.selectedRequestRole">
                                    <option value="0" disabled>Select a Role</option>
                                    <option v-for="role in this.roles" :value="role.Id">{{ role.Name }}</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top:2rem;margin-bottom:1rem">
                        <div class="col-md-1"></div>      
                        <div class="col-md-5 text-center">
                            <button type="button" class="btn btn-danger" v-on:click="updateRequestDecision(false)">Reject Request</button>
                        </div>
                        <div class="col-md-5 text-center">
                            <button type="button" class="btn btn-success" v-on:click="updateRequestDecision(true)">Create Account</button>
                        </div>
                        <div class="col-md-1"></div>
                    </div>
                    <div class="row" id="confirmAccountRequest" style="margin-top:2rem;margin-bottom:1rem;display:none">
                        <div class="col-12 text-center">
                            <span style="font-size:20px;">{{createAccount ? "Create a user account?" : "Reject the request?"}}</span><br>
                        </div>
                        <div class="col-12 text-center" style="margin-top:0.5rem">
                            <button type="button" class="btn btn-primary" v-on:click="submitAccountRequestDecision()">Confirm</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
  import RequestsMixin from '../mixins/RequestsMixin'
  import { useStore } from "vuex";
  export default {
    mixins: [RequestsMixin],
    async created() {
        await this.getAccountRequests();
        $('#RequestsTable').dataTable({
            "order": [],
            responsive: true
        });
        await this.getRoles();
    },
    data () {
      return {
        store: useStore(),
        selectedRequest: {},
        selectedRequestRole: 0,
        createAccount: false
      }
    },
      
    methods: {
        async getAccountRequestDetails(request) {
            this.selectedRequest = request;
            $("#AccountCreationRequestModal").modal('show')
        },
        updateRequestDecision(decision) {
            this.createAccount = decision;
            var confirmation = $("#confirmAccountRequest");
            confirmation.slideDown(200)
        },
        async submitAccountRequestDecision() {
            var decision = {
                selectedRequest: this.selectedRequest,
                role: this.selectedRequestRole
            }
            await this.submitAccountRequestDecisionAPI(decision)
        }
    },
  }
</script>