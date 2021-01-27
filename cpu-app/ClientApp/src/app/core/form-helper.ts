import { AbstractControl } from '@angular/forms';
import { NotificationQueueService } from './services/notification-queue.service';

export class FormHelper {
  showValidFeedback(control: AbstractControl): boolean {
    if (control.untouched) return true;
    return !(control.value && control.valid && this.isDirtyOrTouched(control));
  }
  showInvalidFeedbackOld(control: AbstractControl): boolean {
    return !(control.value && control.invalid && this.isDirtyOrTouched(control));
  }
  showInvalidFeedback(value: any, pattern: RegExp, control: AbstractControl) {
    return (!value || pattern.test(value));
  }
  checkBoxRequired(value: any): boolean {
    return value;
  }
  validateHours(scheduledHours: number, serviceHours: number) {
    if (!scheduledHours) return true;
    return scheduledHours < serviceHours;
  }
  isRequired(control: AbstractControl, required: boolean) {
    if (required && control.value && this.isDirtyOrTouched(control)) {
      return true;
    } else {
      return null;
    }
  }
  isDisabled(disabled: boolean) {
    if (disabled) {
      return true;
    } else {
      return null;
    }
  }
  isDirtyOrTouched(control: AbstractControl) {
    if (control.dirty || control.touched) {
      return true;
    } else {
      return null;
    }
  }
  showWarningBeforeExit() {
    let dirtyControls = document.querySelectorAll(".ng-dirty");
    let count = 0;
    if (dirtyControls.length > 0) {
      for (let i = 0; i < dirtyControls.length; ++i) {
        if (dirtyControls[i].classList.contains("ng-untouched")) continue;
        ++count;
      }
    }
    if (count > 0) {
      return true;
    }

    let incompleteTabs = document.querySelectorAll(".tab-incomplete");
    if (incompleteTabs.length > 0) {
      return true;
    }
    let invalidTabs = document.querySelectorAll(".tab-invalid");
    if (invalidTabs.length > 0) {
      return true;
    }
    return false;
  }
  isFormDirty() {
    let dirtyControls = document.querySelectorAll(".ng-dirty");
    let count = 0;
    if (dirtyControls.length > 0) {
      for (let i = 0; i < dirtyControls.length; ++i) {
        if (dirtyControls[i].classList.contains("ng-untouched")) continue;
        ++count;
      }
    }
    if (count > 0) {
      return true;
    }
    return false;
  }
  isFormValid(notificationQueueService: NotificationQueueService = null, currentTabHasInvalidClass: number = 0, ignoreTabErrors = false) {
    if (document.getElementsByClassName("ng-invalid").length > 0) {
      if (notificationQueueService) notificationQueueService.addNotification('All fields must be in a valid format.', 'warning');
      return false;
    }
    if (!ignoreTabErrors && document.getElementsByClassName("tab-invalid").length > currentTabHasInvalidClass) {
      if (notificationQueueService) notificationQueueService.addNotification('There is a problem on another tab preventing save.', 'warning');
      return false;
    }
    return true;
  }
  isDialogValid(notificationQueueService: NotificationQueueService = null) {
    if (document.querySelectorAll(".mat-dialog-container .ng-invalid").length > 0) {
      if (notificationQueueService) notificationQueueService.addNotification('All fields must be in a valid format.', 'warning');
      return false;
    }
    return true;
  }
  getFormState() {
    if (document.getElementsByClassName("ng-invalid").length > 0) {
      return 'invalid';
    }

    let dirtyControls = document.querySelectorAll(".ng-dirty");
    let count = 0;
    if (dirtyControls.length > 0) {
      for (let i = 0; i < dirtyControls.length; ++i) {
        if (dirtyControls[i].classList.contains("ng-untouched") || dirtyControls[i].classList.contains("reporting-month")) continue;
        ++count;
      }
    }
    if (count > 0) {
      return 'incomplete';
    }

    return 'untouched';
  }
  makeFormClean() {
    let dirtyControls = document.querySelectorAll(".ng-dirty");
    if (dirtyControls.length > 0) {
      for (let i = 0; i < dirtyControls.length; ++i) {
        dirtyControls[i].classList.remove("ng-dirty");
        dirtyControls[i].classList.remove("ng-touched");
        dirtyControls[i].classList.add("ng-untouched");
        dirtyControls[i].classList.add("ng-pristine");
      }
    }
  }
  numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }
  moneyMaskToNumber(e: any, context: any, varName, max: number = 0, alwaysEnforceMax: boolean = false) {
    if (!max) max = 0;
    let moneyString = e.value.replace(/[$,]/g, '');
    let val = parseFloat(moneyString);
    if ((max > 0 || alwaysEnforceMax) && val > max) {
      val = max;
      e.value = "$" + this.numberWithCommas(val);
    }
    if (!val) val = 0;
    if (varName) {
      let variable = this.fetchVarInfo(context, varName);
      variable.obj[variable.prop] = val;
    }
  }
  maskToNumber(e: any, context: any, varName) {
    let variable = this.fetchVarInfo(context, varName);
    variable.obj[variable.prop] = parseFloat(e.value);
  }
  maskToPositiveNumber(e: any, context: any, varName) {
    let variable = this.fetchVarInfo(context, varName);
    e.value = e.value.replace(/-/g, '');
    variable.obj[variable.prop] = parseFloat(e.value);
  }
  maskToTime(e: any, context: any, varName) {
    let variable = this.fetchVarInfo(context, varName);
    let timeParts = e.value.split(' : ').map(a => parseInt(a));
    if (e.value.length == 1 && parseInt(e.value) > 1) {
      e.value = "0" + e.value;
    }
    if (timeParts[0] && timeParts[0] > 12) {
      e.value = "12" + e.value.substr(2);
    }
    if (timeParts[1] && timeParts[1] > 59) {
      e.value = e.value.substring(0, 5) + "59";
    }
    variable.obj[variable.prop] = e.value.replace(/ /g, '');
  }
  numberFormatter(e: any, context: any, varName) {
    if (e.value.toString().length > 1 && e.value.toString()[0] === "0") {
      e.value = parseFloat(e.value.toString().substr(1));
    }
    if (e.value < 0) {
      e.value = 0;
    }
    if (e.value > 9999) {
      e.value = parseFloat(e.value.toString().substring(0, 4));;
    }
    if (this.countDecimals(e.value) > 2) {
      e.value = parseFloat(e.value).toFixed(2);
    }

    let variable = this.fetchVarInfo(context, varName);
    variable.obj[variable.prop] = parseFloat(e.value);
  }
  fetchVarInfo(obj, prop) {
    if (typeof obj === 'undefined') {
      return false;
    }

    var _index = prop.indexOf('.')
    if (_index > -1) {
      return this.fetchVarInfo(obj[prop.substring(0, _index)], prop.substr(_index + 1));
    }

    return { obj: obj, prop: prop };
  }
  countDecimals(value) {
    if (Math.floor(value) === value) return 0;
    if (value.toString().indexOf(".") < 0) return 0;
    return value.toString().split(".")[1].length || 0;
  }
  trimInput(e: any) {
    e.value = e.value.trimRight();
  }
  trimPostalCode(e: any) {
    let spaceIndex = e.value.indexOf(' ');

    if (spaceIndex >= 0 && spaceIndex !== 3) {
      e.value = e.value.replace(/ /g, '');
    }
    var countSpaces = (e.value.match(/ /g) || []).length;
    if (countSpaces > 1) {
      e.value = e.value.trimRight();
    }
  }
  fetchFromObject(obj, prop) {
    if (typeof obj === 'undefined') {
      return false;
    }

    var _index = prop.indexOf('.')
    if (_index > -1) {
      return this.fetchFromObject(obj[prop.substring(0, _index)], prop.substr(_index + 1));
    }

    if (obj[prop] === false) {
      return 'false'
    }

    return obj[prop];
  }
}
