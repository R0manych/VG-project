function formSuccess() {
    $("#msgSubmit").removeClass("hidden");
}

function submit() {
    alert("Yes!");
    if (event.isDefaultPrevented()) {
        formError();
        submitMSG(false, "Did you fill in the form properly?");
    } else {
        event.preventDefault();
        submitForm();
    }
}

function submitMSG(valid, msg) {
    varmsgClasses;
    if (valid) {
        msgClasses = "h3 text-center tada animated text-success";
    } else {
        msgClasses = "h3 text-center text-danger";
    }
    $("#msgSubmit").removeClass().addClass(msgClasses).text(msg);
}

function formError() {
    $("#contactForm").removeClass().addClass('shake animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
        $(this).removeClass();
    });
}

function formSuccess() {
    $("#contactForm")[0].reset();
    submitMSG(true, "Message Submitted!");
}

function submitForm() {
    alert("Start");
    postdata = JSON.stringify(
         {
             "From": document.getElementById("email").value,
             "Name": document.getElementById("name").value,
             "Phone": document.getElementById("phone").value,
             "Body": document.getElementById("message").value
         });
    try {
        $.ajax({    
            type: "POST",
            url: "/Home/Contact",
            data: postdata
        });
    }
    catch (e){
        alert(e);
    }
    alert("Yes");
}



/*//Generates the captcha function    
function DrawCaptcha()
{
    var a = alpha[Math.floor(Math.random() * alpha.length)];
    var b = alpha[Math.floor(Math.random() * alpha.length)];
    var c = alpha[Math.floor(Math.random() * alpha.length)];
    var d = alpha[Math.floor(Math.random() * alpha.length)];
    var e = alpha[Math.floor(Math.random() * alpha.length)];
    var f = alpha[Math.floor(Math.random() * alpha.length)];
    var g = alpha[Math.floor(Math.random() * alpha.length)];
    var code = a + ' ' + b + ' ' + ' ' + c + ' ' + d + ' ' + e + ' '+ f + ' ' + g;
    document.getElementById("txtCaptcha").value = "15";
}

// Validate the Entered input aganist the generated security code function   
function ValidCaptcha(){
    var str1 = removeSpaces(document.getElementById('txtCaptcha').value);
    var str2 = removeSpaces(document.getElementById('txtInput').value);
    if (str1 === str2) return true;        
    return false;        
}

// Remove the spaces from the entered and generated code
function removeSpaces(string)
{
    return string.split(' ').join('');
}*/
    
 