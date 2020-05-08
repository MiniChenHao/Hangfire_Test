function InsertIntoTable(url) {
    var dataObj = new Object();
    $.ajax({
        url: url,
        type: "post",
        dataType: "json",
        data: dataObj,
        success: function (data) {
            if (data.IsOK) {
                swal({ title: '成功', text: '任务添加成功', icon: 'success', button: '确定' });
            } else {
                swal({ title: '错误', text: '任务添加失败', icon: 'error', button: '确定' });
            }
        }
    });
}