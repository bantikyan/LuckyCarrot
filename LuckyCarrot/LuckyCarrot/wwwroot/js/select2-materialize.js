
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
    var allowClear = $(el).attr('data-option-allowClear') ? $(el).attr('data-option-allowClear') == "true" : !$(el).hasClass('multiselect');

    var maxSelection = $(el).attr('data-option-maxSelect') ? $(el).attr('data-option-maxSelect') : 0;
    var minSelection = $(el).attr('data-option-minSelect') ? $(el).attr('data-option-minSelect') : 0;

    $(el).select2({
        multiple: $(el).hasClass('multiselect'),
        closeOnSelect: !$(el).hasClass('multiselect'),
        minimumInputLength: 0,
        allowClear: allowClear,
        maximumSelectionLength: maxSelection,
        width: '100%', // https://github.com/select2/select2/issues/3278
        placeholder: $(el).attr('placeholder'),
        tags: $(el).attr('data-option-allowtags'),
       
        createTag: function (params) {

            if (!$(el).attr('data-option-allowtags') || !isNaN(parseInt(params.term)))
            {
                // Return null to disable tag creation for numbers
                return null;
            }
            return {
                id: params.term,
                text: params.term
            };
        },
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

    var $searchField = $(el).data('select2').dropdown.$search || $(el).data('select2').selection.$search;
    $searchField.addClass('form-control');
    $searchField.parent().addClass('fg-line');

    $(el).on("select2:select", function (evt) {
        if ($(this).hasClass('multiselect')) {
            $(window).scroll();
        }
    });

    $(el).on("select2:unselecting", function (evt) {
        var opts = $(this).data('select2').options;
        opts.set('disabled', true);
        setTimeout(function () {
            opts.set('disabled', false);
        }, 1);
    });

    $(el).on("select2:unselect", function (evt) {
        if ($(this).hasClass('multiselect')) {
            $(window).scroll();
        }
    });

    $(el).on("select2:close", function () {
        $(this).focus();
    });

    //Minimum LEnght Section 

    $(el).on("change", function () {
        if (el.hasClass("multiselect")) {
            el.select2('close');
            $("#minSelect").remove("#minSelect");
            var message = "#" + el["0"].name + " + span";
            var count = el.val().length;
            var width = el["0"].nextSibling.clientWidth;
            if (minSelection > count) {
                $(message).append("<span id='minSelect' class='select2-results select2-results__option' style ='width:" + width + "px; position:absolute'> Minimum count of items: " + minSelection + "</span>");
                $("#btnSubmit").prop('disabled', true);
            }
            else {
                $("#btnSubmit").prop('disabled', false);
            }

            if (count >= maxSelection) {
                el.select2('close');
            }
        }
    });

    // Trigger search
    $(el).on('keydown', function (e) {
        var $select = $(el), $select2 = $select.data('select2'), $container = $select2.$container;

        // Unprintable keys
        if (typeof e.which === 'undefined' || $.inArray(e.which, [0, 8, 9, 12, 16, 17, 18, 19, 20, 27, 33, 34, 35, 36, 37, 38, 39, 44, 45, 46, 91, 92, 93, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 123, 124, 144, 145, 224, 225, 57392, 63289]) >= 0) {
            return true;
        }

        // Opened dropdown
        if ($container.hasClass('select2-container--open')) {
            return true;
        }

        $select.select2('open');

        // Default search value
        var $search = $select2.dropdown.$search || $select2.selection.$search, query = $.inArray(e.which, [13, 40, 108]) < 0 ? String.fromCharCode(e.which) : '';
        if (query !== '') {
            $search.val(query).trigger('keyup');
        }
    });

    // Format, placeholder

    //if (!$(el).hasClass('multiselect')) {
    //    $(el).on('select2:open', function (e) {
    //        var $select = $(el), $select2 = $select.data('select2'), $dropdown = $select2.dropdown.$dropdown || $select2.selection.$dropdown, $search = $select2.dropdown.$search || $select2.selection.$search, data = $select.select2('data');

    //        // Placeholder
    //        if (data[0]) {
    //            $search.attr('placeholder', (data[0].text !== '' ? data[0].text : $select.attr('placeholder')));
    //        }
    //    });
    //}
}

function select2FillLocal(el) {
    if ($(el).data('select2')) {
        return;
    }

    $(el).select2({
        width: '100%',
        placeholder: $(el).attr('placeholder'),
        allowClear: $(el).attr('allowClear'),
        tags: $(el).attr('data-option-allowtags'),
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