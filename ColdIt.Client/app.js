$(function () {
  const URI = "http://localhost:51044/api/productos";

  // GET PRODUCTS
  $("#getProducts").on("click", () => {
    $.ajax({
      url: URI,
      success: function (products) {
        let tbody = $("tbody");
        tbody.html("");
        products.forEach((product) => {
          tbody.append(`
              <tr>
                <td class="id">${product.id}</td>
                <td>
                  <input type="text" class="codigo" value="${product.codigo}"/>
                </td>
                <td>
                  <input type="text" class="nombre" value="${product.nombre}"/>
                </td>
                <td>
                  <input type="text" class="valor" value="${product.valor}"/>
                </td>
                <td>
                  <button class="update-button">Actualizar</button>
                  <button class="delete-button">Eliminar</button>
                </td>
              </tr>
          `);
        });
      },
    });
  });

  // POST PRODUCTS
$("#buttonCreate").on("click", () => {
  let codigoProducto = $("#codigoProducto");
  let nombreProducto = $("#nombreProducto");
  let valorProducto = $("#valorProducto");

  let producto = {
    codigo: codigoProducto.val(),
    nombre: nombreProducto.val(),
    valor: valorProducto.val(),
  };

  $.ajax({
    url: URI,
    method: "POST",
    dataType: "json",
    data: producto,
    success: function (response) {
      codigoProducto.val("");
      codigoProducto.val("");
      nombreProducto.val("");
      valorProducto.val("");
      $("#getProducts").click();
    },
    error: function (err) {
      console.log(err);
    },
  });
});

  $("table").on("click", ".update-button", function () {
    let row = $(this).closest("tr");
    let id = row.find(".id").text();
    let codigo = row.find(".codigo").val();
    let nombre = row.find(".nombre").val();
    let valor = row.find(".valor").val();

    $.ajax({
      url: `${URI}/${id}`,
      method: "PUT",
      data: {
        codigo: codigo,
        nombre: nombre,
        valor: valor,
      },
      success: function (response) {
        console.log(response);
        $("#getProducts").click();
      },
    });
  });

  $("table").on("click", ".delete-button", function () {
    let row = $(this).closest("tr");
    let id = row.find(".id").text();

    $.ajax({
      url: `${URI}/${id}`,
      method: "DELETE",
      success: function (response) {
        $("#getProducts").click();
      },
    });
  });
});
