var ProductoReporte ={
    ListProduct:[{productoId:0,nombre:'',codigo:'',precio:0.0,stock:0.0,cantidadVendida:0.0,VentaTotal:0.0}],
    main(){
        this.GetReporte();
    },
    GetReporte(){
        ProductoServices.GetAllByReport()
        .done(result => {
            if (result.data.length > 0) {
                this.ListProduct = result.data;
                this.ListToTable();
            }
        });
    },
    ListToTable(){
        $('#tblProduct tbody').empty();
        this.ListProduct.map(item=> {
            let row=`<tr><th scope="row">${item.productoId}</th>
            <td>${item.nombre}|${item.codigo}</td>
            <td>${formatter.format(item.precio)}</td>
            <td>${item.stock}</td>
            <td>${item.cantidadVendida}</td>
            <td>${formatter.format(item.ventaTotal)}</td>
          </tr>`;
          jQuery("#tblProduct tbody").append(row);
        });
    }
};

ProductoReporte.main();