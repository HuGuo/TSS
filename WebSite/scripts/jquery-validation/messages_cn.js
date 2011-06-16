jQuery.extend(jQuery.validator.messages, {
    required: "必选字段",
    remote: "请修正该字段",
    email: "请输入正确格式的电子邮件",
    url: "请输入合法的网址",
    date: "请输入合法的日期",
    dateISO: "请输入合法的日期 (ISO).",
    number: "请输入合法的数字",
    digits: "只能输入整数",
    creditcard: "请输入合法的信用卡号",
    equalTo: "请再次输入相同的值",
    accept: "请输入拥有合法后缀名的字符串",
    maxlength: jQuery.validator.format("请输入一个长度最多是 {0} 的字符串"),
    minlength: jQuery.validator.format("请输入一个长度最少是 {0} 的字符串"),
    rangelength: jQuery.validator.format("请输入一个长度介于 {0} 和 {1} 之间的字符串"),
    range: jQuery.validator.format("请输入一个介于 {0} 和 {1} 之间的值"),
    max: jQuery.validator.format("请输入一个最大为 {0} 的值"),
    min: jQuery.validator.format("请输入一个最小为 {0} 的值")
});
jQuery.metadata.setType("attr", "validate"); //自定义验证属性，添加属性validate
//jQuery.validator.setDefaults({
//    submithandler: function (form) {
//        alert("submitted!"); form.submit(); //form提交，用form.submit()
//    }, //设置submit
//    // meta: "tt",//在class中添加tt,{tt:{}}
//    debug: true, //只验证，不提交
//    ignore: "#ignore", //排除验证
//    errorPlacement: function (error, element) {
//        error.appendTo(element.parent().parent());
//    }, //错误的位置
//    errorClass: "tt",
//    errorElement: "em",
//    errorContainer: "div.error",
//    errorLabelContainer: "#signForm div.error",
//    wrapper: "li",
//    nsubmit: true,
//    focusInvalid: true
//});
