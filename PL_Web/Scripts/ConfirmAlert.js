class ConfirmAlert {
    constructor(mainContainer = 'alert-container') {
        let html = `
        <div class="modal fade" id="confirm-modal" tabindex="-1" aria-labelledby="confirm-modal-title" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <strong class="text-center" id="confirm-modal-title"></strong>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="text-center">
                            <img src="/content/imagenes/advertenciaDelete.png" class="mb-3" style="width:25%" alt="Imagen alerta" />
                            <p id="confirm-modal-message"></p>
                        </div>
                    </div>
                    <div class="modal-footer text-center">
                        <button type="button" class="btn btn-primary" data-bs-dismiss="modal">
                            Cancelar <i class="bi bi-x-square-fill"></i>
                        </button>
                        <button type="button" class="btn btn-danger" id="confirm-modal-btn">
                            <i class="bi bi-trash3-fill"></i> Sí, eliminar
                        </button>
                    </div>
                </div>
            </div>
        </div>
        `;

        // Agregamos el modal al contenedor principal
        $("#" + mainContainer).html(html);

        // Inicializamos las referencias
        this.modal = $("#confirm-modal");
        this.title = $("#confirm-modal-title");
        this.message = $("#confirm-modal-message");
        this.confirmButton = $("#confirm-modal-btn");
    }

    show(title = "Confirmación", message = "¿Está seguro?", callback = null) {
        this.title.html(title);
        this.message.html(message);

        // Eliminamos eventos anteriores y asignamos el nuevo callback
        this.confirmButton.off("click").on("click", function () {
            if (typeof callback === "function") {
                callback();
            }
            $("#confirm-modal").modal("hide")
        });

        $("#confirm-modal").modal('show')
    }
}
