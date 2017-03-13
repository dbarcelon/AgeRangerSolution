'use strict'
 
var gulp = require("gulp");

var plugins = {
    gulpUtil: require("gulp-util"),
    concat: require('gulp-concat'),
    domSrc: require('gulp-dom-src'),
    flatten: require('gulp-flatten'),
    inject: require('gulp-inject'),
    minifyHtml: require('gulp-htmlmin'),
    purifyCss: require('gulp-purifycss'),
    ngAnnotate: require('gulp-ng-annotate'),
    ignore: require('gulp-ignore'),
    uglify: require('gulp-uglify'),
    rev: require('gulp-rev'),
    revReplace: require('gulp-rev-replace'),
    size: require('gulp-size'),
    cheerio: require('gulp-cheerio'),
    angularTemplatecache: require('gulp-angular-templatecache'),
    cleanCSS: require('gulp-clean-css'),
    browserSync: require('browser-sync').create()
}


var appRoot = "app";
var distRoot = "dist";
var config = {
    moduleName: "age.ranger",
    port: 3003,
    appJSFileName: "app.min.js",
    templateJSFileName: "template.min.js",
    appCSSFileName: "styles.css",
    paths: {
        baseDir: "./app/",
        appRoot: appRoot,
        indexHtml: appRoot + "/index.html",
        jsFiles: appRoot + "/**/*.js",
        htmlFiles: appRoot + '/**/*.html',
        bowerComponent: appRoot + "/bower_components/**/*",
        dist: {
            root: distRoot,
            indexHtml: distRoot + "/index.html",
            stylesFolder: distRoot + "/styles",
            fontFiles: distRoot + "/styles/fonts",
            imageFiles: distRoot + "/images/",
        }
    }
};

// Static server
// Static Server + watching scss/html files
gulp.task("serve", function () {
    plugins.browserSync.init({
        server: {
            baseDir: config.paths.baseDir
        },
        port: config.port
    });
    gulp.watch(config.paths.htmlFiles).on("change", plugins.browserSync.reload);
    gulp.watch(config.paths.jsFiles).on("change", plugins.browserSync.reload);
});

gulp.task("minify-html", function () {
    return gulp.src([config.paths.htmlFiles, "!" + config.paths.bowerComponent, "!" + config.paths.indexHtml])
      .pipe(plugins.minifyHtml({ collapseWhitespace: true }))
      .pipe(plugins.angularTemplatecache(config.templateJSFileName, {
          module: config.moduleName,
          standAlone: false
      }))
      .pipe(gulp.dest(config.paths.dist.root));
});

gulp.task("minify-js", function () {
    return plugins.domSrc({ file: config.paths.indexHtml, selector: "script", attribute: "src" })
    .pipe(plugins.ignore.exclude([ "**/*.map" ]))
    .pipe(plugins.uglify().on('error', plugins.gulpUtil.log))
    .pipe(plugins.concat(config.appJSFileName))
    .pipe(gulp.dest(config.paths.dist.root));
});

gulp.task("minify-css", function () {
    return plugins.domSrc({ file: config.paths.indexHtml, selector: "link", attribute: "href" })
    .pipe(plugins.cleanCSS())
    .pipe(plugins.concat(config.appCSSFileName))
    .pipe(gulp.dest(config.paths.dist.root));
});

gulp.task("update-indexHtml", function () {
    var scriptVersion = Math.floor((Math.random() * 100) + 1);
    return gulp.src(config.paths.indexHtml)
        .pipe(plugins.cheerio(function ($) {
            $("script").remove();
            $("link").remove();
            $("body").append("<script src='" + config.appJSFileName + "?v=" + scriptVersion + "'></script>");
            $("body").append("<script src='" + config.templateJSFileName + "?v=" + scriptVersion + "'></script>");
            $("head").append("<link href='" + config.appCSSFileName + "?v=" + scriptVersion + "' rel='stylesheet'></link>");
        }))
        .pipe(plugins.minifyHtml({ collapseWhitespace: true }))
        .pipe(gulp.dest(config.paths.dist.root));
});

//gulp.task("serve:dist", ["minify-html", "minify-js", "update-indexHtml"]);
gulp.task("serve:dist", ["minify-html", "minify-js", "minify-css", "update-indexHtml"]);

