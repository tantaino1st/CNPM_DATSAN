// Handle Navbar scroll effect
$(window).scroll(function () {
    if ($(this).scrollTop() > 50) {
        $('#mainNav').addClass('scrolled shadow-sm').removeClass('bg-transparent');
    } else {
        $('#mainNav').removeClass('scrolled shadow-sm').addClass('bg-transparent');
    }
});

// Smooth scroll for anchors
$('a.nav-link[href^="#"]').on('click', function (e) {
    e.preventDefault();
    var target = this.hash;
    var $target = $(target);
    if ($target.length) {
        $('html, body').stop().animate({
            'scrollTop': $target.offset().top - 80
        }, 500, 'swing');
    }
});

// Initialize tooltips if any
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
});
