document.addEventListener('DOMContentLoaded', function () {
    const btnGuardar = document.getElementById('btnGuardarNota');
    const form = document.getElementById('formCreateNota'); // Usando el ID del formulario

    if (btnGuardar) {
        btnGuardar.addEventListener('click', function (e) {
            e.preventDefault(); // Detener el envío de formulario estándar

            const calificacion = {
                // Obtenemos los valores de los inputs del formulario
                IdEstudiante: parseInt(document.getElementById('idEstudiante').value),
                IdMateria: parseInt(document.getElementById('idMateria').value),

                Periodo: document.getElementById('periodo').value,

                Nota: parseFloat(document.getElementById('nota').value),

                Observaciones: document.getElementById('observacion').value || ""
            };

            // Deshabilitar el botón mientras se procesa
            btnGuardar.disabled = true;
            btnGuardar.textContent = 'Guardando...';

            fetch('/Estudiante/GuardarNota', { // Revisa que la ruta del controlador sea correcta
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': form.querySelector('input[name="__RequestVerificationToken"]').value // Si usas Anti-Forgery
                },
                body: JSON.stringify(calificacion)
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('El servidor devolvió un error: ' + response.statusText);
                    }
                    return response.json();
                })
                .then(data => {
                    if (data.success) {
                        alert('✅ ' + data.message);
                        window.location.href = '/Estudiante/Index';
                    } else {
                        alert('⚠️ Error al guardar: ' + data.message);
                    }
                })
                .catch(error => {
                    console.error('Error en la operación AJAX:', error);
                    alert('⚠️ Error de conexión o de servidor. Revisa la consola.');
                })
                .finally(() => {
                    btnGuardar.disabled = false;
                    btnGuardar.textContent = 'Guardar Nota';
                });
        });
    }
});