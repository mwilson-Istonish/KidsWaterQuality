<template>
    <div class="h20-dashboard-view h20-full-height">
        <div class="row h20-full-height">
            <div class="col">
                <div class="card h20-full-height">
                    <div class="card-body" style="padding:2rem;">
                        <div class="row">
                            <div class="col-lg-9" style="font-size: 20px">
                                <div>
                                    <span :class="{'text-danger': getNumberWaiting != 0}" style="font-weight:600; font-size:22px">
                                        {{ getNumberWaiting }}
                                    </span>
                                    request{{ getNumberWaiting != 1 ? "s" : "" }} waiting
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="input-group mb-3">
                                    <span class="input-group-text" id="basic-addon1"><i class="fa-solid fa-magnifying-glass"></i></span>
                                    <input type="text" class="form-control" placeholder="search requests...">
                                </div>
                            </div>
                        </div>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th scope="col">School #</th>
                                    <th scope="col">Status</th>
                                    <th scope="col">Module</th>
                                    <th scope="col">Updated</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="request in this.Requests" class="h20-request-row" data-bs-toggle="modal" data-bs-target="#RequestDetailsModal">
                                    <td>{{ request.SchoolNumber }}</td>
                                    <td :class="{'text-danger': request.Status == 'Attention Needed', 'text-success': request.Status == 'In Progress'}">{{ request.Status }}</td>
                                    <td>{{ request.Module }}</td>
                                    <td>{{ request.Updated }}</td>
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
            <div class="modal-content" style="max-height:700px; overflow-y: auto;">
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
                    <div class="row h20-modal-spacing">
                        <div class="col">
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="facility" role="tabpanel" aria-labelledby="facility-tab">
                                    <div class="row">
                                        <div class="col-xl-1"></div>
                                        <div class="col-xl-10">
                                            <div class="row">
                                                <div class="col">
                                                    <label>Name</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.facility.name" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl-8">
                                                    <label>Facility</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.facility.facility" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-4">
                                                    <label>WQCID</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.facility.id" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <label>Provider</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.facility.provider" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl-4">
                                                    <label>Phone</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.facility.phone" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-8">
                                                    <label>Email</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.facility.email" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl-6">
                                                    <label>City</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.facility.city" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-3">
                                                    <label>State</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.facility.state" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-3">
                                                    <label>Zip</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.facility.zip" disabled type="text" class="form-control">
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
                                            <div class="row">
                                                <div class="col">
                                                    <label>Sample Name</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.request.sampleName" disabled type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl-6">
                                                    <label>Initial Sample Date</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.request.initialSampleDate" type="date" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-6">
                                                    <label>Sample Result (ppb)</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.request.initialSampleResult" type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl-6">
                                                    <label>Flush Sample Date</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.request.flushSampleDate" type="date" class="form-control">
                                                    </div>
                                                </div>
                                                <div class="col-xl-6">
                                                    <label>Sample Result (ppb)</label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.request.flushSampleResult" type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <label>Total Cost <span style="font-size:12px">(not to be exceeded)</span></label>
                                                    <div class="input-group mb-3">
                                                        <input v-model="selectedRequest.request.totalCost" type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div><hr>
                                            <div class="row">
                                                <div class="col">
                                                    <h4>Expected Cost</h4>
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <div class="row" style="">
                                                                <div class="col-lg-5">
                                                                    <label>Sample Result (ppb)</label>
                                                                    <div class="input-group mb-3">
                                                                        <input v-model="selectedRequest.request.materialCost" type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-5">
                                                                    <label>Sample Result (ppb)</label>
                                                                    <div class="input-group mb-3">
                                                                        <input v-model="selectedRequest.request.laborCost" type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div><hr>
                                            <div class="row">
                                                <div class="col">
                                                    <h4>Request Details</h4>
                                                    <div class="row">
                                                        <div class="col">
                                                            <table class="table table-bordered">
                                                                <thead class="text-center">
                                                                    <tr>
                                                                        <th>Name</th>
                                                                        <th>Date</th>
                                                                        <th>Result</th>
                                                                        <th>Action</th>
                                                                        <th>Cost</th>
                                                                        <th>Material</th>
                                                                        <th>Labor</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div><hr>
                                            <div class="row">
                                                <div class="col">
                                                    <h4>Totals</h4>
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-lg-4">
                                                                    <label>Cost</label>
                                                                    <div class="input-group mb-3">
                                                                        <input v-model="selectedRequest.request.currentCost" disabled type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <label>Material</label>
                                                                    <div class="input-group mb-3">
                                                                        <input v-model="selectedRequest.request.currentMaterial" type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <label>Labor</label>
                                                                    <div class="input-group mb-3">
                                                                        <input v-model="selectedRequest.request.currentLabor" type="text" class="form-control">
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
  import { useStore } from "vuex";
  export default {
    data () {
      return {
        store: useStore(),
        Requests:
        [
            {
                SchoolNumber: 12345,
                Status: "Attention Needed",
                Module: "Water Quality",
                Updated: new Date().toLocaleString('en-US')
            },
            {
                SchoolNumber: 54321,
                Status: "In Progress",
                Module: "Water Quality",
                Updated: new Date().toLocaleString('en-US')
            },
            {
                SchoolNumber: 67890,
                Status: "Complete",
                Module: "Water Quality",
                Updated: new Date().toLocaleString('en-US')
            },
        ],
        selectedRequest: {
            facility: {
                id: "CCH0067023",
                name: "Wilson, Hunter B",
                facility: "Green Mountain Falls Middle School",
                provider: "Green Mountain School District 1",
                phone: "803.123.4567",
                email: "hunter.wilson@school.name.edu",
                address: "456 Main St",
                city: "Green Mountain Falls",
                state: "CO",
                zip: 80819
            },
            request: {
                sampleName: "DW_Hallway_001",
                initialSampleDate: '2021-08-12',
                flushSampleDate: '2021-08-12',
                initialSampleResult: "6",
                flushSampleResult: "<1",
                remedialAction: "POU Filter device & filters",
                materials: "$500",
                labor: "$1,000",
                totalCost: "$5,750",
                materialCost: "$500",
                laborCost: "$1,000",
                currentCost: "$1,570",
                currentMaterial: "$1,000",
                currentLabor: "$570"
            },
        }
      }
    },
      
    methods: {
        updateRequests() {
            this.Requests[0].Status = "In Progress"
            this.Requests[0].Updated = new Date().toLocaleString('en-US')
            this.store.commit('updateCount', 0)
        }
        
    },
    computed: {
        getNumberWaiting() {
            var numberWaiting = 0;
            for (var i = 0; i < this.Requests.length; i++){
                numberWaiting += (this.Requests[i].Status == "Attention Needed") ? 1 : 0
            }
            return numberWaiting;
        }
    }
  }
</script>