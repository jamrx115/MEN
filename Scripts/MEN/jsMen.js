
function nobackbutton() {
    window.location.hash = "no-back-button";
    window.location.hash = "Again-No-back-button" //chrome
    window.onhashchange = function () { window.location.hash = "no-back-button"; }
}

function mensajePopUp(texto) {
    var modal = document.getElementById("textoModal");
    modal.innerHTML = texto;
    //modal.style.backgroundColor
    var boton = document.getElementById("btnModal");
    boton.click();
}

function LLamaSolicitud(referencia){
    var boton = document.getElementById(referencia);
    boton.click();
}

function acordeonSolicitud(spanIglesia) {
    var span = document.getElementById(spanIglesia);

    if (span.className == "wizard_genera_title glyphicon glyphicon-chevron-down") {
        span.className = "wizard_genera_title glyphicon glyphicon-chevron-up";
    } else {
        span.className = "wizard_genera_title glyphicon glyphicon-chevron-down";
    }
}

function llamaFIleUpload(archivo) {
    var file = document.getElementById(archivo);
    file.click();
}

function mensajeEliminaArchivo(txtElimina) {
    var txt = document.getElementById(txtElimina);    
    txt.value = "";
}

function cambiaFileUpload(file, texto, tipo, label) {
    var file1 = document.getElementById(file).files[0];
    var txt = document.getElementById(texto);
    var ref = document.getElementById(tipo);
    var path = URL.createObjectURL(file1);
    var lbl = document.getElementById(label);
    
    if (file1.size > 15000000) {
        mensajePopUp("no se pueden subir archivos mayores a 15 MB");
        file1.Value = "";
        txt.value = "";
        ref.href = null;
    } else if (file1.type != "application/pdf") {
        mensajePopUp("solo se admiten archivos PDF"); 
        file1.Value = "";
        txt.value = "";
        ref.href = null;
    } else {        
        txt.value = file1.name;
        ref.href = path;
        lbl.innerText = "Tamaño del archivo: " + file1.size/1000 + " KB";
    }

    //var fileGeneral = document.getElementById("fileGeneral");
    //fileGeneral = file; 

    //var btnAceptaIV = document.getElementById("btnAceptaIV");
    //btnAceptaIV.click();

    //if (tipo == "1") {
    //    var fileGeneral = document.getElementById("fileGeneral");
    //    fileGeneral.click();
    //}
    
}

function confirmaRegistrarSolicitud() {
    var boton = document.getElementById("btnModal_2");
    boton.click();
}

function mensajeEliminaSolicitud(sol,fec,tipo,estado) {
    var td_1 = document.getElementById("td_1");
    var td_2 = document.getElementById("td_2");
    var td_3 = document.getElementById("td_3");
    var td_4 = document.getElementById("td_4");
    td_1.innerHTML = sol;
    td_2.innerHTML = fec;
    td_3.innerHTML = tipo;
    td_4.innerHTML = estado;
    var boton = document.getElementById("btnModal_1");
    boton.click();
}

function saleFaseIII(solicitudId) {
    //var botonSale = document.getElementById("btnSalida"); 
    //botonSale.click();
    var param = { "solicidutId": solicitudId };
    $.ajax({
        url: '/Inicio/guardaSolicitud',
        type: 'POST',
        data: param,
        success: function (responce) {
            if (responce == "OK") {
                location.reload(true);
            } else {
                mensajePopUp(responce)
            }

        },
        error: function (responce) { mensajePopUp(responce.data) }
    });
}

function eliminaSolicitud() {
    var td_1 = document.getElementById("td_1").innerHTML;
    var param = { "solicidutId": td_1};
    $.ajax({
        url: '/Inicio/eliminaSolicitud',
        type: 'POST',
        data: param,
        success: function (responce) {
            if (responce == "OK") {
                location.reload(true);
            } else {
                mensajePopUp(responce)
            }
            
        },
        error: function (responce) { mensajePopUp(responce.data) }
    });
}

function verOcultar() {
    //var spOjo = document.getElementById("spOjo");
    //var txtPsw = document.getElementById("txtPsw");
    //if (spOjo.className == "glyphicon glyphicon-eye-open") {
    //    spOjo.className = "glyphicon glyphicon-eye-close";
    //    txtPsw.type = "text";
    //} else {
    //    spOjo.className = "glyphicon glyphicon-eye-open";
    //    txtPsw.type = "password";
    //}

    var txtPsw = document.getElementById("txtPsw");
    if (txtPsw.type == "password") {
        txtPsw.type = "text";
    } else {
        txtPsw.type = "password";
    }
    
}

