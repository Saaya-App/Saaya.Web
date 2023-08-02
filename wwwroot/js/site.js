function displayTooltip(event) {
    const target = event.target;
    if (target.classList.contains('sp-liw__emoji')) {
        const tooltipText = target.getAttribute('alt');

        const tooltip = document.createElement('div');
        tooltip.classList.add('sp-liw__emoji-info');
        tooltip.innerHTML = '<img class="sp-liw__emoji" src="' + target.getAttribute('src') + '"/><br/>:' + tooltipText + ':'; //<br/><i>This emoji can be used anywhere!</i>

        document.body.appendChild(tooltip);

        const x = event.clientX;
        const y = event.clientY;
        tooltip.style.top = `${y + 10}px`;
        tooltip.style.left = `${x}px`;

        tooltip.style.opacity = 1;

        target.addEventListener('mouseout', () => {
            document.body.removeChild(tooltip);
        });
    }
}

document.addEventListener('mouseover', displayTooltip);


function displayEvent(event) {
    const target = event.target;
    if (target.classList.contains('saaya-timeline__event')) {
        const tooltipText = target.getAttribute('value');

        const tooltip = document.createElement('div');
        tooltip.classList.add('saaya-timeline__event-details');
        tooltip.innerHTML = '<span class="saaya-timeline__date">' + target.getAttribute('name') + '</span><p class="saaya-timeline__description">' + tooltipText + '</p>'; //<br/><i>This emoji can be used anywhere!</i>

        document.body.appendChild(tooltip);

        const x = event.clientX;
        const y = event.clientY;
        tooltip.style.top = `${y + 10}px`;
        tooltip.style.left = `${x}px`;

        tooltip.style.opacity = 1;

        target.addEventListener('mouseout', () => {
            document.body.removeChild(tooltip);
        });
    }
}

document.addEventListener('mouseover', displayEvent);