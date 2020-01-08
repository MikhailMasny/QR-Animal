let timeout = 10;
window.onload = function RedirectPage() {

    timeout--;

    if (timeout <= 0) {
        location.replace(document.location.origin)
    }
    else {
        setTimeout(RedirectPage, 1000)
    }
}