function tidSeleccionado(opcion) {
    //var tid_selected = document.getElementById("tid_selected");
    var txtTipoId = document.getElementById("txtTipoId");

    var text_selected = opcion.innerText.substring(0, 2);
    //tid_selected.innerText = text_selected;
    txtTipoId.value = text_selected;
}

function ingreso() {
    var valida = validaIngreso();
    if (valida == "") {
        document.getElementById("btnFaseI").click();
    }else {
        alert(valida);
    }
}

function validaIngreso() {
    var tid_selected = document.getElementById("tid_selected").innerText;
    var txtUser = document.getElementById("txtUser");
    var txtPsw = document.getElementById("txtPsw");
    var resp = "";
    if (tid_selected == "T. Id") {
        resp += "Seleccione Tipo Id. \n";
    }
    if (txtUser.value == null || txtUser.value == "") {
        resp += "Ingrese Número de Documento.\n";
    }
    if (txtPsw.value == null || txtPsw.value == "") {
        resp += "Ingrese contraseña. \n";
    }
    return resp;
}

function validaFaseII(pregrado) {
    var btnAceptaII = document.getElementById("btnAceptaII");
    if (pregrado == "0") {        
        btnAceptaII.value = "atrasF1";
        btnAceptaII.click();
    }
    else
    {
        if (pregrado == "1") {
            btnAceptaII.value = "atrasBandeja";
            btnAceptaII.click();
        } else {
            btnAceptaII.value = "adelanteF3";
            if (pregrado.toUpperCase() == "TRUE") {
                btnAceptaII.click();
            } else {
                var rbtnNacional = document.getElementsByName("nacional");
                if (rbtnNacional[0].checked) {
                    btnAceptaII.click();
                } else {
                    var convalidado = document.getElementsByName("preGradoValidado");
                    if (convalidado[0].checked) {
                        btnAceptaII.click();
                    } else {
                        mensajePopUp("Si el Pregrado no es nacional y no está convalidado, no puede continuar el proceso");
                    }
                }
            }    
        }
        
    }
}

function atrasFaseII(convalidacion) {

}

function cambiaNacional() {
    var rbtnNacional = document.getElementById("nacional");
    var preGradoValidado = document.getElementById("divConvalidado_1");
    var divConvalidado = document.getElementById("divConvalidado");
    if (!rbtnNacional.checked) {
        divConvalidado.style.display = "inline";
        preGradoValidado.style.display = "inline";
    } else {
        divConvalidado.style.display = "none";
        preGradoValidado.style.display = "none";
    }
}

