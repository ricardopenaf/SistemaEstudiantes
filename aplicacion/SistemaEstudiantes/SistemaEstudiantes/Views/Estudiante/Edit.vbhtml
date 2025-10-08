@Code
    ViewBag.Title = "Editar Estudiante"
End Code

<h2>Editar Estudiante</h2>
<link href="~/Content/Estudiantes/estudiante.css" rel="stylesheet" />

<div class="container mt-4">
    <form id="formEditarEstudiante" class="card p-4 shadow">
        @Html.AntiForgeryToken()
        <input type="hidden" id="IdEstudiante" />

        <div class="mb-3">
            <label for="Nombres" class="form-label">Nombres</label>
            <input type="text" id="Nombres" class="form-control" required />
        </div>

        <div class="mb-3">
            <label for="Apellidos" class="form-label">Apellidos</label>
            <input type="text" id="Apellidos" class="form-control" required />
        </div>

        <div class="mb-3">
            <label for="Edad" class="form-label">Edad</label>
            <input type="number" id="Edad" class="form-control" min="1" required />
        </div>

        <div class="mb-3">
            <label for="Sexo" class="form-label">Sexo</label>
            @Html.DropDownList("Sexo",
                New SelectList(New List(Of Object) From {
                    New With {.Value = "M", .Text = "Masculino"},
                    New With {.Value = "F", .Text = "Femenino"}
                }, "Value", "Text"),
                "Seleccione...",
                New With {.class = "form-select", .id = "Sexo"})
        </div>

        <div class="mb-3">
            <label for="FechaNacimiento" class="form-label">Fecha de Nacimiento</label>
            <input type="date" id="FechaNacimiento" class="form-control" required />
        </div>

        <div class="mb-3">
            <label for="Direccion" class="form-label">Dirección</label>
            <input type="text" id="Direccion" class="form-control" required />
        </div>

        <div class="mb-3">
            <label for="Telefono" class="form-label">Teléfono</label>
            <input type="text" id="Telefono" class="form-control" required />
        </div>

        <div class="mb-3">
            <label for="Email" class="form-label">Correo Electrónico</label>
            <input type="email" id="Email" class="form-control" required />
        </div>

        <div class="mb-3">
            <label for="IdCurso" class="form-label">Curso</label>
            @Html.DropDownList("IdCurso", CType(ViewBag.Cursos, SelectList), "Seleccione un curso", New With {.class = "form-select", .id = "IdCurso"})
        </div>

        <div class="mb-3 form-check">
            <input type="checkbox" id="Activo" class="form-check-input" />
            <label class="form-check-label" for="Activo">Activo</label>
        </div>

        <div class="d-flex justify-content-between">
            <a href="/Estudiante/Index" class="btn btn-secondary">
                <i class="bi bi-arrow-left-circle"></i> Volver
            </a>
            <button type="button" id="btnGuardar" class="btn btn-primary">
                <i class="bi bi-save"></i> Guardar Cambios
            </button>
        </div>
    </form>
</div>


@*<script src="~/Scripts/Estudiantes/estudiante.js"></script>*@
<script src="~/Scripts/Estudiantes/EstudianteEdit.js"></script>