
var ddlPageSize = 20;

initSelect2();

function initSelect2() {
    $("select.remote").each(function (index) {
        select2FillRemote($(this));
    });

    $("select.local").each(function (index) {
        select2FillLocal($(this));
    });
}

function select2FillRemote(el) {
    if ($(el).data('select2')) {
        return;
    }
    //var allowClear = $(el).attr('data-option-allowClear') ? $(el).attr('data-option-allowClear') == "true" : !$(el).hasClass('multiselect');

    $(el).select2({
        multiple: $(el).hasClass('multiselect'),
        closeOnSelect: !$(el).hasClass('multiselect'),
        allowClear: true,
        width: '100%', // https://github.com/select2/select2/issues/3278
        placeholder: $(el).attr('placeholder'),
        ajax: {
            url: $(el).attr('data-url'),
            dataType: 'json',
            delay: 250,
            data: function (params) {
                var req = {
                    pageSize: ddlPageSize,
                    pageNum: params.page ? params.page : 1,
                    searchTerm: params.term ? params.term : ''
                };

                var attrs = $(el).get(0).attributes;
                $.each(attrs, function (index, attr) {
                    if (attr && attr.name && attr.name.indexOf('data-param-') === 0) {
                        req[attr.name.replace('data-param-', "")] = attr.value;
                    }
                });

                return req;
            },
            processResults: function (data, params) {
                var page = params.page ? params.page : 1;
                var more = (page * ddlPageSize) < data.Total;
                setTimeout(function () {
                    var grItems = $('.select2-results .select2-results__group');
                    if (grItems.length) {
                        $(grItems).each(function (index, element) {
                            if ($(element).html().indexOf('[remove]') >= 0) {
                                $(element).remove();
                            }
                        });
                    }
                }, 10);
                return {
                    results: data.Results,
                    pagination: {
                        more: more
                    }
                };
            },
            cache: false
        }
    });
}

function select2FillLocal(el) {
    if ($(el).data('select2')) {
        return;
    }

    $(el).select2({
        width: '100%',
        placeholder: $(el).attr('placeholder'),
        allowClear: $(el).attr('allowClear'),
        selectOnClose: $(el).attr('data-option-selectonclose'),
        createTag: function (params) {

            if (!$(el).attr('data-option-allowtags') || !isNaN(parseInt(params.term))) {
                // Return null to disable tag creation for numbers
                return null;
            }
            return {
                id: params.term,
                text: params.term
            };
        }
    });
}