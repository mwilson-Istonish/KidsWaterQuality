<template>
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
                    <div class="row h20-modal-spacing" style="max-height:700px; overflow-y: auto;">
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
                                                        <input v-model="this.currentRequestDetails.facilityName" disabled type="text" class="form-control">
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
                                                    <div class="col-xl-5">
                                                        <label>Initial Sample Date</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.initialSampleDate = $event.target.value" disabled type="date" :value="new Date(detail.initialSampleDate).toISOString().slice(0, 10)" class="form-control">
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-2">
                                                        <label> </label>
                                                        <div class="input-group mb-3">
                                                            <select class="form-select" v-model="detail.sampleResultOperator" disabled>
                                                                <option value="0" selected>=</option>
                                                                <option value="1">&lt;</option>
                                                                <option value="2">></option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-5">
                                                        <label>Initial Result (ppb)</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.initialSampleResult = $event.target.value" disabled type="number"  :value="detail.initialSampleResult" class="form-control">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-5">
                                                        <label>Flush Sample Date</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.flushSampleDate = $event.target.value" disabled type="date" :value="new Date(detail.flushSampleDate).toISOString().slice(0, 10)" class="form-control">
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-2">
                                                        <label> </label>
                                                        <div class="input-group mb-3">
                                                            <select class="form-select" v-model="detail.flushResultOperator" disabled>
                                                                <option value="0" selected>=</option>
                                                                <option value="1">&lt;</option>
                                                                <option value="2">></option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-5">
                                                        <label>Flush Result (ppb)</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.flushSampleResult = $event.target.value" disabled type="number"  :value="detail.flushSampleResult" class="form-control">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-5">
                                                        <label>Confirmation Sample Date</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.confirmationSampleResultDate = $event.target.value" disabled type="date" :value="new Date(detail.confirmationSampleResultDate).toISOString().slice(0, 10)" class="form-control">
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-2">
                                                        <label> </label>
                                                        <div class="input-group mb-3">
                                                            <select class="form-select" v-model="detail.confirmationSampleResultOperator" disabled>
                                                                <option value="0" selected>=</option>
                                                                <option value="1">&lt;</option>
                                                                <option value="2">></option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-5">
                                                        <label>Confirmation Result (ppb)</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.confirmationSampleResult = $event.target.value" disabled type="number"  :value="detail.confirmationSampleResult" class="form-control">
                                                        </div>
                                                    </div>
                                                </div>             
                                                <div class="row">
                                                    <div class="input-group mb-3">
                                                        <label>
                                                            <input type="checkbox" disabled v-model="detail.inHouseLabor" @change="updateRemedialAction(detail)">
                                                            Using In-House Plumber
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-6">
                                                        <label>Remedial Action</label>
                                                        <div class="input-group mb-3">
                                                            <select class="form-select" disabled v-model="detail.remedialActionId">
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
                                                            <input type="number" :value="detail.materialCost" disabled class="form-control">
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6">
                                                        <label>Expected Labor Cost</label>
                                                        <div class="input-group mb-3">
                                                            <input type="number" :value="detail.materialLabor" disabled class="form-control">
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
                                            </div><hr>
                                            <!-- <div class="row">
                                                <div class="col">
                                                    <h4>W-9</h4>
                                                    <div v-for="file in this.currentRequestFiles.filter(singleFile => singleFile.FileType === 'W-9')">
                                                        <a style="font-size:18px" :href="file.Url">{{ file.Name }}</a>
                                                    </div>
                                                    <input type="file" id="newW9" class="btn btn-secondary" placeholder="Upload W9">&nbsp;<button class="btn btn-primary" disabled type="button" id="newW9Button" v-on:click="uploadW9()">Upload</button>
                                                </div>
                                            </div><hr> -->
                                            <div class="row">
                                                <div class="col">
                                                    <h4>Invoice</h4>
                                                    <div v-for="file in this.currentRequestFiles.filter(singleFile => singleFile.FileType === 'Invoice')">
                                                        <a style="font-size:18px" :href="file.Url">{{ file.Name }}</a>
                                                    </div> 
                                                    <input type="file" id="newInvoice" class="btn btn-secondary" placeholder="Upload Invoice">&nbsp;<button class="btn btn-primary" id="newInvoiceButton" disabled type="button" v-on:click="uploadInvoice()">Upload</button>
                                                </div>
                                            </div><hr>
                                            <div class="row">
                                                <div class="col">
                                                    <h4>Receipt</h4>   
                                                    <div v-for="file in this.currentRequestFiles.filter(singleFile => singleFile.FileType === 'Receipt')">
                                                        <a style="font-size:18px" :href="file.Url">{{ file.Name }}</a>
                                                    </div>     
                                                    <input type="file" id="newReceipt" class="btn btn-secondary" placeholder="Upload Receipt">&nbsp;<button class="btn btn-primary" id="newReceiptButton" disabled type="button" v-on:click="uploadReceipt()">Upload</button>
                                                </div>
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
        </div>
    </div>
