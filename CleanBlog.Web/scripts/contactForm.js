var contactForm = contactForm || {
    init: function () {
        this.listeners();
    },
    listeners: function () {
        $(document).on('click', '.contact-submit', function (e) {
            e.preventDefault();
            let form = $("#contact-form");
            form.submit();
        });
    },
    showResult: function () {
        $("#form-outer").hide('slow');
        $('#form-result').show('slow');
    }
}
contactForm.init();