function validaFaseIII(opcion) {
    var btnAceptaIII = document.getElementById("btnAceptaIII");
    btnAceptaIII.value = opcion ;
    //var instrucciones = [];
    var iguales = true;

    if (opcion == "siguiente") {
        var txtEmailNotificacion = document.getElementById("txtEmailNotificacion");
        var txtConfirmaEmail = document.getElementById("txtConfirmaEmail");
        var txtEmailNotificacion2 = document.getElementById("txtEmailNotificacion2");
        var txtConfirmaEmail2 = document.getElementById("txtConfirmaEmail2");

        if (txtEmailNotificacion != null) {
            if (txtEmailNotificacion.value.trim().toUpperCase() != txtConfirmaEmail.value.trim().toUpperCase()) {
                iguales = false;
                mensajePopUp("El correo electrónico para notificación No coincide");
                txtConfirmaEmail.focus();
            }

            if (txtEmailNotificacion2.value.trim().toUpperCase() != txtConfirmaEmail2.value.trim().toUpperCase()) {
                iguales = false;
                mensajePopUp("El correo electrónico para notificación No coincide");
                txtConfirmaEmail2.focus();
            }
        }   
    }
     

    if (iguales) {
        var paises = [];
        var instituciones = [];
        var institucionesText = [];
        var estados = [];
        var ciudades = [];
        var ciudadesText = [];
        var facultades = [];
        var indice;

        var tblInstituciones = document.getElementById("tblInstituciones");
        var trMultiInstitucion = document.getElementById("trMultiInstitucion");
        if (trMultiInstitucion == null) {
            indice = 1;
        } else {
            indice = 2;
        }

        if (tblInstituciones.rows.length > indice) {
            for (i = indice; i < tblInstituciones.rows.length; i++) {
                var cell = tblInstituciones.rows[i].cells;
                var cmbPaises = cell[0].childNodes[1];
                if (cmbPaises == null) {
                    cmbPaises = cell[0].childNodes[0];
                    ciudades[i - indice] = cell[1].childNodes[1].value;
                    ciudadesText[i - indice] = cell[1].childNodes[2].value;
                    instituciones[i - indice] = cell[2].childNodes[1].value;
                    institucionesText[i - indice] = cell[2].childNodes[2].value;
                    estados[i - indice] = cell[3].childNodes[0].value;
                    facultades[i - indice] = cell[4].childNodes[0].value;
                    // instrucciones[i - indice] = cell[5].childNodes[0].value;
                } else {
                    ciudades[i - indice] = cell[1].childNodes[3].value;
                    ciudadesText[i - indice] = cell[1].childNodes[5].value;
                    instituciones[i - indice] = cell[2].childNodes[3].value;
                    institucionesText[i - indice] = cell[2].childNodes[5].value;
                    estados[i - indice] = cell[3].childNodes[1].value;
                    facultades[i - indice] = cell[4].childNodes[1].value;
                    // instrucciones[i - indice] = cell[5].childNodes[1].value;
                }
                paises[i - indice] = cmbPaises.options[cmbPaises.selectedIndex].value;
            }

            var param = { "paises": paises, "intitutos": instituciones, "institucionesText": institucionesText, "estados": estados, "ciudades": ciudades, "facultades": facultades, "ciudadesText": ciudadesText };
            $.ajax({
                url: '/Inicio/llenaListaInstituciones',
                type: 'POST',
                data: param,
                success: function (responce) {
                    var btnAceptaIII = document.getElementById("btnAceptaIII");
                    btnAceptaIII.click();
                },
                error: function (responce) { mensajePopUp(responce.data) }
            });

        } else {
            mensajePopUp("No hay información de instituciones");
        }
    }
    
}

function validaResumen(opcion){
    var btnResumen = document.getElementById("btnResumen");
    btnResumen.value = opcion;
    btnResumen.click();
}

function llamaPago(opcion) {
    var solId = document.getElementById("hidSolid").value;
    var param1 = { "solId": solId };
    $.ajax({
        url: '/Inicio/llamaPaginaPago',
        type: 'POST',
        data: param1,
        success: function (responce) {
            var redirigePago = document.getElementById("redirigePago")
            redirigePago.href = responce;
            redirigePago.click();
            
            var btnResumen = document.getElementById("btnResumen"); 
            btnResumen.value = opcion;
            btnResumen.click();
        },
        error: function (responce) { mensajePopUp(responce.data); }
    });
}

function SeleccionPrograma(combo, txt) {
    var cmbNombrePrograma = document.getElementById(combo);
    var txtNombrePrograma = document.getElementById(txt);
    var nombreProgramaSel = cmbNombrePrograma.options[cmbNombrePrograma.selectedIndex].value;
    if (nombreProgramaSel == "OTRO") {
        txtNombrePrograma.value = "";
        txtNombrePrograma.readOnly = false;
    } else {
        txtNombrePrograma.value = nombreProgramaSel;
        txtNombrePrograma.readOnly = true;
    }
}

function SeleccionInsitutoTexto(combo, txt, txtName, cmbInst, txtInst) {
    var cmbNombrePrograma = document.getElementById(combo);
    var txtNombrePrograma = document.getElementById(txt);
    var txtNombreProgramaTexto = document.getElementById(txtName);
    var nombreProgramaSel = cmbNombrePrograma.options[cmbNombrePrograma.selectedIndex].value;
    var nombreProgramaTxtoSel = cmbNombrePrograma.options[cmbNombrePrograma.selectedIndex].innerHTML;
    if (nombreProgramaSel == "0") {
        txtNombrePrograma.value = "0";
        txtNombreProgramaTexto.value = "";
        txtNombreProgramaTexto.style.display = "inline";
    } else {
        txtNombrePrograma.value = nombreProgramaSel;
        txtNombreProgramaTexto.value = nombreProgramaTxtoSel;
        txtNombreProgramaTexto.style.display = "none";
    }

    var cmbNombreInstituto = document.getElementById(cmbInst);
    var txtNombreInstituto = document.getElementById(txtInst);
    cmbNombreInstituto.options.length = 0;
    txtNombreInstituto.value = "";
    txtNombreInstituto.style.display = "inline";
    
    var param = { "ciudad": nombreProgramaSel };
    $.ajax({
        url: '/Inicio/ActualizacomboInstitutoXciudad',
        type: 'POST',
        data: param,
        success: function (responce) {
            responce.forEach(function (element) {
                var option = document.createElement('option');
                option.text = element.Text;
                option.value = element.Value;
                cmbNombreInstituto.add(option);
            });
        },
        error: function (responce) { mensajePopUp(responce.data); }
    });
}

