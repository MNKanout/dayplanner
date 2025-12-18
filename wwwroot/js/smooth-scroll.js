// Smooth scroll functionality for navigation links
window.scrollToSection = (sectionId) => {
    const element = document.getElementById(sectionId);
    if (element) {
        const navHeight = 70; // Height of fixed navbar
        const elementPosition = element.getBoundingClientRect().top + window.pageYOffset;
        const offsetPosition = elementPosition - navHeight;

        window.scrollTo({
            top: offsetPosition,
            behavior: 'smooth'
        });
    }
    return true;
};

// Handle hash navigation on page load
window.addEventListener('DOMContentLoaded', () => {
    if (window.location.hash) {
        setTimeout(() => {
            const hash = window.location.hash.substring(1);
            window.scrollToSection(hash);
        }, 100);
    }
});
