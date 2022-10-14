window.addOnlineStatusListener = function (dotNetRef) {
    function updateDotNet() {
        dotNetRef.invokeMethodAsync('OnOnlineStatusChange', navigator.onLine);
    };
    window.addEventListener('online', updateDotNet);
    window.addEventListener('offline', updateDotNet);
    updateDotNet();
}