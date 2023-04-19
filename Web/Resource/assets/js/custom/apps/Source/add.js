"use strict";

// Class definition
var KTUsersAddUser = function () {
    // Shared variables
    var table;
    const element = document.getElementById('kt_modal_add_source');
    const form = element.querySelector('#kt_modal_add_source_form');
    const modal = new bootstrap.Modal(element);

    // Init add schedule modal
    var initAddUser = () => {

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        var validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'ProductNumber': {
                        validators: {
                            notEmpty: {
                                message: 'Product number is required'
                            }
                        }
                    },
                    'Name': {
                        validators: {
                            notEmpty: {
                                message: 'Name is required'
                            }
                        }
                    },
                    'PickupZone': {
                        validators: {
                            notEmpty: {
                                message: 'Pickup Zone is required'
                            }
                        }
                    },
                    'Supplier': {
                        validators: {
                            notEmpty: {
                                message: 'Supplier is required'
                            }
                        }
                    },
                    'Cost': {
                        validators: {
                            notEmpty: {
                                message: 'Cost is required'
                            },
                        }
                    },
                    'TierId': {
                        validators: {
                            notEmpty: {
                                message: 'Tier is required'
                            },
                        }
                    },
                    'CountryId': {
                        validators: {
                            notEmpty: {
                                message: 'Country is required'
                            },
                        }
                    },
                },

                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        );

        // Submit button handler
        const submitButton = element.querySelector('[data-kt-users-modal-action="submit"]');
        submitButton.addEventListener('click', e => {
            e.preventDefault();
            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    console.log('validated!');

                    if (status == 'Valid') {
                        // Show loading indication
                        submitButton.setAttribute('data-kt-indicator', 'on');

                        // Disable button to avoid multiple click 
                        submitButton.disabled = true;

                        // Simulate form submission. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                        setTimeout(function () {
                            // Remove loading indication
                            submitButton.removeAttribute('data-kt-indicator');
                            // Enable button
                            submitButton.disabled = false;

                            Swal.fire({
                                title: 'Are you sure?',
                                text: "Are you sure you would like to save this source?",
                                icon: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Confirm'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $.ajax({
                                        url: '/source/save/',
                                        data: $('#kt_modal_add_source_form').serialize(),
                                        type: 'POST',
                                        dataType: "json",
                                        traditional: true,
                                        success: function (result) {
                                            if (result.ok) {
                                                Swal.fire({ text: result.message, icon: "success", buttonsStyling: false, confirmButtonText: "Ok", customClass: { confirmButton: "btn btn-primary" } }).then(function () {
                                                    window.location = "/source";
                                                });
                                            }
                                            else {
                                                $("html, body").animate({ scrollTop: 0 }, 'slow');
                                                Swal.fire({ text: result.message, html: result.errormgs, icon: "error", buttonsStyling: false, confirmButtonText: "Ok", customClass: { confirmButton: "btn btn-primary" } });
                                            }
                                        }
                                    });
                                }
                            })

                            // Show popup confirmation 
                            //Swal.fire({
                            //    text: "Form has been successfully submitted!",
                            //    icon: "success",
                            //    buttonsStyling: false,
                            //    confirmButtonText: "Ok, got it!",
                            //    customClass: {
                            //        confirmButton: "btn btn-primary"
                            //    }
                            //}).then(function (result) {
                            //    if (result.isConfirmed) {
                            //        modal.hide();
                            //    }
                            //});
                            //form.submit(); // Submit form
                        }, 1000);
                    } else {
                        // Show popup warning. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                        Swal.fire({
                            text: "Sorry, looks like there are some errors detected, please try again.",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    }
                });
            }
        });

        // Cancel button handler
        const cancelButton = element.querySelector('[data-kt-users-modal-action="cancel"]');
        cancelButton.addEventListener('click', e => {
            e.preventDefault();
            Swal.fire({
                text: "Are you sure you would like to cancel?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, cancel it!",
                cancelButtonText: "No, return",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form			
                    modal.hide();	
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        });

        // Close button handler
        const closeButton = element.querySelector('[data-kt-users-modal-action="close"]');
        closeButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                text: "Are you sure you would like to cancel?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, cancel it!",
                cancelButtonText: "No, return",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form			
                    modal.hide();	
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        });
    }




    return {
        // Public functions
        init: function () {
            initAddUser();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTUsersAddUser.init();
});