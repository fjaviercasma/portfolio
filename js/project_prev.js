function showObject(object)
{
	var objects = document.getElementsByClassName("object");

	for (cont = 0; cont < objects.length; cont++)
	{
		if (objects[cont].style.display == "block") 
		{
			objects[cont].style.display = "none";
		}
	}

	objects[object].style.display = "block";
}