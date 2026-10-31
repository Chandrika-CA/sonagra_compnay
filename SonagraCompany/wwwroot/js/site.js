document.addEventListener('DOMContentLoaded', () => {
    const serviceItem = document.querySelector('.services-nav-item');
    const serviceToggle = document.querySelector('.services-toggle');
    const servicesMenu = document.querySelector('#servicesMegaMenu');
    const navigation = document.querySelector('#mainNavigation');
    const navigationCollapse = navigation && window.bootstrap ? bootstrap.Collapse.getOrCreateInstance(navigation, { toggle: false }) : null;
    const heroCarousel = document.querySelector('#homeHeroCarousel');
    const careerModal = document.querySelector('#careerApplicationModal');
    const careerRole = document.querySelector('#careerRole');
    const careerForm = document.querySelector('#careerApplicationForm');
    const careerFormStatus = document.querySelector('#careerFormStatus');
    const careerPhone = document.querySelector('#careerPhone');

    const closeServicesMenu = () => {
        serviceItem?.classList.remove('menu-open');
        serviceToggle?.setAttribute('aria-expanded', 'false');
    };

    if (serviceItem && serviceToggle && servicesMenu) {
        serviceToggle.addEventListener('click', (event) => {
            if (window.matchMedia('(max-width: 991.98px)').matches) {
                event.preventDefault();
                const isOpen = serviceItem.classList.toggle('menu-open');
                serviceToggle.setAttribute('aria-expanded', isOpen.toString());
            }
        });

    document.querySelectorAll('[data-career-role]').forEach((trigger) => {
        trigger.addEventListener('click', () => {
            if (careerRole) {
                careerRole.value = trigger.getAttribute('data-career-role') || '';
            }

            if (careerFormStatus) {
                careerFormStatus.textContent = '';
            }
        });
    });

    careerForm?.addEventListener('submit', async (event) => {
        event.preventDefault();
        const selectedRole = careerRole?.value || '';

        if (!careerForm.checkValidity()) {
            careerForm.reportValidity();
            return;
        }

        try {
            const response = await fetch('/Home/ValidateCareerApplication', {
                method: 'POST',
                body: new FormData(careerForm),
                headers: { 'X-Requested-With': 'XMLHttpRequest' }
            });
            const result = await response.json();

            if (!response.ok) {
                throw new Error(result.message);
            }

            if (careerFormStatus) {
                careerFormStatus.textContent = result.message;
                careerFormStatus.classList.add('is-success');
            }

            careerForm.reset();
            if (careerRole) {
                careerRole.value = selectedRole;
            }
        } catch (error) {
            if (careerFormStatus) {
                careerFormStatus.textContent = error.message || 'Please check the form and try again.';
                careerFormStatus.classList.remove('is-success');
            }
        }
    });

    careerPhone?.addEventListener('input', () => {
        careerPhone.value = careerPhone.value.replace(/\D/g, '').slice(0, 10);
    });

    const contactPhone = document.querySelector('#contactPhone');
    contactPhone?.addEventListener('input', () => {
        contactPhone.value = contactPhone.value.replace(/\D/g, '').slice(0, 10);
    });

        serviceToggle.addEventListener('keydown', (event) => {
            if ((event.key === 'Enter' || event.key === ' ') && window.matchMedia('(max-width: 991.98px)').matches) {
                event.preventDefault();
                serviceToggle.click();
            }

            if (event.key === 'Escape') {
                closeServicesMenu();
                serviceToggle.focus();
            }
        });

        servicesMenu.addEventListener('keydown', (event) => {
            if (event.key === 'Escape') {
                closeServicesMenu();
                serviceToggle.focus();
            }
        });

        document.addEventListener('click', (event) => {
            if (!serviceItem.contains(event.target)) {
                closeServicesMenu();
            }
        });

        navigation?.addEventListener('hidden.bs.collapse', closeServicesMenu);

        servicesMenu.querySelectorAll('a').forEach((link) => {
            link.addEventListener('click', () => {
                if (window.matchMedia('(max-width: 991.98px)').matches) {
                    closeServicesMenu();
                    navigationCollapse?.hide();
                }
            });
        });
    }

    navigation?.querySelectorAll('.nav-link:not(.services-toggle)').forEach((link) => {
        link.addEventListener('click', () => {
            if (window.matchMedia('(max-width: 991.98px)').matches) {
                closeServicesMenu();
                navigationCollapse?.hide();
            }
        });
    });

    if (heroCarousel && window.bootstrap) {
        const carousel = bootstrap.Carousel.getOrCreateInstance(heroCarousel);
        const reducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)');

        if (reducedMotion.matches) {
            carousel.pause();
            carousel._config.interval = false;
        } else {
            heroCarousel.addEventListener('mouseenter', () => carousel.pause());
            heroCarousel.addEventListener('mouseleave', () => carousel.cycle());
            heroCarousel.addEventListener('focusin', () => carousel.pause());
            heroCarousel.addEventListener('focusout', (event) => {
                if (!heroCarousel.contains(event.relatedTarget)) {
                    carousel.cycle();
                }
            });
        }
    }

    const revealElements = document.querySelectorAll('.scroll-reveal');
    const counterElements = document.querySelectorAll('.counter-number[data-counter-target]');

    const animateCounter = (element) => {
        if (!element || element.dataset.animated === 'true') {
            return;
        }

        const target = Number.parseInt(element.dataset.counterTarget || '0', 10);
        if (!Number.isFinite(target) || target <= 0) {
            element.textContent = '0';
            element.dataset.animated = 'true';
            return;
        }

        const duration = 1500;
        const start = performance.now();
        const step = (now) => {
            const progress = Math.min((now - start) / duration, 1);
            element.textContent = Math.floor(progress * target).toString();

            if (progress < 1) {
                window.requestAnimationFrame(step);
            } else {
                element.textContent = target.toString();
                element.dataset.animated = 'true';
            }
        };

        window.requestAnimationFrame(step);
    };

    if ('IntersectionObserver' in window) {
        if (revealElements.length > 0) {
            const revealObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach((entry) => {
                    if (entry.isIntersecting) {
                        entry.target.classList.add('is-visible');
                        observer.unobserve(entry.target);
                    }
                });
            }, { threshold: 0.2, rootMargin: '0px 0px -40px 0px' });

            revealElements.forEach((element) => revealObserver.observe(element));
        }

        if (counterElements.length > 0) {
            const counterObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach((entry) => {
                    if (entry.isIntersecting) {
                        animateCounter(entry.target);
                        observer.unobserve(entry.target);
                    }
                });
            }, { threshold: 0.4 });

            counterElements.forEach((element) => counterObserver.observe(element));
        }
    } else {
        revealElements.forEach((element) => element.classList.add('is-visible'));
        counterElements.forEach((element) => animateCounter(element));
    }
});
// Highlight the current service in the "Our Services" sidebar
document.addEventListener('DOMContentLoaded', () => {
    const normalise = (path) => path.toLowerCase().replace(/\/+$/, '');
    const currentPath = normalise(window.location.pathname);

    document.querySelectorAll('.service-detail-sidebar a').forEach((link) => {
        if (normalise(new URL(link.getAttribute('href'), window.location.origin).pathname) !== currentPath) {
            return;
        }

        link.classList.add('active');
        link.setAttribute('aria-current', 'page');
        link.querySelector('span')?.remove();

        const sidebar = link.closest('.service-detail-sidebar');
        if (sidebar) {
            sidebar.scrollTop = link.offsetTop - sidebar.clientHeight / 2;
        }
    });
});
