@ModelType Estudiante
@Code
    ViewBag.Title = "Eliminar Estudiante"
End Code

<h2 class="text-danger">Eliminar Estudiante</h2>
<link href="~/Content/Estudiantes/estudiante.css" rel="stylesheet" />

<div class="container mt-4">
    <div class="card p-4 shadow">
        <p>¿Está seguro que desea eliminar al siguiente estudiante?</p>

        <input type="hidden" id="IdEstudiante" value="@Model.IdEstudiante" />

        <div class="mb-3">
            <label class="form-label fw-bold">Nombres:</label>
            <span>@Model.Nombres</span>
        </div>

        <div class="mb-3">
            <label class="form-label fw-bold">Apellidos:</label>
            <span>@Model.Apellidos</span>
        </div>

        <div class="mb-3">
            <label class="form-label fw-bold">Activo:</label>
            <span>@(If(Model.Activo, "Sí", "No"))</span>
        </div>

        <div class="d-flex justify-content-between">
            <a href="/Estudiante/Index" class="btn btn-secondary">
                <i class="bi bi-arrow-left-circle"></i> Cancelar
            </a>
            <button type="button" id="btnEliminar" class="btn btn-danger">
                <i class="bi bi-trash"></i> Eliminar
            </button>
        </div>

    </div>
</div>
<script src="~/Scripts/Estudiantes/EstudianteEliminar.js"></script>