function SeleccionProgramaTexto(combo, txt, txtName, div) {
    var divContainer = document.getElementById(div);
    var cmbNombrePrograma = document.getElementById(combo);
    var txtNombrePrograma = document.getElementById(txt);
    var txtNombreProgramaTexto = document.getElementById(txtName);
    var nombreProgramaSel = cmbNombrePrograma.options[cmbNombrePrograma.selectedIndex].value;
    var nombreProgramaTxtoSel = cmbNombrePrograma.options[cmbNombrePrograma.selectedIndex].innerHTML;    

    if (nombreProgramaSel == "0") {
        txtNombrePrograma.value = "0";
        txtNombreProgramaTexto.value = "";
        divContainer.style.display = "inline";
    } else {
        txtNombrePrograma.value = nombreProgramaSel;
        txtNombreProgramaTexto.value = nombreProgramaTxtoSel;
        divContainer.style.display = "none";
    }

    if (txtName == "txtNombreProgramaText") {
        if (nombreProgramaSel == "0") {
            txtNombreProgramaTexto.value = "N/A";
        } else {
            var cmbNombreProgramaTitulo = document.getElementById("cmbNombreProgramaTitulo");
            var nombreProgramaituloSel = cmbNombreProgramaTitulo.options[cmbNombrePrograma.selectedIndex].innerHTML;
            txtNombreProgramaTexto.value = nombreProgramaituloSel;
        }
    }
    //else {//Relacion Instituto - Programa
    //    //var cmbNombrePrograma_1 = document.getElementById("cmbNombrePrograma");
    //    //cmbNombrePrograma_1.options.length = 1;
    //    //var param = { "instituto": nombreProgramaSel };
    //    //$.ajax({
    //    //    url: '/Inicio/programaXinstitutoExtr',
    //    //    type: 'POST',
    //    //    data: param,
    //    //    success: function (responce) {
    //    //        responce.forEach(function (element) {
    //    //            var option = document.createElement('option');
    //    //            option.text = element.Text;
    //    //            option.value = element.Value;
    //    //            cmbNombrePrograma_1.add(option);
    //    //        });
    //    //    },
    //    //    error: function (responce) { mensajePopUp(responce.data); }
    //    //});
    //}
}

function SeleccionTipoEducacion() {
    var cmbTipoEducacionS = document.getElementById("cmbTipoEducacionS");
    var divTipoMaestria = document.getElementById("divTipoMaestria");
    var nombreTipoEduSel = cmbTipoEducacionS.options[cmbTipoEducacionS.selectedIndex].value;

    if (nombreTipoEduSel == "2") {
        divTipoMaestria.style.display = "inline";
    } else {
        divTipoMaestria.style.display = "none";
    }

    SeleccionAreaConocimiento();
}

