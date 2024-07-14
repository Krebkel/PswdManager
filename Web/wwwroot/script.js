$(document).ready(function() {
    loadEntries();

    $("#addEntryBtn").click(showAddEntryModal);
    $("#saveEntryBtn").click(saveEntry);
    $("#cancelEntryBtn").click(cancelEntry);
    $("#searchInput").on("input", filterEntries);
    $("#entriesTableBody").on("click", ".password-cell", togglePasswordVisibility);
});

function loadEntries() {
    $.ajax({
        url: '/api/passwordEntries',
        method: 'GET',
        success: function(entries) {
            $("#entriesTableBody").empty();
            entries.sort((a, b) => new Date(b.dateAdded) - new Date(a.dateAdded));
            entries.forEach(addEntryToTable);
        }
    });
}

function addEntryToTable(entry) {
    $("#entriesTableBody").append(`
        <tr>
            <td>${entry.name}</td>
            <td class="password-cell password-hidden">${entry.password}</td>
            <td>${new Date(entry.dateAdded).toLocaleString()}</td>
        </tr>
    `);
}

function showAddEntryModal() {
    $("#entryForm")[0].reset();
    $("#entryModal").modal('show');
}

function saveEntry() {
    const entry = {
        name: $("#name").val(),
        password: $("#password").val(),
        isEmail: $("#emailType").is(":checked"),
        dateAdded: new Date().toISOString()
    };

    if (!validateEntry(entry)) {
        return;
    }

    $.ajax({
        url: '/api/passwordEntries',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(entry),
        success: function(newEntry) {
            $("#entryModal").modal('hide');
            addEntryToTable(newEntry);
        },
        error: function(xhr) {
            if (xhr.status === 400) {
                alert("Такая запись уже существует!");
            } else {
                alert("Возникла ошибка при создании записи!");
            }
        }
    });
}

function cancelEntry() {
    $('#entryModal').modal('hide');
}

function validateEntry(entry) {
    if (!entry.name || !entry.password) {
        alert("Все поля обязательны к заполнению.");
        return false;
    }

    if (entry.password.length < 8) {
        alert("Пароль должен содержать как минимум 8 символов!");
        return false;
    }

    if (entry.isEmail && !validateEmail(entry.name)) {
        alert("Пожалуйста, введите правильный адрес почты.");
        return false;
    }

    return true;
}

function validateEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(String(email).toLowerCase());
}

function filterEntries() {
    const searchText = $("#searchInput").val().toLowerCase();
    $("#entriesTableBody tr").filter(function() {
        $(this).toggle($(this).text().toLowerCase().indexOf(searchText) > -1);
    });
}

function togglePasswordVisibility() {
    $(this).toggleClass("password-hidden");
}