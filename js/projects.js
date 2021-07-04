function showList(list) 
{
    var Lists = document.getElementsByClassName("projects-list");

    for (var cont = 0; cont < Lists.length; cont++)
    {
    	if (cont != list) 
    	{
    		Lists[cont].style.display = "none";
    	}
    	else
    	{
    		if (Lists[list].style.display != "block") 
		    {
		        Lists[list].style.display = "block";
		    }
		    else 
		    {
		        Lists[list].style.display = "none";
		    }
    	}
    }
}