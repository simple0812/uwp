function sayHelloToJs(name) {
    return bar.sayHelloToJs(name);
}

function sayHelloToApp() {
    // 传递数据给 app
    window.external.notify('foo js to app');
}

var bar = {
    sayHelloToJs: function (name) {
        viewport.innerHTML = 'app to foo js: ' + name;
        return "xxxxx";
    }
}