// ============================================================
// CULTIVA# - COMPONENTES GLOBAIS
// ============================================================

window.CultivaAlert = (() => {

    function createModal() {
        let overlay = document.getElementById('cultivaAlertOverlay');

        if (overlay) {
            return overlay;
        }

        overlay = document.createElement('div');
        overlay.id = 'cultivaAlertOverlay';
        overlay.className = 'cultiva-alert-overlay';

        overlay.innerHTML = `
            <div class="cultiva-alert-modal" role="dialog" aria-modal="true">

                <div class="cultiva-alert-icon" id="cultivaAlertIcon">
                    <i class="fa-solid fa-circle-info"></i>
                </div>

                <div class="cultiva-alert-content">
                    <h2 id="cultivaAlertTitle">Aviso</h2>
                    <p id="cultivaAlertMessage"></p>
                </div>

                <div class="cultiva-alert-actions" id="cultivaAlertActions"></div>

            </div>
        `;

        document.body.appendChild(overlay);

        return overlay;
    }


    function getElements() {
        const overlay = createModal();

        return {
            overlay,
            modal: overlay.querySelector('.cultiva-alert-modal'),
            icon: overlay.querySelector('#cultivaAlertIcon'),
            title: overlay.querySelector('#cultivaAlertTitle'),
            message: overlay.querySelector('#cultivaAlertMessage'),
            actions: overlay.querySelector('#cultivaAlertActions')
        };
    }


    function setType(elements, type) {
        elements.modal.classList.remove(
            'cultiva-alert-success',
            'cultiva-alert-error',
            'cultiva-alert-warning',
            'cultiva-alert-info',
            'cultiva-alert-danger'
        );

        elements.modal.classList.add(`cultiva-alert-${type}`);

        const icons = {
            success: 'fa-circle-check',
            error: 'fa-circle-xmark',
            warning: 'fa-triangle-exclamation',
            info: 'fa-circle-info',
            danger: 'fa-trash-can'
        };

        elements.icon.innerHTML =
            `<i class="fa-solid ${icons[type] || icons.info}"></i>`;
    }


    function open(elements) {
        document.body.classList.add('cultiva-alert-open');

        requestAnimationFrame(() => {
            elements.overlay.classList.add('show');
        });
    }


    function close(elements) {
        elements.overlay.classList.remove('show');
        document.body.classList.remove('cultiva-alert-open');
    }


    function message({
        title = 'Aviso',
        message = '',
        type = 'info',
        buttonText = 'Entendi'
    }) {
        return new Promise(resolve => {

            const elements = getElements();

            setType(elements, type);

            elements.title.textContent = title;
            elements.message.textContent = message;

            elements.actions.innerHTML = '';

            const button = document.createElement('button');
            button.type = 'button';
            button.className = 'cultiva-alert-button primary';
            button.textContent = buttonText;

            elements.actions.appendChild(button);

            const finish = () => {
                close(elements);

                setTimeout(() => {
                    resolve(true);
                }, 180);
            };

            button.onclick = finish;

            open(elements);

            setTimeout(() => button.focus(), 50);
        });
    }


    function confirm({
        title = 'Confirmar ação',
        message = 'Deseja continuar?',
        confirmText = 'Confirmar',
        cancelText = 'Cancelar',
        type = 'warning'
    }) {
        return new Promise(resolve => {

            const elements = getElements();

            setType(elements, type);

            elements.title.textContent = title;
            elements.message.textContent = message;

            elements.actions.innerHTML = '';

            const cancelButton = document.createElement('button');
            cancelButton.type = 'button';
            cancelButton.className = 'cultiva-alert-button secondary';
            cancelButton.textContent = cancelText;

            const confirmButton = document.createElement('button');
            confirmButton.type = 'button';
            confirmButton.className =
                type === 'danger'
                    ? 'cultiva-alert-button danger'
                    : 'cultiva-alert-button primary';

            confirmButton.textContent = confirmText;

            elements.actions.appendChild(cancelButton);
            elements.actions.appendChild(confirmButton);

            const finish = value => {
                close(elements);

                setTimeout(() => {
                    resolve(value);
                }, 180);
            };

            cancelButton.onclick = () => finish(false);
            confirmButton.onclick = () => finish(true);

            open(elements);

            setTimeout(() => cancelButton.focus(), 50);
        });
    }


    return {

        success(message, title = 'Tudo certo!') {
            return messageAlert(title, message, 'success');
        },

        error(message, title = 'Algo deu errado') {
            return messageAlert(title, message, 'error');
        },

        warning(message, title = 'Atenção') {
            return messageAlert(title, message, 'warning');
        },

        info(message, title = 'Informação') {
            return messageAlert(title, message, 'info');
        },

        confirm,

        delete(title, message) {
            return confirm({
                title,
                message,
                type: 'danger',
                confirmText: 'Excluir',
                cancelText: 'Cancelar'
            });
        }
    };


    function messageAlert(title, text, type) {
        return message({
            title,
            message: text,
            type
        });
    }

})();

