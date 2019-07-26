hljs.initHighlightingOnLoad();
// 复制内容
var clipboard = new ClipboardJS('.copy-btn');
clipboard.on('success', function (e) {
    layer.msg('复制成功！', { icon: 6, time: 1200, shade: [0.1, '#000'] });
});
clipboard.on('error', function (e) {
    layer.msg('复制失败！', { icon: 5, time: 1200, shade: [0.1, '#000'] });
});