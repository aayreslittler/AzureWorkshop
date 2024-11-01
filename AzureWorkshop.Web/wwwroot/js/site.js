function viewContainerContents_OnClick() {
    const container = $('#SelectedContainer').val();
    if (container) {
        $('body').css('cursor', 'wait');
        window.location.href = $('#containerBaseUrl').val() + '?container=' + container;
    }

    return false;
}