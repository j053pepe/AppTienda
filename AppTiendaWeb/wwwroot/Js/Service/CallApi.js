var CallApi = (type, url, data) => {
    return $.ajax({
        type: type,
        url: "/api/" + url,
        data: JSON.stringify(data),
        "headers": {
            "Content-Type": "application/json",
            /*"Authorization": "Basic YzUyMzlhMGEtZGQ1YS00OGY2LTk2MDAtZGVhOWQ5ODNjZDVmOjY4NTh5NyM1blY="*/
        }
    }).then(function (response) {
        return response;
    });
};