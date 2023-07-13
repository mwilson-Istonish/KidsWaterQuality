<template>
    <div class="h20-dashboard-view h20-full-height">
        <div class="row h20-full-height">
            <div class="col-12">
                <div class="card h20-full-height">
                    <div class="card-body" style="padding:2rem;">
                        <div class="row">
                            <div class="col-lg-9" style="font-size: 20px; margin-bottom:1rem">
                                <div>
                                    <span :class="{'text-danger': getFilteredRequests.length != 0}" style="font-weight:600; font-size:22px">
                                        {{ getFilteredRequests.length }}
                                    </span>
                                    request{{ getFilteredRequests.length != 1 ? "s" : "" }} waiting
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
                                <tr v-for="request in this.getFilteredRequests" class="h20-request-row" v-on:click="getRequestDetails(request.Id)" data-bs-toggle="modal" data-bs-target="#RequestDetailsModal">
                                    <td>{{ request.Facility }}</td>
                                    <td>{{ request.Status }}</td>
                                    <td>{{ request.TotalCost }}</td>
                                    <td>{{ new Date(request.CreatedAt).toLocaleDateString('en-US') }}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div>       
                <canvas id="budgetChart"></canvas>
            </div>
        </div>
        <RequestDetails ref="requestDetails"/>
    </div>
</template>

<script>
  import { ref } from 'vue';
  import { inject } from 'vue'
  import RequestDetails from '../components/RequestDetails.vue'
  import RequestsMixin from '../mixins/RequestsMixin'
  import { useStore } from "vuex";
  export default {
    components: {
        RequestDetails
    },
    mixins: [RequestsMixin],
    async created() {
        const emitter = inject('emitter');
        emitter.on('updateRequests', async (value) => {
            await this.initializeData(true);
        });
        await this.initializeData(false);
    },
    data () {
      return {
        store: useStore(),
        newW9: "",
        newW9Extension: "",
        newInvoice: "",
        newInvoiceExtension: "",
        newReceipt: "",
        newReceiptExtension: "",
      }
    },
      
    methods: {
        async getRequestDetails(requestId) {
            this.$refs.requestDetails.getRequestDetails(requestId);
        },
        async startNewRequest() {
            this.$refs.newRequest.startNewRequest();
        },
        async initializeData(updateOnly) {
            if(this.store.getters.getUserRole == "Provider"){
                await this.GetRequestsAPI(this.store.getters.getUser.Id);
            }
            else{
                await this.GetRequestsStaffAPI(this.store.getters.getUser.Id);
                await this.getBudget();
            }
            if(!updateOnly) {
                $('#RequestsTable').dataTable({
                    "order": [],
                    responsive: true
                });
                if(this.store.getters.getUserRole != "Provider"){
                    const labels = ['Approved', 'Requested', 'Spent', 'Remaining'];
                    const data = [this.budget.Approved, this.budget.Requested, this.budget.Spent, this.budget.Remaining];
                    const colors = ['rgba(54, 162, 235, 0.8)', 'rgba(75, 192, 192, 0.8)', 'rgba(255, 206, 86, 0.8)', 'rgba(153, 102, 255, 0.8)'];

                    const ctx = document.getElementById('budgetChart').getContext('2d');
                    const myChart = new Chart(ctx, {
                        type: 'pie',
                        data: {
                            labels: labels,
                            datasets: [{
                            data: data,
                            backgroundColor: colors
                            }]
                        },
                        options: {
                            responsive: true,
                            maintainAspectRatio: false,
                            layout: {
                                padding: 10
                            },
                            plugins: {
                                legend: {
                                    labels: {
                                        boxWidth: 20
                                    }
                                }
                            },
                        }
                    });
                }
            }
        },
    },
    computed: {
        getFilteredRequests() {  
            if (this.requests.length > 0) {
                if (this.store.getters.getUserRole == "Provider") {
                    return this.requests.filter(g => g.Status.toLowerCase() == "new" || g.Status.toLowerCase() == "approved")
                }
                else if (this.store.getters.getUserRole == "WQ Staff") {
                    console.log(this.requests)
                    return this.requests.filter(g => g.Status.toLowerCase() == "draft" || g.Status.toLowerCase() == "submitted" || g.Status.toLowerCase() == "complete")
                }
                else {
                    return [];
                }
            }
            else {
                return []
            }
        }
    }
  }
</script>