function autoScroll()
{
    function aScroll()
    {
        var imgs = document.getElementsByClassName("banish")

        for (cont = 0; cont < imgs.length; cont++)
        {
            if (imgs[cont].style.display == "block") 
            {
                if (cont + 1 >= imgs.length) 
                {
                    doBanish(cont, 0);
                }
                else
                {
                    doBanish(cont, cont + 1);
                }

                break;
            }
        }

        setTimeout(aScroll, 7000);
    }
    aScroll();
}



function dotBanish(dot)
{
    var imgs = document.getElementsByClassName("banish");

    for (cont = 0; cont < imgs.length; cont++)
    {
        if (imgs[cont].style.display == "block") 
        {
            doBanish(cont, dot);
            break;
        }
    }
}



function doBanish(ImgBanish, ImgShow) 
{
    var dots = document.getElementsByClassName("banish-dot");
    var imgs = document.getElementsByClassName("banish");
    var cont = 1;

    banish();
    function banish()
    {
        cont = cont - 0.05;
        if (cont > 0.1) 
        {
            imgs[ImgBanish].style.opacity = cont;
            setTimeout(banish, 30);
        }
        else
        {
            dots[ImgBanish].style.backgroundColor = "";
            dots[ImgBanish].style.borderStyle = "none";
            dots[ImgShow].style.backgroundColor = "rgba(255, 255, 255, 90%)";
            dots[ImgShow].style.borderStyle = "solid";
            imgs[ImgBanish].style.display = "none";
            imgs[ImgShow].style.display = "block";
        }
    }
    banish();


    function show()
    {
        cont = cont + 0.05;

        if (cont <= 1) 
        {
            imgs[ImgShow].style.opacity = cont;
            setTimeout(show, 30);
        }
    }
    show();
}