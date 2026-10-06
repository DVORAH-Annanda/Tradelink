(function (global) {
    'use strict';

    // Shared analytics colour helpers. Dye colours are deliberately grouped
    // into broad visual families so related database descriptions remain
    // visually consistent (for example Peach/New Peach and Lilac/New Lilac).
    const fallback = '#6C8EBF';

    const families = [
        { terms: ['black'], color: '#333333' },
        { terms: ['white', 'optic white', 'pfd', 'scour/natural'], color: '#D9D9D9' },
        { terms: ['charcoal', 'granite', 'grey', 'gray', 'platinum', 'silver', 'alloy', 'melange'], color: '#777777' },
        { terms: ['navy'], color: '#243B64' },
        { terms: ['cobalt', 'royal', 'blue', 'sky', 'indigo', 'denim', 'polar', 'poseidon', 'ocean'], color: '#4F81BD' },
        { terms: ['teal', 'turquoise', 'turq', 'aqua', 'petrol'], color: '#2A9D8F' },
        { terms: ['lilac', 'lavendar', 'lavender', 'purple', 'grape', 'mauve', 'violet', 'iris'], color: '#9B7EBD' },
        { terms: ['pink', 'cerise', 'magenta', 'blush', 'rosette'], color: '#D96C9D' },
        { terms: ['burgundy', 'burgandy', 'maroon', 'wine', 'rhubarb', 'plum'], color: '#7A3045' },
        { terms: ['red', 'flame'], color: '#C94C4C' },
        { terms: ['peach', 'apricot', 'coral', 'salmon', 'nude'], color: '#F4A582' },
        { terms: ['orange', 'naartjie', 'tangerine'], color: '#E58B3A' },
        { terms: ['yellow', 'mustard', 'butter', 'golden'], color: '#D6B84C' },
        { terms: ['olive', 'fatigue', 'fatique', 'sage', 'khaki', 'khakhi', 'trooper'], color: '#7A8450' },
        { terms: ['green', 'emerald', 'mint', 'lime', 'jade', 'bottle', 'chartreuse', 'trekking'], color: '#5B9B62' },
        { terms: ['brown', 'brwn', 'camel', 'tobacco', 'tabacco', 'chocolate', 'copper', 'hazelnut', 'tofee', 'toffee', 'rust'], color: '#8B684D' },
        { terms: ['stone', 'cream', 'sand', 'champagne', 'semolina', 'taupe', 'castle wall'], color: '#B8AA91' }
    ];

    function getDyeColour(name) {
        const value = String(name || '').trim().toLowerCase();
        if (!value) return fallback;

        for (let i = 0; i < families.length; i++) {
            const family = families[i];
            for (let j = 0; j < family.terms.length; j++) {
                if (value.indexOf(family.terms[j]) !== -1) return family.color;
            }
        }
        return fallback;
    }

    global.AnalyticsColourPalette = {
        getDyeColour: getDyeColour,
        fallback: fallback
    };
})(window);
