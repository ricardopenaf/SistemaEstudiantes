document.addEventListener('DOMContentLoaded', function () {
    console.log('Script cargado');

    var idEstudianteElement = document.getElementById('idEstudianteVisual');
    var cuerpoTabla = document.getElementById('cuerpoTablaCalificaciones');
    var mensajeError = document.getElementById('mensajeError');
    var mensajeCarga = document.getElementById('mensajeCarga');

    if (!idEstudianteElement) {
        console.error('No se encontró idEstudianteVisual');
        if (cuerpoTabla) {
            cuerpoTabla.innerHTML = '<tr><td colspan="7" class="text-center text-danger">Error: Elemento ID no encontrado.</td></tr>';
        }
        return;
    }

    var idEstudiante = parseInt(idEstudianteElement.textContent.trim());
    console.log('ID Estudiante:', idEstudiante);

    if (isNaN(idEstudiante) || idEstudiante <= 0) {
        console.error('ID inválido');
        if (mensajeError) {
            mensajeError.textContent = 'ID de estudiante inválido.';
            mensajeError.style.display = 'block';
        }
        return;
    }

    var url = '/Estudiante/ObtenerNotasJson?idEstudiante=' + idEstudiante;
    console.log('Fetch URL:', url);

    fetch(url)
        .then(function (response) {
            console.log('Response status:', response.status);
            return response.text().then(function (text) {
                console.log('Response text:', text);
                if (!response.ok) {
                    throw new Error('HTTP ' + response.status + ': ' + text);
                }
                return JSON.parse(text);
            });
        })
        .then(function (result) {
            console.log('Resultado:', result);

            if (mensajeCarga) mensajeCarga.style.display = 'none';
            cuerpoTabla.innerHTML = '';

            if (!result.success) {
                throw new Error(result.message || 'Error del servidor');
            }

            if (!result.data || result.data.length === 0) {
                cuerpoTabla.innerHTML = '<tr><td colspan="7" class="text-center">No hay calificaciones.</td></tr>';
                return;
            }

            result.data.forEach(function (nota) {
                var fila = document.createElement('tr');
                fila.innerHTML = '<td>' + nota.IdCalificacion + '</td>' +
                    '<td>' + (nota.DescripcionMateria || 'N/A') + '</td>' +
                    '<td>' + (nota.Periodo || 'N/A') + '</td>' +
                    '<td>' + (nota.Nota || 0).toFixed(2) + '</td>' +
                    '<td>' + (nota.Observaciones || '') + '</td>' +
                    '<td>' + (nota.FechaCalificacion || '') + '</td>' +
                    '<td>' +
                    '<a href="/Estudiante/ModificarNota?idNota=' + nota.IdCalificacion + '" class="btn btn-sm btn-warning">Editar</a> ' +
                    '<a href="#" data-id-nota="' + nota.IdCalificacion + '" class="btn btn-sm btn-danger">Eliminar</a>' +
                    '</td>';
                cuerpoTabla.appendChild(fila);
            });

            console.log('Tabla cargada');
        })
        .catch(function (error) {
            console.error('Error:', error);
            if (mensajeCarga) mensajeCarga.style.display = 'none';
            if (mensajeError) {
                mensajeError.textContent = 'Error: ' + error.message;
                mensajeError.style.display = 'block';
            }
            cuerpoTabla.innerHTML = '<tr><td colspan="7" class="text-center text-danger">Error al cargar datos.</td></tr>';
        });
});