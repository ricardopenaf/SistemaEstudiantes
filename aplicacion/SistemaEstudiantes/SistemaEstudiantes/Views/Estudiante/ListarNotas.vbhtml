@ModelType ListarNotasViewModel
@* Asumimos que ListarNotasViewModel ya existe y tiene IdEstudiante y NombreCompletoEstudiante *@
@Code
    ViewData("Title") = "Notas del Estudiante"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2 class="mb-3">Calificaciones de @Model.NombreCompletoEstudiante</h2>

<div class="alert alert-info mb-4" role="alert">
    <strong>ID:</strong> <span id="idEstudianteVisual">@Model.IdEstudiante</span>
</div>

<div class="table-responsive">
    <table class="table table-striped table-bordered table-hover" id="tablaCalificaciones">
        <thead class="table-dark">
            <tr>
                <th>IdCalificacion</th>
                <th>Descripción (Materia)</th>
                <th>Periodo</th>
                <th>Nota</th>
                <th>Observaciones</th>
                <th>Fecha Calificación</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody id="cuerpoTablaCalificaciones">
        </tbody>
    </table>
</div>

<p id="mensajeCarga" class="text-secondary">Cargando calificaciones...</p>
<p id="mensajeError" class="text-danger" style="display:none;"></p>

<div class="mt-4">
    @Html.ActionLink("Volver", "Index", "Estudiantes", Nothing, New With {.class = "btn btn-secondary"})
</div>
<a href="~/Views/Estudiante/ListarNotas.vbhtml">~/Views/Estudiante/ListarNotas.vbhtml</a>
@section Scripts
    <script src="~/Scripts/Estudiantes/estudianteeditnotas.js"></script>
End Section