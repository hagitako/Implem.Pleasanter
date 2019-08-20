﻿$p.openDialog = function ($control, appendTo, resizable) {
    $($control.attr('data-selector')).dialog({
        modal: true,
        width: '420px',
        appendTo: appendTo,
        resizable: resizable
    });
}

$p.closeDialog = function ($control) {
    $p.clearMessage();
    $control.closest('.ui-dialog-content').dialog('close');
}

$p.clearDialogs = function () {
    $('body > .ui-dialog').remove();
}