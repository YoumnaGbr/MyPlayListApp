document.addEventListener('DOMContentLoaded', function () {
    $('#editSongModal').on('show.bs.modal', function (event) {
        const button = $(event.relatedTarget);
        const modal = $(this);

        modal.find('#Id').val(button.data('id'));
        modal.find('#Name').val(button.data('name'));
        modal.find('#SingerId').val(button.data('singerid'));
        modal.find('#CategoryId').val(button.data('categoryid'));
    });

    $('.singer-details-link').on('click', function (event) {
        event.preventDefault();

        var singerId = $(this).data('id');

        $.ajax({
            url: getSingerUrl,
            type: 'GET',
            data: { id: singerId },
            success: function (data) {
                $('#modalContent').html(data);
            },
            error: function () {
                $('#modalContent').html('<p class="text-danger">Failed to load singer details.</p>');
            }
        });
    });
});

function deleteSong(songId) {
    if (confirm("Are you sure you want to delete this song?")) {
        $.ajax({
            type: "POST",
            dataType: 'json',
            url: deleteSongUrl,
            data: { songId: songId },
            success: function (e) {
                alert(e.message);
                window.location.reload();
            },
            error: function (e) {
                alert(e.message);
            }
        });
    }
}