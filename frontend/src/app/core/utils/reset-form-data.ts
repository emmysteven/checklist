import { AbstractControl, FormGroup } from "@angular/forms";

export function resetFormData(form: FormGroup): void {
  let control: AbstractControl;
  form.reset();
  Object.keys(form.controls).forEach((name) => {
    control = form.controls[name];
    control.setErrors(null);
  });
}