</template>

<script>
  import RequestsMixin from '../mixins/RequestsMixin'
  import { useStore } from "vuex";
  export default {
    mixins: [RequestsMixin],
    mounted() {
        // document.getElementById('newW9').addEventListener('input', (e) => {
        //     const files = e.target.files || e.dataTransfer.files;
        //     const file = files[0];
        //     this.newW9Extension = file.name.split('.').pop();
        //     const reader = new FileReader();
        //     reader.readAsDataURL(file);
        //     reader.onload = () => {
        //         this.newW9 = reader.result.split(',')[1].toString();
        //     };
        //     var buttonEle = document.getElementById("newW9Button");
        //     buttonEle.disabled = false;
        // })
        document.getElementById('newInvoice').addEventListener('input', (e) => {
            const files = e.target.files || e.dataTransfer.files;
            const file = files[0];
            this.newInvoiceExtension = file.name.split('.').pop();
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => {
                this.newInvoice = reader.result.split(',')[1].toString();
            };
            var buttonEle = document.getElementById("newInvoiceButton");
            buttonEle.disabled = false;
        })
        document.getElementById('newReceipt').addEventListener('input', (e) => {
            const files = e.target.files || e.dataTransfer.files;
            const file = files[0];       
            this.newReceiptExtension = file.name.split('.').pop();
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => {
                this.newReceipt = reader.result.split(',')[1].toString();
            };
            var buttonEle = document.getElementById("newReceiptButton");
            buttonEle.disabled = false;
        })
    },
    async created() {
        await this.GetRemedialActions();
        console.log(this.store.getters.getUser)
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
            console.log('in')
            await this.getRequestDetailsAPI(requestId);
            console.log(this.currentRequestDetails);
            $('#RequestDetailsModal').modal('show')
        },
        getOperator(operatorCode) {
            return operatorCode == 1 ? "< " : operatorCode == 2 ? "> " : "";
        },
        async uploadW9() {
            await this.uploadFile(this.newW9.toString(), "W9", this.newW9Extension, this.currentRequestDetails.id);
            this.newW9 = "";
            this.newW9Extension = "";
            await this.getRequestDetailsAPI(this.currentRequestDetails.id)
            var buttonEle = document.getElementById("newW9Button");
            buttonEle.disabled = true;
            var inputEle = document.getElementById("newW9");
            inputEle.value = ''
        },
        async uploadInvoice() {
            await this.uploadFile(this.newInvoice.toString(), "invoice", this.newInvoiceExtension, this.currentRequestDetails.id);
            this.newInvoice = "";
            this.newInvoiceExtension = "";
            await this.getRequestDetailsAPI(this.currentRequestDetails.id)
            var buttonEle = document.getElementById("newInvoiceButton");
            buttonEle.disabled = true;
            var inputEle = document.getElementById("newInvoice");
            inputEle.value = ''
        },
        async uploadReceipt() {
            await this.uploadFile(this.newReceipt.toString(), "receipt", this.newReceiptExtension, this.currentRequestDetails.id);
            this.newReceipt = "";
            this.newReceiptExtension = "";
            await this.getRequestDetailsAPI(this.currentRequestDetails.id)
            var buttonEle = document.getElementById("newReceipt");
            buttonEle.disabled = true;
            var inputEle = document.getElementById("newW9");
            inputEle.value = ''
        },
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