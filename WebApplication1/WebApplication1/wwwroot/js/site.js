//BACKGROUND SLIDESHOW JS

let slideIndex = 0;
showSlides();

function showSlides() {
    let slides = document.getElementsByClassName("SlidesFade");

    for (let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }//for
    slideIndex++;
    if (slideIndex > slides.length) {
        slideIndex = 1
    }//if

    slides[slideIndex - 1].style.display = "block";
    setTimeout(showSlides, 4000); // Change image every 2 seconds*/
}
