import { FormGroup } from '@angular/forms';

export interface ICrudBase {
    insert();
    edit(entity);
    remove(entity);
}

export interface IFormBase {
    cancel();
    save();
    formEntity: FormGroup;
}