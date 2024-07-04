$(function () {
    // ***************** Crear y definir propiedades de tabla '.tabla-indice' *******************
    DataTable.datetime('DD/MM/YYYY');
    $('.tabla-indice').DataTable({
        search: {
            return: false
        },
        language: {
            lengthMenu: 'Mostrar _MENU_ entradas por página',
            emptyTable: 'No hay datos disponibles',
            info: 'Mostrando _START_ a _END_ de _TOTAL_ entradas',
            infoEmpty: 'Mostrando 0 a 0 de 0 entradas',
            infoFiltered: '(filtradas de _MAX_ entradas totales)',
            search: '',
            searchPlaceholder: 'Filtrar entradas por...',
            zeroRecords: 'No se encontraron entradas coincidentes.',
        },
        layout: {
            topStart: 'search',
            topEnd: 'pageLength',
            bottomStart: {
                paging: {
                    type: 'full_numbers'
                }
            },
            bottomEnd: 'info',
        },
        colReorder: true,
    });

    $('.tabla-sin-orden').DataTable({
        search: {
            return: false
        },
        ordering: false,
        language: {
            lengthMenu: 'Mostrar _MENU_ entradas por página',
            emptyTable: 'No hay datos disponibles',
            info: 'Mostrando _START_ a _END_ de _TOTAL_ entradas',
            infoEmpty: 'Mostrando 0 a 0 de 0 entradas',
            infoFiltered: '(filtradas de _MAX_ entradas totales)',
            search: '',
            searchPlaceholder: 'Filtrar entradas por...',
            zeroRecords: 'No se encontraron entradas coincidentes.',
        },
        layout: {
            topStart: 'search',
            topEnd: 'pageLength',
            bottomStart: {
                paging: {
                    type: 'full_numbers'
                }
            },
            bottomEnd: 'info',
        },
        colReorder: false,
    });

    $('.tabla-export').DataTable({
        search: {
            return: false
        },
        ordering: false,
        language: {
            lengthMenu: 'Mostrar _MENU_ entradas por página',
            emptyTable: 'No hay datos disponibles',
            info: 'Mostrando _START_ a _END_ de _TOTAL_ entradas',
            infoEmpty: 'Mostrando 0 a 0 de 0 entradas',
            infoFiltered: '(filtradas de _MAX_ entradas totales)',
            search: '',
            searchPlaceholder: 'Filtrar entradas por...',
            zeroRecords: 'No se encontraron entradas coincidentes.',
        },
        buttons: ['excel', 'spacer', 'pdf'],
        layout: {
            topStart: 'search',
            top: 'buttons',
            topEnd: 'pageLength',
            bottomStart: {
                paging: {
                    type: 'full_numbers'
                }
            },
            bottomEnd: 'info',
        },
        colReorder: false,
    });

    // ***************** Carrusel de fondo *******************
    const imagenes = ['/img/fondos/fondo1-byn.jpg', '/img/fondos/fondo2-byn.jpg', '/img/fondos/fondo3-byn.jpg', '/img/fondos/fondo4-byn.jpg'];
    let numFondo = 1;
    setInterval(cambiarFondo, 5000);

    function cambiarFondo() {
        $("body").css("background-image", "url('" + imagenes[numFondo] + "')");
        numFondo += 1;
        if (numFondo === imagenes.length) { numFondo = 0 };
    }

    // make it as accordion for smaller screens
    if (window.innerWidth < 992) {

        // close all inner dropdowns when parent is closed
        $('.navbar .dropdown').forEach(function (everydropdown) {
            everydropdown.addEventListener('hidden.bs.dropdown', function () {
                // after dropdown is hidden, then find all submenus
                this.querySelectorAll('.submenu').forEach(function (everysubmenu) {
                    // hide every submenu as well
                    everysubmenu.style.display = 'none';
                });
            })
        });

        $('.dropdown-menu a').forEach(function (element) {
            element.addEventListener('click', function (e) {
                let nextEl = this.nextElementSibling;
                if (nextEl?.classList.contains('submenu')) {
                    // prevent opening link if link needs to open dropdown
                    e.preventDefault();
                    if (nextEl.style.display == 'block') {
                        nextEl.style.display = 'none';
                    } else {
                        nextEl.style.display = 'block';
                    }

                }
            });
        })
    }
    // end if innerWidth
});
