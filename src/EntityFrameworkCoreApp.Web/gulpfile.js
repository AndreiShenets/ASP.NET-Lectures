var gulp = require('gulp');

gulp.task("frontend:create-lib", function (cb) {
    var npm = {
        "bootstrap": "bootstrap/dist/**/*.{js,css,map}",
        "jquery": "jquery/dist/*.{js,css,map}",
        "popper": "popper.js/dist/umd/*.{js,css,map}"
    }
    for (var package in npm) {
        gulp.src("./node_modules/" + npm[package])
            .pipe(gulp.dest("./wwwroot/lib/" + package));
    }
    cb();
});