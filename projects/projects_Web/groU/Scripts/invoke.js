function nuevaPublicacion()
{
    var comentar = document.getElementById('Content');
    var boton = document.getElementById('mainNewPost');

    if (comentar.style.display == "none") {
        comentar.style.display = "block";
        boton.style.display = "block";
    }
    else {
        comentar.style.display = "none";
        boton.style.display = "none";
    }
}

function nuevoEvento() {
    var titulo = document.getElementById('Title');
    var comentar = document.getElementById('Content');
    var fecha = document.getElementById('Date');
    var boton = document.getElementById('newEvent');

    if (comentar.style.display == "none") {
        titulo.style.display = "block";
        comentar.style.display = "block";
        fecha.style.display = "block";
        boton.style.display = "block";
    }
    else {
        titulo.style.display = "none";
        comentar.style.display = "none";
        fecha.style.display = "none";
        boton.style.display = "none";
    }
}

function darLikePublicacion(idPublicacion) {
    var boton = document.getElementById('likePost');
    document.cookie = "idPost=" + idPublicacion;

    boton.click();
}

function agregarContacto(idContacto) {
    var boton = document.getElementById('addContact');
    document.cookie = "idUsuario=" + idContacto;

    boton.click();
}

function inscribirseEvento(idEvento) {
    var boton = document.getElementById('addEvent');
    document.cookie = "idEvento=" + idEvento;

    boton.click();
}

function verPublicacion(idPublicacion) {
    var boton = document.getElementById('showPost');
    document.cookie = "idPost=" + idPublicacion;

    boton.click();
}

function verComentario(idComentario) {
    var boton = document.getElementById('showComment');
    document.cookie = "idComentario=" + idComentario;
    document.cookie = "subcomentario=si";

    boton.click();
}

function verUsuario(idUsuario) {
    var boton = document.getElementById('showUser');
    document.cookie = "idUsuario=" + idUsuario;

    boton.click();
}

function verContactosContacto(idUsuario) {
    var boton = document.getElementById('showContactsContact');
    document.cookie = "idUsuario=" + idUsuario;

    boton.click();
}

function verUsuariosEvento(idUsuario) {
    var boton = document.getElementById('showUsersEvent');
    document.cookie = "idUsuario=" + idUsuario;

    boton.click();
}

function mostrarEvento(idEvento) {
    var boton = document.getElementById('showEvent');
    document.cookie = "idEvento=" + idEvento;

    boton.click();
}

function eventosAsistir() {
    var boton = document.getElementById('userAssistance');

    boton.click();
}

function eventosUsuario() {
    var boton = document.getElementById('userEvents');

    boton.click();
}

function favoritos() {
    var boton = document.getElementById('favs');

    boton.click();
}

function comentarComentario() {
    var comentar = document.getElementById('Content');
    var boton = document.getElementById('commentComment');

    if (comentar.style.display == "none") {
        comentar.style.display = "block";
        boton.style.display = "block";
    }
    else {
        comentar.style.display = "none";
        boton.style.display = "none";
    }
}

function comentarPublicacion() {
    var comentar = document.getElementById('Content');
    var boton = document.getElementById('commentPost');

    if (comentar.style.display == "none") {
        comentar.style.display = "block";
        boton.style.display = "block";
    }
    else {
        comentar.style.display = "none";
        boton.style.display = "none";
    }
}