function SeleccionAreaConocimiento() {
    var tipoSolicitud = document.getElementById("hidenTipoSol").value;
    var cmbAreaConocimiento = document.getElementById("cmbAreaConocimiento");
    var nombreTipoEduSel = cmbAreaConocimiento.options[cmbAreaConocimiento.selectedIndex].value;
    var divVerificacionPrograma = document.getElementById("divVerificacionPrograma");

    if (nombreTipoEduSel == "91") {
        divVerificacionPrograma.style.display = "none";
    } else {
        if (tipoSolicitud.toUpperCase() == "PREGRADO") {
            if (nombreTipoEduSel == "42" || nombreTipoEduSel == "11" || nombreTipoEduSel == "41" ) {
                divVerificacionPrograma.style.display = "none";
            } else {
                divVerificacionPrograma.style.display = "inline";
            }
        }
        else{
            divVerificacionPrograma.style.display = "inline";
        }
    }

    if (tipoSolicitud.toUpperCase() == "POSGRADO") {        
        var cmbTipoEducacionS = document.getElementById("cmbTipoEducacionS");
        var cmbTipoMaestria = document.getElementById("cmbTipoMaestria");
        //var rbtnEspecialidadMedicaSalud = document.getElementById("rbtnEspecialidadMedicaSalud");
        var divSegundaEspecialidad = document.getElementById("divSegundaEspecialidad");
        var divEspecialidadMedica = document.getElementById("divEspecialidadMedica");        
        var nombreTipoEducacion = cmbTipoEducacionS.options[cmbTipoEducacionS.selectedIndex].value;
        var tipoMaestria = cmbTipoMaestria.options[cmbTipoMaestria.selectedIndex].value;

        if (nombreTipoEduSel == "91") {
            divSegundaEspecialidad.style.display = "inline";
            if (nombreTipoEducacion == "1" || (nombreTipoEducacion == "2" && tipoMaestria == "1")) {
                divEspecialidadMedica.style.display = "inline";
                //rbtnEspecialidadMedicaSalud.checked = true;
            } else {
                divEspecialidadMedica.style.display = "none";
                //rbtnEspecialidadMedicaSalud.checked = false;
            }
        } else {
            divSegundaEspecialidad.style.display = "none";
            divEspecialidadMedica.style.display = "none";
            //rbtnEspecialidadMedicaSalud.checked = false;
        }
    }    
}

function segundaDisplay() {
    var nacional = document.getElementsByName("segundaEspecialidadSalud");
    var divSegundaEspecialidadNacional = document.getElementById("divSegundaEspecialidadNacional");
    if (nacional[0].checked) {
        divSegundaEspecialidadNacional.style.display = "inline";
    } else {
        divSegundaEspecialidadNacional.style.display = "none";
    }
}

function segundaNacional() {
    var nacional = document.getElementsByName("segundaEspecialidadSaludNal");
    var divSegundaEspecialidadNacionalSI = document.getElementById("divSegundaEspecialidadNacionalSI");
    var divSegundaEspecialidadNacionalNO = document.getElementById("divSegundaEspecialidadNacionalNO");
    if (nacional[0].checked) {
        divSegundaEspecialidadNacionalSI.style.display = "inline";
        divSegundaEspecialidadNacionalNO.style.display = "none";
    } else {
        divSegundaEspecialidadNacionalSI.style.display = "none";
        divSegundaEspecialidadNacionalNO.style.display = "inline";
    }
}

function notificaTerceroDisplay() {
    //var notificaElectrinica = document.getElementsByName("notificaElectrinica");
    var notificaElectrinica = document.getElementById("notificaElectrinica");
    var divNotificaTercero = document.getElementById("divNotificaTercero");
    var divNotificaTercero_1 = document.getElementById("divNotificaTercero_1");
    //var notificaElectrinica_1 = document.getElementById("notificaElectrinica_1");
    if (!notificaElectrinica.checked) {
        divNotificaTercero.style.display = "inline";
        divNotificaTercero_1.style.display = "inline";
        //notificaElectrinica_1.checked = false;
    } else {
        divNotificaTercero.style.display = "none";
        divNotificaTercero_1.style.display = "none";
        //notificaElectrinica_1.checked = true;
    }
}

var contador = 0;

