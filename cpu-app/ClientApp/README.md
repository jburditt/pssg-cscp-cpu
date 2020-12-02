What it has: 
- The BCGov Bootstrap theme
- Docker development files in place
- The BCGov Logo and header/footer branding.

# Victims Services Community Programs Unit

## Angular Structure

Within the `ClientApp/src/app` folder there are some key locations.

```
├───authenticated (Route guarded)
│   ├───components (subcomponents shared across pages)
│   └───subforms (components that represent form parts)
├───core (Models and other data related elements)
│   ├───constants (Regex validation)
│   ├───guards (Check for authentication/authorization)
│   ├───interceptors (If we get a particular response code from BE we catch it here.)
│   ├───models (Classes and interfaces)
│   └───services (All API calls in here)
├───landing-page (the front page)
├───modules (Not lazy loaded but each represents a single form.)
│   ├───budget-proposal
│   ├───expense-report
│   ├───personnel
│   └───program-application
├───shared (General purpose components)
```

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

### Docker

Local development is done inside of a docker container. The docker container shares space on the development machine using a docker `volume`. The docker container then runs the front-end using the packages as listed in `package.json` instead of using the version of node and angular cli found installed globally on the development machine. 

When adding or removing packages to the docker container it adds the additional step of rebuilding the docker container. Fortunately, the repository comes with convenience scripts for most of the tasks that can be run in unix-like shells.

> `docker-clean.sh` Removes old and dead containers.

> `docker-shell.sh` Opens a shell into the cpu-public-app-frontend. This allows you to inspect installed dependencies in the npm directory or make changes to packages without rebuilding the container.

> `docker-rebuild.sh` Rebuild the docker container after making a change to the node packages. Takes time and only needs to be done to initialize docker or update after module change.

> `docker-start.sh` Start the docker container and development server. This angular development server will restart itself when it detects typescript changes.

> `docker-halt.sh` If a window crashed or closed and left the docker container running it can generate errors about port 4200 being in-use it can be helpful to manually stop the docker container and ensure that the port is available. (Halt for tab completion.)

## Code scaffolding

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 8.2.1.

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
