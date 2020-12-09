import { AbstractControl, ValidatorFn } from '@angular/forms'

export class CustomValidators {

    public static matchValueWith(val: string) {
        return (control: AbstractControl): { [key: string]: any } | null =>
            control.value === control.parent.controls[val].value ? null : { valuesMissmatch: control.value }
    }

    public static containInList(data: string[]): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } | null =>
            data.includes(control.value) ? null : { wrongCity: control.value }
    }

    public static dateIsMore17AndLess100(): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } | null => {
            let nowYear = new Date().getFullYear()
            let chosenYear = new Date(control.value).getFullYear()
            return (chosenYear + 18 <= nowYear) && (nowYear - chosenYear <= 100) ? null : { wrongDate: control.value }
        }
    }
}