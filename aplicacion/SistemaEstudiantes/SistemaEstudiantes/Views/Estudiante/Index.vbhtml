@Code
    ViewBag.Title = "Listado de Estudiantes"
End Code

<h2>Listado de Estudiantes</h2>
<link href="~/Content/Estudiantes/estudiante.css" rel="stylesheet" />


<div id="contenedorEstudiantes" class="tabla-estudiantes" style="display:none;">
    <table id="tablaEstudiantes" class="table table-bordered table-hover">
        <thead class="table-light">
            <tr>
                <th>ID</th>
                <th>Nombres</th>
                <th>Apellidos</th>
                <th>Edad</th>
                <th>Curso</th>
                <th>Editar</th>
                <th>Eliminar</th>
                <th>Adicionar Nota</th>
                <th>Modificar Nota</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
</div>

<p id="mensajeNoDatos" style="display:none; color:red;">No existe información para mostrar.</p>

<button id="btnCargar" class="btn btn-primary">Cargar Estudiantes</button>

<div id="botonesAccion" style="display:none; margin-top:15px;">
    <a href="/Estudiante/Create" id="btnNuevo" class="btn btn-success">Crear</a>
</div>

@section Scripts
    <script src="~/Scripts/Estudiantes/estudiante.js"></script>
End Section