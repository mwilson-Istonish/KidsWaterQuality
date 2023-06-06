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
                                    <td :class="{'text-danger': request.Status == 'Attention Needed', 'text-success': request.Status == 'In Progress'}">{{ request.Status }}</td>
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
        <NewRequest ref="newRequest"/>
    </div>
</template>

<script>
  import { ref } from 'vue';
  import RequestDetails from '../components/RequestDetails.vue'
  import NewRequest from '../components/NewRequest.vue'
  import RequestsMixin from '../mixins/RequestsMixin'
  import { useStore } from "vuex";
  export default {
    components: {
        RequestDetails,
        NewRequest
    },
    mixins: [RequestsMixin],
    async created() {
        if(this.store.getters.getUserRole == "Provider"){
            await this.GetRequestsAPI(this.store.getters.getUser.Id);
        }
        else{
            await this.GetRequestsAPI();
            await this.getBudget();
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
        $('#RequestsTable').dataTable({
            "order": [],
            responsive: true
        });
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
        }
    },
    computed: {
        getFilteredRequests() {
            if (this.requests.length > 0) {
                return this.requests.filter(req => req.Status == 'Attention Needed');
            }
            else {
                return []
            }
        }
    }
  }
</script>