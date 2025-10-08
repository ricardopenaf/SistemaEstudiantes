document.addEventListener('DOMContentLoaded', function () {
    const btnEliminar = document.querySelector('#btnEliminar');
    const idEstudiante = document.querySelector('#IdEstudiante').value;

    if (btnEliminar) {
        btnEliminar.addEventListener('click', async function () {
            const confirmacion = confirm('⚠️ ¿Está seguro de eliminar este estudiante? Esta acción no se puede deshacer.');

            if (!confirmacion) return;

            try {
                const response = await fetch(`/Estudiante/EliminarConfirmado/${idEstudiante}`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' }
                });

                if (response.ok) {
                    alert('✅ Estudiante eliminado correctamente.');
                    window.location.href = '/Estudiante/Index';
                } else {
                    const errorText = await response.text();
                    console.error('Error al eliminar:', errorText);
                    alert('⚠️ Error al eliminar el estudiante.');
                }
            } catch (error) {
                console.error('Error:', error);
                alert('❌ Error al conectar con el servidor.');
            }
        });
    } else {
        console.error('No se encontró el botón #btnEliminar.');
    }
});