document.addEventListener('DOMContentLoaded', () => {
    const searchInput = document.getElementById('globalSearch');
    const resultsContainer = document.getElementById('globalSearchResults');

    if (!searchInput || !resultsContainer) {
        return;
    }

    let debounceTimeout;
    let activeRequest;
    let activeIndex = -1;

    function hideResults() {
        resultsContainer.hidden = true;
        searchInput.setAttribute('aria-expanded', 'false');
        searchInput.removeAttribute('aria-activedescendant');
        activeIndex = -1;
    }

    function showMessage(message) {
        resultsContainer.replaceChildren();

        const element = document.createElement('p');
        element.className = 'global-search-message';
        element.setAttribute('role', 'status');
        element.textContent = message;

        resultsContainer.appendChild(element);
        resultsContainer.hidden = false;
        searchInput.setAttribute('aria-expanded', 'true');
        searchInput.removeAttribute('aria-activedescendant');
        activeIndex = -1;
    }

    function renderResults(items) {
        resultsContainer.replaceChildren();

        const safeItems = items.filter(item =>
            item &&
            typeof item.title === 'string' &&
            typeof item.category === 'string' &&
            typeof item.description === 'string' &&
            typeof item.url === 'string' &&
            item.url.startsWith('/') &&
            !item.url.startsWith('//'));

        if (safeItems.length === 0) {
            showMessage('Nenhum resultado encontrado.');
            return;
        }

        safeItems.forEach((item, index) => {
            const link = document.createElement('a');
            link.className = 'global-search-result';
            link.href = item.url;
            link.id = `globalSearchOption-${index}`;
            link.setAttribute('role', 'option');
            link.setAttribute('aria-selected', 'false');

            const category = document.createElement('span');
            category.className = 'global-search-result-category';
            category.textContent = item.category;

            const title = document.createElement('span');
            title.className = 'global-search-result-title';
            title.textContent = item.title;

            const description = document.createElement('span');
            description.className = 'global-search-result-description';
            description.textContent = item.description;

            link.append(category, title, description);
            resultsContainer.appendChild(link);
        });

        resultsContainer.hidden = false;
        searchInput.setAttribute('aria-expanded', 'true');
        activeIndex = -1;
    }

    function setActiveResult(index) {
        const options = resultsContainer.querySelectorAll('.global-search-result');

        if (options.length === 0) {
            return;
        }

        activeIndex = (index + options.length) % options.length;

        options.forEach((option, optionIndex) => {
            const isActive = optionIndex === activeIndex;
            option.classList.toggle('active', isActive);
            option.setAttribute('aria-selected', String(isActive));
        });

        searchInput.setAttribute(
            'aria-activedescendant',
            options[activeIndex].id);
    }

    async function search(term) {
        activeRequest?.abort();

        const controller = new AbortController();
        activeRequest = controller;

        showMessage('Pesquisando...');

        try {
            const headers = { Accept: 'application/json' };
            const token = localStorage.getItem('token');

            if (token) {
                headers.Authorization = `Bearer ${token}`;
            }

            const response = await fetch(
                `/Home/Search?term=${encodeURIComponent(term)}`,
                { headers, signal: controller.signal });

            if (!response.ok) {
                throw new Error('A pesquisa não pôde ser concluída.');
            }

            const results = await response.json();

            if (searchInput.value.trim() === term) {
                renderResults(Array.isArray(results) ? results : []);
            }
        } catch (error) {
            if (error.name !== 'AbortError' && searchInput.value.trim() === term) {
                showMessage('Erro ao pesquisar. Tente novamente.');
            }
        }
    }

    searchInput.addEventListener('input', () => {
        const term = searchInput.value.trim();
        activeRequest?.abort();
        clearTimeout(debounceTimeout);

        if (term.length === 0) {
            hideResults();
            return;
        }

        if (term.length < 2) {
            showMessage('Digite ao menos 2 caracteres para pesquisar.');
            return;
        }

        debounceTimeout = setTimeout(() => search(term), 250);
    });

    searchInput.addEventListener('keydown', event => {
        if (resultsContainer.hidden) {
            return;
        }

        const options = resultsContainer.querySelectorAll('.global-search-result');

        if (event.key === 'ArrowDown' && options.length > 0) {
            event.preventDefault();
            setActiveResult(activeIndex + 1);
        } else if (event.key === 'ArrowUp' && options.length > 0) {
            event.preventDefault();
            setActiveResult(activeIndex < 0 ? options.length - 1 : activeIndex - 1);
        } else if (event.key === 'Enter' && options.length > 0) {
            event.preventDefault();
            const selectedIndex = activeIndex < 0 ? 0 : activeIndex;
            window.location.assign(options[selectedIndex].href);
        } else if (event.key === 'Escape') {
            hideResults();
        }
    });

    document.addEventListener('click', event => {
        if (!event.target.closest('.topbar-search')) {
            hideResults();
        }
    });
});