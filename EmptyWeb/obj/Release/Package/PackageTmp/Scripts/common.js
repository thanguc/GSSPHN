$.fn.AjaxData = function (url, params, success, failure) {
    $.ajax({
        url: url,
        type: 'post',
        data: params,
        dataType: 'json',
        success: function (data) {
            success(data);
        },
        error: function (data) {
            if (failure) {
                failure(data);
            }
        }
    });
}

$.fn.AjaxHtml = function (url, params) {
    var $this = $(this);
    $.ajax({
        url: url,
        type: 'post',
        data: params,
        dataType: 'html',
        success: function (html) {
            $this.empty().html(html);
        }
    });
};

$.fn.bindSelect2 = function (rawData, placeholder, hideSearch, startId) {
    var selectData = [];
    var $this = $(this);
    for (var item in rawData) {
        selectData.push({ id: startId ? startId : selectData.length, text: rawData[selectData.length] });
        startId++;
    }
    $this.select2({
        theme: "bootstrap",
        placeholder: placeholder,
        data: selectData,
        minimumResultsForSearch: hideSearch ? -1 : 0
    });
    if ($this.attr('value')) {
        //console.log($this.select2());
        $this.select2('val', $this.attr('value'));
    }
};