<template>
    <div class="modal modal-lg" tabindex="-1" id="AdminSettingsModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style="max-height:700px; overflow-y: auto;">
                <div class="modal-body">
                    <div class="row">
                        <div class="col">
                            <ul class="nav nav-pills">
                                <li class="nav-item">
                                    <a class="nav-link active" id="tos-tab" data-bs-toggle="tab" data-bs-target="#tos" type="button" role="tab" aria-controls="tos" aria-selected="true">Terms of Service</a>
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
                                <div class="tab-pane fade show active" id="tos" role="tabpanel" aria-labelledby="tos-tab">
                                    <div id="editor">
                                    </div>
                                    <div class="row rowSpacing">
                                        <div class="col text-end">
                                            <button class="btn h20-btn btn-spacing" v-on:click="updateTermsofService()">Save</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="request" role="tabpanel" aria-labelledby="request-tab">

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
    import LoginMixin from '@/mixins/LoginMixin'
    export default {
        mixins: [LoginMixin],
        data () {
            return {
                store: useStore(),
                terms: "",
            }
        },
        async created() {
            const fontSizeArr = ['8px','12px','16px','24px','32px','54px',];

            //this allows you to use normal font sizes instead of 'huge' and 'normal' like in the quill docs
            var Size = Quill.import('attributors/style/size');
            Size.whitelist = fontSizeArr;
            Quill.register(Size, true);

            var toolbarOptions = [      
                [{ 'font': [] }],
                [{ 'size': fontSizeArr }],
                ['bold', 'italic', 'underline', 'strike'],  
                [{ 'color': [] }, { 'background': [] }],
                [{ 'list': 'ordered'}, { 'list': 'bullet' }],
                [{ 'script': 'sub'}, { 'script': 'super' }],
                [{ 'align': [] }],
                ['blockquote'],     
                ['clean']
            ];
            var options = {
                modules: {
                    toolbar: toolbarOptions
                },
                theme: 'snow'
            };
            var editor = {};
            setTimeout( () =>
            {
                editor = new Quill('#editor', options);
            }, 10 );

            await this.getTOS();
            
            editor.root.innerHTML = this.tos

            editor.on('text-change', (delta, source) => {
                this.tos = editor.root.innerHTML;
            });
        },
        methods: {
            async updateTermsofService() {
                await this.updateTOS(this.tos);
            }
        },
        computed: {}
    }


</script>