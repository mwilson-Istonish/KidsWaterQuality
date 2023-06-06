<template>
    <div class="modal modal-lg" tabindex="-1" id="NewRequestModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col text-right text-end">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    </div>
                    <div class="row h20-modal-spacing" style="max-height:700px; overflow-y: auto; margin-top:0!important">
                        <div class="col">
                            <div class="row">
                                <div class="col text-center">
                                    <h3>
                                        {{ this.profile.name }}
                                    </h3>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xl-1"></div>
                                <div class="col-xl-10">
                                    <div v-for="detail in this.newRequest.details">
                                        <div class="row">
                                            <div class="col">
                                                <label>Sample Name</label>
                                                <div class="input-group mb-3">
                                                    <input v-model="detail.sampleName" type="text" class="form-control">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xl-5">
                                                <label>Initial Sample Date</label>
                                                <div class="input-group mb-3">
                                                    <input @input="detail.initialSampleDate = $event.target.value" type="date" :value="new Date(detail.initialSampleDate).toISOString().slice(0, 10)" class="form-control">
                                                </div>
                                            </div>
                                            <div class="col-xl-2">
                                                <label> </label>
                                                <div class="input-group mb-3">
                                                    <select class="form-select" v-model="detail.sampleResultOperator">
                                                        <option value="0" selected>=</option>
                                                        <option value="1">&lt;</option>
                                                        <option value="2">></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-xl-5">
                                                <label>Initial Sample Result (ppb)</label>
                                                <div class="input-group mb-3">
                                                    <input @input="detail.initialSampleResult = $event.target.value" type="number"  :value="detail.initialSampleResult" class="form-control">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xl-5">
                                                <label>Flush Sample Date</label>
                                                <div class="input-group mb-3">
                                                    <input @input="detail.flushSampleDate = $event.target.value" type="date" :value="new Date(detail.flushSampleDate).toISOString().slice(0, 10)" class="form-control">
                                                </div>
                                            </div>
                                            <div class="col-xl-2">
                                                <label> </label>
                                                <div class="input-group mb-3">
                                                    <select class="form-select" v-model="detail.flushResultOperator">
                                                        <option value="0" selected>=</option>
                                                        <option value="1">&lt;</option>
                                                        <option value="2">></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-xl-5">
                                                <label>Flush Sample Result (ppb)</label>
                                                <div class="input-group mb-3">
                                                    <input @input="detail.flushSampleResult = $event.target.value" type="number"  :value="detail.flushSampleResult" class="form-control">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="input-group mb-3">
                                                <label>
                                                    <input type="checkbox" v-model="detail.inHouseLabor" @change="updateRemedialAction(detail)">
                                                    Using In-House Plumber
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xl-6">
                                                <label>Remedial Action</label>
                                                <div class="input-group mb-3">
                                                    <select class="form-select" v-model="detail.remedialActionId" @change="updateRemedialAction(detail)">
                                                        <option value="0" disabled selected>Select a Remedial Action</option>
                                                        <option v-for="remedialAction in this.remedialActions.filter(rem => rem.id !== 12)" :value="remedialAction.Id">{{ remedialAction.Name }}</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-xl-6">
                                                <label>Not to Exceed</label>
                                                <div class="input-group mb-3">
                                                    <input type="text" :value="this.getNotToExceedValue(detail.remedialActionId)" disabled class="form-control">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xl-6">
                                                <label>Expected Materials Cost</label>
                                                <div class="input-group mb-3">
                                                    <input type="number" :value="detail.expectedMaterialCost" class="form-control expectedMats">
                                                </div>
                                            </div>
                                            <div class="col-xl-6">
                                                <label>{{detail.inHouseLabor ? "" : "Expected "}}Labor Cost</label>
                                                <div class="input-group mb-3">
                                                    <input type="number" :value="detail.expectedLaborCost" :disabled="detail.inHouseLabor" class="form-control expectedLabor">
                                                </div>
                                            </div>
                                        </div><hr>
                                    </div>
                                </div>
                                <div class="col-xl-1"></div>
                                <div class="row" style="margin-top:1rem">
                                    <div class="col text-end">
                                        <button class="btn btn-primary" data-bs-dismiss="modal" disabled v-on:click="updateRequests()">Submit</button>
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
        var store = useStore();
        await this.GetRemedialActions();
        await this.getProfileAPI(store.getters.getUser.WQCID)
    },
    data () {
      return {
        store: useStore(),
        newRequest: {
            id: null,
            facilityId: null,
            facilityName: null,
            wqcid: null,
            address1: null,
            address2: null,
            address3: null,
            city: null,
            state: null,
            zipcode: null,
            provider: null,
            providerId: null,
            email: null,
            phone: null,
            createdAt: null,
            status: null,
            totalCostLabor: null,
            totalCostMaterials: null,
            totalCost: null,
            details: [],
        }
      }
    },
      
    methods: {
        addNewRequestDetail() {
            var newDetail = {
                id: null,
                requestId: null,
                sampleName: null,
                initialSampleDate: new Date().toISOString().slice(0, 10),
                sampleResultOperator: 0,
                initialSampleResult: null,
                flushSampleDate: new Date().toISOString().slice(0, 10),
                flushResultOperator: 0,
                flushSampleResult: null,
                remedialActionId: 0,
                expectedMaterialCost: 0,
                expectedLaborCost: 0,
                actualMaterialCost: null,
                actualLaborCost: null,
                confirmationSampleResultDate: null,
                confirmationSampleResultOperator: null,
                confirmationSampleResult: null,
                inHouseLabor: true,
            }
            this.newRequest.details.push(newDetail)
        },
        async startNewRequest() {
            this.addNewRequestDetail();
            $('#NewRequestModal').modal('show')
        },
        getOperator(operatorCode) {
            return operatorCode == 1 ? "< " : operatorCode == 2 ? "> " : "";
        },
        updateRemedialAction(detail) {
            if (detail.inHouseLabor && detail.remedialActionId != 0) {
                var rate = this.profile.rateTable.find(g => g.id == detail.remedialActionId).notToExceed
                var rateNum = parseFloat(rate.replace(/\$/g, "").replace(/,/g, ""))
                detail.expectedLaborCost = rateNum
                console.log(rateNum)
            }
            else {
                detail.expectedLaborCost = 0;
            }
        }
    },
    computed: {
        getNotToExceedValue(){
            return (id) => {
                if(id != 0) {
                    const selectedRemedialAction = this.remedialActions.find(obj => obj.Id === id);
                    return selectedRemedialAction.NotToExceed;
                }
                return null;
            };
        },

    }
  }
</script>