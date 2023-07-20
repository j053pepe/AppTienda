var CallApi = (type, url, data = "") => {
    blockScreen();
    var dfd = $.Deferred();

    var Api = $.ajax({
        url: "/api/" + url,
        contentType: 'application/json; charset=utf-8',
        "headers": {
            "Token": localStorage.getItem("token")
        },
        dataType: 'json',
        data: data == "" ? "" : JSON.stringify(data),
        type: type,
    });

    Api.done(function (data) {
        $('#bodyPage').unblock();
        if (data.statusCode == 500) { main.CerrarSesion(); }
        else {
            if (data.newToken != null) { localStorage.setItem("token", data.newToken); }
            dfd.resolve(data);
        }
    }).fail(function (data) {
        $('#bodyPage').unblock();
        console.log("fail");
        dfd.reject(data);
    });

    return dfd.promise();
};

var CallApiFormData = (type, url, data) => {
    blockScreen();
    var dfd = $.Deferred();

    var Api = $.ajax({
        url: "/api/" + url,
        processData: false,
        "headers": {
            "Token": localStorage.getItem("token")
        },
        mimeType: "multipart/form-data",
        contentType: false,
        data: data,
        type: type,
    });

    Api.done(function (data) {
        $('#bodyPage').unblock();
        if (data.statusCode == 500) { main.CerrarSesion(); }
        else {
            if (data.newToken != null) { localStorage.setItem("token", data.newToken); }
            dfd.resolve(JSON.parse(data));
        }
    }).fail(function (data) {
        $('#bodyPage').unblock();
        dfd.reject(JSON.parse(data));
    });

    return dfd.promise();
};

var CallJson = (url) => {

    var dfd = $.Deferred();

    var Api = $.ajax({
        url: url,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: "Get",
    });

    Api.done(function (data) {
        dfd.resolve(data);
    }).fail(function (data) {
        dfd.reject(data);
    });

    return dfd.promise();
};

var CallEstadoApi = {
    GetAll() {
        return CallJson("data/Estadosdb.json")
            .done(result => {
                return result;
            });
    },
    GetById(id) {
        return CallJson("data/Estadosdb.json")
            .then(result => {
                return result.find(x => x.EstadoId == id);
            });
    }
};

var LoadContent = (htmlUrl, divId) => {
    var dfd = $.Deferred();

    var Api = $.ajax({
        url: htmlUrl,
        contentType: 'text/html; charset=utf-8',
        type: "Get",
    });

    Api.done(function (data) {
        dfd.resolve(data);
    }).fail(function (data) {
        dfd.reject(data);
    });

    return dfd.promise();
}

// Create our number formatter.
const formatter = new Intl.NumberFormat('es-MX', {
    style: 'currency',
    currency: 'MXN',

    // These options are needed to round to whole numbers if that's what you want.
    //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
    //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
});

var blockScreen = (id = '') => {
    $(id == '' ? '#bodyPage' : id).block({
        message: `<div class="spinner-border text-primary" role="status">
    <span class="visually-hidden">Loading...</span>
  </div>`,
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}