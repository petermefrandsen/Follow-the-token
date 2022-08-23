const fs = require("fs-extra");
const concat = require("concat");

(async function() {
  const files = [
    "./dist/fmt-charts/runtime.js",
    "./dist/fmt-charts/main.js",
    "./dist/fmt-charts/polyfils",
  ]

  const exists = fs.existsSync("elements");

  if (exists) {
    fs.removeSync("elements");
  }

  await fs.ensureDir("elements");
  await concat(files, "elements/elements.js");
  await fs.copyFile("./dist/fmt-charts/styles.css", "elements/styles.css");
})();
