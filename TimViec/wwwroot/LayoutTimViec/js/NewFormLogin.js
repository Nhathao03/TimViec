
function showNewForm() {
    document.getElementById("currentForm").classList.add("hiddenForm");
    document.getElementById("newForm").classList.remove("hiddenForm");
}

function hideNewForm() {
    document.getElementById("currentForm").classList.remove("hiddenForm");
    document.getElementById("newForm").classList.add("hiddenForm");
}
