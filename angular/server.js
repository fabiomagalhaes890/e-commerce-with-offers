const express = require('express');
const path = require('path');
const klirTechChallenge = process.env.npm_package_name;
const app = express();

app.use(express.static(`${__dirname}/dist/${klirTechChallenge}`));

app.get('/*', (req, res) => {
res.sendFile(path.join(`${__dirname}/dist/${klirTechChallenge}/index.html`));
});

app.listen(process.env.PORT || 4200);