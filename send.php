<?php
$name = $_POST['name'];
$company = $_POST['company'];
$contactinfo = $_POST['contact-info'];
$subject = $_POST['subject'];

$header = 'From: ' . $name . " \r\n";
$header .= "X-Mailer: PHP/" . phpversion() . " \r\n";
$header .= "Mime-Version: 1.0 \r\n";
$header .= "Content-Type: text/plain";

$text = "Este mensaje fue enviado por: " . $name . " \r\n";
$text .= "De la empresa: " . $company . " \r\n";
$text .= "Información de contacto: " . $contactinfo . " \r\n";
$text .= "Con asunto: " . $subject . " \r\n";
$text .= "Mensaje: " . $_POST['text'] . " \r\n";
$text .= "Enviado el: " . date('d/m/Y', time());

$para = 'fjaviercasma@gmail.com';
$asunto = 'Mensaje de... (Escribe como quieres que se vea el remitente de tu correo)';

mail($para, $asunto, utf8_decode($text), $header);

header("Location:contact_me_ES.html");
?>