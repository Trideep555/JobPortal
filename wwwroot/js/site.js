// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Initialize Swiper

var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.maxHeight) {
            panel.style.maxHeight = null;
        } else {
            panel.style.maxHeight = panel.scrollHeight + "px";
        }
    });
}
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
    $('.modal-toggler2').on("click", (event) => {
        $('footer').hide();
        $(event.target.dataset.target).modal('show');
    })
    $('.close-modal2').on("click", (event) => {
        
        $(event.target.dataset.target).modal('hide');
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
    $('.about-update').on("click", () => {
        
        let form = new FormData();
        form.append("First_Name", $('#f_name').val());
        form.append("Last_Name", $('#l_name').val());
        form.append("Country", $('#country').val());
        form.append("State", $('#state').val());
        form.append("Address", $('#add').val());
        form.append("Git", $('#git').val());
        form.append("Link", $('#link').val());
        form.append("Resume", $('#about_resume')[0].files[0]);
        form.append("DOB", $('#dob').val());
        form.append("Skills", $('#skills').val());
        form.append("Id", $('#about-id').val());
        form.append("phone", $('#phone').val());
        $.ajax({
            url: "/Home/EditAbout",
            processData: false,
            contentType: false,
            data: form,
            type: "POST",
            success: function (result) {
                //alert("Worked");
                location.reload(true);
            },
            error: function (error) {
                console.log("didnt work");
            }
        })

       

    })
    $('.education-add').on("click", () => {

        let form = new FormData();
        form.append("Id", $('#ed-id').val());
        form.append("EmployeeId", $('#emp-id').val());
        form.append("Institute_Name", $('#ins_name').val());
        form.append("Course_Name", $('#stream_name').val());
        form.append("start_date", $('#start_date').val());
        form.append("end_date", $('#end_date').val());
        form.append("marks", $('#marks').val());
        $.ajax({
            url: "/Home/EditEducation",
            processData: false,
            contentType: false,
            data: form,
            type: "POST",
            success: function (result) {
                //alert("Worked");
                location.reload(true);
            },
            error: function (error) {
                console.log("didnt work");
            }
        })
    });
    $('.experience-add').on("click", () => {

        let form = new FormData();
        form.append("Id", $('#ex-id').val());
        form.append("EmployeeId", $('#emp-id').val());
        form.append("Company", $('#comp_name').val());
        form.append("Role", $('#role_name').val());
        form.append("start_date", $('#start_date2').val());
        form.append("end_date", $('#end_date2').val());
        //form.append("marks", $('#yoe').val());
        $.ajax({
            url: "/Home/EditExperience",
            processData: false,
            contentType: false,
            data: form,
            type: "POST",
            success: function (result) {
                //alert("Worked");
                location.reload(true);
            },
            error: function (error) {
                console.log("didnt work");
            }
        })
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
    jobCard.addEventListener("click", (event) => {
        const number = Math.floor(Math.random() * 10);
        const url = `https://unsplash.it/640/425?image=${number}`;
        jobBg.src = url;
        let id = event.currentTarget.dataset.target;
        let logo = event.currentTarget.dataset.logo;
        let name = event.currentTarget.dataset.company;
        $.ajax({
            url: `/Job/Details/${id}`,
            processData: false,
            contentType: false,
            type: "GET",
            success: function (result) {
                console.log(result);
                let ans = result.job;
                $('#title').html(ans.jobTitle);
                $('#date').html(ans.date.split(" ")[0]);
                $('#exp').html(ans.jobExperience);
                $('#type').html(ans.jobtype);
                $('#skills').html(ans.jobSkill);
                $('#sal').html(ans.jobsalary);
                $('#loc').html(ans.locations);
                $('#desc').html(ans.jobDescription);
                $('#pos').html(ans.total_req - ans.total_select);
                $('#logo').attr('src',logo );
                $('#comp').html(name);
                $('#id').val(ans.id);
                $('#com_id').val(ans.companyId);
                if (result.status == 'Applied') {
                    $('#napply').css({ display: "none" })
                    $('#apply').css({ display: "" })

                }
                wrapper.classList.add("detail-page");
                wrapper.scrollTop = 0;
            },
            error: function (error) {
                console.log("didnt work");
            }
        })

       
    });
});


function closeit() {

    wrapper.classList.remove("detail-page");
    wrapper.scrollTop = 0;
    
};

function previewProfileImage() {
    var file = document.getElementById('profile-image').files[0];
    var reader = new FileReader();
    document.getElementById('com_image').style.display = "";
    reader.onloadend = function () {
        document.getElementById('com_image').src = reader.result;
    }
    if (file) {
        reader.readAsDataURL(file);
    } else {
        document.getElementById('com_image').src = "";
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
function previewResume2(index) {
    var file = document.getElementsByClassName('resume')[index].files[0];
    var reader = new FileReader();
    document.getElementsByClassName('preview-resume')[index].style.display = "";
    reader.onloadend = function () {
        document.getElementsByClassName('preview-resume')[index].src = reader.result;
    }
    if (file) {
        reader.readAsDataURL(file);
    } else {
        document.getElementsByClassName('preview-resume')[index].src = "";
    }
}
function trigger(id) {
    document.getElementById(id).click();
}
function trigger2(cls, index) {
    document.getElementsByClassName(cls)[index].click();
}

function eduget(id) {
    $.ajax({
        url: `/Home/Edu/${id}`,
        processData: false,
        contentType: false,
        type: "GET",
        success: function (result) {
            let ans = result.edu[id].a;
            console.log(result);
            $('#ins_name').val(ans.institute_Name);
            $('#stream_name').val(ans.course_Name);
            $('#start_date').val(ans.start_date);
            $('#end_date').val(ans.end_date);
            $('#marks').val(ans.marks);
            $('#ed-id').val(ans.id);

            $('#EducationModal').modal('show');
        },
        error: function (error) {
            console.log("didnt work");
        }
    })
}

    function expget(id) {
        $.ajax({
            url: `/Home/Exp/${id}`,
            processData: false,
            contentType: false,
            type: "GET",
            success: function (result) {
                let ans = result.edu[id].a;
                console.log(result);
                $('#comp_name').val(ans.company);
                $('#role_name').val(ans.role);
                $('#start_date2').val(ans.start_date);
                $('#end_date2').val(ans.end_date);
                $('#ex-id').val(ans.id);

                $('#ExperienceModal').modal('show');
            },
            error: function (error) {
                console.log("didnt work");
            }
        })
    
}
function photochange() {
    document.getElementById('profile-pic').click();
}
 function  photoupload() {
    var file = document.getElementById('profile-pic').files[0];
    var reader = new FileReader();
    document.getElementById('pic').style.display = "";
     reader.onloadend = function () {
         document.getElementById('pic').src = reader.result;
         document.getElementById('nav-pic').src = reader.result;

    }
    if (file) {
        reader.readAsDataURL(file);
    } else {
        document.getElementById('pic').src = "";
    } 
    //console.log(file);
    var form = new FormData();
    console.log($('#profile-pic')[0].files[0])
    form.append("Photo", $('#profile-pic')[0].files[0]);
    $.ajax({
        url: `/Home/PhotoUpload`,
        processData: false,
        contentType: false,
        data : form,
        type: "POST",
        success: function (result) {
            console.log(result);
        },
        error: function (error) {
            console.log("didnt work");
        }
    })
}
function deleteAdmin(id) {
    $.ajax({
        url: `/Admin/Delete/${id}`,
        processData: false,
        contentType: false,
        type: "GET",
        success: function (result) {
            location.reload(true);
            },
        error: function (error) {
            console.log("didnt work");
        }
    })

}
function deletecom(id) {
    $.ajax({
        url: `/Company/Delete/${id}`,
        processData: false,
        contentType: false,
        type: "GET",
        success: function (result) {
            location.reload(true);
        },
        error: function (error) {
            console.log("didnt work");
        }
    })

}
function comget(id) {
    $.ajax({
        url: `/Admin/Company/${id}`,
        processData: false,
        contentType: false,
        type: "GET",
        success: function (result) {
            var ans = result.company.a;
            console.log(result);
            $('#com_name').val(ans.company_Name);
            $('#id').val(ans.id);

            $('#com_email').val(result.company.b.email);
            $('#com_email').attr("disabled","true");

            $('#com_add').val(ans.address);
            $('#com_country').val(ans.country);
            $('#com_loc').val(ans.locations);
            $('#com_phone').val(ans.phone);
            $('#com_image').attr("src", "/" + ans.logo.split("\\")[0] + "/" + ans.logo.split("\\")[1]);
            $('#com_image').css({"display":""});





            $('#EmployerModal').modal('show');
        },
        error: function (error) {
            console.log("didnt work");
        }
    })
}
function jobget(id) {
    $.ajax({
        url: `/Company/Job/${id}`,
        processData: false,
        contentType: false,
        type: "GET",
        success: function (result) {
            let ans = result.job;
            $('#title').val(ans.jobTitle);
            $('#id').val(ans.id);

            $('#ex').val(ans.jobExperience);
           
            $('#loc').val(ans.locations);
            $('#role').val(ans.jobRole);
            $('#desc').val(ans.jobDescription);
            $('#skill').val(ans.jobSkill);
            $('#salary').val(ans.jobsalary);
            $('#req').val(ans.total_req);
            $('#type').val(ans.jobtype);
            $('footer').hide();


            $('#EmployerModal').modal('show');
        },
        error: function (error) {
            console.log("didnt work");
        }
    })
}
function jobstatus(id) {
    $.ajax({
        url: `/Company/Status/${id}`,
        processData: false,
        contentType: false,
        type: "GET",
        success: function (result) {
            location.reload(true);
        },
        error: function (error) {
            console.log("didnt work");
        }
    })
}
function jobapply(id) {
    $.ajax({
        url: `/Job/Accept/${id}`,
        processData: false,
        contentType: false,
        type: "GET",
        success: function (result) {
            location.reload(true);
        },
        error: function (error) {
            console.log("didnt work");
        }
    })
}
function jobreject(id) {
    $.ajax({
        url: `/Job/Reject/${id}`,
        processData: false,
        contentType: false,
        type: "GET",
        success: function (result) {
            location.reload(true);
        },
        error: function (error) {
            console.log("didnt work");
        }
    })
}
 
function searchtype() {
    let jobtypes = ""
    let experience = ""
    let salaryfic = ""
    for (let i of document.getElementsByClassName('type')) {
        if (i.checked) {
            jobtypes=i.value
        }
    }
    for (let i of document.getElementsByClassName('exp')) {
        if (i.checked) {
            experience=i.value
        }
    }
    for (let i of document.getElementsByClassName('sal')) {
        if (i.checked) {
            salaryfic = i.value;
        }
    }
    /*let url = new URL("/Job/Filters")
    url.searchParams.set("salary", JSON.stringify(salary))
    url.searchParams.set("types", JSON.stringify(jobtypes))
    url.searchParams.set("experience", JSON.stringify(experience)) */



    window.location = "/Job/Filters?salary=" + salaryfic+ "&types=" + jobtypes + "&experience=" + experience
   // window.location.pathname = "Job/Filters";
}