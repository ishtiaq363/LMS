$(document).ready(function () {
    hideLinks();

});

function loadAllSubjectData(obj) {
    document.getElementById("nv").style.display = "block";
   // document.getElementById("profile-feed").style.display = "block";
    getSubjectResources(obj);
    getPaperInformation(obj);
    getsubjectOutlineInformation(obj);
}
function getSubjectResources(obj) {
    var Id = obj.id;
    $("#myname").text(obj.getAttribute('name'));
    $("#assessmentsubjectname").text(" Assessment of "+ obj.getAttribute('name'));
    $("#outline").text(obj.getAttribute('name') + " Outline");
   // alert(Id)
    //hideLinks();
    $.ajax({
        url: "/student/Home/GetSubjectLinksInformation",
        type: "GET",
        data: { Id: Id },
        success: function (response) {
            let data = [...response];
            unload();
          
            if (data) {
                showResourceOnTable(data);
                getPaperInformation(obj)
            }


            if (response == null) {
                var tableBody = $("#resourceDataTable tbody");
                tableBody.empty();
                var row = $("<tr>");
                tableBody.append(row.append($("<p>").attr("text", "No Resource found")));
                // Clear existing rows
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log("Error: " + errorThrown);
            var tableBody = $("#resourceDataTable tbody");
            tableBody.empty();
            var row = $("<tr>");
            tableBody.append(row.append($("<p>").text("No Resource found")));
            
            unload();
        }
    });
}



function getPaperInformation(obj) {
   // alert('hello')
    var Id = obj.id;
    $("#myname").text(obj.getAttribute('name'));
   // alert(Id)
    hideLinks();
    $.ajax({
        url: "/student/Home/GetPaperInformation",
        type: "GET",
        data: { Id: Id },
        success: function (response) {
            let data = [...response];
            unload();
            
            if (data) {
                showAssessmentResourceOnTable(data);

            }


            if (response == null) {
                var tableBody = $("#assessmentDataTable tbody");
                tableBody.empty();
                var row = $("<tr>");
                tableBody.append(row.append($("<p>").attr("text", "No Resource found")));
                // Clear existing rows
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            
            var tableBody = $("#assessmentDataTable tbody");
            tableBody.empty();
            var row = $("<tr>");
            tableBody.append(row.append($("<p>").text("No Resource found")));

            unload();
        }
    });
}


function showAssessmentResourceOnTable(data) {
   
 
   
    var tableBody = $("#assessmentDataTable tbody");
    // Clear existing rows
    tableBody.empty();
    // Iterate over data and generate table rows
    $.each(data, function (index, item) {
       // alert(isDateEqualToToday(data[index].assessmentDate))
        var row = $("<tr>");
        let counter = 0;
        console.log(data);
        let check = checkDateAndTime(data[index].assessmentDate, data[index].startTime, data[index].endTime);// console.log(data[index].assessmentDate)
        // Iterate over object properties and generate table cells
        $.each(item, function (key, value) {

            console.log(key + " hi = " + value);

            if (key == "assessmentSource") {


                if (check) {
                    let anchor = $("<a>").attr("href", value).text("Download").attr("target", "_blank");
                    row.append($("<td>").append(anchor));
                } else {
                    row.append($("<td>").text("--"));
                }
            } else if (key == "assessmentDate" || key == "startTime" || key == "endTime") {
                row.append($("<td>").text(value));
            } else if (key == "status") {
                if(check) {
                    
                    let fileInput = $("<input>").attr({
                        type: "file",
                        name: "file", 
                        id: "file-" + data[index].id

                    });
                    let submitButton = $("<button>").text("Submit Paper").click(submitAssessmentToDB).attr("id", data[index].id).addClass("btn btn-primary");
                    
                  
                    row.append($("<td>").append(fileInput).append("<hr>").append(submitButton));
                } else {
                    
                    row.append($("<td>").text("--"));
                }
            }
            
           
         });

        tableBody.append(row);
    });
    

}
function submitAssessmentToDB() {
    let fileId = "file-" + this.id;
    var fileInput = document.getElementById(fileId);
        var file = fileInput.files[0];
    var assessmentScheduleId = this.id;
    var studentId = $("#StudentId").val();
       
        if (file) {
            var formData = new FormData();
            formData.append("file", file);
            formData.append("assessmentScheduleId", assessmentScheduleId);
            formData.append("studentId", studentId);
            fetch("/student/Home/SubmitAssessment", {
                method: "POST",
                body: formData
            })
                .then(response => {
                    if (response.ok) {
                        // File submitted successfully
                        alert("File submitted successfully.");
                        window.location.reload();
                    } else {
                        // Error handling
                        alert("Error submitting file.");
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert("Error submitting file.");
                });
        } else {
            console.log("No file selected.");
        }
   
}

function checkDateAndTime(d, start, end) {
    var currentDate = new Date();
    var assessmentDate = new Date(d);
    var startTime = new Date(d + "T" + start); // Combine date and time
    var endTime = new Date(d + "T" + end); // Combine date and time

    // Convert times to milliseconds since midnight
    var currentMs =
        currentDate -
        new Date(
            currentDate.getFullYear(),
            currentDate.getMonth(),
            currentDate.getDate()
        );
    var startMs =
        startTime -
        new Date(
            startTime.getFullYear(),
            startTime.getMonth(),
            startTime.getDate()
        );
    var endMs =
        endTime -
        new Date(endTime.getFullYear(), endTime.getMonth(), endTime.getDate());

    // Display AssessmentSource and UploadPaper only if current date is according to AssessmentDate
    // and just before StartTime
    if (
        currentDate.toDateString() === assessmentDate.toDateString() &&
        currentMs > startMs &&
        currentMs < endMs
    ) {
        // Display AssessmentSource and UploadPaper
        // Your code goes here
        return true;
    } else {
        // Don't display AssessmentSource and UploadPaper
        // Your code goes here
        return false;
    }
}


function isDateEqualToToday(givenDate) {
    // Get today's date
    var today = new Date();

    // Convert both dates to ISO strings and compare their date parts
    return givenDate.toISOString().slice(0, 10) === today.toISOString().slice(0, 10);
}




function showResourceOnTable(data) {
    $("#resource").show();
    var tableBody = $("#resourceDataTable tbody");
    // Clear existing rows
    tableBody.empty();
    // Iterate over data and generate table rows
    $.each(data, function (index, item) {

        var row = $("<tr>");
        let counter = 0;
        // Iterate over object properties and generate table cells
        $.each(item, function (key, value) {
            if (counter > 1) {
                if (key == "resourceUrl") {
                    let anchor = $("<a>").attr("href", value).text("Download").attr("target", "_blank");
                    row.append($("<td>").append(anchor));
                } else {
                    row.append($("<td>").text(value));
                }
            }
            counter++;
        });

        tableBody.append(row);
    });


}

function hideLinks() {
   
}


//////paperoutline


function getsubjectOutlineInformation(obj) {
    // alert('hello')
    var Id = obj.id;
   
   
    $.ajax({
        url: "/student/Home/GetSubjectOutlineInformation",
        type: "GET",
        data: { Id: Id },
        success: function (response) {
            let data = response;
            unload();
            
           
            if (data) {
                 showSubjectOutlineOnTable(data);
              
            }


            if (response == null) {
                var tableBody = $("#assessmentDataTable tbody");
                tableBody.empty();
                var row = $("<tr>");
                tableBody.append(row.append($("<p>").attr("text", "No Resource found")));
                // Clear existing rows
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log("Error: " + errorThrown);
            const outlineUl = document.getElementById('outlineUl');
            outlineUl.innerHTML = "";
            const listItem = document.createElement('li');
            listItem.textContent = "No outline Data Found";
            outlineUl.appendChild(listItem);

            unload();
        }
    });
}

function getRandomColor() {
    const colors = ['#ff5733', '#33ff57', '#5733ff']; // You can add more colors as needed
    return colors[Math.floor(Math.random() * colors.length)];
}

function showSubjectOutlineOnTable(data) {

    const outlineUl = document.getElementById('outlineUl');
    outlineUl.innerHTML = '';

    for (const [key, value] of Object.entries(data)) {
        if (key != "id" && key != "subjectId" && key != "isDeleted" && key != "subject") {
            const listItem = document.createElement('li');
            listItem.innerHTML = `
                <div class="timeline-dots timeline-dot1 border-primary text-primary" ></div>
                <h6 class="float-left mb-1">${key}</h6>                               
                <div class="d-inline-block w-100">
                    <p>${value}</p>
                </div>
            `;
            outlineUl.appendChild(listItem);
        }
    }


}

function fetchFeeByDate(obj) {

       let newHeading = `Fee Payment Report For Date ${obj.value}`;
    //   alert(newHeading)
       var heading = document.getElementById('myHeading');

    // Change its text content
     heading.textContent = newHeading;
    // alert(obj.value)
    let paymentId = obj.id;
    $.ajax({
        url: '/Admin/FeePrint/GetStudentFee',
        type: 'GET',
        data: { paymentDate: obj.value },
        success: function (response) {
            console.log(response);
            $('#basic-table tbody').empty();

            // Iterate through the response data and add rows to the table
            if (response && response.length > 0) {
                response.forEach(function (fee) {
                    var newRow = '<tr>' +
                        '<td>' + fee.batchName + '</td>' +
                        '<td>' + fee.studentName + '</td>' +
                        '<td>' + fee.status + '</td>' +
                        '<td>' + fee.paymentAmount + '</td>' +
                        '<td>' + fee.paymentDate + '</td>' +
                        '</tr>';
                    $('#basic-table tbody').append(newRow);
                });
            } else {
                var newRow = '<tr><td>' + 'No Fee record found for this Date' + '</td></tr>';
                $('#basic-table tbody').append(newRow);
            }
            // location.reload();
            // Handle success response
            // alert("Fee status updated successfully.");
            // You can add further logic here, such as displaying a success message or updating the UI.
        },
        error: function (xhr, status, error) {
            //  location.reload();
            // Handle error response
            alert(status)
            var newRow = '<tr><td>' + 'No Fee record found for this Date' + '</td></tr>';
            $('#basic-table tbody').append(newRow);
        }
    });
}

function printTheReport() {

    var paperWidth = 8.5 * 96; // 8.5 inches converted to pixels (assuming 96 pixels per inch)
    var paperHeight = 11 * 96; // 11 inches converted to pixels (assuming 96 pixels per inch)

    var mywindow = window.open('', 'PRINT', 'height=' + paperHeight + ',width=' + paperWidth);

    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + document.title + '</h1>');
    mywindow.document.write(document.getElementById("printDiv").innerHTML);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();

    return true;
}