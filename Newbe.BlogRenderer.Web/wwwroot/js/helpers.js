const wechatRender = (id) => {
    // set style for pre
    const pres = document.querySelectorAll(id + " pre");
    pres.forEach(function (pre) {
        pre.style = "margin-top: 10px; margin-bottom: 10px; border-radius: 5px; box-shadow: rgba(0, 0, 0, 0.55) 0px 2px 10px;";
        // insert a span into pre
        const span = document.createElement("span");
        span.style = "display: block; background-image: url('data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZlcnNpb249IjEuMSIgIHg9IjBweCIgeT0iMHB4IiB3aWR0aD0iNDUwcHgiIGhlaWdodD0iMTMwcHgiPgogIDxlbGxpcHNlIGN4PSI2NSIgY3k9IjY1IiByeD0iNTAiIHJ5PSI1MiIgc3Ryb2tlPSJyZ2IoMjIwLDYwLDU0KSIgc3Ryb2tlLXdpZHRoPSIyIiBmaWxsPSJyZ2IoMjM3LDEwOCw5NikiLz4KICA8ZWxsaXBzZSBjeD0iMjI1IiBjeT0iNjUiIHJ4PSI1MCIgcnk9IjUyIiAgc3Ryb2tlPSJyZ2IoMjE4LDE1MSwzMykiIHN0cm9rZS13aWR0aD0iMiIgZmlsbD0icmdiKDI0NywxOTMsODEpIi8+CiAgPGVsbGlwc2UgY3g9IjM4NSIgY3k9IjY1IiByeD0iNTAiIHJ5PSI1MiIgIHN0cm9rZT0icmdiKDI3LDE2MSwzNykiIHN0cm9rZS13aWR0aD0iMiIgZmlsbD0icmdiKDEwMCwyMDAsODYpIi8+Cjwvc3ZnPg=='); height: 30px; width: 100%; background-size: 40px; background-repeat: no-repeat; background-color: #fafafa; margin-bottom: -7px; border-radius: 5px; background-position: 10px 10px;";
        pre.insertBefore(span, pre.firstChild);
    });

    // set style for pre.code
    const codePres = document.querySelectorAll(id + " pre code");
    codePres.forEach(function (codePre) {
        codePre.style = "overflow-x: auto; padding: 16px; color: #383a42; display: -webkit-box; font-family: Operator Mono, Consolas, Monaco, Menlo, monospace; font-size: 12px; -webkit-overflow-scrolling: touch; padding-top: 15px; background: #fafafa; border-radius: 5px;";

        // replace space with &nbsp;
        codePre.innerHTML = codePre.innerHTML.replace(/> </g, ">&nbsp;<");
    });

    // set style for class="hljs-keyword" style="color: #a626a4; line-height: 26px;"
    const keywords = document.querySelectorAll(id + " .hljs-keyword");
    keywords.forEach(function (keyword) {
        keyword.style = "color: #a626a4; line-height: 26px;";
    });

    // class="hljs-function" style="line-height: 26px;"
    const functions = document.querySelectorAll(id + " .hljs-function");
    functions.forEach(function (func) {
        func.style = "line-height: 26px;";
    });

    // class="hljs-title" style="color: #4078f2; line-height: 26px;"
    const titles = document.querySelectorAll(id + " hljs-title");
    titles.forEach(function (title) {
        title.style = "color: #4078f2; line-height: 26px;";
    });

    // class="hljs-params" style="line-height: 26px;"
    const params = document.querySelectorAll(id + " .hljs-params");
    params.forEach(function (param) {
        param.style = "line-height: 26px;";
    });
};

window.mdRender = function (platform, id) {
    console.log("jsRender", platform, id);
    // highlight id
    document.querySelectorAll(id + " pre code").forEach((block) => {
        hljs.highlightBlock(block);
    });
    switch (platform) {
        case "Wechat":
            wechatRender(id);
            break;
    }

}

window.copyOut = async function (platform, content) {
    console.info("copyOut", platform);
    let copyMimeType = 'text/html';
    let contentToCopy = document.querySelector("#copyOut").innerHTML;
    const planTextPlatforms = ["InfoQ", "TencentCloud", "Bilibili", "Csdn", "Sifou"];
    if (planTextPlatforms.includes(platform)) {
        // for plain text, it should come from C# since there is some html tag in the content.
        // it is not a good idea to use innerHTML to get the content.
        copyMimeType = 'text/plain';
        contentToCopy = content;
    }
    console.info("copyOut", copyMimeType, contentToCopy);
    const blobInput = new Blob([contentToCopy], {type: copyMimeType});
    const clipboardItemInput = new ClipboardItem({[copyMimeType]: blobInput});
    await navigator.clipboard.write([clipboardItemInput]);
}

window.copyMarkdown = async function () {
    const content = document.querySelector("#markdown_source").value;
    const blobInput = new Blob([content], {type: 'text/plain'});
    const clipboardItemInput = new ClipboardItem({'text/plain': blobInput});
    await navigator.clipboard.write([clipboardItemInput]);
}