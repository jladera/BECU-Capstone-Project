/**
 * @author Victor Chimenti
 * @file filterCards.js
 */




// check results for null
$(function assignVisibleItems() {
    // assign array of currently visible content items
    visibleItems = $('.card').not('.hideByText,' +
        ' .hideByHours,' +
        ' .hideByDriveThruOnly,' +
        ' .hideBySurcharge,' +
        ' .hideByAcceptDeposit,' +
        ' .hideByCoinStar,' +
        ' .hideByTellerServices,' +
        ' .hideBy_24hourExpressBox,' +
        ' .hideByPartnerCreditUnion,' +
        ' .hideByMemberConsultant,' +
        ' .hideByInstantDebitCardReplacement');
    // check to see if array is empty
    if (visibleItems.length == 0) {
        // when array is empty show the results message
        $('.noResultsToShow').removeClass('hideResultsMessage');
    } else {
        // when array has content items suppress the results message
        $('.noResultsToShow').addClass('hideResultsMessage');
    }
});



$(function () {
    // After the DOM is ready, Wait until the window loads
    $(document).ready(function () {
        //$(document).ready(function () {

        // Once window loads set a timeout delay
        setTimeout(function () {



            //   ***   Keyword Search   ***   //
            $(function () {
                // scan the keyword each character the user inputs
                $('#keyword_search').on('keyup', function () {
                    // Assign Search Key
                    var key = $(this).val().toLowerCase();
                    // filter the cards for the input key
                    $(function () {
                        $('.card').filter(function () {
                            // when the search key is not present in the item then hide the item
                            $(this).toggleClass('hideByText', !($(this).text().toLowerCase().indexOf(key) > -1));
                        });
                    });
                });
                assignVisibleItems();
            });




            //   ***   24 Hours Filter   ***   //
            $(function () {
                // When the select box Hours changes - Execute change function
                $('#Hours').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the Hours card items in card
                    if ($('#Hours:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.Hours').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideByHours');
                                } else {
                                    $(this).parents('.card').addClass('hideByHours');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideByHours');
                        }
                    } else {
                        $('.card').removeClass('hideByHours');
                    }
                    assignVisibleItems();
                });
            });




            //   ***   Drive Thru Only   ***   //
            $(function () {
                // When the select box Drive Thru changes - Execute change function
                $('#DriveThruOnly').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the Drive Thru card items
                    if ($('#DriveThruOnly:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.InstallationType').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideByDriveThruOnly');
                                } else {
                                    $(this).parents('.card').addClass('hideByDriveThruOnly');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideByDriveThruOnly');
                        }
                    } else {
                        $('.card').removeClass('hideByDriveThruOnly');
                    }
                    assignVisibleItems();
                });
            });




            //   ***   Surcharge   ***   //
            $(function () {
                // When the select box Surchargechanges - Execute change function
                $('#Surcharge').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the Surcharge items
                    if ($('#Surcharge:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.Surcharge').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideBySurcharge');
                                } else {
                                    $(this).parents('.card').addClass('hideBySurcharge');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideBySurcharge');
                        }
                    } else {
                        $('.card').removeClass('hideBySurcharge');
                    }
                    assignVisibleItems();
                });
            });




            //   ***   Accepts Deposits   ***   //
            $(function () {
                // When the select box Deposits changes - Execute change function
                $('#AcceptDeposit').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the ccepts Deposits items
                    if ($('#AcceptDeposit:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.AcceptsDeposits').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideByAcceptDeposit');
                                } else {
                                    $(this).parents('.card').addClass('hideByAcceptDeposit');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideByAcceptDeposit');
                        }
                    } else {
                        $('.card').removeClass('hideByAcceptDeposit');
                    }
                    assignVisibleItems();
                });
            });




            //   ***   NFC Drive Thru  ***   //
            $(function () {
                // When the select box Coin Star changes - Execute change function
                $('#NFCDriveThruOnly').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the Coin Star items
                    if ($('#NFCDriveThruOnly:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.DriveThruOnly').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideByDriveThruOnly');
                                } else {
                                    $(this).parents('.card').addClass('hideByDriveThruOnly');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideByDriveThruOnly');
                        }
                    } else {
                        $('.card').removeClass('hideByDriveThruOnly');
                    }
                    assignVisibleItems();
                });
            });




            //   ***   Coin Star   ***   //
            $(function () {
                // When the select box Coin Star changes - Execute change function
                $('#CoinStar').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the Coin Star items
                    if ($('#CoinStar:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.CoinStar').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideByCoinStar');
                                } else {
                                    $(this).parents('.card').addClass('hideByCoinStar');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideByCoinStar');
                        }
                    } else {
                        $('.card').removeClass('hideByCoinStar');
                    }
                    assignVisibleItems();
                });
            });




            //   ***   Teller Services   ***   //
            $(function () {
                // When the select box Teller Services changes - Execute change function
                $('#TellerServices').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the Teller Services items
                    if ($('#TellerServices:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.TellerServices').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideByTellerServices');
                                } else {
                                    $(this).parents('.card').addClass('hideByTellerServices');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideByTellerServices');
                        }
                    } else {
                        $('.card').removeClass('hideByTellerServices');
                    }
                    assignVisibleItems();
                });
            });




            //   ***   24 Hour Express Box   ***   //
            $(function () {
                // When the select box 24 Hour Express changes - Execute change function
                $('#_24hourExpressBox').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the 24 Hour Express card items
                    if ($('#_24hourExpressBox:checkbox').is(':checked', true)) {
                        if (key) {
                            $('._24hourExpressBox').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideBy_24hourExpressBox');
                                } else {
                                    $(this).parents('.card').addClass('hideBy_24hourExpressBox');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideBy_24hourExpressBox');
                        }
                    } else {
                        $('.card').removeClass('hideBy_24hourExpressBox');
                    }
                    assignVisibleItems();
                });
            });




            //   ***   Partner Credit Union   ***   //
            $(function () {
                // When the select box Partner changes - Execute change function
                $('#PartnerCreditUnion').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the Partner card items
                    if ($('#PartnerCreditUnion:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.PartnerCreditUnion').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideByPartnerCreditUnion');
                                } else {
                                    $(this).parents('.card').addClass('hideByPartnerCreditUnion');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideByPartnerCreditUnion');
                        }
                    } else {
                        $('.card').removeClass('hideByPartnerCreditUnion');
                    }
                    assignVisibleItems();
                });
            });




            //   ***   Member Consultant   ***   //
            $(function () {
                // When the select box Consultant changes - Execute change function
                $('#MemberConsultant').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the Consultant items
                    if ($('#MemberConsultant:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.MemberConsultant').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideByMemberConsultant');
                                } else {
                                    $(this).parents('.card').addClass('hideByMemberConsultant');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideByMemberConsultant');
                        }
                    } else {
                        $('.card').removeClass('hideByMemberConsultant');
                    }
                    assignVisibleItems();
                });
            });





            //   ***   Instant Debit Card Replacement   ***   //
            $(function () {
                // When the select box Debit Card Replacement changes - Execute change function
                $('#InstantDebitCardReplacement').change(function () {
                    // Assign Search Key
                    var key = $(this).val();
                    // If Search Key is Not Null then Compare to the Debit Card Replacement items
                    if ($('#InstantDebitCardReplacement:checkbox').is(':checked', true)) {
                        if (key) {
                            $('.InstantDebitCardReplacement').filter(function (i, e) {
                                var value = $(this).text();
                                // Check to see if the Key and Value are a Match
                                if (value.match(key)) {
                                    $(this).parents('.card').removeClass('hideByInstantDebitCardReplacement');
                                } else {
                                    $(this).parents('.card').addClass('hideByInstantDebitCardReplacement');
                                }
                            });
                            // Else the Search Key is Null so Reset all Content Items to Visible
                        } else {
                            $('.card').removeClass('hideByInstantDebitCardReplacement');
                        }
                    } else {
                        $('.card').removeClass('hideByInstantDebitCardReplacement');
                    }
                    assignVisibleItems();
                });
            });
        }, 5);
    });
});