function agregarIntitucion() {
    var sel = document.getElementById("cmbInstituto");
    var listaSelect = "";

    var paisInst = document.getElementById("cmbPaisInstituto");
    var listaSelectPaisInst = "";

    var listaSelectCiudadExtr = '<option value="0">OTRA</option>';

    for (var i = 0; i < sel.length; i++) {
        var opt = sel[i];
        listaSelect += '<option value="' + opt.value + '">' + opt.innerHTML + '</option>';
    }

    for (var i1 = 0; i1 < paisInst.length; i1++) {
        var opt1 = paisInst[i1];
        
        if (opt1.innerHTML.toUpperCase() == "COLOMBIA") {
            listaSelectPaisInst += '<option selected value="' + opt1.value + '">' + opt1.innerHTML + '</option>';
        } else{
            listaSelectPaisInst += '<option value="' + opt1.value + '">' + opt1.innerHTML + '</option>';
        }
    }

    var row = '<td><select class="text-uppercase" onchange="multiplePaisInsti(this,\'txtInputInstructivo1_' + contador +
        '\',\'txtInputRutaInstructivo1_' + contador + '\',\'txtInputRutaInstructivo1_0_' + contador +
        '\',\'cmbCiudadExtrangera1_' + contador + '\',\'txtCiudadExtrangeraText1_' + contador + '\')">' + listaSelectPaisInst + '</select></td>' +

        '<td><select id="cmbCiudadExtrangera1_' + contador + '" class="text-uppercase" ' +
        'onchange="SeleccionInsitutoTexto(\'cmbCiudadExtrangera1_' + contador + '\',\'txtCiudadExtrangera1_' + contador +
        '\',\'txtCiudadExtrangeraText1_' + contador + '\',\'cmbInstituciones_' + contador + '\',\'txtInstitucionesText_' + contador + '\')">' + listaSelectCiudadExtr + '</select>' +
        '<input type="text" id="txtCiudadExtrangera1_' + contador + '" class="text-uppercase" style="display:none" />' +
        '<input type="text" id="txtCiudadExtrangeraText1_' + contador + '" class="text-uppercase" /></td>' +

        '<td><select id="cmbInstituciones_' + contador + '" class="text-uppercase" ' +
        'onchange="SeleccionProgramaTexto(\'cmbInstituciones_' + contador + '\',\'txtInstituciones_' + contador + '\',\'txtInstitucionesText_' + contador + '\',\'txtInstitucionesText_' + contador + '\')">' + listaSelect + '</select>' +
        '<input type="text" id="txtInstituciones_' + contador + '" class="text-uppercase" style="display:none" />' +  
        '<input type="text" id="txtInstitucionesText_' + contador + '" class="text-uppercase" /></td>' +
        '<td><input type="text" class="text-uppercase" maxlength="50"/></td>' +        
        '<td><input type="text" class="text-uppercase" maxlength="15"/></td>' +
        '<td><a class="button general_button button_pink button_small" data-toggle="tooltip" title="Elimina Institucion" ' +
        'onclick="retiraIntitucion(this.parentElement.parentElement)" > ' +
        '<img src="../assets/images/icons/men-borrar.svg" alt=""></a></td>';

    document.getElementById("tblInstituciones").insertRow(-1).innerHTML = row;

    var row1 = '<td><input id="txtInputInstructivo1_' + contador + '" type="text" class="text-uppercase" readOnly = true /></td>' +        
        '<td><a id="txtInputRutaInstructivo1_' + contador + '" target="_blank">Instructivo PDF1</a>' +
        '<a id="txtInputRutaInstructivo1_0_' + contador++ + '" target="_blank"></a></td>';

    document.getElementById("tblInstructivos").insertRow(-1).innerHTML = row1;
}

function multiplePaisInsti(seleccion, cajaTextoId, instructivoId, instructivo_0_Id, cmbCiudadExtrangera_, txtCiudadExtrangera_) {
    var pais = seleccion.options[seleccion.selectedIndex].value;
    var txtInputRutaInstructivo_ = document.getElementById(instructivoId);
    var txtInputRutaInstructivo_0_ = document.getElementById(instructivo_0_Id);
    var txtInputInstructivo_ = document.getElementById(cajaTextoId);
    var cmbCiudadExtrangera = document.getElementById(cmbCiudadExtrangera_);
    var txtCiudadExtrangera = document.getElementById(txtCiudadExtrangera_);
    txtInputInstructivo_.className = "screen_box_active_tr";
    txtInputInstructivo_.value = pais;

    var table1 = document.getElementById("tblInstructivos");
    var rowCount = table1.rows.length;

    for (i = 0; i < rowCount; i++) {
        var cell = table1.rows[i].cells;
        var cmbPais = cell[0].childNodes[1];
    }

    cmbCiudadExtrangera.options.length = 0;

    var param = { "pais": pais };
    $.ajax({
        url: '/Inicio/buscaInstructivo',
        type: 'POST',
        data: param,
        success: function (responce) {
            if (responce != null) {
                
                if (responce.startsWith('Señor')) {
                    txtInputRutaInstructivo_0_.style.display = "inline";
                    txtInputRutaInstructivo_.style.display = "none";
                    txtInputRutaInstructivo_0_.innerText = responce;
                } else {
                    txtInputRutaInstructivo_0_.style.display = "none";
                    txtInputRutaInstructivo_.style.display = "inline"
                    txtInputRutaInstructivo_.href = responce;
                    txtInputRutaInstructivo_.innerText = responce;
                    txtInputRutaInstructivo_.target = "_blank";
                }   
            }
        },
        error: function (responce) { mensajePopUp(responce.data); }
    });

    if (pais != "") {
        $.ajax({
            url: '/Inicio/ActualizacomboCiudadExtrangera',
            type: 'POST',
            data: param,
            success: function (responce) {
                responce.forEach(function (element) {
                    var option = document.createElement('option');
                    option.text = element.Text;
                    option.value = element.Value;
                    cmbCiudadExtrangera.add(option);
                });
                txtCiudadExtrangera.style.display = "inline";
                txtCiudadExtrangera.value = "";
            },
            error: function (responce) { mensajePopUp(responce.data); }
        });
    }
}

