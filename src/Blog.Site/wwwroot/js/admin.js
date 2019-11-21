(function ($) {
    //$('.edit select.dropdown').dropdown({
    //    action: 'activate',
    //    onChange: function (value) {
    //        $('.edit #type').val(value);
    //    }
    //});

    $('.ui.checkbox').checkbox();

    let selectedTag = $('.edit #selected-tag').val();
    selectedTag = selectedTag.replace(/\[|\]/g, '')
    let tagList = selectedTag.split(',');
    tagList = tagList.filter(a => a);
    $('.edit .itemTag').each(function () {
        const tagId = parseInt($(this).children('label').attr('data-id'));
        if (tagList.indexOf(tagId) >= 0) {
            $(this).checkbox('check');
        }
    });

    $('.edit .itemTag').checkbox({
        onChecked: function () {
            const tagId = parseInt($(this).siblings('label').attr('data-id'));
            if (tagList.indexOf(tagId) < 0) {
                tagList.push(tagId);
                $('.edit #selected-tag').val(tagList.join(','));
            }
        },
        onUnchecked: function () {
            const tagId = parseInt($(this).siblings('label').attr('data-id'));
            const index = tagList.indexOf(tagId)
            if (index >= 0) {
                tagList.splice(index, 1);
                $('.edit #selected-tag').val(tagList.join(','));
            }
        }
    });

    var editor = editormd("editor", {
        width: "100%",
        height: "600",
        path: "/libs/editor.md/lib/",
        theme: "dark",
        previewTheme: "dark",
        editorTheme: "pastel-on-dark",
        saveHTMLToTextarea: true,// 保存 HTML 到 Textarea
        flowChart: true,             // 开启流程图支持，默认关闭
        sequenceDiagram: true,       // 开启时序/序列图支持，默认关闭
        codeFold: true,
        atLink: false,
        emoji: true,
        editorTheme: 'default',
    });

    $('.ui.form').form({
        inline: true,
        on: 'blur',
        fields: {
            title: {
                identifier: 'title',
                rules: [
                    {
                        type: 'empty',
                        prompt: '请输入文章标题'
                    },
                    {
                        type: 'length[3]',
                        prompt: '您的文章标题太短，请重新输入。'
                    }
                ]
            },
            articleKey: {
                identifier: 'articleKey',
                rules: [{
                    type: 'length[3]',
                    prompt: '您的文章内容太短，请重新输入。'
                }]
            }
        },
        onSuccess: function (event, fields) {
            event.preventDefault();
            console.info(fields);

            fields.markdown = editor.getMarkdown();
            fields.html = editor.getHTML();
            fields.tags = fields.selected-tag.split(',');

            delete fields['editor-html-code'];
            delete fields['editor-markdown-doc'];
            delete fields['selected-tag'];

            $.ajax({
                type: 'post',
                headers: {
                    'Authorization': 'Bearer ' + window.localStorage.getItem('token')
                },
                contentType: 'application/json',
                cache: false,
                url: 'http://localhost:5011/articles',
                data: JSON.stringify(fields),
                success: function (data, status) {
                    if (status === 'success') {
                        console.info(data);

                        //window.localStorage.setItem('token', data.access_token);
                        //window.location.href = "/";
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log('ERROR:' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5, time: 5000 });
                }
            });
        }
    });
})($);