function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}
document.addEventListener('DOMContentLoaded', function () {

    // Toggle sidebar catégories
    const toggleBtn = document.getElementById('toggleMenu');
    const sidebar = document.getElementById('sidebar');

    if (toggleBtn && sidebar) {
        toggleBtn.addEventListener('click', function () {
            const isHidden = sidebar.style.display === 'none' || sidebar.style.display === '';
            sidebar.style.display = isHidden ? 'block' : 'none';
        });
    }

    // Mini‑panier (lecture depuis localStorage si tu utilises le panier côté client)
    const cartBadge = document.getElementById('cartCountBadge');
    if (cartBadge) {
        const cart = JSON.parse(localStorage.getItem('cart') || '[]');
        const count = cart.reduce((sum, item) => sum + (item.qty || 0), 0);
        cartBadge.textContent = count;
        if (count === 0) cartBadge.classList.add('d-none');
    }
});

document.addEventListener('DOMContentLoaded', function () {
    const toggleBtn = document.getElementById('toggleMenu');
    const sidebar = document.getElementById('sidebar');

    if (toggleBtn && sidebar) {
        toggleBtn.addEventListener('click', function () {
            if (sidebar.style.display === 'none' || sidebar.style.display === '') {
                sidebar.style.display = 'block';
            } else {
                sidebar.style.display = 'none';
            }
        });
    }
});
