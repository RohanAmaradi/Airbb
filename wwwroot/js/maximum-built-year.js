jQuery.validator.addMethod("builtyear", function (value, element, param) {
    if (value === '') return false;

    var builtDate = new Date(value);
    if (builtDate === "Invalid Date") return false;

    var maxYearsBack = Number(param);
    var today = new Date();

    // Check if date is in the future
    if (builtDate > today) {
        return false;
    }

    // Check if date is too far in the past
    var minAllowedDate = new Date();
    minAllowedDate.setFullYear(today.getFullYear() - maxYearsBack);

    return (builtDate >= minAllowedDate);
});

jQuery.validator.unobtrusive.adapters.addSingleVal("builtyear", "years");