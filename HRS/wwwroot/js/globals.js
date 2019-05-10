$(document).ready(function () {
    //only use if there are two dependencies in other words, two select boxes.
    function regDependentSelect(dependeeDOMId, dependantDOMId, url, dependeeFirstOption, dependantFirstOption) {
        $(dependeeDOMId).on('change', function () {
            $(dependantDOMId).html("<option>" + dependantFirstOption + "</option>");
            if (this.value !== dependeeFirstOption) {
                $(dependantDOMId).html("<option>Yükleniyor...</option>");
                $.ajax({
                    url: url + "/" + this.value,
                    success: function (data) {
                        $(dependantDOMId).html("<option>" + dependantFirstOption + "</option>");

                        $.each(data, function () {
                            $(dependantDOMId).append($("<option />").val(this.id).text(this.name));
                        });
                        $(dependeeDOMId).trigger("OptionsLoaded");
                    }
                });
            }
        });
    }

    window.regDependentSelect = regDependentSelect;
});