const fs = require("fs-extra");
const concat = require("concat");

(async function() {
  const files = [
    "./dist/fmt-charts/runtime.js",
    "./dist/fmt-charts/main.js",
    "./dist/fmt-charts/polyfills.js",
  ]

  const exists = fs.existsSync("fmt-charts");

  if (exists) {
    fs.removeSync("fmt-charts");
  }

  await fs.ensureDir("elements");
  await concat(files, "elements/fmt-charts.js");
  await fs.copyFile("./dist/fmt-charts/styles.css", "elements/styles.css");
})();
