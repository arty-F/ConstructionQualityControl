import { AbstractControl, ValidatorFn } from '@angular/forms'

export class CustomValidators {

    public static passwordsMatch(password: string, confirmedPassword: string) {
        return (control: AbstractControl): { [key: string]: any } | null =>
            password === confirmedPassword ? null : { passwordMissmatch: control.value }
    }

    public static containInList(data: string[]): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } | null =>
            data.includes(control.value) ? null : { wrongCity: control.value }

    }

    public static dateIsOk(): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } | null =>
            (new Date(control.value).getFullYear() + 18 <= new Date().getFullYear()) &&
                (new Date().getFullYear() - new Date(control.value).getFullYear() <= 100) ?
                null :
                { wrongDate: control.value }
    }
}


    //public function containInList(data: string[]): ValidatorFn {
    //    return (control: AbstractControl): { [key: string]: any } | null =>
    //        data.includes(control.value) ? null : { wrongCity: control.value }
    //}