function retiraIntitucion(fila) {
    var table = document.getElementById("tblInstituciones");
    var table1 = document.getElementById("tblInstructivos");

    var rowCount = table.rows.length;

    if (rowCount <= 2) {
        mensajePopUp('No se pueden eliminar todas las instituciones');
    }
    else {
        var filaNro = fila.rowIndex;
        table.deleteRow(filaNro);
        table1.deleteRow(filaNro);
    }
}

function SeleccionInstitucion(lista, fila) {
    var nombreTipoEduSel = lista.options[lista.selectedIndex].value;
    var txtInput = fila.childNodes[1].childNodes[1];

    if (nombreTipoEduSel == "OTRO") {
        txtInput.style.display = "inline";
    } else {
        txtInput.style.display = "none";
    }
}

function SeleccionInstitucion0() {
    var cmbInstituto = document.getElementById("cmbInstituto");
    var nombreIntiSel = cmbInstituto.options[cmbInstituto.selectedIndex].value;

    var txtInput = document.getElementById("txtInput");

    if (nombreIntiSel == "OTRO") {
        txtInput.style.display = "inline";
    } else {
        txtInput.style.display = "none";
    }
}

function cambiaDepartamento() {
    var cmbPais = document.getElementById("cmbPais");
    var cmbDpto = document.getElementById("cmbDpto"); 

    var pais = cmbPais.options[cmbPais.selectedIndex].innerHTML;

    var lblDpto = document.getElementById("divCiudad");  
    var lblCiudad = document.getElementById("divCiudadII"); 
    var divCiudad_0 = document.getElementById("divCiudad_0");
    var divDpto = document.getElementById("divDpto");
     
    var txtCiudad = document.getElementById("txtCiudad"); 

    if (pais.toUpperCase() == "COLOMBIA") {
        cmbDpto.options.length = 0;
        lblDpto.style.display = "inline";
        lblCiudad.style.display = "none";
        cmbDpto.style.display = "inline";
        divCiudad_0.style.display = "inline";
        divDpto.style.display = "inline";
        var param = { "pais": pais };
        $.ajax({
            url: '/Inicio/ActualizacomboDepartamento',
            type: 'POST',
            data: param,
            success: function (responce) {
                responce.forEach(function (element) {
                    var option = document.createElement('option');
                    option.text = element.Text;
                    option.value = element.Value;
                    cmbDpto.add(option);
                });
            },
            error: function (responce) { mensajePopUp(responce.data); }
        });
    } else {
        lblDpto.style.display = "none";        
        divCiudad_0.style.display = "none";
        divDpto.style.display = "none";
        lblCiudad.style.display = "inline";

        txtCiudad.focus();
    }    
}

function cambiaCiudad(departamento, ciudad) {
    var cmbDpto = document.getElementById(departamento);
    var dpto = cmbDpto.options[cmbDpto.selectedIndex].innerHTML;

    var cmbCiudad = document.getElementById(ciudad);
    cmbCiudad.options.length = 0;

    if (departamento == "cmbDptoInstitutoPreSeg") {
        document.getElementById("cmbNombreIntitutoSegunda").options.length = 0;
        document.getElementById("cmbNombreProgramaOtorgaPreSeg").options.length = 0;
        document.getElementById("txtNombreIntitutoSegunda").value = "";
    }

    if (departamento == "cmbDptoInstitutoPre") {
        document.getElementById("cmbNombreIntitutoPregrado").options.length = 0;
        document.getElementById("cmbNombreProgramaOtorgaPre").options.length = 0;
        document.getElementById("txtNombreIntitutoPregrado").value = "";
    }

    var param = { "dpto": dpto };
    $.ajax({
        url: '/Inicio/ActualizacomboCiudad',
        type: 'POST',
        data: param,
        success: function (responce) {
            responce.forEach(function (element) {
                var option = document.createElement('option');
                option.text = element.Text;
                option.value = element.Value;
                cmbCiudad.add(option);
            });
            //if (ciudad == "cmbCiudadInstitutoPre"){
            //    institucionXciudad();
            //}
        },
        error: function (responce) { mensajePopUp(responce.data); }
    });
}

