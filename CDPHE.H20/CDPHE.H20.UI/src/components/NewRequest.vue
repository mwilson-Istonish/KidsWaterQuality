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
                    <div class="row h20-modal-spacing" style="max-height:700px; overflow-y: auto; margin-top:0!important" v-show="!requestSubmitted">
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
                                                    <input @input="detail.initialSampleDate = new Date($event.target.value)" type="date" :value="new Date(detail.initialSampleDate).toISOString().slice(0, 10)" class="form-control">
                                                </div>
                                            </div>
                                            <div class="col-xl-2">
                                                <label> </label>
                                                <div class="input-group mb-3">
                                                    <select class="form-select" v-model="detail.sampleResultOperator">
                                                        <option :value="0" selected>=</option>
                                                        <option :value="1">&lt;</option>
                                                        <option :value="2">></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-xl-5">
                                                <label>Initial Sample Result (ppb)</label>
                                                <div class="input-group mb-3">
                                                    <input type="number" v-model="detail.initialSampleResult" class="form-control">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xl-5">
                                                <label>Flush Sample Date</label>
                                                <div class="input-group mb-3">
                                                    <input @input="detail.flushSampleDate = new Date($event.target.value)" type="date" :value="new Date(detail.flushSampleDate).toISOString().slice(0, 10)" class="form-control">
                                                </div>
                                            </div>
                                            <div class="col-xl-2">
                                                <label> </label>
                                                <div class="input-group mb-3">
                                                    <select class="form-select" v-model="detail.flushResultOperator">
                                                        <option :value="0" selected>=</option>
                                                        <option :value="1">&lt;</option>
                                                        <option :value="2">></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-xl-5">
                                                <label>Flush Sample Result (ppb)</label>
                                                <div class="input-group mb-3">
                                                    <input type="number" v-model="detail.flushSampleResult" class="form-control">
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
                                                    <input type="text" :value="this.getNotToExceedValue(detail.remedialActionId)" :readonly="true" disabled class="form-control">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xl-6">
                                                <label>Expected Materials Cost</label>
                                                <div class="input-group mb-3">
                                                    <input type="number" v-model="detail.expectedMaterialCost" class="form-control expectedMats">
                                                </div>
                                            </div>
                                            <div class="col-xl-6">
                                                <label>{{detail.inHouseLabor ? "" : "Expected "}}Labor Cost</label>
                                                <div class="input-group mb-3">
                                                    <input type="number" v-model="detail.expectedLaborCost" :disabled="detail.inHouseLabor" class="form-control expectedLabor">
                                                </div>
                                            </div>
                                        </div><hr>
                                    </div>
                                </div>
                                <div class="col-xl-1"></div>
                                <div class="row">
                                    <div class="col-6 text-end">
                                        <button class="btn btn-danger" v-on:click="removeRequestDetail()" >Remove Sample</button>
                                    </div>
                                    <div class="col-6 text-start">
                                        <button class="btn btn-success" v-on:click="addNewRequestDetail()">Add Sample</button>
                                    </div>
                                    <div class="col text-center" style="margin-top:1rem;font-weight: 600;font-size:18px">
                                        <span class="text-danger" v-show="this.formError">Please provide a value for all editable fields.</span>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:1rem">
                                    <div class="col text-end">
                                        <button class="btn btn-primary" type="button" :disabled="!checkFormValidityComputed" v-on:click="submitRequest()">Submit Request</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row h20-modal-spacing" style="max-height:700px; overflow-y: auto; margin-top:0!important" v-show="requestSubmitted">
                        <div class="col text-center" style="font-size:20px;margin-bottom:1.5rem">
                            <div style="font-size:24px; font-weight:800;margin-bottom:1rem">Thank you!</div>
                            <div style="margin-bottom:0.5rem">Your request has been submitted and will be reviewed by the CDPHE team.</div> You will receive an email at {{ this.store.getters.getUser.Email }} when further action is required.
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
  import { inject } from 'vue'
  import RequestsMixin from '../mixins/RequestsMixin'
  import { useStore } from "vuex";
  export default {
    mixins: [RequestsMixin],
    async created() {
        var store = useStore();
        console.log(store.getters.getUser.WQCID)
        await this.GetRemedialActions();
        await this.getProfileAPI(store.getters.getUser.WQCID)
        console.log(this.profile);
        this.newRequest.facilityId = this.profile.facilityId;
        this.newRequest.address1 = this.profile.address.address1;
        this.newRequest.address2 = this.profile.address.address2;
        this.newRequest.address3 = this.profile.address.address3;
        this.newRequest.city = this.profile.address.city;
        this.newRequest.state = this.profile.address.state;
        this.newRequest.zipcode = this.profile.zip;
        this.newRequest.address1 = this.profile.address1;
        this.newRequest.address1 = this.profile.address1;
        this.newRequest.providerId = parseInt(this.store.getters.getUser.Id)
    },
    data () {
      return {
        store: useStore(),
        emitter: inject('emitter'),    
        requestSubmitted: false,
        formError: false,
        newRequest: {
            facilityId: null,
            facilityName: null,
            wqcid: null,
            address1: null,
            address2: null,
            address3: null,
            city: null,
            state: null,
            zipcode: null,
            providerId: null,
            status: null,
            details: [],
        }
      }
    },
      
    methods: {
        addNewRequestDetail() {
            var newDetail = {
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
                inHouseLabor: true,
            }
            this.newRequest.details.push(newDetail)
        },
        removeRequestDetail() {
            if (this.newRequest.details.length <= 1) {          
                this.newRequest.details.pop();
                this.addNewRequestDetail();
            }
            else {
                this.newRequest.details.pop();
            }
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
            }
            else {
                detail.expectedLaborCost = 0;
            }
        },
        async submitRequest() {
            var validForm = this.checkFormValidityComputed;
            if (validForm) {
                await this.submitRequestAPI(this.newRequest);
                this.requestSubmitted = true;
                this.emitter.emit('updateWFKRequests', true);
                this.formError = false;
            }
            else {
                this.formError = true;
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
        checkFormValidityComputed() {
            var validForm = true;
            if(this.newRequest.details != null && this.newRequest.details.length > 0) {
                for(var i = 0; i < this.newRequest.details.length; i++) {
                    if(
                        this.newRequest.details[i].sampleName && this.newRequest.details[i].sampleName.length > 0 &&
                        this.newRequest.details[i].initialSampleDate &&
                        !isNaN(this.newRequest.details[i].initialSampleResult) && this.newRequest.details[i].initialSampleResult &&
                        this.newRequest.details[i].flushSampleDate &&
                        !isNaN(this.newRequest.details[i].flushSampleResult) && this.newRequest.details[i].flushSampleResult &&
                        this.newRequest.details[i].remedialActionId && this.newRequest.details[i].remedialActionId != 0 &&
                        this.newRequest.details[i].expectedMaterialCost.toString().trim() !== "" && !isNaN(this.newRequest.details[i].expectedMaterialCost) && this.newRequest.details[i].expectedMaterialCost >= 0 &&
                        this.newRequest.details[i].expectedLaborCost.toString().trim() !== "" && !isNaN(this.newRequest.details[i].expectedLaborCost) && this.newRequest.details[i].expectedLaborCost >= 0
                    ) {}
                    else {
                        validForm = false
                    }
                }
                return validForm
            }
            else {
                return false;
            }

        }
    }
  }
</script>