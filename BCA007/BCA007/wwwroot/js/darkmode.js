// Dark mode persistence and SSR toggle support + clock
window.darkModeHelper = {
    get: function () {
        var stored = localStorage.getItem('isDarkMode');
        return stored === null ? true : stored === 'true';
    },
    set: function (value) {
        localStorage.setItem('isDarkMode', value.toString());
        document.cookie = 'isDarkMode=' + value + ';path=/;max-age=31536000;SameSite=Lax';
    },
    // Toggle dark mode — works on both SSR and interactive pages
    toggle: function () {
        var current = this.get();
        var next = !current;
        this.set(next);

        // If Blazor interactive is active, call the .NET callback
        if (window._blazorDotNetRef) {
            window._blazorDotNetRef.invokeMethodAsync('ToggleDarkModeFromJs', next);
            return;
        }
        // SSR fallback: reload the page
        location.reload();
    },
    // Called from Blazor to register a .NET object reference for callbacks
    registerDotNetRef: function (dotNetRef) {
        window._blazorDotNetRef = dotNetRef;
    },
    init: function () {
        var isDark = this.get();
        this.set(isDark);
    }
};
window.darkModeHelper.init();

// Live clock for both SSR and interactive pages
window.clockHelper = {
    _intervalId: null,
    start: function () {
        if (this._intervalId) clearInterval(this._intervalId);
        function pad(n) { return n < 10 ? '0' + n : n; }
        function update() {
            // Look up element fresh every tick — Blazor may replace the DOM
            var el = document.getElementById('live-clock');
            if (!el) return;
            var now = new Date();
            var day = pad(now.getDate());
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
            var mon = months[now.getMonth()];
            var year = now.getFullYear();
            var h = now.getHours();
            var ampm = h >= 12 ? 'PM' : 'AM';
            h = h % 12; if (h === 0) h = 12;
            var time = day + ' ' + mon + ' ' + year + '  ' + pad(h) + ':' + pad(now.getMinutes()) + ':' + pad(now.getSeconds()) + ' ' + ampm;
            el.textContent = time;
        }
        update();
        this._intervalId = setInterval(update, 1000);
    }
};
// Start clock on load
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', function () { window.clockHelper.start(); });
} else {
    window.clockHelper.start();
}
