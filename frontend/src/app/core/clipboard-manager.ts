export class ClipboardManager {
    public static copyUrl(url: string) {
        const selectedBox = document.createElement('textarea');
        selectedBox.style.position = 'fixed';
        selectedBox.style.left = '0';
        selectedBox.style.top = '0';
        selectedBox.style.opacity = '0';
        selectedBox.value = url;
        document.body.appendChild(selectedBox);
        selectedBox.focus();
        selectedBox.select();
        document.execCommand('copy');
        document.body.removeChild(selectedBox);
    }
}
