@ModelType SistemaEstudiantes.AdicionarNotaViewModel
@Code
    ViewData("Title") = "AdicionarNota"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Adicionar Nota</h2>

<form id="formCreateNota">
    @Html.AntiForgeryToken()

    @Html.Hidden("idEstudiante", Model.Estudiante.IdEstudiante, New With {.id = "idEstudiante"})

    <div class="form-group mb-3">
        <label class="form-label">Nombre Estudiante:</label>
        <p class="form-control-static"><strong>@Model.Estudiante.Nombres @Model.Estudiante.Apellidos</strong></p>
    </div>


    <div class="form-group mb-3">
        @Html.Label("idMateria", "Materia")
        @Html.DropDownList("idMateria",
                     New SelectList(Model.Materias, "IdMateria", "Descripcion"),
                     "--- Seleccione una Materia ---",
                     New With {.class = "form-control", .required = "required", .id = "idMateria"})
    </div>

    <hr />

    @Code
        ' Creamos la lista estática para el Periodo
        Dim periodos = New List(Of SelectListItem) From {
        New SelectListItem With {.Value = "2024-1", .Text = "2024-1"},
        New SelectListItem With {.Value = "2024-2", .Text = "2024-2"},
        New SelectListItem With {.Value = "2024-3", .Text = "2024-3"},
        New SelectListItem With {.Value = "2024-4", .Text = "2024-4"}
    }
    End Code
    <div class="form-group mb-3">
        @Html.Label("periodo", "Periodo")
        @Html.DropDownList("periodo",
                periodos,
                "--- Seleccione un Periodo ---",
                New With {.class = "form-control", .required = "required", .id = "periodo"}) 
        </div>

        <div class="form-group mb-3">
            @Html.Label("observacion", "Observación")
            @Html.TextArea("observacion", Nothing, New With {.class = "form-control", .rows = 3, .id = "observacion"})
        </div>

        <hr />

        <div class="form-group mb-4">
            @Html.Label("nota", "Nota")
            @Html.TextBox("nota", Nothing,
                            New With {.class = "form-control", .type = "number", .step = "0.01", .min = "0", .max = "10", .required = "required", .placeholder = "Ej: 10", .id = "nota"})
            <small class="form-text text-muted">Ingrese la nota numérica.</small>
        </div>

        <button type="button" id="btnGuardarNota" class="btn btn-success">Guardar Nota</button> @* Cambié type="submit" a "button" si usas AJAX *@
        @Html.ActionLink("Cancelar", "Index", "Estudiante", Nothing, New With {.class = "btn btn-secondary"})

    </form>
    <script src="~/Scripts/Estudiantes/estudiantenotas.js"></script>