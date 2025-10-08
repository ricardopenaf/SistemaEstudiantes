@ModelType Estudiante

@Code
    ViewData("Title") = "Crear Estudiante"
End Code

<h2>@ViewData("Title")</h2>
<link href="~/Content/Estudiantes/estudiante.css" rel="stylesheet" />

<form id="formCreateEstudiante">
    @Html.AntiForgeryToken()
    <div class="form-horizontal">
        <hr />

        <div class="form-group">
            @Html.LabelFor(Function(m) m.Nombres, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextBoxFor(Function(m) m.Nombres, New With {.class = "form-control", .id = "Nombres"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(m) m.Apellidos, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextBoxFor(Function(m) m.Apellidos, New With {.class = "form-control", .id = "Apellidos"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(m) m.Edad, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextBoxFor(Function(m) m.Edad, New With {.class = "form-control", .id = "Edad", .type = "number"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(m) m.Sexo, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.DropDownListFor(Function(m) m.Sexo,
                    New SelectList(New List(Of Object) From {
                        New With {.Value = "M", .Text = "Masculino"},
                        New With {.Value = "F", .Text = "Femenino"}
                    }, "Value", "Text"),
                    "Seleccione...",
                    New With {.class = "form-control", .id = "Sexo"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(m) m.FechaNacimiento, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextBoxFor(Function(m) m.FechaNacimiento, "{0:yyyy-MM-dd}", New With {.class = "form-control", .type = "date", .id = "FechaNacimiento"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(m) m.Direccion, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextBoxFor(Function(m) m.Direccion, New With {.class = "form-control", .id = "Direccion"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(m) m.Telefono, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextBoxFor(Function(m) m.Telefono, New With {.class = "form-control", .id = "Telefono"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(m) m.Email, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control", .id = "Email", .type = "email"})
            </div>
        </div>

        <div class="form-group">
            @Html.Label("Curso", htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.DropDownListFor(Function(m) m.IdCurso, CType(ViewBag.Cursos, SelectList), "Seleccione un curso", New With {.class = "form-control", .id = "IdCurso"})
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <button type="button" id="btnGuardar" class="btn btn-primary" onclick="registrarEstudiante()">Guardar</button>
                @Html.ActionLink("Cancelar", "Index", "Estudiante", Nothing, New With {.class = "btn btn-secondary"})
            </div>
        </div>
    </div>
</form>

<script src="~/Scripts/Estudiantes/estudiante.js"></script>
