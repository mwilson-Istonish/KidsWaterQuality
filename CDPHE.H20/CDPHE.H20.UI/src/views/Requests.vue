<template>
    <div class="h20-dashboard-view h20-full-height">
        <div class="row h20-full-height">
            <div class="col">
                <div class="card h20-full-height">
                    <div class="card-body" style="padding:2rem;">
                        <div class="row">
                            <div class="col-lg-9" style="font-size: 20px; margin-bottom:1rem">
                                <div>
                                    <span :class="{'text-danger': getNumberWaiting != 0}" style="font-weight:600; font-size:22px">
                                        {{ getNumberWaiting }}
                                    </span>
                                    request{{ getNumberWaiting != 1 ? "s" : "" }} waiting
                                </div>
                            </div>
                        </div>
                        <table class="table" id="RequestsTable">
                            <thead>
                                <tr>
                                    <th scope="col">Facility</th>
                                    <th scope="col">Status</th>
                                    <th scope="col">Total Cost</th>
                                    <th scope="col">Created</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="request in this.requests" class="h20-request-row" v-on:click="getRequestDetails(request.Id)" data-bs-toggle="modal" data-bs-target="#RequestDetailsModal">
                                    <td>{{ request.Facility }}</td>
                                    <td :class="{'text-danger': request.Status == 'Attention Needed', 'text-success': request.Status == 'In Progress'}">{{ request.Status }}</td>
                                    <td>{{ request.TotalCost }}</td>
                                    <td>{{ new Date(request.CreatedAt).toLocaleDateString('en-US') }}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal modal-lg" tabindex="-1" id="RequestDetailsModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col">
                            <ul class="nav nav-pills">
                                <li class="nav-item">
                                    <a class="nav-link active" id="facility-tab" data-bs-toggle="tab" data-bs-target="#facility" type="button" role="tab" aria-controls="facility" aria-selected="true">Facility Info</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" id="request-tab" data-bs-toggle="tab" data-bs-target="#request" type="button" role="tab" aria-controls="request" aria-selected="false">Request</a>
                                </li>
                            </ul>
                        </div>
                        <div class="col text-right text-end">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    </div>
                    <div class="row h20-modal-spacing" style="max-height:650px; overflow-y: auto;">
                        <div class="col">
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="facility" role="tabpanel" aria-labelledby="facility-tab">
                                    <div class="row">
                                        <div class="col-xl-1"></div>
                                        <div class="col-xl-10">
                                            <div class="row">
                                                <div class="col-xl-8">
                                                    <label>Facility</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="this.currentRequestDetails.facility" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-4">
                                                    <label>WQCID</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="this.currentRequestDetails.wqcid" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <label>Provider</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="this.currentRequestDetails.provider" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl-4">
                                                    <label>Phone</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="this.currentRequestDetails.phone" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-8">
                                                    <label>Email</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="this.currentRequestDetails.email" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl-6">
                                                    <label>City</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="this.currentRequestDetails.city" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-3">
                                                    <label>State</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="this.currentRequestDetails.state" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-3">
                                                    <label>Zip</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="this.currentRequestDetails.zipcode" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col text-end">
                                                    <button class="btn btn-secondary" data-bs-dismiss="modal" aria-label="Close">Cancel</button>&nbsp;
                                                    <button class="btn btn-primary">Submit</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-1"></div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="request" role="tabpanel" aria-labelledby="request-tab">
                                    <div class="row">
                                        <div class="col-xl-1"></div>
                                        <div class="col-xl-10">
                                            <div v-for="detail in this.currentRequestDetails.details">
                                                <div class="row">
                                                    <div class="col">
                                                        <label>Sample Name</label>
                                                        <div class="input-group mb-3">
                                                            <input v-model="detail.sampleName" disabled type="text" class="form-control">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-6">
                                                        <label>Initial Sample Date</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.initialSampleDate = $event.target.value" :value="new Date(detail.initialSampleDate).toISOString().slice(0, 10)" type="date" class="form-control">
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6">
                                                        <label>Sample Result (ppb)</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.initialSampleResult = $event.target.value" :value="getOperator(detail.sampleResultOperator) + detail.initialSampleResult" type="text" class="form-control">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-6">
                                                        <label>Flush Sample Date</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.flushSampleDate = $event.target.value" :value="new Date(detail.flushSampleDate).toISOString().slice(0, 10)" type="date" class="form-control">
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6">
                                                        <label>Sample Result (ppb)</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.flushSampleResult = $event.target.value" :value="getOperator(detail.flushResultOperator) + detail.flushSampleResult" type="text" class="form-control">
                                                        </div>
                                                    </div>
                                                </div><hr>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <h4>Totals</h4>
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-lg-4">
                                                                    <label>Cost</label>
                                                                    <div class="input-group mb-3">
                                                                        <input v-model="this.currentRequestDetails.totalCost" disabled type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <label>Material</label>
                                                                    <div class="input-group mb-3">
                                                                        <input v-model="this.currentRequestDetails.totalCostMaterials" disabled type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <label>Labor</label>
                                                                    <div class="input-group mb-3">
                                                                        <input v-model="this.currentRequestDetails.totalCostLabor" disabled type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top:1rem">
                                                <div class="col text-end">
                                                    <button class="btn btn-secondary" data-bs-dismiss="modal" aria-label="Close">Cancel</button>&nbsp;
                                                    <button class="btn btn-primary" data-bs-dismiss="modal" v-on:click="updateRequests()">Submit</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-1"></div>
                                    </div>
                                </div>
                            </div>
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
        await this.GetRequestsByProviderID(this.store.getters.getUser.Id);
        $('#RequestsTable').dataTable({
            "order": [],
            responsive: true
        });
    },
    data () {
      return {
        store: useStore(),
      }
    },
      
    methods: {
        updateRequests() {
            this.Requests[0].Status = "In Progress"
            this.Requests[0].Updated = new Date().toLocaleString('en-US')
            this.store.commit('updateCount', 0)
        },
        async getRequestDetails(requestId) {
            await this.getRequestDetailsAPI(requestId);
        },
        getOperator(operatorCode) {
            return operatorCode == 1 ? "< " : operatorCode == 2 ? "> " : "";
        }    
    },
    computed: {
        getNumberWaiting() {
            var numberWaiting = 0;
            // for (var i = 0; i < this.Requests.length; i++){
            //     numberWaiting += (this.Requests[i].Status == "Attention Needed") ? 1 : 0
            // }
            return numberWaiting;
        }
    }
  }
</script>