function institucionXciudad(cmbCIudadInst, cmbNombreInst) {
    var cmbCiudadInstitutoPre = document.getElementById(cmbCIudadInst);
    var ciudad = cmbCiudadInstitutoPre.options[cmbCiudadInstitutoPre.selectedIndex].value;

    var cmbCiudad = document.getElementById(cmbNombreInst);
    cmbCiudad.options.length = 0;

    if (cmbCIudadInst == "cmbCiudadInstitutoPre") {
        document.getElementById("txtNombreIntitutoPregrado").value = "";
        document.getElementById("cmbNombreProgramaOtorgaPre").options.length = 0;
    }

    if (cmbCIudadInst == "cmbCiudadInstitutoPreSeg") {
        document.getElementById("txtNombreIntitutoSegunda").value = "";
        document.getElementById("cmbNombreProgramaOtorgaPreSeg").options.length = 0;
    }

    var param = { "ciudad": ciudad };
    $.ajax({
        url: '/Inicio/institucionXciudad',
        type: 'POST',
        data: param,
        success: function (responce) {
            responce.forEach(function (element) {
                var option = document.createElement('option');
                option.text = element.Text;
                option.value = element.Value;
                cmbCiudad.add(option);
            });
        },
        error: function (responce) { mensajePopUp(responce.data); }
    });
}

function programaXinstitucion(cmbNombreInst, txtNombreInst, txtNombreInstText, cmbNombreProg, cmbNombreProgramaTitulo) {
    var cmbNombreIntitutoPregrado = document.getElementById(cmbNombreInst);
    var institucion = cmbNombreIntitutoPregrado.options[cmbNombreIntitutoPregrado.selectedIndex].value;
    var institucionText = cmbNombreIntitutoPregrado.options[cmbNombreIntitutoPregrado.selectedIndex].innerHTML;
    var txtNombrePrograma = document.getElementById(txtNombreInst);
    var txtNombreProgramaText = document.getElementById(txtNombreInstText);
    if (institucion == "0") {
        txtNombrePrograma.value = "";
        txtNombreProgramaText.value = "";
    } else {
        txtNombrePrograma.value = institucion;
        txtNombreProgramaText.value = institucionText;
    }
    
    

    var cmbPrograma = document.getElementById(cmbNombreProg);
    cmbPrograma.options.length = 0;

    var param = { "institucion": institucion };
    $.ajax({
        url: '/Inicio/programaXinstitucion',
        type: 'POST',
        data: param,
        success: function (responce) {
            responce.forEach(function (element) {
                var option = document.createElement('option');
                option.text = element.Text;
                option.value = element.Value;
                cmbPrograma.add(option);
            });
        },
        error: function (responce) { mensajePopUp(responce.data); }
    });

    var cmbPrograma1 = document.getElementById(cmbNombreProgramaTitulo);
    cmbPrograma1.options.length = 0;
    var param1 = { "institucion": institucion };
    $.ajax({
        url: '/Inicio/programaTituloXinstitucion',
        type: 'POST',
        data: param1,
        success: function (responce) {
            responce.forEach(function (element) {
                var option = document.createElement('option');
                option.text = element.Text;
                option.value = element.Value;
                cmbPrograma1.add(option);
            });
        },
        error: function (responce) { mensajePopUp(responce.data); }
    });

}

function SeleccionProgramaPregrado(cmbNombrePrograma, txtTituloPre, cmbNombreProgramaTit) {
    var cmbNombrePrograma = document.getElementById(cmbNombrePrograma);
    var txtNombreProgramaTexto = document.getElementById(txtTituloPre);
    var nombreProgramaSel = cmbNombrePrograma.options[cmbNombrePrograma.selectedIndex].value;

    if (nombreProgramaSel == "0") {
        txtNombreProgramaTexto.value = "N/A";
    } else {
        var cmbNombreProgramaTitulo = document.getElementById(cmbNombreProgramaTit); 
        var nombreProgramaituloSel = cmbNombreProgramaTitulo.options[cmbNombrePrograma.selectedIndex].innerHTML;
        txtNombreProgramaTexto.value = nombreProgramaituloSel;
    }
}

