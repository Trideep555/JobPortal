// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Initialize Swiper

let tabSwitchers = document.querySelectorAll('[target-wrapper]')
tabSwitchers.forEach(item => {
    item.addEventListener('click', (e) => {
        let currentWrapperId = item.getAttribute('target-wrapper')
        let currentWrapperTargetId = item.getAttribute('target-tab')

        let currentWrapper = document.querySelector(`#${currentWrapperId}`)
        let currentWrappersTarget = document.querySelector(`#${currentWrapperTargetId}`)

        let allCurrentTabItem = document.querySelectorAll(`[target-wrapper='${currentWrapperId}']`)
        let allCurrentWrappersTarget = document.querySelectorAll(`#${currentWrapperId} .tab-content`)

        if (currentWrappersTarget) {
            if (!currentWrappersTarget.classList.contains('active')) {
                allCurrentWrappersTarget.forEach(tabItem => {
                    tabItem.classList.remove('active')
                })
                allCurrentTabItem.forEach(item => {
                    item.classList.remove('active')
                })
                item.classList.add('active')
                currentWrappersTarget.classList.add('active')
            }
        }
    })
})
var swiper = new Swiper(".swiper", {
    slidesPerView: 4,
    spaceBetween: 25,
    loop: false,
    centerSlide: "true",
    fade: "true",
    grabCursor: "true",
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },

    breakpoints: {
        0: {
            slidesPerView: 1,
        },
        680: {
            slidesPerView: 2,
        },
        950: {
            slidesPerView: 3,
            slidesPerColumn: 2,
            grid: {
                row: 2,
            },

        },
    },
});
if (window.location.pathname == "/Home" || window.location.pathname == "/" || window.location.pathname == "/Home/Index") {
    var multipleCardCarousel = document.querySelector("#carouselExampleControls");

    if (window.matchMedia("(min-width: 576px)").matches) {
        var carousel = new bootstrap.Carousel(multipleCardCarousel, {
            interval: false
        });
        var carouselWidth = $(".inner")[0].scrollWidth;
        var cardWidth = $(".item").width();
        var scrollPosition = 0;
        $("#carouselExampleControls .next").on("click", function () {
            if (scrollPosition < carouselWidth - cardWidth * 3) {
                scrollPosition += cardWidth;
                $("#carouselExampleControls .inner").animate(
                    { scrollLeft: scrollPosition },
                    600
                );
            }
        });
        $("#carouselExampleControls .prev").on("click", function () {
            if (scrollPosition > 0) {
                scrollPosition -= cardWidth;
                $("#carouselExampleControls .inner").animate(
                    { scrollLeft: scrollPosition },
                    600
                );
            }
        });
    } else {
        $(multipleCardCarousel).addClass("slide");
    }
}
$(document).ready(function () {
    if (window.location.pathname.includes("/Home/Register") == true) {
        $.validator.setDefaults({
            ignore: []
        });
    }
    $('.modal-toggler').click(() => {
        $('footer').hide();
        $('#EmployerModal').modal('show');
    })
    $('.close-modal').click(() => {
        $('#EmployerModal').modal('hide');
        setTimeout(() => $('footer').show(), 500);

       })

    //Enable Tooltips


    var tooltipTriggerList = [].slice.call(
        document.querySelectorAll('[data-bs-toggle="tooltip"]')
    );
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    //Advance Tabs
    $(".next-tab").click(function () {
        const nextTabLinkEl = $(".nav-tabs .active")
            .closest("li")
            .next("li")
            .find("a")[0];
        const nextTab = new bootstrap.Tab(nextTabLinkEl);
        nextTab.show();
    });

    $(".previous").click(function () {
        const prevTabLinkEl = $(".nav-tabs .active")
            .closest("li")
            .prev("li")
            .find("a")[0];
        const prevTab = new bootstrap.Tab(prevTabLinkEl);
        prevTab.show();
    });
});
//$(document).ready(function() {
//$('.search-box').focus();
//});

const wrapper = document.querySelector(".wrapper");
const header = document.querySelector(".header");




const jobCards = document.querySelectorAll(".job-card");
const logo = document.querySelector(".logo");
const jobLogos = document.querySelector(".job-logos");
const jobDetailTitle = document.querySelector(
    ".job-explain-content .job-card-title"
);
const jobBg = document.querySelector(".job-bg");

jobCards.forEach((jobCard) => {
    jobCard.addEventListener("click", () => {
        const number = Math.floor(Math.random() * 10);
        const url = `https://unsplash.it/640/425?image=${number}`;
        jobBg.src = url;

        const logo = jobCard.querySelector("svg");
        const bg = logo.style.backgroundColor;
        console.log(bg);
        jobBg.style.background = bg;
        const title = jobCard.querySelector(".job-card-title");
        jobDetailTitle.textContent = title.textContent;
        jobLogos.innerHTML = logo.outerHTML;
        wrapper.classList.add("detail-page");
        wrapper.scrollTop = 0;
    });
});


function closeit() {

    wrapper.classList.remove("detail-page");
    wrapper.scrollTop = 0;
    
};

function previewProfileImage() {
    var file = document.getElementById('profile-image').files[0];
    var reader = new FileReader();
    document.getElementById('preview-profile-image').style.display = "";
    reader.onloadend = function () {
        document.getElementById('preview-profile-image').src = reader.result;
    }
    if (file) {
        reader.readAsDataURL(file);
    } else {
        document.getElementById('preview-profile-image').src = "";
    }
}

// Preview Resume
function previewResume() {
    var file = document.getElementById('resume').files[0];
    var reader = new FileReader();
    document.getElementById('preview-resume').style.display = ""; 
    reader.onloadend = function () {
        document.getElementById('preview-resume').src = reader.result;
    }
    if (file) {
        reader.readAsDataURL(file);
    } else {
        document.getElementById('preview-resume').src = "";
    }
}
function trigger(id) {
    document.getElementById(id).click();
}