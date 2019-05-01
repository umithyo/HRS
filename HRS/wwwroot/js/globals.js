$(document).ready(function () {
    function regDependentSelect(dependeeDOMId, dependantDOMId, url, dependeeFirstOption, dependantFirstOption, twoWay) {
        $(dependeeDOMId).on('change', function () {
            $(dependantDOMId).html("<option>" + dependantFirstOption + "</option>");
            if (this.value != dependeeFirstOption) {
                if (twoWay == null) {
                    $(dependantDOMId).html("<option>Yükleniyor...</option>");
                }
                $.ajax({
                    url: url + "/" + this.value,
                    success: function (data) {
                        if (twoWay == null) {
                            $(dependantDOMId).html("<option>" + dependantFirstOption + "</option>");
                        } else {
                            let current = $(dependantDOMId).find("option").children();
                            console.log(current);
                        }

                        $.each(data, function () {
                            $(dependantDOMId).append($("<option />").val(this.id).text(this.name));
                        });
                    }
                });
            }
        });
    }

    window.regDependentSelect = regDependentSelect;
});