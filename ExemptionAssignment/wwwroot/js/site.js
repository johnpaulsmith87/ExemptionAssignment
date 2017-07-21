// Write your Javascript code
$(function () {
    $(".showOverdraft").hide();
    $(".debitoverdraft").change(function () {
        var balance = $(".balance").val();
        var overdraftLimit = $(".overdraft").val();
        var debit = $(".debitoverdraft").val();
        if (parseFloat(debit) > parseFloat(balance))
        {
            $(".showOverdraft").show();
        }
        else
        {
            $(".showOverdraft").hide();
        }
    });
    $(".debitoverdraft").keyup(function () {
        var balance = $(".balance").val();
        var overdraftLimit = $(".overdraft").val();
        var debit = $(".debitoverdraft").val();
        if (parseFloat(debit) > parseFloat(balance)) {
            $(".showOverdraft").show();
        }
        else {
            $(".showOverdraft").hide();
        }
    });
    
});