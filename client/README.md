___

# MercerOS UI Scaffolding

## Quick start
**Make sure you have Node version >= 6.0 and NPM >= 3**

```bash
# clone our repo
git clone git@bitbucket.org:oliverwymantechssg/ngpd-merceros-ui-starter-kit.git name-of-project

# change directory to our repo
cd name-of-project

# install the repo with npm
npm install

# start the server
npm start

# OR
# develop with Hot Module Replacement
npm run start:hmr

# ------
# to see a full demo site checkout the `docs` branch after the steps above
git checkout docs

# Note: extra task for building docs
npm run docsite

# npm start or npm start:hmr here
...
```
go to [http://localhost:4200](http://localhost:4200) in your browser


## Overview

> An Angular 2+ starter kit based on Angular CLI

MercerOS feature enhancements:

- Common `merceros` SCSS include for shared utilities and variables for scss development
- [@ngrx/store](https://github.com/ngrx/store) Redux state management
- Access to MercerOS components library which you can find documentation here:
    - Internal: [http://usdf23v0218.mrshmc.com:3025/docs](http://usdf23v0218.mrshmc.com:3025/docs)
    - External: [http://cryptic-dusk-44035.herokuapp.com](http://cryptic-dusk-44035.herokuapp.com)

# Table of Contents
* [MercerOS UI Goals](#merceros-ui-goals)
* [Getting Started](#getting-started)
    * [Dependencies](#dependencies)
    * [Installing](#installing)
    * [Running the app](#running-the-app)
    * [File Structure](#file-structure)
* [Angular CLI Generator Usage](#angular-cli-generator)
    * [Angular CLI Generator default command set](#angular-cli-generator-default-command-set)
    * [Angular CLI Generator with ngrx](#angular-cli-generator-with-ngrx)
* [Development Hints/Guide](#development-guide)
    * [AoT Don'ts](#aot-donts)
    * [Editor setup](#use-a-typescript-aware-editor)
    * [Custom type definitions](#custom-definitions-type)


# MercerOS UI Goals

Our goals for MercerOS UI is to provide a compelling and intuitive User Experience for our customers across the Mercer lines-of-business. In collaboration with the Global UX team, the design and development approach takes into account the modern day challenges with the rapidly evolving digital world and sophistication of user interactions across multiple devices. Therefore, our guiding principles with the MercerOS UI team is to create a visual language that allows our customers to interact based on modern gestures and responsive behaviors while maintaining Mercer brand-compliant visual designs.

In addition to our customer goals, another primary goal of MercerOS UI is focused on reducing costs and time-to-market across the Health, Wealth, and Careers lines-of-business. These critical goals are achieved by developing and maintaining a centralized UI component library with a re-use and adopt-and-go mentality across Mercer applications. Because this approach, we have a single governance model that enables the Mercer Material design system to be managed in a common UI code base, which will lower the cost of ownership by decreasing development, testing, and quality assurance costs.

**Summary of our Goals**
* Provide our customers with a consistent brand-compliant look and feel across the Mercer lines-of-business
* Develop and deploy a standard set of responsive UI components
* Enable re-use and ease of integration allowing for faster application development times
* Lower product development costs
* Improving time-to-market from product inception to go-live deployments
* Provide governance over development, testing, and quality assurance

# Getting Started
## Dependencies
What you need to run this app:
* `node` and `npm` (`brew install node`)
* Ensure you're running the latest versions Node `v6.x.x`+ (or `v8.x.x`) and NPM `5.x.x`+

> If you have `nvm` installed, which is highly recommended (`brew install nvm`) you can do a `nvm install --lts && nvm use` in `$` to run with the latest Node LTS. You can also have this `zsh` done for you [automatically](https://github.com/creationix/nvm#calling-nvm-use-automatically-in-a-directory-with-a-nvmrc-file) 

## Installing 

* Install `@angular/cli` globally which will give you access to the generator commands. View the available features here: [https://github.com/angular/angular-cli/wiki](https://github.com/angular/angular-cli/wiki)
    * `npm install --global @angular/cli`
* Install node dependencies in the project
    * `npm install`

## Running the app in development

* Run the application in development mode
    * `npm start`

-- OR --

* Run the application in development mode with [hot module reload](https://webpack.js.org/concepts/hot-module-replacement/)
    * `npm run start:hmr`

## File Structure
We use the component approach in our starter. This is the new standard for developing Angular apps and a great way to ensure maintainable code by encapsulation of our behavior logic. A component is basically a self contained app usually in a single file or a folder with each concern as a file: style, template, specs, e2e, and component class. Here's how it looks:
```
ngpd-merceros-ui-starter-kit/
 ├──scss/                          * our global scss include directory
 │   ├──_project.config.scss       * global project config and scss entrypoint for MercerOS
 │   └──merceros.scss              * global MercerOS include, DO NOT TOUCH!!!
 │
 ├──src/                           * our source files that will be compiled to javascript
 │   ├──config.ts                  * legacy config support for former MercerOS Applications (DO NOT TOUCH!!!)
 │   ├──hmr.ts                     * hot module reloader configuration (generally do not need to edit this)
 │   ├──index.html                 * Index.html: where we generate our index page
 │   ├──main.ts                    * our entry file for our browser environment
 │   ├──polyfills-ie.browser.ts    * IE specific polyfills
 │   ├──polyfills-ie.browser.ts    * IE specific polyfills
 │   ├──polyfills.ts               * our polyfills file
 │   ├──test.ts                    * Karma test runner entrypoint (do not edit this)
 │   ├──tsconfig.app.json          * Typescript config for app code
 │   ├──tsconfig.spec.json         * Typescript config for unit test code
 │   ├──typings.d.ts               * Ambient type definitions
 │   │
 │   ├──app/                       * WebApp: folder
 │   │   ├── app-routing.module.ts * routing module for app module
 │   │   ├── app.component.html    * app component template
 │   │   ├── app.component.scss    * app component styles
 │   │   ├── app.component.spec.ts * a simple unit test of app.component.ts
 │   │   ├── app.component.ts      * app component
 │   │   ├── app.module.ts         * application app module 
 │   │   ├── reducers              * application level reducers
 │   │   │   └── index.ts          
 │   │   └── shared                
 │   │       └── utils.ts          * holds a fix for ngrx devtools router issues
 │   │
 │   ├── environments/
 │   │   ├── environment.dev.ts    * dev environment configuration
 │   │   ├── environment.hmr.ts    * DO NOT TOUCH - For hmr configuration; uses dev enviroment config
 │   │   ├── environment.prod.ts   * prod environment configuration
 │   │   └── environment.ts
 │   └──assets/                    * static assets are served here
 │
 │
 ├──tslint.json                    * typescript lint config generated from Angular CLI
 ├──typedoc.json                   * typescript documentation generator
 ├──tsconfig.json                  * typescript config
 └──package.json                   * what npm uses to manage it's dependencies
```

## Useful commands

Most of these commands come from Angular CLI and you can check

* Building files
    * `ng build` - development build
    * `ng build --prod` - production build with aot
* Developing
    * `ng serve` - local development
    * `npm run start:hmr` - local development with hot module reload
        * Note: This is a shorthand for `ng serve --hmr -e=hmr`
* Testing
    * `ng test` - run unit tests in watch mode
    * `ng e2e` - run e2e tests with protractor
    * `npm run ci` - run the same command that is used in the Mercer jenkins CI pipeline
        * Note: this runs the linter, unit tests, and the production build
* Linting
    * `ng lint` - run the linter based on tslint
* Code scaffolding/generation
    * Look at [Angular CLI Generator](#angular-cli-generator)



# Angular CLI Generator

## Angular CLI Generator default command set

Angular comes with code scaffolding capabilities via the CLI. You can view detailed documentation here:

[Angular CLI Wiki on the generate command](https://github.com/angular/angular-cli/wiki/generate)

## Angular CLI Generator with ngrx

Ngrx provides [schematics](https://blog.angular.io/schematics-an-introduction-dc1dfbc2a2b2) that extends Angular CLI's generator capabilities to be aware of ngrx's specific requirements. You can read detailed documentation here:

[ngrx docs on schematics](https://github.com/ngrx/platform/tree/master/docs/schematics)

# Development guide


## AoT Don'ts
Angular uses a compilation technique called Ahead-of-Time (AOT) compilation, which processes the source files into efficient JavaScript code. You can read more about it [here](https://angular.io/guide/aot-compiler).

The following are some things that will make AoT compile fail.

- Don’t use require statements for your templates or styles, use styleUrls and templateUrls, the angular2-template-loader plugin will change it to require at build time.
- Don’t use default exports.
- Don’t use `form.controls.controlName`, use `form.get(‘controlName’)`
- Don’t use `control.errors?.someError`, use `control.hasError(‘someError’)`
- Don’t use functions in your providers, routes or declarations, export a function and then reference that function name
- @Inputs, @Outputs, View or Content Child(ren), Hostbindings, and any field you use from the template or annotate for Angular should be public
- Read the AOT rules over at: [https://github.com/rangle/angular-2-aot-sandbox](https://github.com/rangle/angular-2-aot-sandbox)

## Use a TypeScript-aware editor
We have good experience using these editors:

* [Visual Studio Code](https://code.visualstudio.com/)
* [Webstorm 10](https://www.jetbrains.com/webstorm/download/)
* [Atom](https://atom.io/) with [TypeScript plugin](https://atom.io/packages/atom-typescript)
* [Sublime Text](http://www.sublimetext.com/3) with [Typescript-Sublime-Plugin](https://github.com/Microsoft/Typescript-Sublime-plugin#installation)

For each of these editors you should grab the [Editor Config Plugin](http://editorconfig.org/) and a tslint plugin for your editor.

## Custom Type Definitions
When including 3rd party modules you also need to include the type definition for the module
if they don't provide one within the module. You can try to install it with @types

```bash
npm install @types/node
npm install @types/lodash
```

If you can't find the type definition in the registry we can make an ambient definition in
this file for now. For example

```typescript
declare module "my-module" {
  export function doesSomething(value: string): string;
}
```


If you're prototyping and you will fix the types later you can also declare it as type any

```typescript
declare var assert: any;
declare var _: any;
declare var $: any;
```

If you're importing a module that uses Node.js modules which are CommonJS you need to import as

```typescript
import * as _ from 'lodash';
```
