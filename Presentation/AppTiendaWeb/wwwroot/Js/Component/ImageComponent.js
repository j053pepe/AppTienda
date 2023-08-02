var ImageComponent = {
    AddChangeWithPreview(idFile, idImage) {
        var ReadFile = () => {
            let file = $(idFile)[0].files;
            if (!file || !file[0]) return;

            const FR = new FileReader();
            FR.addEventListener("load", function (evt) {
                $(idImage).attr("src", evt.target.result);
            });

            FR.readAsDataURL(file[0]);
        }
        $(idFile).on("change", ReadFile);        
    }
}