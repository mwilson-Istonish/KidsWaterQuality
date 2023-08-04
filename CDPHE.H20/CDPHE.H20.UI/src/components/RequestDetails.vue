<template>
    <div class="modal modal-lg fade" tabindex="-1" id="RequestDetailsModal" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-10">
                            <ul class="nav nav-pills">
                                <li class="nav-item">
                                    <a class="nav-link" id="facility-tab" data-bs-toggle="tab" data-bs-target="#facility" type="button" role="tab" aria-controls="facility" aria-selected="false">Facility Info</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link active" id="request-tab" data-bs-toggle="tab" data-bs-target="#request" type="button" role="tab" aria-controls="request" aria-selected="true">Request</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" id="notes-tab" data-bs-toggle="tab" data-bs-target="#notes" type="button" role="tab" aria-controls="notes" aria-selected="false" @click="scrollToNotesBottom()">Notes</a>
                                </li>
                            </ul>
                        </div>
                        <div class="col text-right text-end">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    </div>
                    <div class="row h20-modal-spacing" id="requestDetailsModalRow" style="max-height:700px; overflow-y: auto;">
                        <div class="col">
                            <div class="tab-content">
                                <div class="tab-pane fade" id="facility" role="tabpanel" aria-labelledby="facility-tab">
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
                                        </div>
                                        <div class="col-xl-1"></div>
                                    </div>
                                </div>
                                <div class="tab-pane fade show active" id="request" role="tabpanel" aria-labelledby="request-tab">
                                    <div class="row">
                                        <div class="col-xl-1"></div>
                                        <div class="col-xl-10">
                                            <div class="card" style="margin-bottom:1rem" v-show="fundedAndProvider">
                                                <div class="card-body" style="background-color:#C1E1C1">
                                                    Your remediation plan has been approved and funded! Upon completion of the work, please upload all invoices and receipts at the bottom. Then, update the confirmation data and actual costs.
                                                </div>
                                            </div>
                                            <div class="card" style="margin-bottom:1rem" v-show="showNewMessage">
                                                <div class="card-body" style="background-color:#C1E1C1">
                                                    A request has been started for this facility. You will find a proposed remediation plan for each sample below. Please review and update the remedial action(s) and expected costs where needed, then click the "Update: Submitted" button at the bottom. If you need assistance, click the notes tab above and send us a message! 
                                                </div>
                                            </div>
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
                                                            <input @input="detail.initialSampleDate = new Date($event.target.value)" disabled type="date" :value="new Date(detail.initialSampleDate).toISOString().slice(0, 10)" class="form-control">
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
                                                            <input @input="detail.flushSampleDate = new Date($event.target.value)" disabled type="date" :value="new Date(detail.flushSampleDate).toISOString().slice(0, 10)" class="form-control">
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
                                                            <input @input="detail.flushSampleResult = $event.target.value" disabled type="number" :value="detail.flushSampleResult" class="form-control">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-5">
                                                        <label>Confirmation Sample Date</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.confirmationSampleResultDate = new Date($event.target.value)" :disabled="!fundedAndProvider" type="date" :value="detail.confirmationSampleResultDate.toString() == '0001-01-01T00:00:00' || detail.confirmationSampleResultDate.toString() == '1900-01-01T00:00:00' ? '' : new Date(detail.confirmationSampleResultDate).toISOString().slice(0, 10)" class="form-control">
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-2">
                                                        <label> </label>
                                                        <div class="input-group mb-3">
                                                            <select class="form-select" v-model="detail.confirmationSampleResultOperator" :disabled="!fundedAndProvider">
                                                                <option value="0" selected>=</option>
                                                                <option value="1">&lt;</option>
                                                                <option value="2">></option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-5">
                                                        <label>Confirmation Result (ppb)</label>
                                                        <div class="input-group mb-3">
                                                            <input @input="detail.confirmationSampleResult = $event.target.value" :disabled="!fundedAndProvider" type="number"  v-model="detail.confirmationSampleResult" class="form-control">
                                                        </div>
                                                    </div>
                                                </div>             
                                                <div class="row">
                                                    <div class="input-group mb-3">
                                                        <label>
                                                            <input type="checkbox" :disabled="!canEditPlan" v-model="detail.inHouseLabor" @change="updateRemedialAction(detail)">
                                                            Using In-House Plumber
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-6">
                                                        <label>Remedial Action</label>
                                                        <div class="input-group mb-3">
                                                            <select class="form-select" :disabled="!canEditPlan" v-model="detail.remedialActionId" @change="updateRemedialAction(detail)">
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
                                                            <input type="number" pattern="\d*\.?\d{0,2}" @input="detail.expectedMaterialCost = detail.expectedMaterialCost ? Number(detail.expectedMaterialCost.toString().replace(/[^\d.]/g, '').substr(0, 6)) : 0" v-model="detail.expectedMaterialCost" :disabled="!canEditPlan" class="form-control">
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6">
                                                        <label>{{detail.inHouseLabor ? "" : "Expected "}}Labor Cost</label>
                                                        <div class="input-group mb-3">
                                                            <input type="number" pattern="\d*\.?\d{0,2}" @input="detail.expectedLaborCost = detail.expectedLaborCost ? Number(detail.expectedLaborCost.toString().replace(/[^\d.]/g, '').substr(0, 6)) : 0" v-model="detail.expectedLaborCost" :disabled="!canEditPlan || detail.inHouseLabor" class="form-control">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-6">
                                                        <label>Actual Materials Cost</label>
                                                        <div class="input-group mb-3">
                                                            <input type="number" pattern="\d*\.?\d{0,2}" @input="detail.actualMaterialCost = detail.actualMaterialCost ? Number(detail.actualMaterialCost.toString().replace(/[^\d.]/g, '').substr(0, 6)) : 0" v-model="detail.actualMaterialCost" :disabled="!fundedAndProvider" class="form-control">
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6">
                                                        <label>Actual Labor Cost</label>
                                                        <div class="input-group mb-3">
                                                            <input type="number" pattern="\d*\.?\d{0,2}" @input="detail.actualLaborCost = detail.actualLaborCost ? Number(detail.actualLaborCost.toString().replace(/[^\d.]/g, '').substr(0, 6)) : 0" v-model="detail.actualLaborCost" :disabled="!fundedAndProvider" class="form-control">
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
                                                                        <input :value="'$' + this.currentRequestDetails.totalCost" disabled type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <label>Material</label>
                                                                    <div class="input-group mb-3">
                                                                        <input :value="'$' + this.currentRequestDetails.totalCostMaterials" disabled type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <label>Labor</label>
                                                                    <div class="input-group mb-3">
                                                                        <input :value="'$' + this.currentRequestDetails.totalCostLabor" disabled type="text" class="form-control">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div><hr>
                                            <div v-show="showFileUploadSection">
                                                <h4>Choose your file and then press the upload button.</h4><hr>
                                                <div class="row">
                                                    <div class="col">
                                                        <h4>W-9</h4>
                                                        <div v-for="file in this.currentRequestFiles.filter(singleFile => singleFile.FileType === 'W-9')">
                                                            <a style="font-size:18px" :href="file.Url">{{ file.Name }}</a>
                                                        </div>
                                                        <div v-show="showFileUploadButton">
                                                            <input type="file" id="newW9" accept="application/pdf, image/png, image/jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, image/heic, video/hevc, application/vnd.openxmlformats-officedocument.wordprocessingml.document, image/tiff" class="btn btn-secondary" placeholder="Upload W9">&nbsp;<button class="btn btn-primary" disabled type="button" id="newW9Button" v-on:click="uploadW9()">Upload</button>
                                                        </div>
                                                    </div>
                                                </div><hr>
                                                <div class="row">
                                                    <div class="col">
                                                        <h4>Invoice</h4>
                                                        <div v-for="file in this.currentRequestFiles.filter(singleFile => singleFile.FileType === 'Invoice')">
                                                            <a style="font-size:18px" :href="file.Url">{{ file.Name }}</a>
                                                        </div>
                                                        <div v-show="showFileUploadButton">
                                                            <input type="file" id="newInvoice" accept="application/pdf, image/png, image/jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, image/heic, video/hevc, application/vnd.openxmlformats-officedocument.wordprocessingml.document, image/tiff" class="btn btn-secondary" placeholder="Upload Invoice">&nbsp;<button class="btn btn-primary" id="newInvoiceButton" disabled type="button" v-on:click="uploadInvoice()">Upload</button>
                                                        </div>
                                                    </div>
                                                </div><hr>
                                                <div class="row">
                                                    <div class="col">
                                                        <h4>Receipt</h4>   
                                                        <div v-for="file in this.currentRequestFiles.filter(singleFile => singleFile.FileType === 'Receipt')">
                                                            <a style="font-size:18px" :href="file.Url">{{ file.Name }}</a>
                                                        </div>
                                                        <div v-show="showFileUploadButton">
                                                            <input type="file" id="newReceipt" accept="application/pdf, image/png, image/jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, image/heic, video/hevc, application/vnd.openxmlformats-officedocument.wordprocessingml.document, image/tiff" class="btn btn-secondary" placeholder="Upload Receipt">&nbsp;<button class="btn btn-primary" id="newReceiptButton" disabled type="button" v-on:click="uploadReceipt()">Upload</button>
                                                        </div>   
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-1"></div>
                                        <div class="row" style="margin-top:1rem">
                                            <div class="col-xl-4">
                                                <!-- <button class="btn btn-danger" v-show="this.currentRequestDetails.status != 'Cancelled' && this.currentRequestDetails.status != 'Funded' && this.currentRequestDetails.status != 'Complete' &&this.currentRequestDetails.status != 'Archived'" v-on:click="updateRequestStatusModal('Cancelled')">Cancel Request</button> -->
                                            </div>
                                            <div class="col text-end">
                                                <button disabled class="btn btn-dark">Status: {{ this.currentRequestDetails.status }}</button>&nbsp;
                                                <button class="btn btn-danger" v-if="statusChangeSecondaryButtonNewStatus != 'x'" v-on:click="updateRequestStatusModal(statusChangeSecondaryButtonNewStatus, true)">Return: {{ statusChangeSecondaryButtonNewStatus }}</button>&nbsp;
                                                <button class="btn btn-primary" v-if="statusChangeButtonNewStatus != 'x' && statusChangeButtonNewStatus != 'Update Request'" v-on:click="updateRequestStatusModal(statusChangeButtonNewStatus, false)">Update: {{ statusChangeButtonNewStatus }}</button>
                                                <button class="btn btn-primary" v-if="statusChangeButtonNewStatus == 'Update Request'" v-on:click="updateFundedData()">{{ statusChangeButtonNewStatus }}</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="notes" role="tabpanel" aria-labelledby="notes-tab">
                                    <div class="row">
                                        <div class="col-xl-1"></div>
                                        <div class="col-xl-10" style="margin-top:0.5rem">
                                            <div v-for="note in getSortedNotes" class="row" style="margin-bottom:0.7rem">
                                                <div v-show="note.UserId == this.store.getters.getUser.Id" class="col-12 text-end">               
                                                    <div style="margin-bottom:0.2rem">{{ note.CreatedBy }} {{ note.UserId == this.currentRequestDetails.providerId ? "| " + this.currentRequestDetails.facilityName + " " : "| CDPHE " }} | {{ note.CreatedAt.slice(0, -3) }}</div>
                                                    <div class="card" style="display: inline-block; width: auto; background-color: #009add; color:white">
                                                        <div class="card-body">
                                                            {{ note.Text }}
                                                        </div>
                                                    </div>
                                                </div>
                                                <div v-show="note.UserId != this.store.getters.getUser.Id" class="col-12 text-start">
                                                    <div style="margin-bottom:0.2rem">{{ note.CreatedBy }} {{ note.UserId == this.currentRequestDetails.providerId ? "| " + this.currentRequestDetails.facilityName + " " : "| CDPHE " }} | {{ note.CreatedAt.slice(0, -3) }}</div>
                                                    <div class="card" style="display: inline-block; width: auto; background-color: #ececeb;">
                                                        <div class="card-body">
                                                            {{ note.Text }}
                                                        </div>
                                                    </div>
                                                </div>          
                                            </div>
                                            <div v-if="getSortedNotes.length == 0">
                                                <div class="row">
                                                    <div class="col text-center">
                                                        No notes have been added yet.
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="input-group mb-3" style="margin-top:1rem!important">
                                                <textarea class="form-control" v-model="this.newNote.Text" rows="3">

                                                </textarea>
                                            </div>
                                            <button class="btn btn-primary text-end" v-on:click="addNote()">Add Note</button>
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
    <div class="modal modal-sm fade" tabindex="-1" id="RequestStatusUpdateModal" data-bs-theme="dark" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style=" background-color:#6c757d!important; color:white">
                <div class="modal-body">
                    <div class="row">
                        <div class="col text-center">
                            Move the request to <b>{{ this.newStatus }}</b>?
                        </div>
                        <div class="col-12 text-center" style="margin-top:1rem;font-weight: 600;font-size:18px">
                            <b class="text-danger" v-show="!this.validForm">Please provide a value for all remedial action plans and expected costs.</b>
                        </div>
                    </div>
                    <div class="row" v-show="!statusUpdated" style="margin-top:1rem">
                        <div class="col text-end">
                            <button class="btn btn-success" v-on:click="updateRequestStatus()">Yes</button>
                        </div>
                        <div class="col">
                            <button class="btn btn-danger" data-bs-dismiss="modal">No</button>
                        </div>
                    </div>
                    <div class="row" v-show="statusUpdated">
                        <div class="col text-center">
                            <i class="fa-solid fa-check" style="font-size:48px;color:#52b963"></i><br>
                            The request has been moved to<br><b>{{ this.newStatus }}</b>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal modal-sm fade" tabindex="-1" id="RequestFundedUpdateModal" data-bs-theme="dark" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style=" background-color:#6c757d!important; color:white">
                <div class="modal-body">
                    <div class="row">
                        <div class="col text-right text-end">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col text-center" style="font-size:18px;margin-top:0.5rem;margin-bottom:0.5rem">
                            Your remediation plan has been updated! If you have not uploaded files for each entry, please do so as soon as possible.
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
  import NoteMixin from '../mixins/NoteMixin'
  import { useStore } from "vuex";
  export default {
    mixins: [RequestsMixin, NoteMixin],
    mounted() {
        document.getElementById('newW9').addEventListener('input', (e) => {
            const files = e.target.files || e.dataTransfer.files;
            const file = files[0];
            this.newW9Extension = file.name.split('.').pop();
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => {
                this.newW9 = reader.result.split(',')[1].toString();
            };
            var buttonEle = document.getElementById("newW9Button");
            buttonEle.disabled = false;
        })
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
    },
    data () {
      return {
        store: useStore(),
        emitter: inject('emitter'),  
        newStatus: "",
        statusUpdated: false,
        newW9: "",
        newW9Extension: "",
        newInvoice: "",
        newInvoiceExtension: "",
        newReceipt: "",
        newReceiptExtension: "",
        newNote: {
            Id: 0,
            RequestId: null,
            Text: "",
            UserId: 0,
            CreatedBy: "",
            CreatedAt: ""
        },
        validForm: true,
        isResent: false,
      }
    },
    watch: {
        'currentRequestDetails.details': {
            deep: true,
            handler() {
                this.updateTotals();
            }
        }
    },
    methods: {
        async getRequestDetails(requestId) {
            $('#RequestDetailsModal').modal('show')
            await this.getRequestDetailsAPI(requestId);
            await this.getRateTableInfo(this.currentRequestDetails.county)
            console.log(this.currentRequestDetails)
            this.statusUpdated = false;
            this.newStatus = "";
        },
        getOperator(operatorCode) {
            return operatorCode == 1 ? "< " : operatorCode == 2 ? "> " : "";
        },
        sanitizeInput(toSanitize) {
            toSanitize = toSanitize.toString().replace(/[^\d]/g, '').substr(0, 6);
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
            var buttonEle = document.getElementById("newReceiptButton");
            buttonEle.disabled = true;
            var inputEle = document.getElementById("newReceipt");
            inputEle.value = ''
        },
        updateRequestStatusModal(status, isResent) {
            this.newStatus = status;
            this.validForm = true;
            this.isResent = isResent;
            $('#RequestStatusUpdateModal').modal('show')
        },
        async updateRequestStatus() {
            console.log(this.canEditPlan)
            if(this.canEditPlan) {
                if(this.currentRequestDetails.details != null && this.currentRequestDetails.details.length > 0) {
                    var currentFormValid = true;
                    for(var i = 0; i < this.currentRequestDetails.details.length; i++) {
                        var currDetail = this.currentRequestDetails.details[i]
                        if(
                            currDetail.remedialActionId && currDetail.remedialActionId != 0 &&
                            currDetail.expectedMaterialCost.toString().trim() !== "" && !isNaN(currDetail.expectedMaterialCost) && currDetail.expectedMaterialCost >= 0 &&
                            currDetail.expectedLaborCost.toString().trim() !== "" && !isNaN(currDetail.expectedLaborCost) && currDetail.expectedLaborCost >= 0
                        ) {}
                        else {
                            currentFormValid = false
                        }
                    }
                    this.validForm = currentFormValid
                    console.log(this.validForm)
                    if(this.validForm) {
                        await this.updateRequestAPI(this.currentRequestDetails)
                        await this.updateRequestStatusAPI(this.currentRequestDetails.id, this.newStatus, this.isResent)
                        this.statusUpdated = true;
                        await this.getRequestDetailsAPI(this.currentRequestDetails.id)
                        this.emitter.emit('updateRequests', true);
                        this.emitter.emit('updateWFKRequests', true);
                    }
                }
            }
            else {
                await this.updateRequestStatusAPI(this.currentRequestDetails.id, this.newStatus)
                this.statusUpdated = true;
                await this.getRequestDetailsAPI(this.currentRequestDetails.id)
                this.emitter.emit('updateRequests', true);
                this.emitter.emit('updateWFKRequests', true);
            }

        },
        async updateFundedData() {
            await this.updateRequestAPI(this.currentRequestDetails)
            $('#RequestFundedUpdateModal').modal('show')
            this.newStatus = "Complete"
            await this.updateRequestStatus()
        },
        updateRemedialAction(detail) {
            if (detail.inHouseLabor && detail.remedialActionId != 0) {
                console.log(this.rateTable)
                var rate = this.rateTable.find(g => g.Id == detail.remedialActionId).NotToExceed
                var rateNum = parseFloat(rate.replace(/\$/g, "").replace(/,/g, ""))
                detail.expectedLaborCost = rateNum
            }
            else {
                detail.expectedLaborCost = 0;
            }
        },
        async addNote() {
            this.newNote.RequestId = this.currentRequestDetails.id
            await this.addNoteAPI(this.newNote);
            this.newNote.Text = "";
            await this.getRequestDetailsAPI(this.currentRequestDetails.id, true)
            this.scrollToNotesBottom()
        },
        scrollToNotesBottom(){
            $('#requestDetailsModalRow').scrollTop($('#requestDetailsModalRow')[0].scrollHeight)
        },   
        updateTotals() {   
            var currentStatus = this.currentRequestDetails.status
            if(currentStatus && currentStatus != "" && currentStatus != null && (currentStatus.toLowerCase() == "new" || currentStatus.toLowerCase() == "draft")){
                var totalCost = 0;
                var totalCostMaterials = 0;
                var totalCostLabor = 0;
                for(var i = 0; i < this.currentRequestDetails.details.length; i++) {
                    var currDetail = this.currentRequestDetails.details[i]
                    if(currDetail.remedialActionId && currDetail.remedialActionId != 0) {
                        totalCost += currDetail.expectedMaterialCost + currDetail.expectedLaborCost
                        totalCostMaterials += currDetail.expectedMaterialCost
                        totalCostLabor += currDetail.expectedLaborCost
                    }
                }
                this.currentRequestDetails.totalCost = totalCost
                this.currentRequestDetails.totalCostMaterials = totalCostMaterials
                this.currentRequestDetails.totalCostLabor = totalCostLabor
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
        showFileUploadSection() {
            if (this.currentRequestDetails.details.length > 0) {
                return this.currentRequestDetails.status.toLowerCase() == 'approved' || this.currentRequestDetails.status.toLowerCase() == 'complete' || this.currentRequestDetails.status.toLowerCase() == 'paid' || this.currentRequestDetails.status.toLowerCase() == 'archived'
            }
            else {
                return false
            }
        },
        showFileUploadButton() {
            return this.store.getters.getUserRole == "Provider" && this.showFileUploadSection
        },
        statusChangeButtonNewStatus() {
            var currentStatus = this.currentRequestDetails.status
            if(currentStatus && currentStatus != "" && currentStatus != null){
                //determines which next action button should be shown, if any
                if (currentStatus.toLowerCase() == "draft" && (this.store.getters.getUserRole == "Admin" || this.store.getters.getUserRole == "WQ Staff")) {
                    return "New"
                }
                if (currentStatus.toLowerCase() == "new" && (this.store.getters.getUserRole == "Admin" || this.store.getters.getUserRole == "Provider")) {
                    return "Submitted"
                }
                if (currentStatus.toLowerCase() == "submitted" && (this.store.getters.getUserRole == "Admin" || this.store.getters.getUserRole == "WQ Staff")) {
                    return "Approved"
                }
                if (currentStatus.toLowerCase() == "approved" && this.store.getters.getUserRole == "Provider") {
                    return "Update Request"
                }
                if (currentStatus.toLowerCase() == "complete" && (this.store.getters.getUserRole == "Admin" || this.store.getters.getUserRole == "WQ Staff")) {
                    return "Paid"
                }
                return "x";
            }
            else {
                return "x";
            }
        },
        statusChangeSecondaryButtonNewStatus() {
            var currentStatus = this.currentRequestDetails.status
            if(currentStatus && currentStatus != "" && currentStatus != null){
                //determines which second next action button should be shown, if any
                if (currentStatus.toLowerCase() == "submitted" && (this.store.getters.getUserRole == "Admin" || this.store.getters.getUserRole == "WQ Staff")) {
                    return "New"
                }
                if (currentStatus.toLowerCase() == "complete" && (this.store.getters.getUserRole == "Admin" || this.store.getters.getUserRole == "WQ Staff")) {
                    return "Approved"
                }
                return "x";
            }
            else {
                return "x";
            }
        },
        fundedAndProvider() {
            var currentStatus = this.currentRequestDetails.status
            if(currentStatus && currentStatus != "" && currentStatus != null){
                if (currentStatus.toLowerCase() == "approved" && this.store.getters.getUserRole == "Provider") {
                    return true
                }
            }
            return false;
        },
        showNewMessage() {
            var show = false;
            var currentStatus = this.currentRequestDetails.status
            if(currentStatus && currentStatus != "" && currentStatus != null){
                if (currentStatus.toLowerCase() == "new" && this.store.getters.getUserRole == "Provider") {
                    return true
                }
            }
        },
        canEditPlan() {
            var currentStatus = this.currentRequestDetails.status
            if(currentStatus && currentStatus != "" && currentStatus != null){
                if ((currentStatus.toLowerCase() == "draft" && (this.store.getters.getUserRole == "Admin" || this.store.getters.getUserRole == "WQ Staff"))
                || (currentStatus.toLowerCase() == "new" && (this.store.getters.getUserRole == "Admin" || this.store.getters.getUserRole == "Provider"))) {
                    return true
                }
            }
            return false;
        },
        logRequest() {
            console.log(this.currentRequestDetails.details)
        },
        getSortedNotes() {
            if(this.currentRequestDetails.Notes != [] && this.currentRequestDetails.Notes != null) {
                return this.currentRequestDetails.Notes.sort((a, b) => new Date(a.CreatedAt) - new Date(b.CreatedAt));
            }
            else {
                return []
            }
        },
    }
  }
</script>