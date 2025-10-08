document.addEventListener('DOMContentLoaded', function () {
    const btnCargar = document.getElementById('btnCargar');
    const contenedorTabla = document.getElementById('contenedorEstudiantes');
    const tbody = document.querySelector('#tablaEstudiantes tbody');
    const mensajeNoDatos = document.getElementById('mensajeNoDatos');
    const botonesAccion = document.getElementById('botonesAccion');
    const btnNuevo = document.getElementById('btnNuevo');

    let idSeleccionado = null;

    if (contenedorTabla) contenedorTabla.style.display = 'none';
    if (mensajeNoDatos) mensajeNoDatos.style.display = 'none';
    if (botonesAccion) botonesAccion.style.display = 'none';

    if (btnCargar) {
        btnCargar.addEventListener('click', function () {
            btnCargar.style.display = 'none';
            fetch('/Estudiante/ObtenerTodos')
                .then(response => {
                    if (!response.ok) throw new Error('Error al obtener los estudiantes');
                    return response.json();
                })
                .then(data => {
                    tbody.innerHTML = '';
                    if (data && data.length > 0) {
                        contenedorTabla.style.display = 'block';
                        mensajeNoDatos.style.display = 'none';
                        botonesAccion.style.display = 'block';
                        btnNuevo.style.display = 'inline-block';

                        data.forEach(est => {
                            const fila = document.createElement('tr');
                            fila.innerHTML = `
                                <td>${est.IdEstudiante}</td>
                                <td>${est.Nombres}</td>
                                <td>${est.Apellidos}</td>
                                <td>${est.Edad}</td>
                                <td>${est.CursoAsociado ? est.CursoAsociado.NombreCurso : ''}</td>
                                <td>
                                    <a href="/Estudiante/Edit/${est.IdEstudiante}" class="btn btn-sm btn-warning">
                                        <i class="bi bi-pencil-square"></i> Editar
                                    </a>
                                </td>
                                <td>
                                    <a href="/Estudiante/Eliminar/${est.IdEstudiante}" class="btn btn-sm btn-danger">
                                        <i class="bi bi-trash"></i> Eliminar
                                    </a>
                                </td>
                                
                                <td>
                                    <a href="/Estudiante/AdicionarNota?idEstudiante=${est.IdEstudiante}" 
                                       class="btn btn-sm btn-info">
                                        <i class="bi bi-journal-check"></i> Adicionar Nota
                                    </a>
                                </td>

                               <td>
                                <a href="/Estudiante/ListarNotas?idEstudiante=${est.IdEstudiante}" 
                                   class="btn btn-sm btn-primary">
                                    <i class="bi bi-eye"></i> Ver/Modificar Notas 
                                </a>
                              </td>
                                                            `;

                            fila.addEventListener('click', function () {
                                document.querySelectorAll('#tablaEstudiantes tr').forEach(tr => tr.classList.remove('fila-seleccionada'));
                                fila.classList.add('fila-seleccionada');
                                idSeleccionado = est.IdEstudiante;
                            });

                            tbody.appendChild(fila);
                        });
                    } else {
                        contenedorTabla.style.display = 'none';
                        mensajeNoDatos.style.display = 'block';
                        botonesAccion.style.display = 'block';
                        btnNuevo.style.display = 'inline-block';
                    }
                })
                .catch(error => {
                    console.error('Error al cargar estudiantes:', error);
                    contenedorTabla.style.display = 'none';
                    mensajeNoDatos.style.display = 'block';
                    mensajeNoDatos.textContent = 'Ocurrió un error al cargar los estudiantes.';
                    botonesAccion.style.display = 'block';
                    btnNuevo.style.display = 'inline-block';
                });
        });
    }
});

// ---------------------------------------
// Función global para registrar estudiante (se mantiene sin cambios)
// ---------------------------------------
async function registrarEstudiante() {
    const estudiante = {
        Nombres: document.querySelector('#Nombres').value.trim(),
        Apellidos: document.querySelector('#Apellidos').value.trim(),
        Edad: parseInt(document.querySelector('#Edad').value),
        Sexo: document.querySelector('#Sexo').value,
        FechaNacimiento: document.querySelector('#FechaNacimiento').value,
        Direccion: document.querySelector('#Direccion').value.trim(),
        Telefono: document.querySelector('#Telefono').value.trim(),
        Email: document.querySelector('#Email').value.trim(),
        IdCurso: parseInt(document.querySelector('#IdCurso').value)
    };

    try {
        const response = await fetch('/Estudiante/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify(estudiante)
        });

        if (response.ok) {
            alert('✅ Estudiante creado exitosamente.');
            window.location.href = '/Estudiante/Index';
        } else {
            const errorText = await response.text();
            console.error('Error al crear el estudiante:', errorText);
            alert('⚠️ Error al crear el estudiante.');
        }
    } catch (error) {
        console.error('Excepción:', error);
        alert('⚠️ Ocurrió un error al conectar con el servidor.');
    }
}