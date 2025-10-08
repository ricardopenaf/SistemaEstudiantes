document.addEventListener('DOMContentLoaded', async function () {
    const urlParts = window.location.pathname.split('/');
    const idEstudiante = urlParts[urlParts.length - 1];
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

    if (!idEstudiante || isNaN(idEstudiante)) {
        alert('❌ ID de estudiante inválido.');
        window.location.href = '/Estudiante/Index';
        return;
    }

    // 🔹 Cargar datos del estudiante
    try {
        const response = await fetch(`/Estudiante/ObtenerPorId/${idEstudiante}`);
        if (!response.ok) throw new Error('Error al obtener los datos del estudiante.');

        const estudiante = await response.json();

        if (estudiante) {
            // Asignación de campos básicos
            document.querySelector('#IdEstudiante').value = estudiante.IdEstudiante;
            document.querySelector('#Nombres').value = estudiante.Nombres;
            document.querySelector('#Apellidos').value = estudiante.Apellidos;
            document.querySelector('#Edad').value = estudiante.Edad;
            document.querySelector('#Sexo').value = estudiante.Sexo || "";
            document.querySelector('#Direccion').value = estudiante.Direccion || "";
            document.querySelector('#Telefono').value = estudiante.Telefono || "";
            document.querySelector('#Email').value = estudiante.Email || "";
            document.querySelector('#IdCurso').value = estudiante.IdCurso;
            document.querySelector('#Activo').checked = estudiante.Activo === true;

            console.log('Fecha de Nacimiento original:', estudiante.FechaNacimiento);

            if (estudiante.FechaNacimiento) {
                console.log("Fecha de Nacimiento original:", estudiante.FechaNacimiento);

                // Verifica si viene en formato /Date(1286341200000)/
                const match = estudiante.FechaNacimiento.match(/\/Date\((\d+)\)\//);
                if (match) {
                    const timestamp = parseInt(match[1]); // Extrae los milisegundos
                    const dateObj = new Date(timestamp);  // Crea el objeto Date

                    // Convierte al formato yyyy-MM-dd para el input[type="date"]
                    const formattedDate = dateObj.toISOString().split('T')[0];
                    document.querySelector('#FechaNacimiento').value = formattedDate;
                } else {
                    // Si viene ya como "2025-10-06", se asigna directamente
                    document.querySelector('#FechaNacimiento').value = estudiante.FechaNacimiento;
                }
            }



        } else {
            alert('⚠️ No se encontraron datos del estudiante.');
            window.location.href = '/Estudiante/Index';
        }
    } catch (error) {
        console.error('Error:', error);
        alert('⚠️ Error al cargar los datos del estudiante.');
        window.location.href = '/Estudiante/Index';
    }

    // 🔹 Guardar cambios
    const btnGuardar = document.querySelector('#btnGuardar');
    if (btnGuardar) {
        alert('Guardar Nota');
        btnGuardar.addEventListener('click', async function () {
            const estudiante = {
                IdEstudiante: parseInt(document.querySelector('#IdEstudiante').value),
                Nombres: document.querySelector('#Nombres').value.trim(),
                Apellidos: document.querySelector('#Apellidos').value.trim(),
                Edad: parseInt(document.querySelector('#Edad').value),
                Sexo: document.querySelector('#Sexo').value,
                FechaNacimiento: document.querySelector('#FechaNacimiento').value,
                Direccion: document.querySelector('#Direccion').value.trim(),
                Telefono: document.querySelector('#Telefono').value.trim(),
                Email: document.querySelector('#Email').value.trim(),
                IdCurso: parseInt(document.querySelector('#IdCurso').value),
                Activo: document.querySelector('#Activo').checked
            };

            try {
                const response = await fetch('/Estudiante/GuardarNota', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': token
                    },
                    body: JSON.stringify(estudiante)
                });

                if (response.ok) {
                    alert('✅ Cambios guardados correctamente.');
                    window.location.href = '/Estudiante/Index';
                } else {
                    const errorText = await response.text();
                    console.error('Error al editar:', errorText);
                    alert('⚠️ Error al guardar los cambios.');
                }
            } catch (error) {
                console.error('Error:', error);
                alert('⚠️ Error al conectar con el servidor.');
            }
        });
    } else {
        console.error('Botón de guardar (#btnGuardar) no encontrado.');
    }
});
